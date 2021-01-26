﻿Imports System.IO

Module Converter
    Public Sub Pack(files As List(Of String))
        FrmMain.pbConversion.Minimum = 0
        FrmMain.pbConversion.Maximum = files.Count + 1
        FrmMain.pbConversion.Value = 0
        If File.Exists(dropPath & "\-new.vpk") Then
            File.Delete(dropPath & "\-new.vpk")
        End If
        If File.Exists(dropPath & "\pak01_dir.vpk") Then
            File.Delete(dropPath & "\pak01_dir.vpk")
        End If
        If Directory.Exists(dropPath & "\-new") Then
            Directory.Delete(dropPath & "\-new", True)
        End If
        Directory.CreateDirectory(dropPath & "\-new")
        For Each file In files
            FrmMain.pbConversion.Value += 1
            VTFify(file)
        Next

        If (options.pack) Then
            Dim psi = New ProcessStartInfo With {
                .FileName = CurDir() & "\vpk",
                .WindowStyle = ProcessWindowStyle.Hidden,
                .Arguments = Escape(dropPath & "\-new")
            }
            Dim pro = Process.Start(psi)
            pro.WaitForExit()
            If Directory.Exists(dropPath & "\-new") Then
                Directory.Delete(dropPath & "\-new", True)
            End If
            File.Move(dropPath & "\-new.vpk", dropPath & "\pak01_dir.vpk")
        End If

        FrmMain.pbConversion.Value = 0
        FrmMain.lblFolder.Text = "Done!"
    End Sub



    Public Sub VTFify(path As String)
        Dim outdir = path.Substring(0, path.LastIndexOf("\"))
        If (outdir.Replace(dropPath, "").Contains("ignore")) Then
            Return
        End If
        outdir = GetDir(path)

        Directory.CreateDirectory(outdir)

        Select Case (path.Substring(path.LastIndexOf(".")))
            Case ".txt"

                'Animated textures are formatted like foobar000.tga foobar001.tga
                'And referenced using a text file like foobar.txt containing
                'startindex and endindex (eg 0 and 1)
                'see example.

                'we check if this is for an animated texture by seeing if an image with filename000 exists

                If ImageExists(path.Replace(".txt", "") & "000") Then
                    'it's for an animated texture, convert it to vtf using vtex
                    Dim convert = Not File.Exists(path.Substring(0, path.LastIndexOf(".")) & "000.tga")
                    If convert Then
                        For Each f In Directory.GetFiles(path.Substring(0, path.LastIndexOf("\")), path.Substring(0, path.LastIndexOf(".")).Substring(path.LastIndexOf("\") + 1) & "*", SearchOption.TopDirectoryOnly)
                            If Not f.Substring(f.LastIndexOf(".")).Equals(".txt") Then
                                Dim si = New ProcessStartInfo With {
                                    .FileName = CurDir() & "\ffmpeg",
                                    .WindowStyle = ProcessWindowStyle.Hidden,
                                    .Arguments = "-i " & Escape(f) & " " & Escape(f.Substring(0, f.LastIndexOf(".")) & ".tga")
                                }
                                Dim ro = Process.Start(si)
                                ro.WaitForExit()
                            End If
                        Next
                    End If 'vtex can only handle targa, we convert other formats using ffmpeg
                    Dim psi = New ProcessStartInfo With {
                        .FileName = CurDir() & "\vtex",
                        .WindowStyle = ProcessWindowStyle.Hidden,
                        .Arguments = "-quiet -game " & Escape(CurDir()) & " -outdir " & Escape(outdir) & " " & Escape(path)
                    } 'vtex
                    Dim pro = Process.Start(psi)
                    pro.WaitForExit()
                    If convert Then
                        'delete converted tgas
                        For Each f In Directory.GetFiles(path.Substring(0, path.LastIndexOf("\")), path.Substring(0, path.LastIndexOf(".")).Substring(path.LastIndexOf("\") + 1) & "*.tga", SearchOption.TopDirectoryOnly)
                            File.Delete(f)
                        Next
                    End If 'delete converted tgas after
                End If

            Case ".bmp", ".jpg", ".png", ".tga"

                'It's a texture to be converted to vtf

                'we check if it is part of an animated texture by checking if a txt named the same as texture without ### exists

                If (File.Exists(path.Substring(0, path.Length - 7) & ".txt")) Then
                    'it is part of an animated texture, do nothing
                Else
                    'it is not part of an animated texture, we're good to convert
                    Dim psi = New ProcessStartInfo With {
                        .FileName = CurDir() & "\vtfcmd",
                        .WindowStyle = ProcessWindowStyle.Hidden,
                        .Arguments = "-silent -file " & Escape(path) & " -output " & Escape(outdir)
                    } 'vtfcmd
                    Dim pro = Process.Start(psi)
                    pro.WaitForExit()
                End If
            Case Else
                'it's some other file, copy it over
                File.Copy(path, outdir & path.Substring(path.LastIndexOf("\")))
        End Select
    End Sub
    Public Function GetDir(path As String)
        Dim e = path.Substring(0, path.LastIndexOf("\"))
        If Not e.Equals(dropPath) Then
            If (options.fileMode = 0) Then
                Dim n = e.Replace(dropPath, "").Substring(0, e.Replace(dropPath & "\", "").IndexOf("\") + 1)
                e = e.Replace(dropPath & n, dropPath & "\-new")
            Else
                e = e.Replace(dropPath, dropPath & "\-new")
            End If
        End If
        Return e
    End Function

    Public Function ImageExists(path As String)
        Return File.Exists(path & ".bmp") Or
               File.Exists(path & ".jpg") Or
               File.Exists(path & ".png") Or
               File.Exists(path & ".tga")
    End Function

    Public Function Escape(path As String)
        Return """" & path & """"
    End Function
End Module


Class Settings
    Public fileMode As Integer
    Public pack As Boolean

    Public Sub New(fileMode As Integer, pack As Boolean)
        Me.fileMode = fileMode
        Me.pack = pack
    End Sub
End Class