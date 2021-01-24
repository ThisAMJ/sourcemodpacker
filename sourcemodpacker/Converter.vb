Imports System.IO

Module Converter
    Public Sub Pack(files As List(Of String))
        FrmMain.pbConversion.Minimum = 0
        FrmMain.pbConversion.Maximum = files.Count
        FrmMain.pbConversion.Value = 0
        For Each file In files
            VTFify(file)
            FrmMain.pbConversion.Value += 1
        Next
    End Sub



    Public Sub VTFify(path As String)
        Dim outdir = path.Substring(0, path.LastIndexOf("\"))
        Select Case (path.Substring(path.LastIndexOf(".")))
            Case ".txt"
                'Animated textures are formatted like foobar000.tga foobar001.tga
                'And referenced using a text file like foobar.txt containing
                'startindex and endindex (eg 0 and 1)

                'we check if this is for an animated texture
                If (ImageExists(path.Substring(0, path.LastIndexOf(".")) & "000")) Then
                    'it is for an animated texture, convert it to vtf using vtex

                    'todo: vtex can only handle targa, convert other formats using ffmpeg

                    Dim psi = New ProcessStartInfo With {
                        .FileName = CurDir() & "\vtex",
                        .WindowStyle = ProcessWindowStyle.Hidden,
                        .Arguments = "-quiet -game " & Escape(CurDir()) & " -outdir " & Escape(outdir) & " " & Escape(path)
                    }
                    Process.Start(psi)
                Else
                    'it's some other txt
                End If
            Case ".bmp", ".jpg", ".png", ".tga"
                'It's a texture to be converted to vtf

                'we check if it is part of an animated texture by checking if a txt named the same as texture without ### exists
                If (File.Exists(path.Substring(0, path.Length - 7) & ".txt")) Then
                    'it is part of an animated texture
                    'don't do anything
                Else
                    'it is not part of an animated texture
                    'convert it to vtf using vtfcmd
                    Dim psi = New ProcessStartInfo With {
                        .FileName = CurDir() & "\vtfcmd",
                        .WindowStyle = ProcessWindowStyle.Hidden,
                        .Arguments = "-silent -file " & Escape(path) & " -output " & Escape(outdir)
                    }
                    Process.Start(psi)
                End If
        End Select
    End Sub

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
    Dim animated
    Public Sub New()
    End Sub
End Class