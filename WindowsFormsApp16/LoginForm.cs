using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp16
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.PassField.AutoSize = false;
            this.PassField.Size = new Size(this.PassField.Width, 64);
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Closebutton_MouseEnter(object sender, EventArgs e)
        {
            Closebutton.ForeColor = Color.Red;
        }

        private void Closebutton_MouseLeave(object sender, EventArgs e)
        {
            Closebutton.ForeColor = Color.White;
        }
        Point lastpoint;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button== MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y -lastpoint.Y ;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string loginuser = LoginField.Text;
            string passuser = PassField.Text;
            DB db = new DB();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("select * from users where [login]=@ul and [Password]=@up",db.getconnection());
            command.Parameters.Add("@ul", SqlDbType.VarChar).Value = loginuser;
            command.Parameters.Add("@up", SqlDbType.VarChar).Value = passuser;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                this.Hide();
                MainForm mainform = new MainForm();
                mainform.Show();
            }
            else MessageBox.Show("no");
        }

        private void Registerlabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm registerform = new RegisterForm();
            registerform.Show();
        }
    }
}
