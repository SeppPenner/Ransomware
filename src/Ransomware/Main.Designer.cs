namespace Ransomware;

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

partial class Main
{
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
        this.SuspendLayout();
        // 
        // Main
        // 
        this.AutoScaleDimensions = new SizeF(6F, 13F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(0, 0);
        this.Name = "Main";
        this.ShowInTaskbar = false;
        this.Text = "COM Surrogate";
        this.ResumeLayout(false);
        this.Visible = false;
        this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    }

    #endregion
}