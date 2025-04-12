using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace TechForgeGUI.Utils
{
    public enum AlertType
    {
        success, error, warning
    }

    public partial class AlertForm : Form
    {
        private Timer timer;
        private Label lblMessage;
        private IconPictureBox pictureBox;
        private IconButton btnClose;
        private TableLayoutPanel tlpWrap;

        public AlertForm(string message, AlertType type = AlertType.success)
        {
            InitializeComponent(message, type);
        }

        private void InitializeComponent(string message, AlertType type = AlertType.success)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = 360;
            this.Height = 80;
            this.Opacity = 0.9; 
            this.TopMost = true;
            this.Padding = new Padding(10, 0, 0, 0);
            this.StartPosition = FormStartPosition.CenterScreen;

            switch (type)
            {
                case AlertType.success:
                    this.BackColor = Color.SeaGreen;
                    break;
                case AlertType.error:
                    this.BackColor = Color.Red;
                    break;
                case AlertType.warning:
                    this.BackColor = Color.Gold;
                    break;
            }

            InitializeTableLayoutPanel();
            InitializePictureBox(type);
            InitializeMessageLabel(message);
            InitializeButton();
            InitializeTimer();
        }

        private void InitializeTableLayoutPanel()
        {
            tlpWrap = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
            };
            // Set column widths
            tlpWrap.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60)); 
            tlpWrap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); 
            tlpWrap.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20)); 
            tlpWrap.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); 
            this.Controls.Add(tlpWrap);
        }

        private void InitializeButton()
        {
            btnClose = new IconButton
            {
                Size = new Size(20, 20),
                IconChar = IconChar.Xmark,
                IconColor = Color.White,
                IconSize = 20,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance =
                {
                    BorderSize = 0
                },
                Cursor = Cursors.Hand
            };
            
            tlpWrap.Controls.Add(btnClose, 2, 0);
            btnClose.Click += (s, e) => this.Close(); 
        }

        private void InitializePictureBox(AlertType type = AlertType.success)
        {
            pictureBox = new IconPictureBox
            {
                Width = 60,
                IconSize = 60,
                IconColor = Color.White,
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
            };
            switch (type)
            {
                case AlertType.success:
                    pictureBox.IconChar = IconChar.CircleCheck;
                    break;
                case AlertType.error:
                    pictureBox.IconChar = IconChar.CircleXmark;
                    break;
                case AlertType.warning:
                    pictureBox.IconChar = IconChar.CircleExclamation;
                    break;
            }
            tlpWrap.Controls.Add(pictureBox, 0, 0);
        }

        private void InitializeMessageLabel(string message)
        {
            lblMessage = new Label
            {
                Text = message,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12),
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill, 
                Padding = new Padding(5, 0, 5, 0)
            };
            tlpWrap.Controls.Add(lblMessage, 1, 0);
        }

        private void InitializeTimer()
        {
            timer = new Timer
            {
                Enabled = true,
                Interval = 3000
            };
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
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