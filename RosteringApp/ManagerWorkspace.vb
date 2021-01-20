Imports MySql.Data.MySqlClient


Public Class ManagerWorkspace
    'Variables
    Dim sqlConn As New MySqlConnection
    Dim sqlCmd As New MySqlCommand
    Dim sqlRd As MySqlDataReader
    Dim sqlQuery As String

    Dim sqlDt As New DataTable
    Dim DtA As New MySqlDataAdapter

    Dim server As String = "localhost"

    Dim username As String = "root"
    Dim password As String = "Ethan2022et" 'password for Mysql server
    Dim database As String = "rosteringapp"



    Private Sub txtExit_Click(sender As Object, e As EventArgs) Handles txtExit.Click
        Me.Close()
    End Sub

    Private Sub UpdateDbTable()
        sqlConn.ConnectionString = "server =" + server + ";" + "user id=" + username + ";" _
            + "password =" + password + ";" + "database =" + database
        'connect to mysql database
        sqlConn.Open()
        sqlCmd.Connection = sqlConn
        sqlCmd.CommandText = "SELECT * FROM rosteringapp.employeetbl"

        sqlRd = sqlCmd.ExecuteReader
        sqlDt.Load(sqlRd)
        Dim ds As DataSet
        ds = New DataSet
        DtA = New MySqlDataAdapter(sqlCmd.CommandText, sqlConn)
        DtA.Fill(ds)
        dgvEmployee.DataSource = ds.Tables(0)
        sqlRd.Close()
        sqlConn.Close()

    End Sub

    Private Sub ManagerWorkspace_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateDbTable()
    End Sub



    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        sqlConn.ConnectionString = "server =" + server + ";" + "user id=" + username + ";" _
            + "password =" + password + ";" + "database =" + database
        Try
            sqlConn.Open()
            Dim query As String
            query = "insert into rosteringapp.employeetbl (EmployeeID, FirstName, Password, Address, Telephone)
                    values('" & txtEmpID.Text & "','" & txtFirstName.Text & "','" & txtPassword.Text & "','" & txtAddress.Text & "','" & txtTelephone.Text & "')"
            sqlCmd = New MySqlCommand(query, sqlConn)
            sqlRd = sqlCmd.ExecuteReader
            MsgBox("Employee Successfully added")
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

    Private Sub txtDelete_Click(sender As Object, e As EventArgs) Handles txtDelete.Click
        sqlConn.ConnectionString = "server =" + server + ";" + "user id=" + username + ";" _
            + "password =" + password + ";" + "database =" + database
        'delete by selecting employee's ID
        If txtEmpID.Text = "" Then
            MsgBox("Please enter valid employee id")
        Else
            sqlConn.Open()
            Dim sqlQuery As String
            sqlQuery = "delete from rosteringapp.employeetbl where EmployeeID = " & txtEmpID.Text & ""
            sqlCmd = New MySqlCommand(sqlQuery, sqlConn)
            sqlCmd.ExecuteNonQuery()
            MsgBox("Operation successful")
            sqlConn.Close() 'close the db once operation is complete
            UpdateDbTable() 'call update method to update db table

        End If
    End Sub

    Private Sub txtUpdate_Click(sender As Object, e As EventArgs) Handles txtUpdate.Click
        sqlConn.ConnectionString = "server =" + server + ";" + "user id=" + username + ";" _
            + "password =" + password + ";" + "database =" + database

        If txtEmpID.Text = "" Or txtFirstName.Text = "" Or txtPassword.Text = "" Or txtAddress.Text = "" Or txtTelephone.Text = "" Then
        Else
            Try
                sqlConn.Open()
                sqlCmd = New MySqlCommand("update rosteringapp.employeetbl set EmployeeID ='" & txtEmpID.Text & "', FirstName ='" & txtFirstName.Text & "',  
                Password ='" & txtPassword.Text & "',  Address ='" & txtAddress.Text & "',
                Telephone ='" & txtTelephone.Text & "' where EmployeeID =" & txtEmpID.Text & "", sqlConn) 'note that two arguments are passed to MySqlCommand
                sqlCmd.ExecuteNonQuery()
                MsgBox("Employee details successfully updated")
                sqlConn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            UpdateDbTable()
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            For Each txt In Panel4.Controls
                If TypeOf txt Is TextBox Then
                    txt.text = ""
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub validateInputs(sender As Object, e As KeyPressEventArgs) Handles txtEmpID.KeyPress
        ' Accept only numbers  for employee ID

        If (e.KeyChar < "0" OrElse e.KeyChar > "9") AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If

    End Sub

    Private Sub validateInput(sender As Object, e As KeyPressEventArgs) Handles txtFirstName.KeyPress
        ' Do not Accept numbers for employee first name

        If (e.KeyChar < "0" OrElse e.KeyChar > "9") Then
            e.Handled = False
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If

    End Sub

    Private Sub telephoneValidateInputs(sender As Object, e As KeyPressEventArgs) Handles txtTelephone.KeyPress
        ' Accept only numbers  for Telephone number

        If (e.KeyChar < "0" OrElse e.KeyChar > "9") AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If

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