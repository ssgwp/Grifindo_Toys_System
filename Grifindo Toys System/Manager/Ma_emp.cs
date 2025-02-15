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
    public partial class Ma_emp : Form
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

        public Ma_emp()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }

        string connectionString = "Data Source=ASUS\\SQLEXPRESS;Initial Catalog=Grifindo_Toys_System;Integrated Security=True;";


        private void Ma_emp_Load(object sender, EventArgs e)
        {
            buttexit.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttexit.Width, buttexit.Height, 20, 20));
            buttlogout.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttlogout.Width, buttlogout.Height, 20, 20));
            menupanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, menupanel.Width, menupanel.Height, 20, 20));
            buttonadddde.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonadddde.Width, buttonadddde.Height, 20, 20));
            buttoneditde.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttoneditde.Width, buttoneditde.Height, 20, 20));
            buttondelde.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttondelde.Width, buttondelde.Height, 20, 20));
            buttonclandre.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonclandre.Width, buttonclandre.Height, 20, 20));

            load_data();
        }

        private void load_data()
        {
            //Load data gried view 1
            try
            {
                using (SqlConnection Con = new SqlConnection(connectionString))
                {
                    Con.Open();

                    string query = "SELECT DISTINCT emp_id, f_name, l_name, nic, phone_no1, gender FROM employee";

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

            //Employee ID combobox
            try
            {
                using (SqlConnection Con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdc = new SqlCommand("SELECT * FROM employee", Con);
                    SqlDataAdapter dac = new SqlDataAdapter();
                    dac.SelectCommand = cmdc;
                    DataTable table1 = new DataTable();
                    dac.Fill(table1);

                    comboBoxempid.DataSource = table1;
                    comboBoxempid.DisplayMember = "emp_id";
                    comboBoxempid.ValueMember = "emp_id";
                    Con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
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

        private void buttonclandre_Click(object sender, EventArgs e)
        {
            Ma_emp form = new Ma_emp();
            this.Close();
            form.Show();
        }

        private void comboBoxempid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(comboBoxempid.SelectedValue?.ToString(), out int id))
                {
                    // Conversion successful
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM employee WHERE emp_id = @emp_id";

                            // Use SqlParameter
                            cmd.Parameters.Add(new SqlParameter("@emp_id", SqlDbType.Int)).Value = id;

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    textBoxfname.Text = dt.Rows[0]["f_name"].ToString();
                                    textBoxlname.Text = dt.Rows[0]["l_name"].ToString();
                                    textBoxnic.Text = dt.Rows[0]["nic"].ToString();
                                    textBoxemail.Text = dt.Rows[0]["email"].ToString();
                                    textBoxcono.Text = dt.Rows[0]["phone_no1"].ToString();
                                    comboBoxgender.Text = dt.Rows[0]["gender"].ToString();
                                    textBoxyele.Text = dt.Rows[0]["yearly_leave"].ToString();
                                    textBoxbmsa.Text = dt.Rows[0]["mothly_salary"].ToString();
                                }
                                else
                                {
                                    // Handle the case
                                    MessageBox.Show("No records found for the selected ID.");
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Handle the case
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                comboBoxempid.Text = row.Cells["emp_id"].Value.ToString();
                textBoxfname.Text = row.Cells["f_name"].Value.ToString();
                textBoxlname.Text = row.Cells["l_name"].Value.ToString();
                textBoxnic.Text = row.Cells["nic"].Value.ToString();
                textBoxcono.Text = row.Cells["phone_no1"].Value.ToString();
                comboBoxgender.Text = row.Cells["gender"].Value.ToString();

                if (int.TryParse(row.Cells["emp_id"].Value.ToString(), out int pacid))
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        SqlCommand cmd = Con.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = "SELECT * FROM employee WHERE emp_id = @emp_id";
                        cmd.Parameters.AddWithValue("@emp_id", pacid);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            textBoxemail.Text = dt.Rows[0]["email"].ToString();
                            textBoxyele.Text = dt.Rows[0]["yearly_leave"].ToString();
                            textBoxbmsa.Text = dt.Rows[0]["mothly_salary"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No data found for emp_id " + pacid.ToString(), "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        Con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid emp_id value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonadddde_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxfname.Text) && !string.IsNullOrEmpty(textBoxlname.Text) && !string.IsNullOrEmpty(textBoxnic.Text) && !string.IsNullOrEmpty(textBoxemail.Text) && !string.IsNullOrEmpty(textBoxcono.Text) && !string.IsNullOrEmpty(textBoxyele.Text) && !string.IsNullOrEmpty(textBoxbmsa.Text))
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
                            cmd.CommandText = "INSERT INTO [employee] (f_name, l_name, nic, email, phone_no1, gender, yearly_leave, mothly_salary) VALUES (@f_name, @l_name, @nic, @email, @phone_no1, @gender, @yearly_leave, @mothly_salary)";

                            // Add parameters
                            cmd.Parameters.AddWithValue("@f_name", textBoxfname.Text);
                            cmd.Parameters.AddWithValue("@l_name", textBoxlname.Text);
                            cmd.Parameters.AddWithValue("@nic", textBoxnic.Text);
                            cmd.Parameters.AddWithValue("@email", textBoxemail.Text);
                            cmd.Parameters.AddWithValue("@phone_no1", textBoxcono.Text);
                            cmd.Parameters.AddWithValue("@gender", comboBoxgender.Text);
                            cmd.Parameters.AddWithValue("@yearly_leave", textBoxyele.Text);
                            cmd.Parameters.AddWithValue("@mothly_salary", textBoxbmsa.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Employee Details Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Ma_emp form = new Ma_emp();
                    this.Close();
                    form.Show();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"An SQL error occurred: {sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Handle case when required fields are empty
                MessageBox.Show("Fill in all the fields that can't be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttoneditde_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxfname.Text) && !string.IsNullOrEmpty(textBoxlname.Text) && !string.IsNullOrEmpty(textBoxnic.Text) && !string.IsNullOrEmpty(textBoxemail.Text) && !string.IsNullOrEmpty(textBoxcono.Text) && !string.IsNullOrEmpty(textBoxyele.Text) && !string.IsNullOrEmpty(textBoxbmsa.Text))
            {
                if (comboBoxempid.SelectedValue != null && int.TryParse(comboBoxempid.SelectedValue.ToString(), out int selectedEmployeeId))
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
                                cmd.CommandText = "UPDATE [employee] SET f_name = @f_name, l_name = @l_name, nic = @nic, email = @email, phone_no1 = @phone_no1, gender = @gender, yearly_leave = @yearly_leave, mothly_salary = @mothly_salary WHERE emp_id = @emp_id";

                                // Add parameters
                                cmd.Parameters.AddWithValue("@f_name", textBoxfname.Text);
                                cmd.Parameters.AddWithValue("@l_name", textBoxlname.Text);
                                cmd.Parameters.AddWithValue("@nic", textBoxnic.Text);
                                cmd.Parameters.AddWithValue("@email", textBoxemail.Text);
                                cmd.Parameters.AddWithValue("@phone_no1", textBoxcono.Text);
                                cmd.Parameters.AddWithValue("@gender", comboBoxgender.Text);
                                cmd.Parameters.AddWithValue("@yearly_leave", textBoxyele.Text);
                                cmd.Parameters.AddWithValue("@mothly_salary", textBoxbmsa.Text);
                                cmd.Parameters.AddWithValue("@emp_id", selectedEmployeeId);

                                cmd.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("Employee Details Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Ma_emp form = new Ma_emp();
                        this.Close();
                        form.Show();
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show($"An SQL error occurred: {sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Handle case when the selected employee ID is not valid
                    MessageBox.Show("Selected value is not a valid Employee ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Handle case when required fields are empty
                MessageBox.Show("All fields are required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttondelde_Click(object sender, EventArgs e)
        {
            if (comboBoxempid.SelectedValue != null && int.TryParse(comboBoxempid.SelectedValue.ToString(), out int selectedEmployeeId))
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
                            cmd.CommandText = "DELETE FROM [salary] WHERE emp_id = @emp_id";

                            // Add parameters
                            cmd.Parameters.AddWithValue("@emp_id", selectedEmployeeId);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;

                            // Use parameterized query for the delete statement
                            cmd.CommandText = "DELETE FROM [employee] WHERE emp_id = @emp_id";

                            // Add parameters
                            cmd.Parameters.AddWithValue("@emp_id", selectedEmployeeId);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Employee Record Deleted Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Ma_emp form = new Ma_emp();
                    this.Close();
                    form.Show();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"An SQL error occurred: {sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Handle case
                MessageBox.Show("Selected value is not a valid Employee ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
