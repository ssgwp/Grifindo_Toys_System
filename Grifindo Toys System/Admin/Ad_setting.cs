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
    public partial class Ad_setting : Form
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

        public Ad_setting()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }

        string connectionString = "Data Source=ASUS\\SQLEXPRESS;Initial Catalog=Grifindo_Toys_System;Integrated Security=True;";

        private void Ad_setting_Load(object sender, EventArgs e)
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

        private void label3_Click(object sender, EventArgs e)
        {
            Ad_logins form = new Ad_logins();
            this.Close();
            form.Show();
        }

        private void label1_Click(object sender, EventArgs e)
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

                    string query = "SELECT DISTINCT monthly_cycle_id, start__date, stop_date, ot_rate_hour, gov_tax_rate FROM setting";

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

            //ID combobox
            try
            {
                using (SqlConnection Con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdc = new SqlCommand("SELECT * FROM setting", Con);
                    SqlDataAdapter dac = new SqlDataAdapter();
                    dac.SelectCommand = cmdc;
                    DataTable table1 = new DataTable();
                    dac.Fill(table1);

                    comboBoxid.DataSource = table1;
                    comboBoxid.DisplayMember = "monthly_cycle_id";
                    comboBoxid.ValueMember = "monthly_cycle_id";
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
            Ad_setting form = new Ad_setting();
            this.Close();
            form.Show();
        }

        private void comboBoxid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(comboBoxid.SelectedValue?.ToString(), out int id))
                {
                    // Conversion successful
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM setting WHERE monthly_cycle_id = @monthly_cycle_id";

                            // Use SqlParameter for better type safety
                            cmd.Parameters.Add(new SqlParameter("@monthly_cycle_id", SqlDbType.Int)).Value = id;

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    dateTimePickerstdate.Value = Convert.ToDateTime(dt.Rows[0]["start__date"]);
                                    dateTimePickerstopda.Value = Convert.ToDateTime(dt.Rows[0]["stop_date"]);
                                    textBoxotrate.Text = dt.Rows[0]["ot_rate_hour"].ToString();
                                    textBoxgover_tax.Text = dt.Rows[0]["gov_tax_rate"].ToString();
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
                    // Handle the case when the SelectedValue is not a valid integer
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void buttonadddde_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxotrate.Text) && !string.IsNullOrEmpty(textBoxgover_tax.Text))
            {
                if (float.TryParse(textBoxgover_tax.Text, out float govTaxRate) && govTaxRate <= 100.00)
                {
                    if ((dateTimePickerstopda.Value - dateTimePickerstdate.Value).Days == 30)
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
                                    cmd.CommandText = "INSERT INTO [setting] (start__date, stop_date, ot_rate_hour, gov_tax_rate) VALUES (@start__date, @stop_date, @ot_rate_hour, @gov_tax_rate)";

                                    // Add parameters
                                    cmd.Parameters.AddWithValue("@start__date", dateTimePickerstdate.Value);
                                    cmd.Parameters.AddWithValue("@stop_date", dateTimePickerstopda.Value);
                                    cmd.Parameters.AddWithValue("@ot_rate_hour", textBoxotrate.Text);
                                    cmd.Parameters.AddWithValue("@gov_tax_rate", govTaxRate);

                                    cmd.ExecuteNonQuery();
                                }
                            }
                            MessageBox.Show("Details Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Ad_setting form = new Ad_setting();
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
                        // Handle case where the dates are not 30 days apart
                        MessageBox.Show("The start date and stop date should be 30 days apart.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // Handle case where government tax rate is greater than 100 or invalid
                    MessageBox.Show("Please enter a valid government tax rate (0-100).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Handle case when required fields are empty
                MessageBox.Show("OT Rate and Government Tax cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                comboBoxid.Text = row.Cells["monthly_cycle_id"].Value.ToString();
                dateTimePickerstdate.Text = row.Cells["start__date"].Value.ToString();
                dateTimePickerstopda.Text = row.Cells["stop_date"].Value.ToString();
                textBoxotrate.Text = row.Cells["ot_rate_hour"].Value.ToString();
                textBoxgover_tax.Text = row.Cells["gov_tax_rate"].Value.ToString();
            }
        }

        private void buttoneditde_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxotrate.Text) && !string.IsNullOrEmpty(textBoxgover_tax.Text))
            {
                if (float.TryParse(textBoxgover_tax.Text, out float govTaxRate) && govTaxRate <= 100.00)
                {
                    if ((dateTimePickerstopda.Value - dateTimePickerstdate.Value).Days == 30)
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
                                    cmd.CommandText = "UPDATE [setting] SET start__date = @start__date, stop_date = @stop_date, ot_rate_hour = @ot_rate_hour, gov_tax_rate = @gov_tax_rate WHERE monthly_cycle_id = @monthly_cycle_id";

                                    // Add parameters
                                    cmd.Parameters.AddWithValue("@start__date", dateTimePickerstdate.Value);
                                    cmd.Parameters.AddWithValue("@stop_date", dateTimePickerstopda.Value);
                                    cmd.Parameters.AddWithValue("@ot_rate_hour", textBoxotrate.Text);
                                    cmd.Parameters.AddWithValue("@gov_tax_rate", textBoxgover_tax.Text);
                                    cmd.Parameters.AddWithValue("@monthly_cycle_id", comboBoxid.Text);

                                    cmd.ExecuteNonQuery();
                                }
                            }
                            MessageBox.Show("Details Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Ad_setting form = new Ad_setting();
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
                        // Handle case when the date difference is not 30 days
                        MessageBox.Show("The start date and stop date should be 30 days apart.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // Handle case where government tax rate is greater than 100 or invalid
                    MessageBox.Show("Please enter a valid government tax rate (0-100).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Handle case when required fields are empty
                MessageBox.Show("OT Rate and Government Tax cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void buttondelde_Click(object sender, EventArgs e)
        {
            if (comboBoxid.SelectedValue != null && int.TryParse(comboBoxid.SelectedValue.ToString(), out int selectedRecordId))
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;

                            // Use parameterized query for the delete statement
                            cmd.CommandText = "DELETE FROM [salary] WHERE monthly_cycle_id = @monthly_cycle_id";

                            // Add parameters
                            cmd.Parameters.AddWithValue("@monthly_cycle_id", selectedRecordId);

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
                            cmd.CommandText = "DELETE FROM [setting] WHERE monthly_cycle_id = @monthly_cycle_id";

                            // Add parameters
                            cmd.Parameters.AddWithValue("@monthly_cycle_id", selectedRecordId);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Record Deleted Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Ad_setting form = new Ad_setting();
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
                // Handle case when the selected record ID is not valid
                MessageBox.Show("Selected value is not a valid ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dateTimePickerstdate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerstopda.Value = dateTimePickerstdate.Value.AddDays(30);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Ad_toy form = new Ad_toy();
            this.Close();
            form.Show();
        }
    }
}
