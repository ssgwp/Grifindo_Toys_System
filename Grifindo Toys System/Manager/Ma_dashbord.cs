using Grifindo_Toys_System.Admin;
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

namespace Grifindo_Toys_System.Manager
{
    public partial class Ma_dashbord : Form
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

        public Ma_dashbord()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }

        string connectionString = "Data Source=ASUS\\SQLEXPRESS;Initial Catalog=Grifindo_Toys_System;Integrated Security=True;";

        private void Ma_dashbord_Load(object sender, EventArgs e)
        {
            buttexit.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttexit.Width, buttexit.Height, 20, 20));
            buttlogout.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttlogout.Width, buttlogout.Height, 20, 20));
            menupanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, menupanel.Width, menupanel.Height, 20, 20));
            panelemp.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panelemp.Width, panelemp.Height, 40, 40));
            panelsal.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panelsal.Width, panelsal.Height, 40, 40));
            paneltoy.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, paneltoy.Width, paneltoy.Height, 40, 40));
            textBoxemp.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBoxemp.Width, textBoxemp.Height, 20, 20));
            textBoxsal.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBoxsal.Width, textBoxsal.Height, 20, 20));
            textBoxtoy.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBoxtoy.Width, textBoxtoy.Height, 20, 20));

            load_count();
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

        private void load_count()
        {
            // Employees count code
            try
            {
                using (SqlConnection Con = new SqlConnection(connectionString))
                {
                    Con.Open();

                    string query = "SELECT COUNT(emp_id) FROM employee";
                    SqlCommand cmd = new SqlCommand(query, Con);

                    int count = (int)cmd.ExecuteScalar();

                    textBoxemp.Text = count.ToString();

                    Con.Close();
                }
            }
            catch
            {
                //Handle the case
            }

            // Salaries count code
            try
            {
                using (SqlConnection Con = new SqlConnection(connectionString))
                {
                    Con.Open();

                    string query = "SELECT COUNT(salary_id) FROM salary";
                    SqlCommand cmd = new SqlCommand(query, Con);

                    int count = (int)cmd.ExecuteScalar();

                    textBoxsal.Text = count.ToString();

                    Con.Close();
                }
            }
            catch
            {
                //Handle the case
            }

            // Toyes count code
            try
            {
                using (SqlConnection Con = new SqlConnection(connectionString))
                {
                    Con.Open();

                    string query = "SELECT COUNT(toy_id) FROM toy";
                    SqlCommand cmd = new SqlCommand(query, Con);

                    int count = (int)cmd.ExecuteScalar();

                    textBoxtoy.Text = count.ToString();

                    Con.Close();
                }
            }
            catch
            {
                //Handle the case
            }
        }

        private void panelemp_Click(object sender, EventArgs e)
        {
            Ma_emp form = new Ma_emp();
            this.Close();
            form.Show();
        }

        private void panelsal_Click(object sender, EventArgs e)
        {
            Ma_salary form = new Ma_salary();
            this.Close();
            form.Show();
        }

        private void paneltoy_Click(object sender, EventArgs e)
        {
            Ma_toy form = new Ma_toy();
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

        private void labelacc_Click(object sender, EventArgs e)
        {
            Ma_acc form = new Ma_acc();
            this.Close();
            form.Show();
        }
    }
}
