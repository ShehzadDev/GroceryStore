using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace GroceryStore
{
	partial class Home:Form
	{


		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>

		#endregion

		private List<Product> products = new List<Product>();

		private IContainer components = null;
		private string connectionString = "Data Source=LOGICCOVE\\SQLEXPRESS;Initial Catalog=GroceryStoreDB;Integrated Security=True";
		private ListView productListView;
		private TextBox productIdTextBox;
		private TextBox productNameTextBox;
		private TextBox productPriceTextBox;
		private Button addButton;
		private Button updateButton;
		private Button deleteButton;
		private Button searchButton;
		private Button viewAllButton;
		private Button generateReportButton;

		private void InitializeComponent()
		{
			products = new List<Product>();
			productListView = new ListView();
			productIdTextBox = new TextBox();
			productNameTextBox = new TextBox();
			productPriceTextBox = new TextBox();
			addButton = new Button();
			updateButton = new Button();
			deleteButton = new Button();
			viewAllButton = new Button();
			searchButton = new Button();
			generateReportButton = new Button();
			productId = new Label();
			productName = new Label();
			productPrice = new Label();
			SuspendLayout();
			// 
			// productListView
			// 
			productListView.FullRowSelect = true;
			productListView.GridLines = true;
			productListView.Location = new Point(12, 12);
			productListView.Name = "productListView";
			productListView.Size = new Size(494, 380);
			productListView.TabIndex = 0;
			productListView.UseCompatibleStateImageBehavior = false;
			productListView.View = View.Details;
			// 
			// productIdTextBox
			// 
			productIdTextBox.Location = new Point(691, 24);
			productIdTextBox.Name = "productIdTextBox";
			productIdTextBox.Size = new Size(100, 27);
			productIdTextBox.TabIndex = 1;
			// 
			// productNameTextBox
			// 
			productNameTextBox.Location = new Point(691, 79);
			productNameTextBox.Name = "productNameTextBox";
			productNameTextBox.Size = new Size(100, 27);
			productNameTextBox.TabIndex = 2;
			// 
			// productPriceTextBox
			// 
			productPriceTextBox.Location = new Point(691, 132);
			productPriceTextBox.Name = "productPriceTextBox";
			productPriceTextBox.Size = new Size(100, 27);
			productPriceTextBox.TabIndex = 3;
			// 
			// addButton
			// 
			addButton.Location = new Point(557, 189);
			addButton.Name = "addButton";
			addButton.Size = new Size(75, 30);
			addButton.TabIndex = 4;
			addButton.Text = "Add";
			addButton.UseVisualStyleBackColor = true;
			addButton.Click += AddProductButton_Click;
			// 
			// updateButton
			// 
			updateButton.Location = new Point(652, 189);
			updateButton.Name = "updateButton";
			updateButton.Size = new Size(75, 30);
			updateButton.TabIndex = 5;
			updateButton.Text = "Update";
			updateButton.UseVisualStyleBackColor = true;
			updateButton.Click += UpdateProductButton_Click;
			// 
			// deleteButton
			// 
			deleteButton.Location = new Point(746, 189);
			deleteButton.Name = "deleteButton";
			deleteButton.Size = new Size(75, 30);
			deleteButton.TabIndex = 6;
			deleteButton.Text = "Delete";
			deleteButton.UseVisualStyleBackColor = true;
			deleteButton.Click += DeleteProductButton_Click;
			// 
			// viewAllButton
			// 
			viewAllButton.Location = new Point(557, 244);
			viewAllButton.Name = "viewAllButton";
			viewAllButton.Size = new Size(264, 30);
			viewAllButton.TabIndex = 7;
			viewAllButton.Text = "View All";
			viewAllButton.UseVisualStyleBackColor = true;
			viewAllButton.Click += ViewAllButton_Click;
			// 
			// searchButton
			// 
			searchButton.Location = new Point(557, 289);
			searchButton.Name = "searchButton";
			searchButton.Size = new Size(264, 30);
			searchButton.TabIndex = 8;
			searchButton.Text = "Search Product";
			searchButton.UseVisualStyleBackColor = true;
			searchButton.Click += SearchButton_Click;
			// 
			// generateReportButton
			// 
			generateReportButton.Location = new Point(557, 334);
			generateReportButton.Name = "generateReportButton";
			generateReportButton.Size = new Size(264, 30);
			generateReportButton.TabIndex = 9;
			generateReportButton.Text = "Generate Final Report";
			generateReportButton.UseVisualStyleBackColor = true;
			generateReportButton.Click += GenerateReportButton_Click;
			// 
			// productId
			// 
			productId.AutoSize = true;
			productId.Location = new Point(566, 33);
			productId.Name = "productId";
			productId.Size = new Size(50, 20);
			productId.TabIndex = 10;
			productId.Text = "productId";
			productId.Click += productId_Click;
			// 
			// productName
			// 
			productName.AutoSize = true;
			productName.Location = new Point(566, 82);
			productName.Name = "productName";
			productName.Size = new Size(50, 20);
			productName.TabIndex = 11;
			productName.Text = "productName";
			// 
			// productPrice
			// 
			productPrice.AutoSize = true;
			productPrice.Location = new Point(566, 135);
			productPrice.Name = "productPrice";
			productPrice.Size = new Size(50, 20);
			productPrice.TabIndex = 12;
			productPrice.Text = "productPrice";
			// 
			// Home
			// 
			ClientSize = new Size(817, 394);
			Controls.Add(productPrice);
			Controls.Add(productName);
			Controls.Add(productId);
			Controls.Add(productPriceTextBox);
			Controls.Add(productNameTextBox);
			Controls.Add(productIdTextBox);
			Controls.Add(productListView);
			Controls.Add(deleteButton);
			Controls.Add(updateButton);
			Controls.Add(addButton);
			Controls.Add(viewAllButton);
			Controls.Add(searchButton);
			Controls.Add(generateReportButton);
			Name = "Home";
			Text = "Grocery Store App";
			ResumeLayout(false);
			PerformLayout();
		}


		private void Home_Load(object sender, EventArgs e)
		{
			RetrieveProductsFromDatabase();
			RefreshProductListView();
		}


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

			Product selectedProduct = products.FirstOrDefault(p => p.Id == selectedProductId);
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

			Product selectedProduct = products.FirstOrDefault(p => p.Id == selectedProductId);
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

				// Clear existing items in the list view
				productListView.Items.Clear();

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
			string searchKeyword = productNameTextBox.Text;
			List<Product> searchResults = products.FindAll(p => p.Name.Contains(searchKeyword));

			if (searchResults.Count == 0)
			{
				MessageBox.Show("No products found matching the search criteria.");
			}
			else
			{
				productListView.Items.Clear();
				foreach (Product product in searchResults)
				{
					ListViewItem item = new ListViewItem(product.Id.ToString());
					item.SubItems.Add(product.Name);
					item.SubItems.Add(product.Price.ToString());
					productListView.Items.Add(item);
				}
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
				Product selectedProduct = products.Find(product => product.Id.ToString() == selectedProductId);
				if (selectedProduct != null)
				{
					productNameTextBox.Text = selectedProduct.Name;
					productPriceTextBox.Text = selectedProduct.Price.ToString();
				}
			}
		}

		private Label productId;
		private Label productName;
		private Label productPrice;
	}
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
	}
}