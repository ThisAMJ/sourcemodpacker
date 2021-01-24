Module Converter
    Public Sub Pack(files As List(Of String))
        FrmMain.pbConversion.Minimum = 0
        FrmMain.pbConversion.Maximum = files.Count
        FrmMain.pbConversion.Value = 0
        For Each file In files
            FrmMain.pbConversion.Value += 1
        Next
    End Sub



    Public Sub Vtfify(path As String)

    End Sub
End Module
