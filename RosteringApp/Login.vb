Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32

Public Class Login

    Dim sqlCmd As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlDt As New DataTable
    Dim dTa As New MySqlDataAdapter
    Dim sqlConn As New MySqlConnection


    Dim server As String = "localhost"
    Dim username As String = "root"
    Dim password As String = "Ethan2022et" 'specify your password for Mysql server
    Dim database As String = "rosteringapp"



    Private Sub LogManager()
        sqlConn.ConnectionString = "server =" + server + ";" + "user id=" + username + ";" _
            + "password =" + password + ";" + "database =" + database

        If txtUsername.Text = "" Then
            MsgBox("Enter manager username")
        ElseIf txtPassword.Text = "" Then
            MsgBox("Enter password, field cannot be empty")
        Else

            'connect to mysql database
            sqlConn.Open()
            sqlCmd.Connection = sqlConn
            sqlCmd.CommandText = "SELECT * FROM rosteringapp.managertbl where Surname ='" & txtUsername.Text & "' and Password='" & txtPassword.Text & "'"
            sqlRd = sqlCmd.ExecuteReader
            sqlDt.Load(sqlRd)
            Dim ds As DataSet
            ds = New DataSet
            dTa = New MySqlDataAdapter(sqlCmd.CommandText, sqlConn)
            dTa.Fill(ds)

            Dim i As Integer
            i = ds.Tables(0).Rows.Count
            If i = 0 Then
                MsgBox("Wrong username or password")
            Else
                Dim manager_form As New ManagerForm
                manager_form.Show()
                Me.Hide()
            End If
            sqlRd.Close()
            sqlConn.Close()
        End If
        sqlRd.Close()
        sqlConn.Close()
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub



    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Application.Exit()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        LogManager()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtUsername.Text = String.Empty
        txtPassword.Text = String.Empty
    End Sub
End Class