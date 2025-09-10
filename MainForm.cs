using System;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    /// <summary>
    /// Polished, card-based landing page for the app.
    /// Only "Report Issues" is enabled per Part 1 requirements.
    /// </summary>
    public class MainForm : Form
    {
        // --- Hero header ---
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblTagline;

        // --- "How it works" strip ---
        private Panel howItWorksPanel;
        private Label lblHowItWorks;
        private FlowLayoutPanel stepsFlow;

        // --- Action cards grid ---
        private TableLayoutPanel cardsLayout;

        private ToolTip tip;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Window basics
            Text = "Municipal Services SA — Main Menu";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(980, 680);
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

            // Theme palette (shared across controls)
            Color primary = Color.FromArgb(10, 110, 189);  // #0A6EBD (brand blue)
            Color accent  = Color.FromArgb(52, 168, 83);   // #34A853 (success/positive)
            Color dark    = Color.FromArgb(40, 40, 40);

            tip = new ToolTip();

            // ===== HERO HEADER =====
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 140,
                BackColor = primary
            };

            lblTitle = new Label
            {
                Text = "Municipal Services",
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 26F, FontStyle.Bold),
                Location = new Point(28, 22)
            };

            lblTagline = new Label
            {
                Text = "Report issues, stay informed, help improve your ward.",
                AutoSize = true,
                ForeColor = Color.WhiteSmoke,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                Location = new Point(32, 76)
            };

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblTagline);

            // ===== HOW IT WORKS STRIP =====
            howItWorksPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = Color.White
            };

            lblHowItWorks = new Label
            {
                Text = "How it works",
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold),
                ForeColor = dark,
                Location = new Point(28, 16)
            };

            stepsFlow = new FlowLayoutPanel
            {
                Location = new Point(28, 48),
                Size = new Size(900, 50),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White
            };
            // Simple 1–2–3 flow chips
            stepsFlow.Controls.Add(MakeStepChip("1", "Report an issue"));
            stepsFlow.Controls.Add(MakeStepDivider());
            stepsFlow.Controls.Add(MakeStepChip("2", "We assign it"));
            stepsFlow.Controls.Add(MakeStepDivider());
            stepsFlow.Controls.Add(MakeStepChip("3", "You get updates"));

            howItWorksPanel.Controls.Add(lblHowItWorks);
            howItWorksPanel.Controls.Add(stepsFlow);

            // ===== ACTION CARDS GRID =====
            cardsLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(24),
                ColumnCount = 3,
            };
            cardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            cardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            cardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            cardsLayout.RowCount = 1;
            cardsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            // Card 1: Report Issues (ENABLED)
            var cardReport = CreateCardPanel(
                title: "Report Issues",
                body: "Log service problems in your area (location, category, description and attachments).",
                icon: SystemIcons.Shield,
                buttonText: "Open Report Form",
                enabled: true,
                onClick: BtnReportIssues_Click
            );

            // Card 2: Events & Announcements (DISABLED for Part 1)
            var cardEvents = CreateCardPanel(
                title: "Local Events & Announcements",
                body: "Find city notices and events in your ward (coming in Part 2).",
                icon: SystemIcons.Information,
                buttonText: "Coming Soon",
                enabled: false,
                onClick: null
            );

            // Card 3: Service Request Status (DISABLED for Part 1)
            var cardStatus = CreateCardPanel(
                title: "Service Request Status",
                body: "Track the progress of your submitted issues (coming in Part 2).",
                icon: SystemIcons.Application,
                buttonText: "Coming Soon",
                enabled: false,
                onClick: null
            );

            cardsLayout.Controls.Add(cardReport, 0, 0);
            cardsLayout.Controls.Add(cardEvents, 1, 0);
            cardsLayout.Controls.Add(cardStatus, 2, 0);

            // Add composed sections to the form
            Controls.Add(cardsLayout);
            Controls.Add(howItWorksPanel);
            Controls.Add(headerPanel);

            // Helpful hints for disabled features
            tip.SetToolTip(cardEvents, "This feature will be implemented later (Part 2).");
            tip.SetToolTip(cardStatus, "This feature will be implemented later (Part 2).");
        }

        // === Helper: small number badge + step label ===
        private Control MakeStepChip(string number, string text)
        {
            var panel = new Panel
            {
                Height = 42,
                Width = 230,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 0)
            };

            var badge = new Label
            {
                Text = number,
                AutoSize = false,
                Width = 32,
                Height = 32,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(10, 110, 189),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                Location = new Point(0, 5)
            };
            // Draw circle badge for the step number
            badge.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(10, 110, 189)), 0, 0, 32, 32);
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString(number, new Font("Segoe UI Semibold", 11F, FontStyle.Bold), Brushes.White, new RectangleF(0, 0, 32, 32), sf);
            };

            var lbl = new Label
            {
                Text = text,
                AutoSize = true,
                ForeColor = Color.FromArgb(60, 60, 60),
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                Location = new Point(40, 10)
            };

            panel.Controls.Add(badge);
            panel.Controls.Add(lbl);
            return panel;
        }

        // === Helper: chevron between steps ===
        private Control MakeStepDivider()
        {
            return new Label
            {
                Text = "›",
                AutoSize = true,
                ForeColor = Color.FromArgb(120, 120, 120),
                Font = new Font("Segoe UI", 16F, FontStyle.Regular),
                Padding = new Padding(12, 4, 12, 0)
            };
        }

        /// <summary>
        /// Creates a card panel with icon, title, body text and a CTA button.
        /// </summary>
        private Panel CreateCardPanel(string title, string body, Icon icon, string buttonText, bool enabled, EventHandler onClick)
        {
            Color border  = Color.FromArgb(220, 220, 220);
            Color primary = Color.FromArgb(10, 110, 189);
            Color accent  = Color.FromArgb(52, 168, 83);

            var panel = new Panel
            {
                Margin = new Padding(12),
                BackColor = Color.White,
                Dock = DockStyle.Fill
            };

            // Subtle border for the card
            panel.Paint += (s, e) =>
            {
                using (var pen = new Pen(border, 1))
                using (var brush = new SolidBrush(Color.White))
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.FillRectangle(brush, panel.ClientRectangle);
                    e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
                }
            };

            // Icon
            var pic = new PictureBox
            {
                Image = icon.ToBitmap(),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(20, 18),
                Size = new Size(48, 48)
            };

            // Title
            var lblCardTitle = new Label
            {
                Text = title,
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold),
                ForeColor = primary,
                Location = new Point(80, 22)
            };

            // Body (wraps nicely and resizes with panel)
            var lblBody = new Label
            {
                Text = body,
                AutoSize = false,
                Size = new Size(panel.Width - 40, 60),
                Font = new Font("Segoe UI", 10.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(70, 70, 70),
                Location = new Point(20, 80),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // CTA button (disabled for "coming soon" features)
            var btn = new Button
            {
                Text = buttonText,
                Size = new Size(200, 40),
                Location = new Point(panel.Width - 220, panel.Height - 64),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                BackColor = enabled ? Color.White : Color.Gainsboro,
                FlatStyle = FlatStyle.Flat,
                Enabled = enabled
            };
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.BorderColor = enabled ? primary : Color.Silver;
            if (enabled && onClick != null) btn.Click += onClick;

            // “Coming soon” badge
            if (!enabled)
            {
                var soon = new Label
                {
                    Text = "Coming soon",
                    AutoSize = true,
                    ForeColor = accent,
                    Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                    Location = new Point(panel.Width - 130, 18),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };
                panel.Controls.Add(soon);
            }

            // Keep button pinned to bottom-right on resize
            panel.Resize += (s, e) =>
            {
                btn.Location = new Point(panel.Width - 220, panel.Height - 64);
                lblBody.Size = new Size(panel.Width - 40, 60);
            };

            panel.Controls.Add(pic);
            panel.Controls.Add(lblCardTitle);
            panel.Controls.Add(lblBody);
            panel.Controls.Add(btn);
            return panel;
        }

        // === Navigation handler ===
        private void BtnReportIssues_Click(object sender, EventArgs e)
        {
            using (var form = new ReportIssueForm())
            {
                form.ShowDialog(this); // modal over main menu
            }
        }
    }
}
