using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GroceryStore
{
	partial class Login : Form
	{
		private string connectionString = "Data Source=LOGICCOVE\\SQLEXPRESS;Initial Catalog=GroceryStoreDB;Integrated Security=True";
		private IContainer components = null;

		private TextBox txtUsername;
		private TextBox txtPassword;
		private Button btnLogin;


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
			txtPassword = new TextBox();
			btnLogin = new Button();
			SuspendLayout();


			Label lblUsername = new Label();
			lblUsername.Text = "Username:";
			lblUsername.Location = new Point(50, 175);
			lblUsername.AutoSize = true;


			txtUsername.Location = new Point(143, 171);
			txtUsername.Name = "txtUsername";
			txtUsername.Size = new Size(200, 27);
			txtUsername.TabIndex = 0;


			Label lblPassword = new Label();
			lblPassword.Text = "Password:";
			lblPassword.Location = new Point(50, 225);
			lblPassword.AutoSize = true;

			txtPassword.Location = new Point(143, 225);
			txtPassword.Margin = new Padding(4, 5, 4, 5);
			txtPassword.Name = "txtPassword";
			txtPassword.PasswordChar = '*';
			txtPassword.Size = new Size(200, 27);
			txtPassword.TabIndex = 2;

			btnLogin.Location = new Point(193, 320);
			btnLogin.Name = "btnLogin";
			btnLogin.Size = new Size(100, 30);
			btnLogin.TabIndex = 2;
			btnLogin.Text = "Login";
			btnLogin.UseVisualStyleBackColor = true;
			btnLogin.Click += btnLogin_Click;


			ClientSize = new Size(502, 500);
			Controls.Add(lblUsername);
			Controls.Add(lblPassword);
			Controls.Add(txtUsername);
			Controls.Add(txtPassword);
			Controls.Add(btnLogin);
			Name = "Login";
			Text = "Login";
			ResumeLayout(false);
			PerformLayout();
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			string username = txtUsername.Text;
			string password = txtPassword.Text;

			try
			{
				if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
				{
					MessageBox.Show("Please fill in all the fields.");
					return;
				}

				if (!UserExists(username, password))
				{
					MessageBox.Show("Invalid username or password. Please try again.");
					return;
				}

				MessageBox.Show("Login successful!");

				Home homeForm = new Home();
				homeForm.Show();

				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error occurred during login. Please try again later.");
				Console.WriteLine(ex.ToString());
			}
		}

		private bool UserExists(string username, string password)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM users WHERE username = @username AND password = @password", connection);
					command.Parameters.AddWithValue("@username", username);
					command.Parameters.AddWithValue("@password", password);
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

	}
}
