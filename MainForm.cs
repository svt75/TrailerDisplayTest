using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TrailerDisplayTest;

public partial class MainForm : Form
{
    private TcpListener _listener1;
    private TcpListener _listener2;
    private CancellationTokenSource _cts;
    private bool _listening = false;
    private int _msgCount = 0;
    private int _addCount = 0;
    private int _delCount = 0;
    private StreamWriter _logWriter;

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
            var data = Encoding.UTF8.GetBytes(json + "\n");
            client.GetStream().Write(data, 0, data.Length);
            Log($"TX {ip}:{port} -> {json}");
            return true;
        }
        catch (Exception ex)
        {
            Log($"TX Error ({ip}:{port}): {ex.Message}");
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

    // ========== TCP SNIFFER ==========
    private void btnListen_Click(object sender, EventArgs e)
    {
        if (_listening) StopListening();
        else StartListening();
    }

    private void StartListening()
    {
        try
        {
            int port1 = int.Parse(txtPort1.Text);
            int port2 = int.Parse(txtPort2.Text);
            _cts = new CancellationTokenSource();
            _msgCount = 0;
            _addCount = 0;
            _delCount = 0;

            var logFile = $"sniffer_{DateTime.Now:yyyyMMdd_HHmmss}.log";
            _logWriter = new StreamWriter(logFile, true, Encoding.UTF8) { AutoFlush = true };
            _logWriter.WriteLine($"=== Sniffer started {DateTime.Now:yyyy-MM-dd HH:mm:ss} ports {port1},{port2} ===");

            _listener1 = new TcpListener(IPAddress.Any, port1);
            _listener1.Start();
            Task.Run(() => AcceptLoop(_listener1, port1, "UNLOAD", _cts.Token));

            _listener2 = new TcpListener(IPAddress.Any, port2);
            _listener2.Start();
            Task.Run(() => AcceptLoop(_listener2, port2, "LOAD", _cts.Token));

            _listening = true;
            btnListen.Text = "STOP LISTEN";
            btnListen.BackColor = Color.FromArgb(200, 50, 50);
            lblListenStatus.Text = "\u25CF";
            lblListenStatus.ForeColor = Color.Lime;
            Log($"=== SNIFFER STARTED on ports {port1} and {port2} ===");
            Log($"Log file: {Path.GetFullPath(logFile)}");
            Log("Waiting for incoming TCP connections...");
        }
        catch (Exception ex)
        {
            Log($"Listen Error: {ex.Message}");
            lblListenStatus.Text = "\u25CF";
            lblListenStatus.ForeColor = Color.Red;
        }
    }

    private void StopListening()
    {
        try
        {
            _cts?.Cancel();
            _listener1?.Stop();
            _listener2?.Stop();
            _logWriter?.WriteLine($"=== Sniffer stopped {DateTime.Now:yyyy-MM-dd HH:mm:ss} Total:{_msgCount} ADD:{_addCount} DEL:{_delCount} ===");
            _logWriter?.Close();
            _logWriter = null;
        }
        catch { }
        _listening = false;
        btnListen.Text = "LISTEN (Sniffer)";
        btnListen.BackColor = Color.FromArgb(170, 120, 0);
        lblListenStatus.Text = "\u25CF";
        lblListenStatus.ForeColor = Color.Gray;
        Log($"=== SNIFFER STOPPED (Total:{_msgCount} ADD:{_addCount} DEL:{_delCount}) ===");
    }

    private async Task AcceptLoop(TcpListener listener, int port, string board, CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                var client = await listener.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClient(client, port, board, ct));
            }
            catch { if (!ct.IsCancellationRequested) break; }
        }
    }

    private void HandleClient(TcpClient client, int port, string board, CancellationToken ct)
    {
        try
        {
            var ep = client.Client.RemoteEndPoint?.ToString() ?? "?";
            Invoke(() => Log($"RX [{board}] Connection from {ep}"));
            _logWriter?.WriteLine($"[{DateTime.Now:HH:mm:ss}] Connection from {ep} -> {board}:{port}");
            using var stream = client.GetStream();
            stream.ReadTimeout = 30000;
            var reader = new StreamReader(stream, Encoding.UTF8);
            var buf = new StringBuilder();
            int ch;
            while ((ch = reader.Read()) != -1 && !ct.IsCancellationRequested)
            {
                buf.Append((char)ch);
                var s = buf.ToString().Trim();
                if (s.StartsWith("{") && s.EndsWith("}"))
                {
                    _msgCount++;
                    var isDel = s.Contains("\"trailer_info\":\"\"") || s.Contains("\"trailer_info\": \"\"");
                    if (isDel) _delCount++; else _addCount++;
                    var tag = isDel ? "DEL" : "ADD";
                    _logWriter?.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{board}:{port}] #{_msgCount} [{tag}] {s}");
                    Invoke(() =>
                    {
                        Log($"RX [{board}:{port}] #{_msgCount} [{tag}] {s}");
                        lblMsgCount.Text = $"Total:{_msgCount} ADD:{_addCount} DEL:{_delCount}";
                    });
                    buf.Clear();
                }
            }
        }
        catch (Exception ex)
        {
            if (!ct.IsCancellationRequested)
            {
                _logWriter?.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{board}] Error: {ex.Message}");
                Invoke(() => Log($"RX [{board}] Error: {ex.Message}"));
            }
        }
        finally { try { client.Close(); } catch { } }
    }

    private void btnSaveLog_Click(object sender, EventArgs e)
    {
        var sfd = new SaveFileDialog { Filter = "Log files|*.log|Text files|*.txt", FileName = $"sniffer_{DateTime.Now:yyyyMMdd_HHmmss}.log" };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            File.WriteAllText(sfd.FileName, txtLog.Text, Encoding.UTF8);
            Log($"Log saved to {sfd.FileName}");
        }
    }

    // ========== SEND CONTROLS ==========
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
        Log("Filled test (unload)");
    }

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
        Log("Filled test (load)");
    }

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
