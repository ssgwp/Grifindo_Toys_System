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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Grifindo_Toys_System.Admin
{
    public partial class Ad_salary : Form
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

        public Ad_salary()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }

        string connectionString = "Data Source=ASUS\\SQLEXPRESS;Initial Catalog=Grifindo_Toys_System;Integrated Security=True;";

        private void Ad_salary_Load(object sender, EventArgs e)
        {
            buttexit.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttexit.Width, buttexit.Height, 20, 20));
            buttlogout.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttlogout.Width, buttlogout.Height, 20, 20));
            menupanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, menupanel.Width, menupanel.Height, 20, 20));
            buttonadddde.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonadddde.Width, buttonadddde.Height, 20, 20));
            buttoneditde.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttoneditde.Width, buttoneditde.Height, 20, 20));
            buttondelde.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttondelde.Width, buttondelde.Height, 20, 20));
            buttonclandre.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonclandre.Width, buttonclandre.Height, 20, 20));
            buttoncalsa.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttoncalsa.Width, buttoncalsa.Height, 20, 20));
            buttonsavesa.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonsavesa.Width, buttonsavesa.Height, 20, 20));

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

        private void label2_Click(object sender, EventArgs e)
        {
            Ad_setting form = new Ad_setting();
            this.Close();
            form.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Ad_logins form = new Ad_logins();
            this.Close();
            form.Show();
        }

        private void buttonclandre_Click(object sender, EventArgs e)
        {
            Ad_salary form = new Ad_salary();
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

                    string query = "SELECT DISTINCT salary_id, monthly_cycle_id, emp_id, salary, present_days, ot_hours FROM salary";

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

            //Salary ID combobox
            try
            {
                using (SqlConnection Con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdc = new SqlCommand("SELECT * FROM salary", Con);
                    SqlDataAdapter dac = new SqlDataAdapter();
                    dac.SelectCommand = cmdc;
                    DataTable table1 = new DataTable();
                    dac.Fill(table1);

                    comboBoxsid.DataSource = table1;
                    comboBoxsid.DisplayMember = "salary_id";
                    comboBoxsid.ValueMember = "salary_id";
                    Con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            //Mothly salary ID combobox
            try
            {
                using (SqlConnection Con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdc = new SqlCommand("SELECT * FROM setting", Con);
                    SqlDataAdapter dac = new SqlDataAdapter();
                    dac.SelectCommand = cmdc;
                    DataTable table1 = new DataTable();
                    dac.Fill(table1);

                    comboBoxmsid.DataSource = table1;
                    comboBoxmsid.DisplayMember = "monthly_cycle_id";
                    comboBoxmsid.ValueMember = "monthly_cycle_id";
                    Con.Close();
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

        private void buttonadddde_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxpre.Text) && !string.IsNullOrEmpty(textBoxad.Text) && !string.IsNullOrEmpty(textBoxot.Text) && !string.IsNullOrEmpty(textBoxallo.Text))
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
                            cmd.CommandText = "INSERT INTO [salary] (monthly_cycle_id, emp_id, present_days, absent_days, ot_hours, allowances) VALUES (@monthly_cycle_id, @emp_id, @present_days, @absent_days, @ot_hours, @allowances)";

                            // Add parameters
                            cmd.Parameters.AddWithValue("@monthly_cycle_id", comboBoxmsid.Text);
                            cmd.Parameters.AddWithValue("@emp_id", comboBoxempid.Text);
                            cmd.Parameters.AddWithValue("@present_days", textBoxpre.Text);
                            cmd.Parameters.AddWithValue("@absent_days", textBoxad.Text);
                            cmd.Parameters.AddWithValue("@ot_hours", textBoxot.Text);
                            cmd.Parameters.AddWithValue("@allowances", textBoxallo.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Sslary Details Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Ad_salary form = new Ad_salary();
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
            if (!string.IsNullOrEmpty(textBoxpre.Text) && !string.IsNullOrEmpty(textBoxad.Text) && !string.IsNullOrEmpty(textBoxot.Text) && !string.IsNullOrEmpty(textBoxallo.Text))
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;

                            // Use parameterized query for update
                            cmd.CommandText = "UPDATE [salary] SET present_days = @present_days, absent_days = @absent_days, ot_hours = @ot_hours, allowances = @allowances WHERE salary_id = @salary_id";

                            // Add parameters
                            cmd.Parameters.AddWithValue("@salary_id", comboBoxsid.Text);
                            cmd.Parameters.AddWithValue("@present_days", textBoxpre.Text);
                            cmd.Parameters.AddWithValue("@absent_days", textBoxad.Text);
                            cmd.Parameters.AddWithValue("@ot_hours", textBoxot.Text);
                            cmd.Parameters.AddWithValue("@allowances", textBoxallo.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Salary Details Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Ad_salary form = new Ad_salary();
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

        private void buttondelde_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxsid.Text))
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;

                            // Use parameterized query for delete
                            cmd.CommandText = "DELETE FROM [salary] WHERE salary_id = @salary_id";

                            // Add parameter
                            cmd.Parameters.AddWithValue("@salary_id", comboBoxsid.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Salary Details Deleted Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Ad_salary form = new Ad_salary();
                    this.Close();
                    form.Show();
                }
                catch (SqlException sqlEx)
                {
                    // Handle SQL-specific errors
                    MessageBox.Show($"An SQL error occurred: {sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Handle case when salary_id field is empty
                MessageBox.Show("Please select a salary ID to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                comboBoxsid.Text = row.Cells["salary_id"].Value.ToString();
                comboBoxmsid.Text = row.Cells["monthly_cycle_id"].Value.ToString();
                comboBoxempid.Text = row.Cells["emp_id"].Value.ToString();
                textBoxpre.Text = row.Cells["present_days"].Value.ToString();
                textBoxot.Text = row.Cells["ot_hours"].Value.ToString();
                textBoxsalary.Text = row.Cells["salary"].Value.ToString();

                if (int.TryParse(row.Cells["salary_id"].Value.ToString(), out int pacid))
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        SqlCommand cmd = Con.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = "SELECT * FROM salary WHERE salary_id = @salary_id";
                        cmd.Parameters.AddWithValue("@salary_id", pacid);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            textBoxad.Text = dt.Rows[0]["absent_days"].ToString();
                            textBoxallo.Text = dt.Rows[0]["allowances"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No data found for salary_id " + pacid.ToString(), "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        Con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid salary_id value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBoxsid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(comboBoxsid.SelectedValue?.ToString(), out int id))
                {
                    // Conversion successful
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM salary WHERE salary_id = @salary_id";

                            // Use SqlParameter
                            cmd.Parameters.Add(new SqlParameter("@salary_id", SqlDbType.Int)).Value = id;

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    comboBoxmsid.Text = dt.Rows[0]["monthly_cycle_id"].ToString();
                                    comboBoxempid.Text = dt.Rows[0]["emp_id"].ToString();
                                    textBoxpre.Text = dt.Rows[0]["present_days"].ToString();
                                    textBoxad.Text = dt.Rows[0]["absent_days"].ToString();
                                    textBoxot.Text = dt.Rows[0]["ot_hours"].ToString();
                                    textBoxallo.Text = dt.Rows[0]["allowances"].ToString();
                                    textBoxsalary.Text = dt.Rows[0]["salary"].ToString();
                                }
                                else
                                {
                                    // Handle the case when no rows are returned
                                    MessageBox.Show("No records found for the selected ID.");
                                }
                            }
                        }
                        Con.Close();
                    }
                }
                else
                {
                    // Handle the case when the SelectedValue is not a valid integer
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void textBoxpre_TextChanged(object sender, EventArgs e)
        {
            int preValue;
            if (int.TryParse(textBoxpre.Text, out preValue))
            {
                if (preValue <= 30)
                {
                    int result = 30 - preValue;
                    textBoxad.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show("Please enter a number less than 30.");
                }
            }
            else
            {
                //Handle the case
            }
        }

        float gross_pay;

        private void buttoncalsa_Click(object sender, EventArgs e)
        {
            int m_c_id = 0;
            int absent_days = 0, ot_hours = 0, allowances = 0;
            float ot_rate = 0, gov_tax = 0, basesalary = 0;
            float no_pay, base_pay;

            try
            {
                if (int.TryParse(comboBoxempid.SelectedValue?.ToString(), out int empId))
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();

                        // Retrieve monthly salary from employee table
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT mothly_salary FROM employee WHERE emp_id = @emp_id";
                            cmd.Parameters.Add(new SqlParameter("@emp_id", SqlDbType.Int)).Value = empId;

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    basesalary = Convert.ToInt32(dt.Rows[0]["mothly_salary"]);
                                }
                                else
                                {
                                    MessageBox.Show("No records found for the selected employee ID.");
                                    return;
                                }
                            }
                        }

                        // Retrieve salary details from salary table
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM salary WHERE emp_id = @emp_id";
                            cmd.Parameters.Add(new SqlParameter("@emp_id", SqlDbType.Int)).Value = empId;

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    ot_hours = Convert.ToInt32(dt.Rows[0]["ot_hours"]);
                                    absent_days = Convert.ToInt32(dt.Rows[0]["absent_days"]);
                                    allowances = Convert.ToInt32(dt.Rows[0]["allowances"]);
                                    m_c_id = Convert.ToInt32(dt.Rows[0]["monthly_cycle_id"]);
                                }
                                else
                                {
                                    MessageBox.Show("No records found in the salary table for the selected employee ID.");
                                    return;
                                }
                            }
                        }

                        // Retrieve settings based on monthly_cycle_id
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM setting WHERE monthly_cycle_id = @monthly_cycle_id";
                            cmd.Parameters.Add(new SqlParameter("@monthly_cycle_id", SqlDbType.Int)).Value = m_c_id;

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    ot_rate = Convert.ToInt32(dt.Rows[0]["ot_rate_hour"]);
                                    gov_tax = Convert.ToInt32(dt.Rows[0]["gov_tax_rate"]);
                                }
                                else
                                {
                                    MessageBox.Show("No settings found for the selected monthly cycle ID.");
                                    return;
                                }
                            }
                        }

                        Con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid employee ID.");
                    return;
                }

                no_pay = (basesalary / 30) * absent_days;
                base_pay = basesalary + allowances + (ot_hours * ot_rate);
                gross_pay = base_pay - no_pay - (base_pay * gov_tax / 100);

                textBoxsalary.Text = gross_pay.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void buttonsavesa_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxsalary.Text))
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;

                            // Use parameterized query for update
                            cmd.CommandText = "UPDATE [salary] SET salary = @salary WHERE salary_id = @salary_id";

                            // Add parameters
                            cmd.Parameters.AddWithValue("@salary_id", comboBoxsid.Text);
                            cmd.Parameters.AddWithValue("@salary", textBoxsalary.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Salary Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Ad_salary form = new Ad_salary();
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
                MessageBox.Show("First Calculate the Salary.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
