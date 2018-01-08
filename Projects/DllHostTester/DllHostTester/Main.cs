using System;
using System.Security.Principal;
using System.Windows.Forms;
using Properties;

public partial class Main : Form
{
    public Main()
    {
        InitializeComponent();
        CheckAdminPrivileges();
        SetSuccessText();
    }

    private void CheckAdminPrivileges()
    {
        if (IsElevated()) return;
        MessageBox.Show(Resources.ProgrammInAdministratorModeText, Resources.ProgrammInAdministratorModeCaption,
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        Environment.Exit(0);
    }

    private void SetSuccessText()
    {
        textBox_Test.Text = Resources.SuccessfullyLaunchedInAdminMode;
    }

    private bool IsElevated()
    {
        var id = WindowsIdentity.GetCurrent();
        return id.Owner != id.User;
    }
}