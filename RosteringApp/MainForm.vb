Public Class frmMain
    Private Sub mnuFileExit_Click_1(sender As Object, e As EventArgs) Handles mnuFileExit.Click
        'exit the main form
        Me.Close()
    End Sub

    Private Sub mnuManager_Click(sender As Object, e As EventArgs) Handles mnuManager.Click
        'access manager's login form
        Dim logManager As New Login
        logManager.Show()
        Me.Hide()
    End Sub

    Private Sub mnuEmployee_Click(sender As Object, e As EventArgs) Handles mnuEmployee.Click
        'Access employee login form
        Dim logEmployee As New LoginEmployee
        logEmployee.Show()
        Me.Hide()
    End Sub
End Class
