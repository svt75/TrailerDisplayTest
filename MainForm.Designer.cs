namespace TrailerDisplayTest;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.SuspendLayout();
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
        this.ClientSize = new System.Drawing.Size(700, 770);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = "MainForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Trailer Display Test + Sniffer";

        // Title
        lblTitle = new Label();
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Arial", 14F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(255, 68, 68);
        lblTitle.Location = new Point(175, 10);
        lblTitle.Text = "TRAILER DISPLAY TEST + SNIFFER";
        this.Controls.Add(lblTitle);

        // === Connection Panel ===
        pnlConnection = new Panel();
        pnlConnection.BackColor = Color.FromArgb(45, 45, 45);
        pnlConnection.Location = new Point(12, 42);
        pnlConnection.Size = new Size(676, 45);
        this.Controls.Add(pnlConnection);

        pnlConnection.Controls.Add(new Label { Text = "IP:", ForeColor = Color.White, Location = new Point(10, 13), AutoSize = true });
        txtIP = new TextBox { Text = "192.168.72.199", Location = new Point(35, 10), Size = new Size(120, 23), BackColor = Color.FromArgb(50, 50, 50), ForeColor = Color.White };
        pnlConnection.Controls.Add(txtIP);

        pnlConnection.Controls.Add(new Label { Text = "Port B:", ForeColor = Color.FromArgb(255, 100, 100), Location = new Point(168, 13), AutoSize = true });
        txtPort1 = new TextBox { Text = "5001", Location = new Point(215, 10), Size = new Size(50, 23), BackColor = Color.FromArgb(50, 50, 50), ForeColor = Color.White };
        pnlConnection.Controls.Add(txtPort1);

        pnlConnection.Controls.Add(new Label { Text = "Port P:", ForeColor = Color.FromArgb(100, 255, 100), Location = new Point(278, 13), AutoSize = true });
        txtPort2 = new TextBox { Text = "5002", Location = new Point(322, 10), Size = new Size(50, 23), BackColor = Color.FromArgb(50, 50, 50), ForeColor = Color.White };
        pnlConnection.Controls.Add(txtPort2);

        btnTest = new Button { Text = "Test", Location = new Point(390, 8), Size = new Size(60, 28), BackColor = Color.FromArgb(68, 136, 255), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        btnTest.Click += btnTest_Click;
        pnlConnection.Controls.Add(btnTest);

        lblStatus = new Label { Text = "\u25CF", ForeColor = Color.Gray, Location = new Point(460, 12), AutoSize = true, Font = new Font("Arial", 14F) };
        pnlConnection.Controls.Add(lblStatus);

        // === SNIFFER Panel ===
        pnlSniffer = new Panel();
        pnlSniffer.BackColor = Color.FromArgb(50, 40, 20);
        pnlSniffer.Location = new Point(12, 92);
        pnlSniffer.Size = new Size(676, 40);
        this.Controls.Add(pnlSniffer);

        pnlSniffer.Controls.Add(new Label { Text = "TCP SNIFFER:", ForeColor = Color.FromArgb(255, 200, 50), Location = new Point(10, 10), AutoSize = true, Font = new Font("Arial", 9F, FontStyle.Bold) });

        btnListen = new Button { Text = "LISTEN (Sniffer)", Location = new Point(130, 6), Size = new Size(150, 28), BackColor = Color.FromArgb(170, 120, 0), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Arial", 9F, FontStyle.Bold) };
        btnListen.Click += btnListen_Click;
        pnlSniffer.Controls.Add(btnListen);

        lblListenStatus = new Label { Text = "\u25CF", ForeColor = Color.Gray, Location = new Point(290, 10), AutoSize = true, Font = new Font("Arial", 14F) };
        pnlSniffer.Controls.Add(lblListenStatus);

        lblMsgCount = new Label { Text = "Total:0 ADD:0 DEL:0", ForeColor = Color.FromArgb(255, 200, 50), Location = new Point(320, 12), AutoSize = true, Font = new Font("Consolas", 9F) };
        pnlSniffer.Controls.Add(lblMsgCount);

        pnlSniffer.Controls.Add(new Label { Text = "auto-saves to .log file", ForeColor = Color.Gray, Location = new Point(530, 12), AutoSize = true, Font = new Font("Arial", 7F) });

        // === BOARD 1: UNLOAD ===
        grpBoard1 = new GroupBox();
        grpBoard1.Text = " \u0412\u042B\u0413\u0420\u0423\u0417\u041A\u0410 (TCP 5001) ";
        grpBoard1.ForeColor = Color.FromArgb(255, 100, 100);
        grpBoard1.Font = new Font("Arial", 10F, FontStyle.Bold);
        grpBoard1.Location = new Point(12, 138);
        grpBoard1.Size = new Size(330, 195);
        this.Controls.Add(grpBoard1);

        CreateBoardControls(grpBoard1, 1);

        // === BOARD 2: LOAD ===
        grpBoard2 = new GroupBox();
        grpBoard2.Text = " \u041F\u041E\u0413\u0420\u0423\u0417\u041A\u0410 (TCP 5002) ";
        grpBoard2.ForeColor = Color.FromArgb(100, 255, 100);
        grpBoard2.Font = new Font("Arial", 10F, FontStyle.Bold);
        grpBoard2.Location = new Point(358, 138);
        grpBoard2.Size = new Size(330, 195);
        this.Controls.Add(grpBoard2);

        CreateBoardControls(grpBoard2, 2);

        // === Global Buttons ===
        pnlGlobal = new Panel();
        pnlGlobal.BackColor = Color.FromArgb(45, 45, 45);
        pnlGlobal.Location = new Point(12, 340);
        pnlGlobal.Size = new Size(676, 85);
        this.Controls.Add(pnlGlobal);

        btnSendBoth = new Button { Text = "SEND BOTH", Location = new Point(20, 10), Size = new Size(110, 30), BackColor = Color.FromArgb(68, 170, 68), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Arial", 9F, FontStyle.Bold) };
        btnSendBoth.Click += btnSendBoth_Click;
        pnlGlobal.Controls.Add(btnSendBoth);

        btnDeleteBoth = new Button { Text = "DELETE BOTH", Location = new Point(140, 10), Size = new Size(110, 30), BackColor = Color.FromArgb(170, 68, 68), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Arial", 9F, FontStyle.Bold) };
        btnDeleteBoth.Click += btnDeleteBoth_Click;
        pnlGlobal.Controls.Add(btnDeleteBoth);

        btnFillBoth = new Button { Text = "FILL BOTH", Location = new Point(260, 10), Size = new Size(100, 30), BackColor = Color.FromArgb(80, 80, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        btnFillBoth.Click += btnFillBoth_Click;
        pnlGlobal.Controls.Add(btnFillBoth);

        btnMultiTest = new Button { Text = "MULTI-TEST (6 trailers)", Location = new Point(20, 48), Size = new Size(200, 30), BackColor = Color.FromArgb(68, 68, 170), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Arial", 9F, FontStyle.Bold) };
        btnMultiTest.Click += btnMultiTest_Click;
        pnlGlobal.Controls.Add(btnMultiTest);

        btnClearMulti = new Button { Text = "CLEAR MULTI-TEST", Location = new Point(230, 48), Size = new Size(160, 30), BackColor = Color.FromArgb(120, 68, 68), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        btnClearMulti.Click += btnClearMulti_Click;
        pnlGlobal.Controls.Add(btnClearMulti);

        // === JSON Preview ===
        var lblPreview = new Label { Text = "JSON Format:", ForeColor = Color.Gray, Location = new Point(12, 432), AutoSize = true, Font = new Font("Arial", 8F) };
        this.Controls.Add(lblPreview);

        txtPreview = new TextBox();
        txtPreview.Location = new Point(12, 448);
        txtPreview.Size = new Size(676, 40);
        txtPreview.Multiline = true;
        txtPreview.BackColor = Color.FromArgb(40, 40, 40);
        txtPreview.ForeColor = Color.FromArgb(200, 200, 100);
        txtPreview.Font = new Font("Consolas", 8.5F);
        txtPreview.ReadOnly = true;
        txtPreview.Text = "{\"trailer_id\":\"001\",\"trailer_number\":\"\u041C530\u0421\u041C\",\"trailer_info\":[\"4101\",\"2005\"]}\r\n{\"trailer_id\":\"001\",\"trailer_number\":\"\u041C530\u0421\u041C\",\"trailer_info\":\"\"} // delete";
        this.Controls.Add(txtPreview);

        // === Log + Save ===
        var lblLog = new Label { Text = "Log:", ForeColor = Color.Gray, Location = new Point(12, 494), AutoSize = true };
        this.Controls.Add(lblLog);

        btnSaveLog = new Button { Text = "Save Log", Location = new Point(610, 490), Size = new Size(78, 22), BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Arial", 8F) };
        btnSaveLog.Click += btnSaveLog_Click;
        this.Controls.Add(btnSaveLog);

        txtLog = new TextBox();
        txtLog.Location = new Point(12, 514);
        txtLog.Size = new Size(676, 246);
        txtLog.Multiline = true;
        txtLog.ScrollBars = ScrollBars.Vertical;
        txtLog.BackColor = Color.Black;
        txtLog.ForeColor = Color.Lime;
        txtLog.Font = new Font("Consolas", 9F);
        txtLog.ReadOnly = true;
        this.Controls.Add(txtLog);

        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private void CreateBoardControls(GroupBox grp, int board)
    {
        var normalFont = new Font("Arial", 9F);
        int y = 25;

        grp.Controls.Add(new Label { Text = "Trailer ID:", ForeColor = Color.White, Font = normalFont, Location = new Point(10, y + 3), AutoSize = true });
        var txtId = new TextBox { Location = new Point(90, y), Size = new Size(220, 23), BackColor = Color.FromArgb(50, 50, 50), ForeColor = Color.White };
        grp.Controls.Add(txtId);

        y += 32;
        grp.Controls.Add(new Label { Text = "Number:", ForeColor = Color.White, Font = normalFont, Location = new Point(10, y + 3), AutoSize = true });
        var txtNum = new TextBox { Location = new Point(90, y), Size = new Size(220, 23), BackColor = Color.FromArgb(50, 50, 50), ForeColor = Color.White };
        grp.Controls.Add(txtNum);

        y += 32;
        grp.Controls.Add(new Label { Text = "Info:", ForeColor = Color.White, Font = normalFont, Location = new Point(10, y + 3), AutoSize = true });
        var txtInf = new TextBox { Location = new Point(90, y), Size = new Size(220, 23), BackColor = Color.FromArgb(50, 50, 50), ForeColor = Color.White };
        grp.Controls.Add(txtInf);
        grp.Controls.Add(new Label { Text = "(comma-sep)", ForeColor = Color.Gray, Font = new Font("Arial", 7F), Location = new Point(90, y + 24), AutoSize = true });

        y += 50;
        var btnSend = new Button { Text = "SEND", Location = new Point(10, y), Size = new Size(70, 28), BackColor = Color.FromArgb(68, 170, 68), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Arial", 9F, FontStyle.Bold) };
        grp.Controls.Add(btnSend);

        var btnDel = new Button { Text = "DELETE", Location = new Point(90, y), Size = new Size(70, 28), BackColor = Color.FromArgb(170, 68, 68), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        grp.Controls.Add(btnDel);

        var btnFill = new Button { Text = "Fill", Location = new Point(170, y), Size = new Size(50, 28), BackColor = Color.FromArgb(80, 80, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        grp.Controls.Add(btnFill);

        if (board == 1)
        {
            txtId1 = txtId; txtNumber1 = txtNum; txtInfo1 = txtInf;
            btnSend.Click += btnSend1_Click;
            btnDel.Click += btnDelete1_Click;
            btnFill.Click += btnFill1_Click;
        }
        else
        {
            txtId2 = txtId; txtNumber2 = txtNum; txtInfo2 = txtInf;
            btnSend.Click += btnSend2_Click;
            btnDel.Click += btnDelete2_Click;
            btnFill.Click += btnFill2_Click;
        }
    }

    #endregion

    private Label lblTitle;
    private Panel pnlConnection;
    private TextBox txtIP, txtPort1, txtPort2;
    private Button btnTest;
    private Label lblStatus;

    private Panel pnlSniffer;
    private Button btnListen;
    private Label lblListenStatus;
    private Label lblMsgCount;

    private GroupBox grpBoard1, grpBoard2;
    private TextBox txtId1, txtNumber1, txtInfo1;
    private TextBox txtId2, txtNumber2, txtInfo2;

    private Panel pnlGlobal;
    private Button btnSendBoth, btnDeleteBoth, btnFillBoth, btnMultiTest, btnClearMulti;

    private TextBox txtPreview;
    private TextBox txtLog;
    private Button btnSaveLog;
}
