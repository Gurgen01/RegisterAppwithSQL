using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp16
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            usernameField.Text = "Введите имя";
            usernameField.ForeColor = Color.Gray;
            LoginField.Text = "Введите логин";
            LoginField.ForeColor = Color.Gray;
            passwordfield.Text = "Введите пароль";
            passwordfield.ForeColor = Color.Gray;
            passwordfield.UseSystemPasswordChar = false;
            userSurnameField.Text = "Введите фамилию";
            userSurnameField.ForeColor = Color.Gray;
           
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }




        Point lastpoint;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void Closebutton_MouseEnter_1(object sender, EventArgs e)
        {
            Closebutton.ForeColor = Color.Red;
        }

        private void Closebutton_MouseLeave_1(object sender, EventArgs e)
        {
            Closebutton.ForeColor = Color.White;
        }

        private void usernameField_Enter(object sender, EventArgs e)
        {
            if (usernameField.Text == "Введите имя")
                usernameField.Text = "";
            usernameField.ForeColor = Color.Black;
        }

        private void usernameField_Leave(object sender, EventArgs e)
        {
            if (usernameField.Text == "")
            {
                usernameField.Text = "Введите имя";
                usernameField.ForeColor = Color.Gray;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (LoginField.Text == "Введите логин" || passwordfield.Text == "Введите пароль" || usernameField.Text == "Введите имя" || userSurnameField.Text == "Введите фамилию")
            {
                MessageBox.Show("Надо заполнить все поля");
                return;
            }

            if (isUserExists())
                return;
            DB db = new DB();
            SqlCommand command = new SqlCommand("INSERT INTO [users] ([login],[Password],[Name],[Surname]) VALUES (@login,@password,@name,@surname)", db.getconnection());
            command.Parameters.Add("@login", SqlDbType.VarChar).Value = LoginField.Text;
            command.Parameters.Add("@password", SqlDbType.VarChar).Value = passwordfield.Text;
            command.Parameters.Add("@name", SqlDbType.VarChar).Value = usernameField.Text;
            command.Parameters.Add("@surname", SqlDbType.VarChar).Value = userSurnameField.Text;

            db.openconnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Аккаунт  был создан");
            else
                MessageBox.Show("Аккаунт не был создан");


            db.closeconnection();
        }
        public bool isUserExists()
        {
            DB db = new DB();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("select * from users where [login]=@ul ", db.getconnection());
            command.Parameters.Add("@ul", SqlDbType.VarChar).Value = LoginField.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже существует!");
                return true;
            }
            else return false;
        }

        private void userSurnameField_Enter(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "Введите фамилию")
            {
                userSurnameField.Text = "";
                userSurnameField.ForeColor = Color.Black;
            }

        }

        private void userSurnameField_Leave(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "")
            {
                userSurnameField.Text = "Введите фамилию";
                userSurnameField.ForeColor = Color.Gray;

            }
        }

        private void LoginField_Leave(object sender, EventArgs e)
        {
            if (LoginField.Text == "")
            {
                LoginField.Text = "Введите логин"; 
                LoginField.ForeColor = Color.Gray;

            }

        }

        private void LoginField_Enter(object sender, EventArgs e)
        {
            if (LoginField.Text == "Введите логин")
            {
                LoginField.Text = "";
                LoginField.ForeColor = Color.Black;
            }
        }

        private void passwordfield_Leave(object sender, EventArgs e)
        {
            if (passwordfield.Text == "")
            {
                passwordfield.UseSystemPasswordChar = false;
                passwordfield.Text = "Введите пароль";
                passwordfield.ForeColor = Color.Gray;

            }
        }

        private void passwordfield_Enter(object sender, EventArgs e)
        {
            if (passwordfield.Text == "Введите пароль")
            {
                passwordfield.Text = "";
                passwordfield.UseSystemPasswordChar = true;
                passwordfield.ForeColor = Color.Black;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginform = new LoginForm();
            loginform.Show();
        }
    }
}
