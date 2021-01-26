﻿Imports System.IO

Public Class FrmMain
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AllowDrop = True
    End Sub

    Public Sub FileEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        e.Effect = If(e.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
        lblFolder.Text = CType(e.Data.GetData(DataFormats.FileDrop), String()).First
    End Sub

    Public Sub FileDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        AllowDrop = False
        dropPath = CType(e.Data.GetData(DataFormats.FileDrop), String()).First
        Dim files = GetFiles(dropPath)
        Pack(files)
        AllowDrop = True
    End Sub

    Public Sub DropCancel() Handles Me.DragLeave
        lblFolder.Text = "Drop a folder here!"
    End Sub
End Class

Module code
    Public options As Settings

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