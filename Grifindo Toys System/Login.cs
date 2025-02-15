using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grifindo_Toys_System
{
    public partial class Login : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeft,
                int nTop,
                int nRight,
                int nBottom,
                int nWidhtEllipse,
                int nHeightEllipse
            );

        public Login()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }

        string connectionString = "Data Source=ASUS\\SQLEXPRESS;Initial Catalog=Grifindo_Toys_System;Integrated Security=True;";

        private void Login_Load(object sender, EventArgs e)
        {
            buttlog.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttlog.Width, buttlog.Height, 30, 30));
            buttclrear.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttclrear.Width, buttclrear.Height, 20, 20));
            buttexit.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttexit.Width, buttexit.Height, 20, 20));
        }

        private void piclose_MouseLeave(object sender, EventArgs e)
        {
            piclose.BackColor = Color.WhiteSmoke;
        }

        private void piclose_MouseHover(object sender, EventArgs e)
        {
            piclose.BackColor = Color.Red;
        }

        private void pimini_MouseLeave(object sender, EventArgs e)
        {
            pimini.BackColor = Color.WhiteSmoke;
        }

        private void pimini_MouseHover(object sender, EventArgs e)
        {
            pimini.BackColor = Color.Gray;
        }

        private void pimini_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void piclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttexit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttclrear_Click(object sender, EventArgs e)
        {
            textuser.Clear();
            textpas.Clear();
            checkBox1.Checked = false;
            textuser.Focus();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textpas.UseSystemPasswordChar = false;
            }
            else
            {
                textpas.UseSystemPasswordChar = true;
            }
        }

        private void textpas_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textpas.UseSystemPasswordChar = false;
            }
            else
            {
                textpas.UseSystemPasswordChar = true;
            }
        }

        private void buttlog_Click(object sender, EventArgs e)
        {
            login();
        }

        private void textpas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                login();
            }
        }

        private void login()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    String querry = "SELECT * FROM login WHERE username = @username AND password = @password AND status = @status AND login_type = @login_type";
                    SqlCommand cmd = new SqlCommand(querry, con);
                    cmd.Parameters.AddWithValue("@username", textuser.Text);
                    cmd.Parameters.AddWithValue("@password", textpas.Text);
                    cmd.Parameters.AddWithValue("@status", "Online");

                    //check Addmin Login
                    cmd.Parameters.AddWithValue("@login_type", "Admin");

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        this.Hide();
                        Admin.Ad_dashbord dashboard = new Admin.Ad_dashbord();
                        dashboard.Show();
                        return; 
                    }

                    //Manager login
                    cmd.Parameters["@login_type"].Value = "Manager";
                    sda.SelectCommand = cmd;
                    dt.Clear();
                    sda.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {
                        this.Hide();
                        Manager.Ma_dashbord dashboard = new Manager.Ma_dashbord();
                        dashboard.Show();
                        return;
                    }

                    MessageBox.Show("Invalid Login Details and Please Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textuser.Clear();
                    textpas.Clear();
                    checkBox1.Checked = false;
                    textuser.Focus();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    con.Close();
                }
            }
        }
    }
}
