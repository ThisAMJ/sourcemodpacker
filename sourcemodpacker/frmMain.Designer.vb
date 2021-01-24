<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pbConversion = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'pbConversion
        '
        Me.pbConversion.Location = New System.Drawing.Point(13, 415)
        Me.pbConversion.Name = "pbConversion"
        Me.pbConversion.Size = New System.Drawing.Size(263, 23)
        Me.pbConversion.TabIndex = 0
        '
        'FrmMain
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pbConversion)
        Me.Name = "FrmMain"
        Me.Text = "Source Mod Packer"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pbConversion As ProgressBar
End Class
