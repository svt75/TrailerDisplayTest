namespace TrailerDisplayTest;
partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing)
    { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
    #region Generated code
    private void InitializeComponent()
    {
        this.SuspendLayout();
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.BackColor = Color.FromArgb(30, 30, 30);
        this.ClientSize = new Size(700, 680);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = "MainForm";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Trailer Display Test";
        lblTitle = new Label { AutoSize=true, Font=new Font("Arial",16F,FontStyle.Bold), ForeColor=Color.FromArgb(255,68,68), Location=new Point(200,12), Text="TRAILER DISPLAY TEST" };
        this.Controls.Add(lblTitle);
        pnlConnection = new Panel { BackColor=Color.FromArgb(45,45,45), Location=new Point(12,48), Size=new Size(676,45) };
        this.Controls.Add(pnlConnection);
        pnlConnection.Controls.Add(new Label{Text="IP:",ForeColor=Color.White,Location=new Point(10,13),AutoSize=true});
        txtIP = new TextBox{Text="192.168.1.211",Location=new Point(35,10),Size=new Size(120,23),BackColor=Color.FromArgb(50,50,50),ForeColor=Color.White};
        pnlConnection.Controls.Add(txtIP);
        pnlConnection.Controls.Add(new Label{Text="Port B:",ForeColor=Color.FromArgb(255,100,100),Location=new Point(168,13),AutoSize=true});
        txtPort1 = new TextBox{Text="5001",Location=new Point(218,10),Size=new Size(50,23),BackColor=Color.FromArgb(50,50,50),ForeColor=Color.White};
        pnlConnection.Controls.Add(txtPort1);
        pnlConnection.Controls.Add(new Label{Text="Port P:",ForeColor=Color.FromArgb(100,255,100),Location=new Point(280,13),AutoSize=true});
        txtPort2 = new TextBox{Text="5002",Location=new Point(330,10),Size=new Size(50,23),BackColor=Color.FromArgb(50,50,50),ForeColor=Color.White};
        pnlConnection.Controls.Add(txtPort2);
        btnTest = new Button{Text="Test",Location=new Point(400,8),Size=new Size(60,28),BackColor=Color.FromArgb(68,136,255),ForeColor=Color.White,FlatStyle=FlatStyle.Flat};
        btnTest.Click += btnTest_Click;
        pnlConnection.Controls.Add(btnTest);
        lblStatus = new Label{Text="\u25CF",ForeColor=Color.Gray,Location=new Point(470,12),AutoSize=true,Font=new Font("Arial",14F)};
        pnlConnection.Controls.Add(lblStatus);
        grpBoard1 = new GroupBox{Text=" UNLOAD (TCP 5001) ",ForeColor=Color.FromArgb(255,100,100),Font=new Font("Arial",10F,FontStyle.Bold),Location=new Point(12,100),Size=new Size(330,195)};
        this.Controls.Add(grpBoard1);
        CreateBoardControls(grpBoard1, 1);
        grpBoard2 = new GroupBox{Text=" LOAD (TCP 5002) ",ForeColor=Color.FromArgb(100,255,100),Font=new Font("Arial",10F,FontStyle.Bold),Location=new Point(358,100),Size=new Size(330,195)};
        this.Controls.Add(grpBoard2);
        CreateBoardControls(grpBoard2, 2);
        pnlGlobal = new Panel{BackColor=Color.FromArgb(45,45,45),Location=new Point(12,305),Size=new Size(676,85)};
        this.Controls.Add(pnlGlobal);
        btnSendBoth = new Button{Text="SEND BOTH",Location=new Point(20,10),Size=new Size(110,30),BackColor=Color.FromArgb(68,170,68),ForeColor=Color.White,FlatStyle=FlatStyle.Flat,Font=new Font("Arial",9F,FontStyle.Bold)};
        btnSendBoth.Click += btnSendBoth_Click;
        pnlGlobal.Controls.Add(btnSendBoth);
        btnDeleteBoth = new Button{Text="DELETE BOTH",Location=new Point(140,10),Size=new Size(110,30),BackColor=Color.FromArgb(170,68,68),ForeColor=Color.White,FlatStyle=FlatStyle.Flat,Font=new Font("Arial",9F,FontStyle.Bold)};
        btnDeleteBoth.Click += btnDeleteBoth_Click;
        pnlGlobal.Controls.Add(btnDeleteBoth);
        btnFillBoth = new Button{Text="FILL BOTH",Location=new Point(260,10),Size=new Size(100,30),BackColor=Color.FromArgb(80,80,80),ForeColor=Color.White,FlatStyle=FlatStyle.Flat};
        btnFillBoth.Click += btnFillBoth_Click;
        pnlGlobal.Controls.Add(btnFillBoth);
        btnMultiTest = new Button{Text="MULTI-TEST (6 trailers)",Location=new Point(20,48),Size=new Size(200,30),BackColor=Color.FromArgb(68,68,170),ForeColor=Color.White,FlatStyle=FlatStyle.Flat,Font=new Font("Arial",9F,FontStyle.Bold)};
        btnMultiTest.Click += btnMultiTest_Click;
        pnlGlobal.Controls.Add(btnMultiTest);
        btnClearMulti = new Button{Text="CLEAR MULTI",Location=new Point(230,48),Size=new Size(130,30),BackColor=Color.FromArgb(120,68,68),ForeColor=Color.White,FlatStyle=FlatStyle.Flat};
        btnClearMulti.Click += btnClearMulti_Click;
        pnlGlobal.Controls.Add(btnClearMulti);
        var lblPrev = new Label{Text="JSON Format:",ForeColor=Color.Gray,Location=new Point(12,398),AutoSize=true,Font=new Font("Arial",8F)};
        this.Controls.Add(lblPrev);
        txtPreview = new TextBox{Location=new Point(12,415),Size=new Size(676,40),Multiline=true,BackColor=Color.FromArgb(40,40,40),ForeColor=Color.FromArgb(200,200,100),Font=new Font("Consolas",8.5F),ReadOnly=true};
        txtPreview.Text = "{\"trailer_id\":\"001\",\"trailer_number\":\"M530CM\",\"trailer_info\":[\"4101\",\"2005\"]}";
        this.Controls.Add(txtPreview);
        this.Controls.Add(new Label{Text="Log:",ForeColor=Color.Gray,Location=new Point(12,462),AutoSize=true});
        txtLog = new TextBox{Location=new Point(12,480),Size=new Size(676,190),Multiline=true,ScrollBars=ScrollBars.Vertical,BackColor=Color.Black,ForeColor=Color.Lime,Font=new Font("Consolas",9F),ReadOnly=true};
        this.Controls.Add(txtLog);
        this.ResumeLayout(false);
        this.PerformLayout();
    }
