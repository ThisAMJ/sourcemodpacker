Imports System.IO

Public Class FrmMain
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AllowDrop = True
        'Process.Start("C:\Users\User\Files\Games\Game Files\Run Steam Games\main\run.bat", "320")
    End Sub

    Public Sub FileEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then e.Effect = DragDropEffects.Copy
    End Sub

    Public Sub FileDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Dim files As String() = e.Data.GetData(DataFormats.FileDrop)
        For Each file In files
            Console.WriteLine(file)
        Next
    End Sub
End Class