using APICallerLibrary;

namespace APICallerUI;

public partial class Dashboard : Form
{
    private readonly IAPIController api = new APIController();
    public Dashboard()
    {
        InitializeComponent();
        httpMethodSelection.SelectedItem = "GET";
        statusStrip.BackColor = Color.Green;
    }

    private async void callApi_Click(object sender, EventArgs e)
    {
        systemStatus.Text = "Calling API...";
        statusStrip.BackColor = Color.Yellow;
        resultsText.Text = "";

        if (api.IsValidUrl(apiText.Text) == false)
        {
            systemStatus.Text = "Invalid URL";
            statusStrip.BackColor = Color.Red;
            return;
        }

        HttpMethods method;
        if(Enum.TryParse(httpMethodSelection.SelectedItem!.ToString(), out method) == false)
        {
            systemStatus.Text = "Invalid HTTP Method";
            return;
        }

        try
        {
            resultsText.Text = await api.CallAPIAsync(apiText.Text, bodyText.Text, method, true);
            callData.SelectedTab = resultsTab;
            resultsTab.Focus();
            systemStatus.Text = "Ready";
            statusStrip.BackColor = Color.Green;
        }
        catch (Exception ex)
        {
            resultsText.Text = "Error: " + ex.Message;
            systemStatus.Text = "Error";
        }
    }

    private void InitializeComponent()
    {
        formHeader = new Label();
        apiLabel = new Label();
        apiText = new TextBox();
        callApi = new Button();
        resultsLabel = new Label();
        resultsText = new TextBox();
        systemStatus = new ToolStripStatusLabel();
        statusStrip = new StatusStrip();
        httpMethodSelection = new ComboBox();
        callData = new TabControl();
        bodyTab = new TabPage();
        bodyText = new TextBox();
        resultsTab = new TabPage();
        statusStrip.SuspendLayout();
        callData.SuspendLayout();
        bodyTab.SuspendLayout();
        resultsTab.SuspendLayout();
        SuspendLayout();
        // 
        // formHeader
        // 
        formHeader.AutoSize = true;
        formHeader.Font = new Font("Segoe UI", 22F);
        formHeader.Location = new Point(55, 31);
        formHeader.Name = "formHeader";
        formHeader.Size = new Size(283, 78);
        formHeader.TabIndex = 0;
        formHeader.Text = "API Caller";
        // 
        // apiLabel
        // 
        apiLabel.AutoSize = true;
        apiLabel.Font = new Font("Segoe UI", 18F);
        apiLabel.Location = new Point(55, 142);
        apiLabel.Name = "apiLabel";
        apiLabel.Size = new Size(109, 65);
        apiLabel.TabIndex = 1;
        apiLabel.Text = "API:";
        // 
        // apiText
        // 
        apiText.Font = new Font("Segoe UI", 18F);
        apiText.Location = new Point(440, 134);
        apiText.Name = "apiText";
        apiText.Size = new Size(1269, 71);
        apiText.TabIndex = 2;
        // 
        // callApi
        // 
        callApi.Location = new Point(1715, 131);
        callApi.Name = "callApi";
        callApi.Size = new Size(130, 71);
        callApi.TabIndex = 3;
        callApi.Text = "Go";
        callApi.UseVisualStyleBackColor = true;
        callApi.Click += callApi_Click;
        // 
        // resultsLabel
        // 
        resultsLabel.AutoSize = true;
        resultsLabel.Location = new Point(55, 256);
        resultsLabel.Name = "resultsLabel";
        resultsLabel.Size = new Size(0, 65);
        resultsLabel.TabIndex = 4;
        // 
        // resultsText
        // 
        resultsText.BackColor = Color.White;
        resultsText.Dock = DockStyle.Fill;
        resultsText.Location = new Point(3, 3);
        resultsText.Multiline = true;
        resultsText.Name = "resultsText";
        resultsText.ReadOnly = true;
        resultsText.ScrollBars = ScrollBars.Both;
        resultsText.Size = new Size(1744, 686);
        resultsText.TabIndex = 5;
        // 
        // systemStatus
        // 
        systemStatus.Name = "systemStatus";
        systemStatus.Size = new Size(78, 32);
        systemStatus.Text = "Ready";
        // 
        // statusStrip
        // 
        statusStrip.BackColor = Color.White;
        statusStrip.ImageScalingSize = new Size(32, 32);
        statusStrip.Items.AddRange(new ToolStripItem[] { systemStatus });
        statusStrip.Location = new Point(0, 1026);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(1887, 42);
        statusStrip.TabIndex = 6;
        statusStrip.Text = "System Status";
        // 
        // httpMethodSelection
        // 
        httpMethodSelection.DropDownStyle = ComboBoxStyle.DropDownList;
        httpMethodSelection.FormattingEnabled = true;
        httpMethodSelection.Items.AddRange(new object[] { "GET", "POST", "PATCH", "PUT", "DELETE" });
        httpMethodSelection.Location = new Point(192, 134);
        httpMethodSelection.Name = "httpMethodSelection";
        httpMethodSelection.Size = new Size(242, 73);
        httpMethodSelection.TabIndex = 7;
        // 
        // callData
        // 
        callData.Controls.Add(bodyTab);
        callData.Controls.Add(resultsTab);
        callData.Location = new Point(77, 277);
        callData.Name = "callData";
        callData.SelectedIndex = 0;
        callData.Size = new Size(1766, 746);
        callData.TabIndex = 8;
        // 
        // bodyTab
        // 
        bodyTab.Controls.Add(bodyText);
        bodyTab.Location = new Point(8, 79);
        bodyTab.Name = "bodyTab";
        bodyTab.Padding = new Padding(3);
        bodyTab.Size = new Size(1750, 659);
        bodyTab.TabIndex = 0;
        bodyTab.Text = "Body";
        bodyTab.UseVisualStyleBackColor = true;
        // 
        // bodyText
        // 
        bodyText.Dock = DockStyle.Fill;
        bodyText.Location = new Point(3, 3);
        bodyText.Multiline = true;
        bodyText.Name = "bodyText";
        bodyText.ScrollBars = ScrollBars.Both;
        bodyText.Size = new Size(1744, 653);
        bodyText.TabIndex = 0;
        // 
        // resultsTab
        // 
        resultsTab.Controls.Add(resultsText);
        resultsTab.Location = new Point(8, 46);
        resultsTab.Name = "resultsTab";
        resultsTab.Padding = new Padding(3);
        resultsTab.Size = new Size(1750, 692);
        resultsTab.TabIndex = 1;
        resultsTab.Text = "Results";
        resultsTab.UseVisualStyleBackColor = true;
        // 
        // Dashboard
        // 
        BackColor = Color.White;
        ClientSize = new Size(1887, 1068);
        Controls.Add(callData);
        Controls.Add(httpMethodSelection);
        Controls.Add(statusStrip);
        Controls.Add(resultsLabel);
        Controls.Add(callApi);
        Controls.Add(apiText);
        Controls.Add(apiLabel);
        Controls.Add(formHeader);
        Font = new Font("Segoe UI", 18F);
        Name = "Dashboard";
        Text = "API Caller - by Ujjwal";
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        callData.ResumeLayout(false);
        bodyTab.ResumeLayout(false);
        bodyTab.PerformLayout();
        resultsTab.ResumeLayout(false);
        resultsTab.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label formHeader;
    private Label apiLabel;
    private TextBox apiText;
    private Button callApi;
    private Label resultsLabel;
    private TextBox resultsText;
    private ToolStripStatusLabel systemStatus;
    private StatusStrip statusStrip;
    private ComboBox httpMethodSelection;
    private TabControl callData;
    private TabPage bodyTab;
    private TabPage resultsTab;
    private TextBox bodyText;
}
