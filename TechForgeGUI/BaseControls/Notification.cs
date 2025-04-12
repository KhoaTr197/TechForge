using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using TechForgeGUI.Utils;

namespace TechForgeGUI.BaseControls
{
  public partial class Notification : UserControl
  {
    private Form notificationForm;
    private Timer fadeTimer;
    private Timer closeTimer;
    private int fadeDuration = 500; //ms
    private int showDuration = 3000; //ms

    public Notification(string message, string type = "success", int timeout = 3000)
    {
      fadeDuration = 500;
      showDuration = 3000;

      InitializeComponent();
      ShowNotification(message, type, timeout);
    }

    private void ShowNotification(string message, string type, int timeout)
    {
      // Create the notification form
      notificationForm = new Form
      {
        FormBorderStyle = FormBorderStyle.None,
        ShowInTaskbar = false,
        TopMost = true,
        BackColor = Color.White,
        Size = new Size(300, 80),
        AllowTransparency = true,
        StartPosition = FormStartPosition.Manual
      };

      // Position the form in the bottom-right corner
      Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
      notificationForm.Location = new Point(
          workingArea.Right - notificationForm.Width - 20,
          workingArea.Bottom - notificationForm.Height - 20
      );

      TableLayoutPanel tlpNotify = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        RowCount = 2,
        ColumnStyles = { 
          new ColumnStyle(SizeType.Percent, 20), 
          new ColumnStyle(SizeType.Percent, 80) 
        },
        RowStyles = {
          new RowStyle(SizeType.Percent, 25),
          new RowStyle(SizeType.Percent, 75),
        },
        CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
        Padding = new Padding(8),
      };

      // Create the icon picture box with high quality settings
      PictureBox iconBox = new PictureBox
      {
        Size = new Size(32, 32),
        SizeMode = PictureBoxSizeMode.Zoom,
        Image = GlobalStatics.iconList.Images["circle_check_icon"],
        Dock = DockStyle.Fill,
      };

      // Create the message label
      Label messageLabel = new Label
      {
        Text = message,
        Dock = DockStyle.Fill,
        Font = new Font("Segoe UI", 12),
        ForeColor = Color.White,
      };

      // Add title based on type
      string title = "Notification";
      Color backgroundColor = Color.White;
      switch (type.ToLower())
      {
        case "success":
          title = "Success";
          backgroundColor = Color.Green;
          break;
        case "error":
          title = "Error";
          backgroundColor = Color.FromArgb(254, 235, 238);
          break;
        case "warning":
          title = "Warning";
          backgroundColor = Color.FromArgb(255, 248, 225);
          break;
      }

      // Set the form's background color
      tlpNotify.BackColor = backgroundColor;

      Label titleLabel = new Label
      {
        Text = title,
        Dock = DockStyle.Fill,
        Font = new Font("Segoe UI", 10, FontStyle.Bold),
        ForeColor = Color.White,
      };

      // Add controls to the form
      tlpNotify.Controls.Add(iconBox, 0, 0);
      tlpNotify.SetRowSpan(iconBox, 2);
      tlpNotify.Controls.Add(messageLabel, 1, 1);
      tlpNotify.Controls.Add(titleLabel, 1, 0);
      notificationForm.Controls.Add(tlpNotify);

      // Add rounded corners
      notificationForm.Paint += (s, e) =>
      {
        using (GraphicsPath path = new GraphicsPath())
        {
          int radius = 10;
          Rectangle rect = notificationForm.ClientRectangle;
          path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
          path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
          path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
          path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
          path.CloseFigure();
          notificationForm.Region = new Region(path);
        }
      };

      // Show the form with fade-in effect
      notificationForm.Opacity = 0;
      notificationForm.Show();

      // Fade in
      fadeTimer = new Timer { Interval = 10 };
      fadeTimer.Tick += (s, e) =>
      {
        if (notificationForm.Opacity < 1)
        {
          notificationForm.Opacity += 0.1;
        }
        else
        {
          fadeTimer.Stop();
        }
      };
      fadeTimer.Start();

      // Set up close timer
      closeTimer = new Timer { Interval = timeout };
      closeTimer.Tick += (s, e) =>
      {
        closeTimer.Stop();
        FadeOutAndClose();
      };
      closeTimer.Start();
    }
    private void FadeOutAndClose()
    {
      Timer fadeOutTimer = new Timer(){ Interval = 10 };
      fadeOutTimer.Tick += (s, e) =>
      {
        if (notificationForm.Opacity > 0)
        {
          notificationForm.Opacity -= 0.1;
        }
        else
        {
          fadeOutTimer.Stop();
          notificationForm.Close();
          notificationForm.Dispose();
        }
      };
      fadeOutTimer.Start();
    }
  }
}