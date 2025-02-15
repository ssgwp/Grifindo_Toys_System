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
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Grifindo_Toys_System.Admin
{
    public partial class Ad_toy : Form
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

        public Ad_toy()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }

        string connectionString = "Data Source=ASUS\\SQLEXPRESS;Initial Catalog=Grifindo_Toys_System;Integrated Security=True;";


        private void Ad_toy_Load(object sender, EventArgs e)
        {
            buttexit.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttexit.Width, buttexit.Height, 20, 20));
            buttlogout.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttlogout.Width, buttlogout.Height, 20, 20));
            menupanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, menupanel.Width, menupanel.Height, 20, 20));
            buttonclandre.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonclandre.Width, buttonclandre.Height, 20, 20));
            buttonadddde.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonadddde.Width, buttonadddde.Height, 20, 20));
            buttoneditde.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttoneditde.Width, buttoneditde.Height, 20, 20));
            buttondelde.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttondelde.Width, buttondelde.Height, 20, 20));

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

        private void label3_Click(object sender, EventArgs e)
        {
            Ad_logins form = new Ad_logins();
            this.Close();
            form.Show();
        }

        private void buttonclandre_Click(object sender, EventArgs e)
        {
            Ad_toy form = new Ad_toy();
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

                    string query = "SELECT DISTINCT toy_id, toy_name, price, quntity FROM toy";

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
                    SqlCommand cmdc = new SqlCommand("SELECT * FROM toy", Con);
                    SqlDataAdapter dac = new SqlDataAdapter();
                    dac.SelectCommand = cmdc;
                    DataTable table1 = new DataTable();
                    dac.Fill(table1);

                    comboBoxtoyid.DataSource = table1;
                    comboBoxtoyid.DisplayMember = "toy_id";
                    comboBoxtoyid.ValueMember = "toy_id";
                    Con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void comboBoxtoyid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(comboBoxtoyid.SelectedValue?.ToString(), out int id))
                {
                    // Conversion successful
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM toy WHERE toy_id = @toy_id";

                            // Use SqlParameter for better type safety
                            cmd.Parameters.Add(new SqlParameter("@toy_id", SqlDbType.Int)).Value = id;

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    textBoxtoyn.Text = dt.Rows[0]["toy_name"].ToString();
                                    textBoxqu.Text = dt.Rows[0]["quntity"].ToString();
                                    textBoxpr.Text = dt.Rows[0]["price"].ToString();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                comboBoxtoyid.Text = row.Cells["toy_id"].Value.ToString();
                textBoxtoyn.Text = row.Cells["toy_name"].Value.ToString();
                textBoxqu.Text = row.Cells["quntity"].Value.ToString();
                textBoxpr.Text = row.Cells["price"].Value.ToString();
            }
        }

        private void buttonadddde_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxtoyn.Text) && !string.IsNullOrEmpty(textBoxqu.Text) && !string.IsNullOrEmpty(textBoxpr.Text))
            {
                try
                {
                    // Validate and convert inputs
                    if (!int.TryParse(textBoxqu.Text, out int quantity))
                    {
                        MessageBox.Show("Invalid quantity. Please enter a valid number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!int.TryParse(textBoxpr.Text, out int price))
                    {
                        MessageBox.Show("Invalid price. Please enter a valid number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "INSERT INTO [toy] (toy_name, quntity, price) VALUES (@toy_name, @quntity, @price)";

                            // Add parameters
                            cmd.Parameters.AddWithValue("@toy_name", textBoxtoyn.Text);
                            cmd.Parameters.Add("@quntity", SqlDbType.Int).Value = quantity;
                            cmd.Parameters.Add("@price", SqlDbType.Int).Value = price;

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Toy Details Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Ad_toy form = new Ad_toy();
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
                MessageBox.Show("Please fill all the text boxes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttoneditde_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxtoyn.Text) && !string.IsNullOrEmpty(textBoxqu.Text) && !string.IsNullOrEmpty(textBoxpr.Text))
            {
                try
                {
                    // Validate and convert inputs
                    if (!int.TryParse(textBoxqu.Text, out int quantity))
                    {
                        MessageBox.Show("Invalid quantity. Please enter a valid number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!int.TryParse(textBoxpr.Text, out int price))
                    {
                        MessageBox.Show("Invalid price. Please enter a valid number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "UPDATE [toy] SET toy_name = @toy_name, quntity = @quntity, price = @price WHERE toy_id = @toy_id";

                            // Add parameters
                            cmd.Parameters.AddWithValue("@toy_id", comboBoxtoyid.Text);
                            cmd.Parameters.AddWithValue("@toy_name", textBoxtoyn.Text);
                            cmd.Parameters.Add("@quntity", SqlDbType.Int).Value = quantity;
                            cmd.Parameters.Add("@price", SqlDbType.Int).Value = price;

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Toy Details Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Toy not found. No rows were updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    Ad_toy form = new Ad_toy();
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
                MessageBox.Show("Please fill all the text boxes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttondelde_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxtoyid.Text))
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(connectionString))
                    {
                        Con.Open();
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "DELETE FROM [toy] WHERE toy_id = @toy_id";

                            // Add the parameter for toy_id
                            cmd.Parameters.AddWithValue("@toy_id", comboBoxtoyid.Text);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Toy Details Deleted Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Toy not found. No rows were deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    Ad_toy form = new Ad_toy();
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
                MessageBox.Show("Please select a toy ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
