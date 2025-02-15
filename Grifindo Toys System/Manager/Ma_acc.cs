using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grifindo_Toys_System.Manager
{
    public partial class Ma_acc : Form
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

        public Ma_acc()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }

        string connectionString = "Data Source=ASUS\\SQLEXPRESS;Initial Catalog=Grifindo_Toys_System;Integrated Security=True;";


        private void Ma_acc_Load(object sender, EventArgs e)
        {
            buttexit.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttexit.Width, buttexit.Height, 20, 20));
            buttlogout.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttlogout.Width, buttlogout.Height, 20, 20));
            menupanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, menupanel.Width, menupanel.Height, 20, 20));
            button2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 20, 20));
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 20, 20));
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

        private void buttlogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want Logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
                Login form = new Login();
                form.Show();
            }
        }

        private void buttexit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void labelhome_Click(object sender, EventArgs e)
        {
            Ma_dashbord form = new Ma_dashbord();
            this.Close();
            form.Show();
        }

        private void labelstaff_Click(object sender, EventArgs e)
        {
            Ma_emp form = new Ma_emp();
            this.Close();
            form.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Ma_salary form = new Ma_salary();
            this.Close();
            form.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Ma_toy form = new Ma_toy();
            this.Close();
            form.Show();
        }

        private void checkBoxnp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxnp.Checked == true)
            {
                textBoxnpass.UseSystemPasswordChar = false;
                textBoxrnpass.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxnpass.UseSystemPasswordChar = true;
                textBoxrnpass.UseSystemPasswordChar = true;
            }
        }

        private void textBoxrnpass_TextChanged(object sender, EventArgs e)
        {
            if (checkBoxnp.Checked == true)
            {
                textBoxnpass.UseSystemPasswordChar = false;
                textBoxrnpass.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxnpass.UseSystemPasswordChar = true;
                textBoxrnpass.UseSystemPasswordChar = true;
            }
        }

        private void textBoxnpass_TextChanged(object sender, EventArgs e)
        {
            if (checkBoxnp.Checked == true)
            {
                textBoxnpass.UseSystemPasswordChar = false;
                textBoxrnpass.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxnpass.UseSystemPasswordChar = true;
                textBoxrnpass.UseSystemPasswordChar = true;
            }
        }

        private void checkBoxcp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxcp.Checked == true)
            {
                textBoxcpass.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxcpass.UseSystemPasswordChar = true;
            }
        }

        private void textBoxcpass_TextChanged(object sender, EventArgs e)
        {
            if (checkBoxcp.Checked == true)
            {
                textBoxcpass.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxcpass.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxusername.Clear();
            textBoxcpass.Clear();
            textBoxnpass.Clear();
            textBoxrnpass.Clear();
            checkBoxcp.Checked = false;
            checkBoxnp.Checked = false;
            textBoxusername.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query2 = "SELECT * FROM login WHERE username = @user";
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("@user", textBoxusername.Text);

                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    sda2.Fill(dt2);

                    if (dt2.Rows.Count > 0)
                    {
                        string cupass = dt2.Rows[0]["password"].ToString();

                        if ((textBoxcpass.Text == cupass) && (textBoxnpass.Text == textBoxrnpass.Text))
                        {
                            SqlCommand cmd3 = new SqlCommand("UPDATE [login] SET password = @pass WHERE username = @user", con);
                            cmd3.Parameters.AddWithValue("@pass", textBoxnpass.Text);
                            cmd3.Parameters.AddWithValue("@user", textBoxusername.Text);
                            cmd3.ExecuteNonQuery();

                            MessageBox.Show("Password changed successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            textBoxcpass.Text = "";
                            textBoxnpass.Text = "";
                            textBoxrnpass.Text = "";

                            this.Hide();
                            Ma_acc dashboard = new Ma_acc();
                            dashboard.Show();
                        }
                        else
                        {
                            MessageBox.Show("Passwords do not match or current password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }

    }
}
