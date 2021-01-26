<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pbConversion = New System.Windows.Forms.ProgressBar()
        Me.lblFolder = New System.Windows.Forms.Label()
        Me.lblSettings = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'pbConversion
        '
        Me.pbConversion.Location = New System.Drawing.Point(12, 415)
        Me.pbConversion.Name = "pbConversion"
        Me.pbConversion.Size = New System.Drawing.Size(256, 23)
        Me.pbConversion.TabIndex = 0
        '
        'lblFolder
        '
        Me.lblFolder.AutoSize = True
        Me.lblFolder.ForeColor = System.Drawing.Color.Black
        Me.lblFolder.Location = New System.Drawing.Point(9, 399)
        Me.lblFolder.Name = "lblFolder"
        Me.lblFolder.Size = New System.Drawing.Size(95, 13)
        Me.lblFolder.TabIndex = 3
        Me.lblFolder.Text = "Drop a folder here!"
        '
        'lblSettings
        '
        Me.lblSettings.AutoSize = True
        Me.lblSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSettings.Location = New System.Drawing.Point(13, 13)
        Me.lblSettings.Name = "lblSettings"
        Me.lblSettings.Size = New System.Drawing.Size(91, 26)
        Me.lblSettings.TabIndex = 4
        Me.lblSettings.Text = "Settings"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(12, 55)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(81, 17)
        Me.CheckBox1.TabIndex = 5
        Me.CheckBox1.Text = "CheckBox1"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'FrmMain
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.lblSettings)
        Me.Controls.Add(Me.lblFolder)
        Me.Controls.Add(Me.pbConversion)
        Me.ForeColor = System.Drawing.Color.Black
        Me.Name = "FrmMain"
        Me.Text = "Source Mod Packer"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pbConversion As ProgressBar
    Friend WithEvents lblFolder As Label
    Friend WithEvents lblSettings As Label
    Friend WithEvents CheckBox1 As CheckBox
End Class
