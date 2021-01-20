Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32


Public Class ManagerForm
    'Variables
    Dim sqlConn As New MySqlConnection
    Dim sqlCmd As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlQuery As String

    Dim sqlDt As New DataTable
    Dim DtA As New MySqlDataAdapter

    Dim server As String = "localhost"

    Dim username As String = "root"
    Dim password As String = "Ethan2022et" 'password for Mysql server (change this to access a different database)
    Dim database As String = "rosteringapp"

    Private Sub UpdateDbTable()
        sqlConn.ConnectionString = "server =" + server + ";" + "user id=" + username + ";" _
            + "password =" + password + ";" + "database =" + database
        'connect to mysql database
        sqlConn.Open()
        sqlCmd.Connection = sqlConn
        sqlCmd.CommandText = "SELECT * FROM rosteringapp.managertbl"

        sqlRd = sqlCmd.ExecuteReader
        sqlDt.Load(sqlRd)
        Dim ds As DataSet
        ds = New DataSet
        DtA = New MySqlDataAdapter(sqlCmd.CommandText, sqlConn)
        DtA.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
        sqlRd.Close()
        sqlConn.Close()

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        sqlConn.ConnectionString = "server =" + server + ";" + "user id=" + username + ";" _
            + "password =" + password + ";" + "database =" + database
        Try
            sqlConn.Open()
            Dim query As String
            query = "insert into rosteringapp.managertbl (ManagerID, surname, Password)
                    values('" & txtManagerID.Text & "','" & txtSurname.Text & "','" & txtPassword.Text & "')"
            sqlCmd = New MySqlCommand(query, sqlConn)
            sqlRd = sqlCmd.ExecuteReader
            MsgBox("Manager Successfully added")
            sqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Rostering database", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            'if error occurs
            sqlConn.Dispose()
        End Try
        'call sub method updateDbTable
        UpdateDbTable()
    End Sub

    Private Sub ManagerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateDbTable()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        sqlConn.ConnectionString = "server =" + server + ";" + "user id=" + username + ";" _
           + "password =" + password + ";" + "database =" + database

        If txtManagerID.Text = "" Or txtSurname.Text = "" Or txtPassword.Text = "" Then
        Else
            Try
                sqlConn.Open()
                sqlCmd = New MySqlCommand("update rosteringapp.managertbl set ManagerID ='" & txtManagerID.Text & "', Surname ='" & txtSurname.Text & "',  
                Password ='" & txtPassword.Text & "' where ManagerID =" & txtManagerID.Text & "", sqlConn) 'note that two arguments are passed to MySqlCommand
                sqlCmd.ExecuteNonQuery()
                MsgBox("Manager details successfully updated")
                sqlConn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            UpdateDbTable()
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        'delete by specifying details using ManagerID
        sqlConn.ConnectionString = "server =" + server + ";" + "user id=" + username + ";" _
            + "password =" + password + ";" + "database =" + database

        If txtManagerID.Text.Trim = "" Then
            MsgBox("Please enter valid Manager ID")
        Else
            sqlConn.Open()
            Dim sqlQuery As String
            sqlQuery = "delete from rosteringapp.managertbl where ManagerID = " & txtManagerID.Text & ""
            sqlCmd = New MySqlCommand(sqlQuery, sqlConn)
            sqlCmd.ExecuteNonQuery()
            MsgBox("Operation successful")
            sqlConn.Close() 'close the db once operation is complete

            UpdateDbTable() 'call update method to update db table

        End If
    End Sub

    Private Sub validateInputs(sender As Object, e As KeyPressEventArgs) Handles txtManagerID.KeyPress
        ' Accept only numbers  for Manager ID

        If (e.KeyChar < "0" OrElse e.KeyChar > "9") AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If

    End Sub

    Private Sub validateInput(sender As Object, e As KeyPressEventArgs) Handles txtSurname.KeyPress
        ' Do not Accept numbers  for Manager surname

        If (e.KeyChar < "0" OrElse e.KeyChar > "9") Then
            e.Handled = False
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If

    End Sub

    Private Sub btnMngWSpace_Click(sender As Object, e As EventArgs) Handles btnMngWSpace.Click
        'access manager workspace form
        Dim managerworksp As New ManagerWorkspace
        managerworksp.Show()
        Me.Hide()
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        'logout and access manager's login form
        Dim login As New Login
        login.txtUsername.Text = ""
        login.txtPassword.Text = ""

        login.Show()
        Me.Hide()
    End Sub
End Class