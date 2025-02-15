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

namespace Grifindo_Toys_System.Admin
{
    public partial class Ad_logins : Form
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

        public Ad_logins()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }

        string connectionString = "Data Source=ASUS\\SQLEXPRESS;Initial Catalog=Grifindo_Toys_System;Integrated Security=True;";

        private void Ad_logins_Load(object sender, EventArgs e)
        {
            buttexit.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttexit.Width, buttexit.Height, 20, 20));
            buttlogout.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttlogout.Width, buttlogout.Height, 20, 20));
            menupanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, menupanel.Width, menupanel.Height, 20, 20));
            buttonclandre.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonclandre.Width, buttonclandre.Height, 20, 20));
            buttonadddlog.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonadddlog.Width, buttonadddlog.Height, 20, 20));
            buttoneditde.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttoneditde.Width, buttoneditde.Height, 20, 20));
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 20, 20));

            load_data();
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
            Ad_dashbord form = new Ad_dashbord();
            this.Close();
            form.Show();
        }

        private void labelstaff_Click(object sender, EventArgs e)
        {
            Ad_emp form = new Ad_emp();
            this.Close();
            form.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Ad_salary form = new Ad_salary();
            this.Close();
            form.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Ad_setting form = new Ad_setting();
            this.Close();
            form.Show();
        }

        private void load_data()
        {
            //Load data gried view 1
            try
            {
                using (SqlConnection Con = new SqlConnection(connectionString))
                {
                    Con.Open();

                    string query = "SELECT DISTINCT username, login_type, status FROM login";

                    using (SqlCommand cmd = new SqlCommand(query, Con))
                    {

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                        Con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            //username combobox
            try
            {
                using (SqlConnection Con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdc = new SqlCommand("SELECT * FROM login", Con);
                    SqlDataAdapter dac = new SqlDataAdapter();
                    dac.SelectCommand = cmdc;
                    DataTable table1 = new DataTable();
                    dac.Fill(table1);

                    comboBoxuser.DataSource = table1;
                    comboBoxuser.DisplayMember = "username";
                    comboBoxuser.ValueMember = "username";
                    Con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void buttonclandre_Click(object sender, EventArgs e)
        {
            Ad_logins form = new Ad_logins();
            this.Close();
            form.Show();
        }

        private void textBoxpass_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBoxpass.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxpass.UseSystemPasswordChar = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBoxpass.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxpass.UseSystemPasswordChar = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                comboBoxuser.Text = row.Cells["username"].Value.ToString();
                comboBoxlogst.Text = row.Cells["status"].Value.ToString();
                comboBoxlogtype.Text = row.Cells["login_type"].Value.ToString();

                if (row.Cells["username"].Value != null && row.Cells["username"].Value != DBNull.Value)
                {
                    string usern = row.Cells["username"].Value.ToString();

                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        SqlCommand cmd = Con.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = "SELECT username, password FROM login WHERE username = @username";
                        cmd.Parameters.AddWithValue("@username", usern);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            textBoxpass.Text = dt.Rows[0]["password"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Username not found in the database.");
                        }
                        Con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Username cell is empty.");
                }
            }
        }

        private void buttonadddlog_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxuser.Text) && !string.IsNullOrEmpty(textBoxpass.Text))
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;

                            // Use parameterized query
                            cmd.CommandText = "INSERT INTO [login] (username, password, login_type, status) VALUES (@username, @password, @login_type, @status)";

                            // Add parameters
                            cmd.Parameters.AddWithValue("@username", comboBoxuser.Text);
                            cmd.Parameters.AddWithValue("@password", textBoxpass.Text);
                            cmd.Parameters.AddWithValue("@login_type", comboBoxlogtype.Text);
                            cmd.Parameters.AddWithValue("@status", comboBoxlogst.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Login Details Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Ad_logins form = new Ad_logins();
                    this.Close();
                    form.Show();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"An SQL error occurred: {sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Handle general errors
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Handle case when required fields are empty
                MessageBox.Show("Username and password cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttoneditde_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxuser.Text) && !string.IsNullOrEmpty(textBoxpass.Text))
            {
                using (SqlConnection Con = new SqlConnection(connectionString))
                {
                    Con.Open();
                    SqlCommand cmd = Con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    // Use parameterized query
                    cmd.CommandText = "UPDATE [login] SET password = @password, login_type = @login_type, status = @status WHERE username = @username";

                    // Add parameters
                    cmd.Parameters.AddWithValue("@username", comboBoxuser.Text);
                    cmd.Parameters.AddWithValue("@password", textBoxpass.Text);
                    cmd.Parameters.AddWithValue("@login_type", comboBoxlogtype.Text);
                    cmd.Parameters.AddWithValue("@status", comboBoxlogst.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Login Details Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Ad_logins form = new Ad_logins();
                        this.Close();
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("Username not found in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Username cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxuser.Text))
            {
                using (SqlConnection Con = new SqlConnection(connectionString))
                {
                    Con.Open();
                    SqlCommand cmd = Con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    // Use parameterized query
                    cmd.CommandText = "DELETE FROM [login] WHERE username = @username";

                    // Add parameter
                    cmd.Parameters.AddWithValue("@username", comboBoxuser.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Login Details Deleted Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Ad_logins form = new Ad_logins();
                        this.Close();
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("Username not found in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Username cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void comboBoxuser_SelectedIndexChanged(object sender, EventArgs e)
        {
            string usern = comboBoxuser.SelectedValue as string;
            if (!string.IsNullOrEmpty(usern))
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        SqlCommand cmd = Con.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = "SELECT * FROM login WHERE username = @username";
                        cmd.Parameters.AddWithValue("@username", usern);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            textBoxpass.Text = dt.Rows[0]["password"].ToString();
                            comboBoxlogtype.Text = dt.Rows[0]["login_type"].ToString();
                            comboBoxlogst.Text = dt.Rows[0]["status"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("An error select username:");
                        }
                        Con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                // Handle the case
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Ad_toy form = new Ad_toy();
            this.Close();
            form.Show();
        }
    }
}
