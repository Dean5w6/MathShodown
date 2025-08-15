Public Class Form2
    Public difficulty As Integer = 0
    Public cSelect As Integer = 0
    Private Sub Button5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        PictureBox1.Image = My.Resources.rimururu
        cSelect = 1
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        difficulty = 1
        If cSelect = 1 Then
            Form3.difficulty2 = difficulty
            Me.Hide()
            Form3.Show()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        difficulty = 2
        If cSelect = 1 Then
            Form3.difficulty2 = difficulty
            Me.Hide()
            Form3.Show()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        difficulty = 3
        If cSelect = 1 Then
            Form3.difficulty2 = difficulty
            Me.Hide()
            Form3.Show()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        difficulty = 4
        If cSelect = 1 Then
            Form3.difficulty2 = difficulty
            Me.Hide()
            Form3.Show()
        End If
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class