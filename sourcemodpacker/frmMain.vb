Imports System.IO

Public Class FrmMain
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AllowDrop = True
        'Process.Start("C:\path\to\program.exe", "arg", "arg", "arg", "etc")
    End Sub

    Public Sub FileEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then e.Effect = DragDropEffects.Copy
        lblFolder.Text = CType(e.Data.GetData(DataFormats.FileDrop), String()).First
    End Sub

    Public Sub FileDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        dropPath = CType(e.Data.GetData(DataFormats.FileDrop), String()).First
        Dim files = GetFiles(dropPath)
        e.Effect = DragDropEffects.None
        Pack(files)
    End Sub

    Public Sub DropCancel() Handles Me.DragLeave
        lblFolder.Text = "Drop a folder here!"
    End Sub
End Class

Module code
    Public options As New Settings(0, True)

    Public dropPath As String

    Public Function GetFiles(path As String)
        Dim files = New List(Of String)
        If File.Exists(path) Then
            files.Add(path)
        ElseIf Directory.Exists(path) Then
            files.AddRange(Directory.GetFiles(path, "*", SearchOption.AllDirectories))
        End If
        Return files
    End Function
End Module