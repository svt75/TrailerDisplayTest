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

    private string BuildJson(string trailerId, string trailerNumber, string trailerInfo)
    {
        trailerInfo = trailerInfo.Trim();
        if (string.IsNullOrEmpty(trailerInfo))
        {
            return $"{{\"trailer_id\":\"{Esc(trailerId)}\",\"trailer_number\":\"{Esc(trailerNumber)}\",\"trailer_info\":\"\"}}";
        }
        var parts = trailerInfo.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
        var arr = string.Join(",", parts.Select(p => $"\"{Esc(p.Trim())}\""));
        return $"{{\"trailer_id\":\"{Esc(trailerId)}\",\"trailer_number\":\"{Esc(trailerNumber)}\",\"trailer_info\":[{arr}]}}";
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
                lblStatus.Text = "\u25CF";
                lblStatus.ForeColor = Color.Lime;
            }
            else
            {
                Log($"Cannot connect to {txtIP.Text}:{txtPort1.Text}");
                lblStatus.Text = "\u25CF";
                lblStatus.ForeColor = Color.Red;
            }
        }
        catch (Exception ex)
        {
            Log($"Error: {ex.Message}");
            lblStatus.Text = "\u25CF";
            lblStatus.ForeColor = Color.Red;
        }
    }

    // --- Board 1: UNLOAD ---
    private void btnSend1_Click(object sender, EventArgs e)
    {
        var json = BuildJson(txtId1.Text, txtNumber1.Text, txtInfo1.Text);
        SendTcp(txtIP.Text, int.Parse(txtPort1.Text), json);
    }

    private void btnDelete1_Click(object sender, EventArgs e)
    {
        var json = BuildJson(txtId1.Text, txtNumber1.Text, "");
        SendTcp(txtIP.Text, int.Parse(txtPort1.Text), json);
        Log("Delete from UNLOAD board");
    }

    private void btnFill1_Click(object sender, EventArgs e)
    {
        txtId1.Text = "001";
        txtNumber1.Text = "\u041C530\u0421\u041C";
        txtInfo1.Text = "4101, 2005";
        Log("Filled test data (unload)");
    }

    // --- Board 2: LOAD ---
    private void btnSend2_Click(object sender, EventArgs e)
    {
        var json = BuildJson(txtId2.Text, txtNumber2.Text, txtInfo2.Text);
        SendTcp(txtIP.Text, int.Parse(txtPort2.Text), json);
    }

    private void btnDelete2_Click(object sender, EventArgs e)
    {
        var json = BuildJson(txtId2.Text, txtNumber2.Text, "");
        SendTcp(txtIP.Text, int.Parse(txtPort2.Text), json);
        Log("Delete from LOAD board");
    }

    private void btnFill2_Click(object sender, EventArgs e)
    {
        txtId2.Text = "002";
        txtNumber2.Text = "\u0410777\u0410\u0410";
        txtInfo2.Text = "3022";
        Log("Filled test data (load)");
    }

    // --- Global ---
    private void btnSendBoth_Click(object sender, EventArgs e)
    {
        btnSend1_Click(sender, e);
        btnSend2_Click(sender, e);
    }

    private void btnDeleteBoth_Click(object sender, EventArgs e)
    {
        btnDelete1_Click(sender, e);
        btnDelete2_Click(sender, e);
    }

    private void btnFillBoth_Click(object sender, EventArgs e)
    {
        btnFill1_Click(sender, e);
        btnFill2_Click(sender, e);
    }

    private void btnMultiTest_Click(object sender, EventArgs e)
    {
        Log("=== Multi-test: 3 trailers per board ===");
        var ip = txtIP.Text;
        int port1 = int.Parse(txtPort1.Text);
        int port2 = int.Parse(txtPort2.Text);

        SendTcp(ip, port1, BuildJson("T001", "\u041C530\u0421\u041C", "4101, 2005"));
        SendTcp(ip, port1, BuildJson("T002", "\u041A123\u0410\u0412", "3010"));
        SendTcp(ip, port1, BuildJson("T003", "\u041D456\u041E\u041F", "5200, 5201, 5202"));

        SendTcp(ip, port2, BuildJson("T004", "\u0410777\u0410\u0410", "3022"));
        SendTcp(ip, port2, BuildJson("T005", "\u0412999\u0412\u0412", "1001, 1002"));
        SendTcp(ip, port2, BuildJson("T006", "\u0420111\u0420\u0420", "7050"));

        Log("=== Multi-test sent ===");
    }

    private void btnClearMulti_Click(object sender, EventArgs e)
    {
        Log("=== Clearing multi-test ===");
        var ip = txtIP.Text;
        int port1 = int.Parse(txtPort1.Text);
        int port2 = int.Parse(txtPort2.Text);

        for (int i = 1; i <= 3; i++)
            SendTcp(ip, port1, BuildJson($"T00{i}", "", ""));
        for (int i = 4; i <= 6; i++)
            SendTcp(ip, port2, BuildJson($"T00{i}", "", ""));

        Log("=== Multi-test cleared ===");
    }
}
