
namespace PhoneBook.ReportHandler
{
  partial class frmMain
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.txtLog = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // txtLog
      // 
      this.txtLog.BackColor = System.Drawing.Color.Black;
      this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.txtLog.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
      this.txtLog.ForeColor = System.Drawing.Color.Lime;
      this.txtLog.Location = new System.Drawing.Point(0, 209);
      this.txtLog.Multiline = true;
      this.txtLog.Name = "txtLog";
      this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtLog.Size = new System.Drawing.Size(1037, 351);
      this.txtLog.TabIndex = 0;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1037, 560);
      this.Controls.Add(this.txtLog);
      this.Name = "frmMain";
      this.Text = "frmMain";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtLog;
  }
}