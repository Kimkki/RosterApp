Imports MySql.Data.MySqlClient
Public Class LoginEmployee
    Dim sqlCmd As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlDt As New DataTable
    Dim dTa As New MySqlDataAdapter
    Dim sqlConn As New MySqlConnection


    Dim server As String = "localhost"
    Dim username As String = "root"
    Dim password As String = "Ethan2022et" 'specify your password for Mysql server
    Dim database As String = "rosteringapp"

    Private Sub LogEmployee()
        sqlConn.ConnectionString = "server =" + server + ";" + "user id=" + username + ";" _
            + "password =" + password + ";" + "database =" + database

        If txtUsername.Text = "" Then
            MsgBox("Enter your first name")
        ElseIf txtPassword.Text = "" Then
            MsgBox("Enter password, field cannot be empty")
        Else

            'connect to mysql database
            sqlConn.Open()
            sqlCmd.Connection = sqlConn
            sqlCmd.CommandText = "SELECT * FROM rosteringapp.employeetbl where FirstName ='" & txtUsername.Text & "' and Password='" & txtPassword.Text & "'"
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
                Dim schedule_form As New Schedule
                schedule_form.Show()
                Me.Hide()
            End If
            sqlRd.Close()
            sqlConn.Close()
        End If
        sqlRd.Close()
        sqlConn.Close()
    End Sub

    Private Sub LoginEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        LogEmployee()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtUsername.Text = String.Empty
        txtPassword.Text = String.Empty

    End Sub
End Class