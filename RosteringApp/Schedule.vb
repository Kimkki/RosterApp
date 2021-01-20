Public Class Schedule
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        'logout after employee is done viewing the schedule

        Dim logEmp As New LoginEmployee
        logEmp.txtUsername.Text = ""
        logEmp.txtPassword.Text = ""
        logEmp.Show()
        Me.Hide()

    End Sub
End Class