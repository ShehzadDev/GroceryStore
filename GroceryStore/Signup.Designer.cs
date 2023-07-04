using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GroceryStore
{
	partial class Signup : Form
	{
		private string connectionString = "Data Source=LOGICCOVE\\SQLEXPRESS;Initial Catalog=GroceryStoreDB;Integrated Security=True";
		private IContainer components = null;

		private TextBox txtUsername;
		private TextBox txtEmail;
		private TextBox txtPassword;
		private Button btnSignup;


		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			txtUsername = new TextBox();
			txtEmail = new TextBox();
			txtPassword = new TextBox();
			btnSignup = new Button();
			lblUsername = new Label();
			lblEmail = new Label();
			lblPassword = new Label();
			SuspendLayout();



			txtUsername.Location = new Point(153, 151);
			txtUsername.Margin = new Padding(4, 5, 4, 5);
			txtUsername.Name = "txtUsername";
			txtUsername.Size = new Size(265, 27);
			txtUsername.TabIndex = 0;



			txtEmail.Location = new Point(153, 232);
			txtEmail.Margin = new Padding(4, 5, 4, 5);
			txtEmail.Name = "txtEmail";
			txtEmail.Size = new Size(265, 27);
			txtEmail.TabIndex = 1;
			txtEmail.TextChanged += txtEmail_TextChanged;



			txtPassword.Location = new Point(153, 309);
			txtPassword.Margin = new Padding(4, 5, 4, 5);
			txtPassword.Name = "txtPassword";
			txtPassword.PasswordChar = '*';
			txtPassword.Size = new Size(265, 27);
			txtPassword.TabIndex = 2;



			btnSignup.Location = new Point(133, 385);
			btnSignup.Margin = new Padding(4, 5, 4, 5);
			btnSignup.Name = "btnSignup";
			btnSignup.Size = new Size(133, 46);
			btnSignup.TabIndex = 3;
			btnSignup.Text = "Signup";
			btnSignup.UseVisualStyleBackColor = true;
			btnSignup.Click += btnSignup_Click;



			lblUsername.AutoSize = true;
			lblUsername.Location = new Point(67, 158);
			lblUsername.Margin = new Padding(4, 0, 4, 0);
			lblUsername.Name = "lblUsername";
			lblUsername.Size = new Size(78, 20);
			lblUsername.TabIndex = 0;
			lblUsername.Text = "Username:";



			lblEmail.AutoSize = true;
			lblEmail.Location = new Point(67, 235);
			lblEmail.Margin = new Padding(4, 0, 4, 0);
			lblEmail.Name = "lblEmail";
			lblEmail.Size = new Size(49, 20);
			lblEmail.TabIndex = 1;
			lblEmail.Text = "Email:";



			lblPassword.AutoSize = true;
			lblPassword.Location = new Point(67, 312);
			lblPassword.Margin = new Padding(4, 0, 4, 0);
			lblPassword.Name = "lblPassword";
			lblPassword.Size = new Size(73, 20);
			lblPassword.TabIndex = 2;
			lblPassword.Text = "Password:";



			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(533, 615);
			Controls.Add(lblUsername);
			Controls.Add(lblEmail);
			Controls.Add(lblPassword);
			Controls.Add(btnSignup);
			Controls.Add(txtPassword);
			Controls.Add(txtEmail);
			Controls.Add(txtUsername);
			Margin = new Padding(4, 5, 4, 5);
			Name = "Signup";
			Text = "Signup";
			ResumeLayout(false);
			PerformLayout();
		}

		private void btnSignup_Click(object sender, EventArgs e)
		{
			string username = txtUsername.Text;
			string email = txtEmail.Text;
			string password = txtPassword.Text;

			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
			{
				MessageBox.Show("Please fill in all the fields.");
				return;
			}

			if (UserExists(username,email))
			{
				MessageBox.Show("Username already exists. Please choose a different username.");
				return;
			}

			InsertUser(username, email, password);

			MessageBox.Show("Signup successful!");

			Login loginForm = new Login();
			loginForm.Show();

			this.Close();
		}

		private bool UserExists(string username, string email)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM users WHERE username = @username OR email = @email", connection);
					command.Parameters.AddWithValue("@username", username);
					command.Parameters.AddWithValue("@email", email);
					int count = (int)command.ExecuteScalar();
					return count > 0;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error occurred while checking user existence. Please try again later.");
				Console.WriteLine(ex.ToString());
				return false;
			}
		}


		private void InsertUser(string username, string email, string password)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand("INSERT INTO users (username, email, password) VALUES (@username, @email, @password)", connection);
				command.Parameters.AddWithValue("@username", username);
				command.Parameters.AddWithValue("@email", email);
				command.Parameters.AddWithValue("@password", password);
				command.ExecuteNonQuery();
			}
		}

		private Label lblUsername;
		private Label lblEmail;
		private Label lblPassword;
	}
}
