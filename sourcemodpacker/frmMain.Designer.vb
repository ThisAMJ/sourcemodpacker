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
        Me.lblSettings = New System.Windows.Forms.Label()
        Me.cbPlaceholder = New System.Windows.Forms.CheckBox()
        Me.txtFromPath = New System.Windows.Forms.TextBox()
        Me.txtToPath = New System.Windows.Forms.TextBox()
        Me.lblToPath = New System.Windows.Forms.Label()
        Me.lblFromPath = New System.Windows.Forms.Label()
        Me.btnConvert = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'pbConversion
        '
        Me.pbConversion.Location = New System.Drawing.Point(12, 265)
        Me.pbConversion.Name = "pbConversion"
        Me.pbConversion.Size = New System.Drawing.Size(216, 23)
        Me.pbConversion.TabIndex = 0
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
        'cbPlaceholder
        '
        Me.cbPlaceholder.AutoSize = True
        Me.cbPlaceholder.Location = New System.Drawing.Point(18, 55)
        Me.cbPlaceholder.Name = "cbPlaceholder"
        Me.cbPlaceholder.Size = New System.Drawing.Size(82, 17)
        Me.cbPlaceholder.TabIndex = 5
        Me.cbPlaceholder.Text = "Placeholder"
        Me.cbPlaceholder.UseVisualStyleBackColor = True
        '
        'txtFromPath
        '
        Me.txtFromPath.Location = New System.Drawing.Point(12, 200)
        Me.txtFromPath.Name = "txtFromPath"
        Me.txtFromPath.Size = New System.Drawing.Size(216, 20)
        Me.txtFromPath.TabIndex = 6
        '
        'txtToPath
        '
        Me.txtToPath.Location = New System.Drawing.Point(12, 239)
        Me.txtToPath.Name = "txtToPath"
        Me.txtToPath.Size = New System.Drawing.Size(216, 20)
        Me.txtToPath.TabIndex = 7
        '
        'lblToPath
        '
        Me.lblToPath.AutoSize = True
        Me.lblToPath.Location = New System.Drawing.Point(9, 223)
        Me.lblToPath.Name = "lblToPath"
        Me.lblToPath.Size = New System.Drawing.Size(20, 13)
        Me.lblToPath.TabIndex = 8
        Me.lblToPath.Text = "To"
        '
        'lblFromPath
        '
        Me.lblFromPath.AutoSize = True
        Me.lblFromPath.Location = New System.Drawing.Point(9, 184)
        Me.lblFromPath.Name = "lblFromPath"
        Me.lblFromPath.Size = New System.Drawing.Size(30, 13)
        Me.lblFromPath.TabIndex = 9
        Me.lblFromPath.Text = "From"
        '
        'btnConvert
        '
        Me.btnConvert.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConvert.Location = New System.Drawing.Point(12, 294)
        Me.btnConvert.Name = "btnConvert"
        Me.btnConvert.Size = New System.Drawing.Size(216, 39)
        Me.btnConvert.TabIndex = 10
        Me.btnConvert.Text = "Convert"
        Me.btnConvert.UseVisualStyleBackColor = True
        '
        'FrmMain
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(240, 345)
        Me.Controls.Add(Me.btnConvert)
        Me.Controls.Add(Me.lblFromPath)
        Me.Controls.Add(Me.lblToPath)
        Me.Controls.Add(Me.txtToPath)
        Me.Controls.Add(Me.txtFromPath)
        Me.Controls.Add(Me.cbPlaceholder)
        Me.Controls.Add(Me.lblSettings)
        Me.Controls.Add(Me.pbConversion)
        Me.ForeColor = System.Drawing.Color.Black
        Me.Name = "FrmMain"
        Me.Text = "SMP"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pbConversion As ProgressBar
    Friend WithEvents lblSettings As Label
    Friend WithEvents cbPlaceholder As CheckBox
    Friend WithEvents txtFromPath As TextBox
    Friend WithEvents txtToPath As TextBox
    Friend WithEvents lblToPath As Label
    Friend WithEvents lblFromPath As Label
    Friend WithEvents btnConvert As Button
End Class
