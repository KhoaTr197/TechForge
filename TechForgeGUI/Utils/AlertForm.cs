using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechForgeGUI.Utils
{
    public partial class AlertForm : Form
    {
        private Timer timer;

        private Label messageLabel;

        private PictureBox pictureBox;
        private ImageList imgList;
        public AlertForm(string message, string type = "success")
        {
            InitializeComponent(message, type);

        }

        private void InitializeComponent(string message, string type = "success")
        {
            imgList = new ImageList();
            imgList = GlobalStatics.iconList;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = 360;
            this.Height = 80;
            this.Opacity = 90;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Padding = new Padding(20, 0, 0, 0);
            this.StartPosition = FormStartPosition.Manual;
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;

            int margin = 10;
            int x = screen.Width - this.Width - margin;
            int y = margin;

            this.Location = new Point(x, y);
            switch (type)
            {
                case "success":
                    {
                        this.BackColor = Color.DarkGreen;
                        break;
                    }
                case "error":
                    {
                        this.BackColor = Color.DarkRed;
                        break;
                    }
                case "warning":
                    {
                        this.BackColor = Color.LightGoldenrodYellow;
                        break;
                    }
            }
            InitializeMessageLabel(message);
            InitializePictureBox(type);
            InitializeTimer();
        }

        private void InitializePictureBox(string type = "success")
        {
            pictureBox = new PictureBox()
            {
                Dock = DockStyle.Left,
                Width = 40,
                SizeMode = PictureBoxSizeMode.Zoom,
                
            };
            switch (type)
            {
                case "success":
                    {
                        pictureBox.Image = Properties.Resources.checked_icon;
                        break;
                    }
                case "error":
                    {
                        //pictureBox.Image = Properties.Resources.checked_icon;
                        break;
                    }
                case "warning":
                    {
                        break;
                    }
            }
            this.Controls.Add(pictureBox);
        }
        private void InitializeMessageLabel(string message)
        {
            messageLabel = new Label()
            {
                Text = message,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12),
                Top = 20,
                Left = 20,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            this.Controls.Add(messageLabel);
        }

        private void InitializeTimer()
        {
            timer = new Timer()
            {
                Enabled = true,
                Interval = 3000,
            };
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            // Hiệu ứng mờ dần
            for (double opacity = 1.0; opacity >= 0; opacity -= 0.1)
            {
                this.Opacity = opacity;
                this.Refresh();
                System.Threading.Thread.Sleep(50);
            }
            this.Close();
        }
    }
}
