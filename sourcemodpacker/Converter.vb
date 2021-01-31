Imports System.IO

Module Converter
    Public Sub Pack(path As String)
        If (dropType) Then
            Convert(path)
        Else
            Dim toPath = FrmMain.txtToPath.Text
            If File.Exists(toPath) Then
                File.Delete(toPath)
            End If
            If File.Exists(dropPath & "\-new.vpk") Then
                File.Delete(dropPath & "\-new.vpk")
            End If
            If Directory.Exists(dropPath & "\-new") Then
                Directory.Delete(dropPath & "\-new", True)
            End If
            Directory.CreateDirectory(dropPath & "\-new")
            Dim files = GetFiles(path)
            FrmMain.pbConversion.Minimum = 0
            FrmMain.pbConversion.Maximum = files.Count + 1
            FrmMain.pbConversion.Value = 0
            For Each file In files
                FrmMain.pbConversion.Value += 1
                Convert(file)
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
                Directory.CreateDirectory(toPath.Substring(0, toPath.LastIndexOf("\")))
                File.Move(dropPath & "\-new.vpk", toPath)
            End If
            FrmMain.pbConversion.Value = FrmMain.pbConversion.Maximum
        End If

    End Sub

    Public Sub Convert(path As String)
        Dim pathDir = path.Substring(0, path.LastIndexOf("\"))
        Dim fileExt = path.Substring(path.LastIndexOf("."))
        Dim fileName = path.Substring(pathDir.Length + 1, path.Length - (pathDir.Length + fileExt.Length + 1))
        Dim outdir As String
        If (dropType) Then
            outdir = pathDir
        Else
            outdir = GetDir(path)
            If (path.Replace(dropPath, "").Contains("ignore")) Then Return
        End If
        Directory.CreateDirectory(outdir)
        Select Case fileExt
            Case ".txt"
                'Animated textures are formatted like foobar000.tga foobar001.tga
                'And referenced using a txt file like foobar.txt containing startindex and endindex (eg 0 and 1)
                'we check if this is for an animated texture by seeing if an image with filename000 exists
                If ImageExists(path.Replace(".txt", "000")) Then
                    'vtex can only handle targa, we convert other formats using ffmpeg
                    Dim convert = Not File.Exists(path.Replace(".txt", "000.tga"))
                    If convert Then
                        'if that image exists but is not of type tga, convert all files matching filename wildcard to tga
                        For Each f In Directory.GetFiles(pathDir, fileName & "*", SearchOption.TopDirectoryOnly)
                            Dim fExt = f.Substring(0, f.LastIndexOf("."))
                            If Not fExt.Equals(".txt") Then
                                Dim si = New ProcessStartInfo With {
                                    .FileName = CurDir() & "\ffmpeg",
                                    .WindowStyle = ProcessWindowStyle.Hidden,
                                    .Arguments = "-i " & Escape(f) & " " & Escape(f.Replace(fExt, ".tga"))
                                } 'ffmpeg
                                Dim ro = Process.Start(si)
                                ro.WaitForExit()
                            End If
                        Next
                    End If
                    Dim psi = New ProcessStartInfo With {
                        .FileName = CurDir() & "\vtex",
                        .WindowStyle = ProcessWindowStyle.Hidden,
                        .Arguments = "-quiet -game " & Escape(CurDir()) & " -outdir " & Escape(outdir) & " " & Escape(path)
                    } 'vtex
                    Dim pro = Process.Start(psi)
                    pro.WaitForExit()
                    'delete converted tgas after
                    If convert Then
                        'delete converted tgas
                        For Each f In Directory.GetFiles(pathDir, fileName & "*.tga", SearchOption.TopDirectoryOnly)
                            File.Delete(f)
                        Next
                    End If
                End If
            Case ".bmp", ".jpg", ".png", ".tga"
                'It's a texture to be converted to vtf
                'we check if it is part of an animated texture by checking if a txt named the same as texture without ### exists
                If File.Exists(path.Substring(0, path.Length - 7) & ".txt") Then
                    'it's part of an animated texture
                    'if it was the only file dragged then convert the whole texture
                    'otherwise the txt will be converted later (or already has been)
                    If dropType Then
                        Convert(path.Substring(0, path.Length - 7) & ".txt")
                    End If
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
                'but only if it WASN'T the only file dragged
                If Not dropType Then
                    File.Copy(path, outdir & "\" & fileName & fileExt)
                End If
        End Select
    End Sub

    Public Function GetDir(path As String)
        Dim e = path.Substring(0, path.LastIndexOf("\"))
        If Not e.Equals(dropPath) Then




            If options.fileMode = 0 Then
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
    Public fileMode As Integer = 0
    Public pack As Boolean = True
End Class