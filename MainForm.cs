using System.Net.Sockets;
using System.Text;

namespace TrailerDisplayTest;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void Log(string msg)
    {
        if (txtLog.InvokeRequired)
        {
            txtLog.Invoke(() => Log(msg));
            return;
        }
        txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n");
    }

    private bool SendTcp(string ip, int port, string json)
    {
        try
        {
            using var client = new TcpClient();
            client.Connect(ip, port);
            var data = Encoding.UTF8.GetBytes(json);
            client.GetStream().Write(data, 0, data.Length);
            Log($"TCP {ip}:{port} -> {json}");
            return true;
        }
        catch (Exception ex)
        {
            Log($"TCP Error ({ip}:{port}): {ex.Message}");
            return false;
        }
    }

    private string BuildJson(string tId, string tNum, string tInfo)
    {
        tInfo = tInfo.Trim();
        if (string.IsNullOrEmpty(tInfo))
            return $"{{\"trailer_id\":\"{Esc(tId)}\",\"trailer_number\":\"{Esc(tNum)}\",\"trailer_info\":\"\"}}";
        var parts = tInfo.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
        var arr = string.Join(",", parts.Select(p => $"\"{Esc(p.Trim())}\""));
        return $"{{\"trailer_id\":\"{Esc(tId)}\",\"trailer_number\":\"{Esc(tNum)}\",\"trailer_info\":[{arr}]}}";
    }

    private string Esc(string s) => s.Replace("\\", "\\\\").Replace("\"", "\\\"");

    private void btnTest_Click(object sender, EventArgs e)
    {
        Log("Testing connection...");
        try
        {
            using var client = new TcpClient();
            var result = client.BeginConnect(txtIP.Text, int.Parse(txtPort1.Text), null, null);
            var ok = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3));
            if (ok && client.Connected)
            {
                client.EndConnect(result);
                Log($"Connected to {txtIP.Text}:{txtPort1.Text}");
                lblStatus.ForeColor = Color.Lime;
            }
            else
            {
                Log($"Cannot connect to {txtIP.Text}:{txtPort1.Text}");
                lblStatus.ForeColor = Color.Red;
            }
        }
        catch (Exception ex)
        {
            Log($"Error: {ex.Message}");
            lblStatus.ForeColor = Color.Red;
        }
    }
    private void btnSend1_Click(object s, EventArgs e)
    { SendTcp(txtIP.Text, int.Parse(txtPort1.Text), BuildJson(txtId1.Text, txtNumber1.Text, txtInfo1.Text)); }
    private void btnDelete1_Click(object s, EventArgs e)
    { SendTcp(txtIP.Text, int.Parse(txtPort1.Text), BuildJson(txtId1.Text, txtNumber1.Text, "")); }
    private void btnFill1_Click(object s, EventArgs e)
    { txtId1.Text="001"; txtNumber1.Text="M530CM"; txtInfo1.Text="4101, 2005"; }
    private void btnSend2_Click(object s, EventArgs e)
    { SendTcp(txtIP.Text, int.Parse(txtPort2.Text), BuildJson(txtId2.Text, txtNumber2.Text, txtInfo2.Text)); }
    private void btnDelete2_Click(object s, EventArgs e)
    { SendTcp(txtIP.Text, int.Parse(txtPort2.Text), BuildJson(txtId2.Text, txtNumber2.Text, "")); }
    private void btnFill2_Click(object s, EventArgs e)
    { txtId2.Text="002"; txtNumber2.Text="A777AA"; txtInfo2.Text="3022"; }
    private void btnSendBoth_Click(object s, EventArgs e)
    { btnSend1_Click(s,e); btnSend2_Click(s,e); }
    private void btnDeleteBoth_Click(object s, EventArgs e)
    { btnDelete1_Click(s,e); btnDelete2_Click(s,e); }
    private void btnFillBoth_Click(object s, EventArgs e)
    { btnFill1_Click(s,e); btnFill2_Click(s,e); }
    private void btnMultiTest_Click(object s, EventArgs e)
    {
        Log("=== Multi-test: 3 trailers per board ===");
        var ip = txtIP.Text;
        int p1 = int.Parse(txtPort1.Text);
        int p2 = int.Parse(txtPort2.Text);
        SendTcp(ip, p1, BuildJson("T001","M530CM","4101,2005"));
        SendTcp(ip, p1, BuildJson("T002","K123AB","3010"));
        SendTcp(ip, p1, BuildJson("T003","H456OP","5200,5201,5202"));
        SendTcp(ip, p2, BuildJson("T004","A777AA","3022"));
        SendTcp(ip, p2, BuildJson("T005","B999BB","1001,1002"));
        SendTcp(ip, p2, BuildJson("T006","P111PP","7050"));
        Log("=== Multi-test sent ===");
    }
    private void btnClearMulti_Click(object s, EventArgs e)
    {
        Log("=== Clearing multi-test ===");
        var ip = txtIP.Text;
        int p1 = int.Parse(txtPort1.Text);
        int p2 = int.Parse(txtPort2.Text);
        for (int i=1;i<=3;i++) SendTcp(ip,p1,BuildJson($"T00{i}","",""));
        for (int i=4;i<=6;i++) SendTcp(ip,p2,BuildJson($"T00{i}","",""));
        Log("=== Multi-test cleared ===");
    }
}
