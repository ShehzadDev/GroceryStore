using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceryStore
{
	public partial class Home : Form
	{
		public Home()
		{
			InitializeComponent();
		}


		private void Home_Load(object sender, EventArgs e)
		{
			RetrieveProductsFromDatabase();
			RefreshProductListView();
		}
		private List<Product> searchResults = new List<Product>();


		private void RetrieveProductsFromDatabase()
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();
					SqlCommand command = new SqlCommand("SELECT * FROM Products", connection);
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						int id = (int)reader["Id"];
						string name = (string)reader["Name"];
						decimal price = (decimal)reader["Price"];
						double convertedPrice = Convert.ToDouble(price);

						Product product = new Product { Id = id, Name = name, Price = convertedPrice };
						products.Add(product);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error retrieving products from the database: " + ex.Message);
				}
			}
		}

		private void AddProductButton_Click(object sender, EventArgs e)
		{
			int id;
			if (!int.TryParse(productIdTextBox.Text, out id))
			{
				MessageBox.Show("Invalid product ID.");
				return;
			}

			string name = productNameTextBox.Text;
			double price;
			if (!double.TryParse(productPriceTextBox.Text, out price))
			{
				MessageBox.Show("Invalid product price.");
				return;
			}

			Product product = new Product { Id = id, Name = name, Price = price };
			products.Add(product);
			InsertProductIntoDatabase(product);
			RefreshProductListView();

			ClearProductFields();
		}

		private void InsertProductIntoDatabase(Product product)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();

					// Check if the product ID already exists
					SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Products WHERE Id = @Id", connection);
					checkCommand.Parameters.AddWithValue("@Id", product.Id);
					int count = (int)checkCommand.ExecuteScalar();

					if (count > 0)
					{
						MessageBox.Show("Product with ID " + product.Id + " already exists in the database.");
						return; // Exit the method without inserting
					}

					// Insert the product into the database
					SqlCommand insertCommand = new SqlCommand("INSERT INTO Products (Id, Name, Price) VALUES (@Id, @Name, @Price)", connection);
					insertCommand.Parameters.AddWithValue("@Id", product.Id);
					insertCommand.Parameters.AddWithValue("@Name", product.Name);
					insertCommand.Parameters.AddWithValue("@Price", product.Price);
					insertCommand.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error inserting product into the database: " + ex.Message);
				}
			}
		}

		private void DeleteProductButton_Click(object sender, EventArgs e)
		{
			if (productListView.SelectedItems.Count == 0)
			{
				MessageBox.Show("Please select a product to delete.");
				return;
			}

			ListViewItem selectedItem = productListView.SelectedItems[0];
			int selectedProductId = int.Parse(selectedItem.Text);

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
			Product selectedProduct = products.FirstOrDefault(p => p.Id == selectedProductId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
			if (selectedProduct != null)
			{
				products.Remove(selectedProduct);
				DeleteProductFromDatabase(selectedProduct);
				productListView.Items.Remove(selectedItem);
			}
		}


		private void DeleteProductFromDatabase(Product product)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();
					SqlCommand command = new SqlCommand("DELETE FROM Products WHERE Id = @Id", connection);
					command.Parameters.AddWithValue("@Id", product.Id);
					command.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error deleting product from the database: " + ex.Message);
				}
			}
		}

		private void UpdateProductButton_Click(object sender, EventArgs e)
		{
			if (productListView.SelectedItems.Count == 0)
			{
				MessageBox.Show("Please select a product to update.");
				return;
			}

			ListViewItem selectedItem = productListView.SelectedItems[0];
			int selectedProductId = int.Parse(selectedItem.Text);

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
			Product selectedProduct = products.FirstOrDefault(p => p.Id == selectedProductId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
			if (selectedProduct != null)
			{
				int id;
				if (!int.TryParse(productIdTextBox.Text, out id))
				{
					MessageBox.Show("Invalid product ID.");
					return;
				}

				string name = productNameTextBox.Text;
				double price;
				if (!double.TryParse(productPriceTextBox.Text, out price))
				{
					MessageBox.Show("Invalid product price.");
					return;
				}

				selectedProduct.Id = id;
				selectedProduct.Name = name;
				selectedProduct.Price = price;

				UpdateProductInDatabase(selectedProduct);
				RefreshProductListView();

				ClearProductFields();
			}
		}

		private void UpdateProductInDatabase(Product product)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();
					SqlCommand command = new SqlCommand("UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id", connection);
					command.Parameters.AddWithValue("@Id", product.Id);
					command.Parameters.AddWithValue("@Name", product.Name);
					command.Parameters.AddWithValue("@Price", product.Price);
					command.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error updating product in the database: " + ex.Message);
				}
			}
		}


		private void RefreshProductListView()
		{
			try
			{
				productListView.BeginUpdate();
				productListView.Items.Clear();

				// Add columns to the ListView
				productListView.Columns.Clear();
				productListView.Columns.Add("ID", 100, HorizontalAlignment.Left);
				productListView.Columns.Add("Name", 200, HorizontalAlignment.Left);
				productListView.Columns.Add("Price", 100, HorizontalAlignment.Left);

				foreach (Product product in products)
				{
					ListViewItem item = new ListViewItem(product.Id.ToString());
					item.SubItems.Add(product.Name);
					item.SubItems.Add(product.Price.ToString());

					// Add the item to the list view
					productListView.Items.Add(item);
				}
			}
			catch (NullReferenceException ex)
			{
				MessageBox.Show("An error occurred: " + ex.Message);
			}
			finally
			{
				productListView.EndUpdate();
			}
		}





		private void ClearProductFields()
		{
			productIdTextBox.Text = string.Empty;
			productNameTextBox.Text = string.Empty;
			productPriceTextBox.Text = string.Empty;
		}

		private void ViewAllButton_Click(object sender, EventArgs e)
		{
			RefreshProductListView();
		}

		private void SearchButton_Click(object sender, EventArgs e)
		{
			string searchTerm = searchBox.Text.Trim();

			// Clear the search results list
			searchResults.Clear();

			if (!string.IsNullOrEmpty(searchTerm))
			{
				foreach (Product product in products)
				{
					// Perform a case-insensitive search by converting both the product name and search term to lowercase
					if (product.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
					{
						MessageBox.Show("ID : " + product.Id + "\nName : " + product.Name + "\nPrice: " + product.Price);
					}
				}
			}

			// Refresh the ListView with the search results
			RefreshProductListView();

			// Display a message box if no matching product is found
			if (searchResults.Count == 0)
			{
				MessageBox.Show("No results found.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}



		private void GenerateReportButton_Click(object sender, EventArgs e)
		{
			// Generate and save the report
			string report = "Product Report:\n\n";
			foreach (Product product in products)
			{
				report += $"Product ID: {product.Id}\n";
				report += $"Product Name: {product.Name}\n";
				report += $"Product Price: {product.Price}\n\n";
			}

			// Save the report to a file
			string reportFileName = "product_report.txt";
			try
			{
				System.IO.File.WriteAllText(reportFileName, report);
				MessageBox.Show($"Report generated and saved as {reportFileName}");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error generating the report: " + ex.Message);
			}
		}
		private void productId_Click(object sender, EventArgs e)
		{
			// Get the selected item from the productListView
			if (productListView.SelectedItems.Count > 0)
			{
				// Get the product ID from the selected item
				string selectedProductId = productListView.SelectedItems[0].SubItems[0].Text;

				// Display the product ID in the productIdTextBox
				productIdTextBox.Text = selectedProductId;

				// Retrieve and display the product details based on the selected product ID
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
				Product selectedProduct = products.Find(product => product.Id.ToString() == selectedProductId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
				if (selectedProduct != null)
				{
					productNameTextBox.Text = selectedProduct.Name;
					productPriceTextBox.Text = selectedProduct.Price.ToString();
				}
			}
		}
	}
}
