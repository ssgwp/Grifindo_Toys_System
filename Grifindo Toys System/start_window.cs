using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grifindo_Toys_System
{
    public partial class start_window : Form
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
        public start_window()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }

        private void start_window_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 3;
            int percentage = (int)(((double)panel2.Width / 685.0) * 100.0);

            label2.Text = percentage + "%";

            if (panel2.Width >= 685)
            {
                timer1.Stop();

                Login form = new Login();
                this.Hide();
                form.Show();
            }
            else if (label2.Text == "20%")
            {
                label3.Text = "Using plugins";
            }

            else if (label2.Text == "40%")
            {
                label3.Text = "Checking System Requirements";
            }

            else if (label2.Text == "60%")
            {
                label3.Text = "Connecting Database";
            }

            else if (label2.Text == "80%")
            {
                label3.Text = "Checking Errors";
            }
        }
    }
}
