using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

public partial class Main : Form
{
    private readonly Random _random = new Random();
    private Config _config;

    public Main()
    {
        InitializeComponent();
        Configure();
    }

    private void Configure()
    {
        LoadConfig();
        SpamUser();
    }

    protected override void SetVisibleCore(bool value)
    {
        base.SetVisibleCore(false);
    }

    private void LoadConfig()
    {
        try
        {
            var location = Assembly.GetExecutingAssembly().Location;
            if (location != null)
                _config =
                    Import.LoadConfigFromXmlFile(Path.Combine(Directory.GetParent(location).FullName, "Config.xml"));
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SpamUser()
    {
        while (true)
            try
            {
                var message = GetRandomMessage();
                MessageBox.Show(message.Name, message.Caption, GetRandomButtons(), GetRandomIcon());
                Thread.Sleep(_random.Next(2300));
            }
            catch
            {
                // ignored
            }
        // ReSharper disable once FunctionNeverReturns
    }

    private Message GetRandomMessage()
    {
        return _config.Messages.ElementAt(_random.Next(_config.Messages.Count));
    }

    private MessageBoxButtons GetRandomButtons()
    {
        var values = Enum.GetValues(typeof(MessageBoxButtons));
        return (MessageBoxButtons) values.GetValue(_random.Next(values.Length));
    }

    private MessageBoxIcon GetRandomIcon()
    {
        var values = Enum.GetValues(typeof(MessageBoxIcon));
        return (MessageBoxIcon) values.GetValue(_random.Next(values.Length));
    }
}