namespace GroceryStore
{
	partial class MainForm
	{
		private System.ComponentModel.IContainer components = null;

		private System.Windows.Forms.Label Name;
		private System.Windows.Forms.Label Slogan;
		private System.Windows.Forms.Button btnSignup;
		private System.Windows.Forms.Button btnLogin;


		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);



			this.Name = new System.Windows.Forms.Label();
			this.Slogan = new System.Windows.Forms.Label();
			this.btnSignup = new System.Windows.Forms.Button();
			this.btnLogin = new System.Windows.Forms.Button();



			this.Name.AutoSize = true;
			this.Name.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name.Location = new System.Drawing.Point(100, 100);
			this.Name.Name = "Name";
			this.Name.Size = new System.Drawing.Size(218, 25);
			this.Name.TabIndex = 0;
			this.Name.Text = "Swirl Cash n Carry";

			
			this.Slogan.AutoSize = true;
			this.Slogan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Slogan.Location = new System.Drawing.Point(100, 150);
			this.Slogan.Name = "Slogan";
			this.Slogan.Size = new System.Drawing.Size(167, 18);
			this.Slogan.TabIndex = 1;
			this.Slogan.Text = "Let's But It.";

			
			this.btnSignup.Location = new System.Drawing.Point(100, 200);
			this.btnSignup.Name = "btnSignup";
			this.btnSignup.Size = new System.Drawing.Size(100, 30);
			this.btnSignup.TabIndex = 2;
			this.btnSignup.Text = "Signup";
			this.btnSignup.UseVisualStyleBackColor = true;
			this.btnSignup.Click += new System.EventHandler(this.btnSignup_Click);

			
			this.btnLogin.Location = new System.Drawing.Point(220, 200);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(100, 30);
			this.btnLogin.TabIndex = 3;
			this.btnLogin.Text = "Login";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

			
			this.Controls.Add(this.Name);
			this.Controls.Add(this.Slogan);
			this.Controls.Add(this.btnSignup);
			this.Controls.Add(this.btnLogin);

			this.Text = "Grocery Store";
		}

		#endregion

		
		private void btnSignup_Click(object sender, EventArgs e)
		{
			
			Signup signupForm = new Signup();
			signupForm.Show();
		}

		
		private void btnLogin_Click(object sender, EventArgs e)
		{ 
			Login loginForm = new Login();
			loginForm.Show();
		}
	}
}
