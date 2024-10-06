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
}
