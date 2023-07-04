using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace GroceryStore
{
	partial class Home : Form
	{

		private IContainer components = null;

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
			searchBox = new TextBox();
			searchlabel = new Label();
			SuspendLayout();
			// 
			// productListView
			// 
			productListView.FullRowSelect = true;
			productListView.GridLines = true;
			productListView.Location = new Point(12, 92);
			productListView.Name = "productListView";
			productListView.Size = new Size(690, 362);
			productListView.TabIndex = 0;
			productListView.UseCompatibleStateImageBehavior = false;
			productListView.View = View.Details;
			// 
			// productIdTextBox
			// 
			productIdTextBox.Location = new Point(905, 83);
			productIdTextBox.Name = "productIdTextBox";
			productIdTextBox.Size = new Size(100, 27);
			productIdTextBox.TabIndex = 1;
			// 
			// productNameTextBox
			// 
			productNameTextBox.Location = new Point(905, 138);
			productNameTextBox.Name = "productNameTextBox";
			productNameTextBox.Size = new Size(100, 27);
			productNameTextBox.TabIndex = 2;
			// 
			// productPriceTextBox
			// 
			productPriceTextBox.Location = new Point(905, 191);
			productPriceTextBox.Name = "productPriceTextBox";
			productPriceTextBox.Size = new Size(100, 27);
			productPriceTextBox.TabIndex = 3;
			// 
			// addButton
			// 
			addButton.Location = new Point(741, 231);
			addButton.Name = "addButton";
			addButton.Size = new Size(264, 30);
			addButton.TabIndex = 4;
			addButton.Text = "Add";
			addButton.UseVisualStyleBackColor = true;
			addButton.Click += AddProductButton_Click;
			// 
			// updateButton
			// 
			updateButton.Location = new Point(741, 304);
			updateButton.Name = "updateButton";
			updateButton.Size = new Size(264, 30);
			updateButton.TabIndex = 5;
			updateButton.Text = "Update";
			updateButton.UseVisualStyleBackColor = true;
			updateButton.Click += UpdateProductButton_Click;
			// 
			// deleteButton
			// 
			deleteButton.Location = new Point(741, 340);
			deleteButton.Name = "deleteButton";
			deleteButton.Size = new Size(264, 30);
			deleteButton.TabIndex = 6;
			deleteButton.Text = "Delete";
			deleteButton.UseVisualStyleBackColor = true;
			deleteButton.Click += DeleteProductButton_Click;
			// 
			// viewAllButton
			// 
			viewAllButton.Location = new Point(741, 381);
			viewAllButton.Name = "viewAllButton";
			viewAllButton.Size = new Size(264, 30);
			viewAllButton.TabIndex = 7;
			viewAllButton.Text = "View All";
			viewAllButton.UseVisualStyleBackColor = true;
			viewAllButton.Click += ViewAllButton_Click;
			// 
			// searchButton
			// 
			searchButton.Location = new Point(570, 42);
			searchButton.Name = "searchButton";
			searchButton.Size = new Size(132, 30);
			searchButton.TabIndex = 8;
			searchButton.Text = "Search Product";
			searchButton.UseVisualStyleBackColor = true;
			searchButton.Click += SearchButton_Click;
			// 
			// generateReportButton
			// 
			generateReportButton.Location = new Point(741, 422);
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
			productId.Location = new Point(741, 89);
			productId.Name = "productId";
			productId.Size = new Size(74, 20);
			productId.TabIndex = 10;
			productId.Text = "productId";
			productId.Click += productId_Click;
			// 
			// productName
			// 
			productName.AutoSize = true;
			productName.Location = new Point(741, 138);
			productName.Name = "productName";
			productName.Size = new Size(101, 20);
			productName.TabIndex = 11;
			productName.Text = "productName";
			// 
			// productPrice
			// 
			productPrice.AutoSize = true;
			productPrice.Location = new Point(741, 191);
			productPrice.Name = "productPrice";
			productPrice.Size = new Size(93, 20);
			productPrice.TabIndex = 12;
			productPrice.Text = "productPrice";
			// 
			// searchBox
			// 
			searchBox.Location = new Point(129, 42);
			searchBox.Name = "searchBox";
			searchBox.Size = new Size(378, 27);
			searchBox.TabIndex = 13;
			// 
			// searchlabel
			// 
			searchlabel.AutoSize = true;
			searchlabel.Location = new Point(12, 45);
			searchlabel.Name = "searchlabel";
			searchlabel.Size = new Size(89, 20);
			searchlabel.TabIndex = 14;
			searchlabel.Text = "Search Here";
			// 
			// Home
			// 
			ClientSize = new Size(1066, 462);
			Controls.Add(searchlabel);
			Controls.Add(searchBox);
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
			Load += Home_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		private Label productId;
		private Label productName;
		private Label productPrice;
		private TextBox searchBox;
		private Label searchlabel;
	}
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
	}
}