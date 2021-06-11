Imports System.IO

Public Class FrmMain
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim l = Nothing
        For Each letter In "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray
            If Directory.Exists(letter & ":\Program Files (x86)\Steam\steamapps\common") Then
                l = letter & ":\Program Files (x86)\Steam\steamapps\common"
                Exit For
            End If
        Next
        If l IsNot Nothing Then
            steamDir = l
        End If
    End Sub

    Private Sub FileEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            dropPath = CType(e.Data.GetData(DataFormats.FileDrop), String()).First
            txtFromPath.Text = dropPath
            If dropType Then
                txtToPath.Text = dropPath.Replace(".bmp", ".vtf").Replace(".jpg", ".vtf").Replace(".png", ".vtf").Replace(".tga", ".vtf") 'im lazy
            Else
                txtToPath.Text = dropPath & "\pak01_dir.vpk"
            End If
            dropType = File.Exists(dropPath)
        End If
    End Sub

    Private Sub ConvertClick(sender As Object, e As EventArgs) Handles btnConvert.Click
        Pack(dropPath)
    End Sub
End Class

Module code
    Public steamDir As String
    Public options As New Settings
    Public dropPath As String
    Public dropType As Boolean

    Public Function GetFiles(p As String)
        Dim f = New List(Of String)
        If File.Exists(p) Then
            f.Add(p)
        ElseIf Directory.Exists(p) Then
            f.AddRange(Directory.GetFiles(p, "*", SearchOption.AllDirectories))
        End If
        Return f
    End Function
End Module