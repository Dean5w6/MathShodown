Public Class Form3
    Private WithEvents endTimer As New Timer()
    Private WithEvents flickerTimer As New Timer()
    Private WithEvents answerTimer As New Timer()
    Private WithEvents breakTimer As New Timer()
    Private WithEvents projectileTimer As New Timer()
    Private WithEvents projectileDelayTimer As New Timer()
    Private WithEvents kyoshiroAttackTimer As New Timer()
    Private WithEvents kyoshiroReturnTimer As New Timer()
    Private WithEvents kyoshiroWaitTimer As New Timer()
    Private rand As New Random()
    Public fightCounter As Integer = 0
    Private operand1 As Integer
    Private operand2 As Integer
    Private correctAnswer As Integer
    Private userCounter As Integer = 0
    Private enemyCounter As Integer = 0
    Private projectileHitState As Boolean = False
    Private kyoshiroTargetX As Integer
    Private kyoshiroMoveSpeed As Integer
    Private kyoshiroStartX As Integer
    Private WithEvents rimururuIdleTimer As New Timer()
    Private WithEvents kyoshiroAttackDelayTimer As New Timer()
    Private WithEvents rimururuDeathTimer As New Timer()
    Private WithEvents kyoshiroHitTimer As New Timer()
    Private rimururuLife As Integer = 5
    Private kyoshiroLife As Integer = 5
    Private WithEvents rimururuHitDelayTimer As New Timer()
    Private WithEvents koTimer As New Timer()
    Public difficulty2 As Integer

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If difficulty2 = 2 Then
            Me.BackgroundImage = My.Resources.bg2
        ElseIf difficulty2 = 3 Then
            Me.BackgroundImage = My.Resources.bg3
        ElseIf difficulty2 = 4 Then
            Me.BackgroundImage = My.Resources.bg4
        End If


        endTimer.Interval = 5520
        endTimer.Start()

        Label1.Visible = False
        TextBox1.Visible = False
        Button1.Visible = False
        PictureBox2.Visible = False
        PictureBox4.Visible = False
        PictureBox16.Visible = False
    End Sub

    Private Sub endTimer_Tick(sender As Object, e As EventArgs) Handles endTimer.Tick
        endTimer.Stop()

        PictureBox1.Left -= 10
        PictureBox1.Image = My.Resources.rimururuIdle

        PictureBox2.Visible = True

        flickerTimer.Interval = 5000
        flickerTimer.Start()
    End Sub

    Private Sub flickerTimer_Tick(sender As Object, e As EventArgs) Handles flickerTimer.Tick
        If rimururuLife > 0 And kyoshiroLife > 0 Then
            flickerTimer.Stop()

            If fightCounter = 0 Then
                PictureBox2.Visible = False
            End If

            TextBox1.Text = ""

            Label1.Visible = False
            TextBox1.Visible = False
            Button1.Visible = False

            If difficulty2 = 1 Then
                operand1 = rand.Next(10, 21)
                operand2 = rand.Next(10, 21)
                correctAnswer = operand1 + operand2
                Label1.Text = $"{operand1} + {operand2} = ?"
            ElseIf difficulty2 = 2 Then
                operand1 = rand.Next(30, 41)
                operand2 = rand.Next(15, 26)
                correctAnswer = operand1 - operand2
                Label1.Text = $"{operand1} - {operand2} = ?"
            ElseIf difficulty2 = 3 Then
                operand1 = rand.Next(8, 16)
                operand2 = rand.Next(8, 16)
                correctAnswer = operand1 * operand2
                Label1.Text = $"{operand1} * {operand2} = ?"
            ElseIf difficulty2 = 4 Then
                operand1 = rand.Next(10, 20)
                operand2 = rand.Next(10, 21)
                correctAnswer = operand1 * operand2
                Label1.Text = $"{operand1} * {operand2} = ?"
            End If
            Label1.Visible = True
            Label1.Location = New Point(370, 141)
            TextBox1.Visible = True
            Button1.Visible = True

            answerTimer.Interval = 5000
            answerTimer.Start()

            fightCounter = 1
            End If
    End Sub

    Private Sub answerTimer_Tick(sender As Object, e As EventArgs) Handles answerTimer.Tick
        answerTimer.Stop()

        Label1.Visible = False
        TextBox1.Visible = False
        Button1.Visible = False

        If userCounter = 0 Then
            enemyCounter += 1
        End If

        CheckAttackOutcome()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        answerTimer.Stop()

        Label1.Visible = False
        TextBox1.Visible = False
        Button1.Visible = False

        Dim userInput As Integer
        If Integer.TryParse(TextBox1.Text, userInput) AndAlso userInput = correctAnswer Then
            userCounter += 1
        Else
            enemyCounter += 1
        End If

        CheckAttackOutcome()
    End Sub

    Private Sub CheckAttackOutcome()
        If userCounter > enemyCounter Then
            PictureBox1.Image = My.Resources.rimururuAttack
            projectileDelayTimer.Interval = 1700
            projectileDelayTimer.Start()
        ElseIf enemyCounter > userCounter Then
            ResetKyoshiro()
            KyoshiroAttackSequence()

            rimururuHitDelayTimer.Interval = 900
            rimururuHitDelayTimer.Start()
        End If

        userCounter = 0
        enemyCounter = 0
        breakTimer.Interval = 2500
        breakTimer.Start()
    End Sub

    Private Sub rimururuHitDelayTimer_Tick(sender As Object, e As EventArgs) Handles rimururuHitDelayTimer.Tick
        rimururuHitDelayTimer.Stop()
        rimururuLife -= 1
        If rimururuLife > 0 Then
            PictureBox1.Image = My.Resources.rimururuHit
            Controls("PictureBox" & (6 + rimururuLife)).Visible = False
            rimururuIdleTimer.Interval = 480
            rimururuIdleTimer.Start()

        ElseIf rimururuLife = 0 Then
            PictureBox1.Image = My.Resources.rimururuDeath
            PictureBox6.Visible = False
            koTimer.Interval = 500
            koTimer.Start()
            rimururuDeathTimer.Interval = 5800
            rimururuDeathTimer.Start()
        End If
    End Sub

    Private Sub ResetKyoshiro()
        PictureBox3.Parent = Me
        PictureBox3.Left = 530
        PictureBox3.Top = 260
        PictureBox3.Image = My.Resources.kyoshiroIdle2
        PictureBox3.Visible = True

        PictureBox5.Left = PictureBox3.Left
        PictureBox5.Top = 259
        PictureBox5.Image = My.Resources.kyoshiroIdle2
        PictureBox5.Visible = False
    End Sub

    Private Sub KyoshiroAttackSequence()
        Dim kyoshiroScreenPos As Point = PictureBox3.PointToScreen(Point.Empty)

        PictureBox5.Left = PictureBox3.Left
        PictureBox5.Top = 259
        PictureBox5.Visible = True
        PictureBox5.Image = My.Resources.kyoshiroAttack1

        PictureBox3.Parent = PictureBox1
        PictureBox3.Location = PictureBox1.PointToClient(kyoshiroScreenPos)
        PictureBox3.Image = My.Resources.kyoshiroAttack1

        kyoshiroStartX = PictureBox3.Left
        kyoshiroTargetX = PictureBox1.Left + 270
        kyoshiroMoveSpeed = (kyoshiroStartX - kyoshiroTargetX) \ 8

        kyoshiroAttackTimer.Interval = 50
        kyoshiroAttackTimer.Start()
    End Sub

    Private Sub projectileDelayTimer_Tick(sender As Object, e As EventArgs) Handles projectileDelayTimer.Tick
        projectileDelayTimer.Stop()

        PictureBox4.Parent = Me
        PictureBox4.Left = 181
        PictureBox4.Top = 368
        PictureBox4.Visible = True

        projectileTimer.Interval = 50
        projectileTimer.Start()
    End Sub

    Private Sub projectileTimer_Tick(sender As Object, e As EventArgs) Handles projectileTimer.Tick
        If projectileHitState = False Then
            PictureBox4.Left += 50

            If PictureBox4.Bounds.IntersectsWith(PictureBox5.Bounds) Then
                PictureBox4.Left -= 30
                projectileHitState = True

                If kyoshiroLife > 0 Then
                    Controls("PictureBox" & (10 + kyoshiroLife)).Visible = False
                    kyoshiroLife -= 1
                End If

                PictureBox5.Image = My.Resources.kyoshiroHit2
                kyoshiroHitTimer.Interval = 720
                kyoshiroHitTimer.Start()
            End If

            If PictureBox4.Left > Me.Width Then
                projectileTimer.Stop()
                PictureBox4.Visible = False
            End If
        Else
            projectileHitState = False
            PictureBox4.Parent = PictureBox5
            PictureBox4.Left = PictureBox5.Width \ 2 - PictureBox4.Width \ 2
            PictureBox4.Top = PictureBox5.Height \ 2 - PictureBox4.Height \ 2
            projectileTimer.Stop()
        End If
    End Sub

    Private Sub kyoshiroHitTimer_Tick(sender As Object, e As EventArgs) Handles kyoshiroHitTimer.Tick
        kyoshiroHitTimer.Stop()

        If kyoshiroLife > 0 Then
            PictureBox5.Image = My.Resources.kyoshiroIdle2
        ElseIf kyoshiroLife = 0 Then
            PictureBox5.Image = My.Resources.kyoshiroDeath2

            koTimer.Interval = 500
            koTimer.Start()
            rimururuDeathTimer.Interval = 5800
            rimururuDeathTimer.Start()
        End If
    End Sub

    Private Sub koTimer_Tick(sender As Object, e As EventArgs) Handles koTimer.Tick
        PictureBox16.Visible = Not PictureBox16.Visible
    End Sub

    Private Sub kyoshiroAttackTimer_Tick(sender As Object, e As EventArgs) Handles kyoshiroAttackTimer.Tick
        If Math.Abs(PictureBox3.Left - kyoshiroTargetX) > kyoshiroMoveSpeed Then
            PictureBox3.Left -= kyoshiroMoveSpeed
            PictureBox5.Left -= kyoshiroMoveSpeed

            If PictureBox5.Left <= 163 Then
                PictureBox5.Visible = False
            End If
        Else
            PictureBox3.Left = kyoshiroTargetX
            kyoshiroAttackTimer.Stop()
            kyoshiroWaitTimer.Interval = 1080
            kyoshiroWaitTimer.Start()
        End If
    End Sub

    Private Sub rimururuIdleTimer_Tick(sender As Object, e As EventArgs) Handles rimururuIdleTimer.Tick
        rimururuIdleTimer.Stop()
        PictureBox1.Image = My.Resources.rimururuIdle
    End Sub

    Private Sub kyoshiroWaitTimer_Tick(sender As Object, e As EventArgs) Handles kyoshiroWaitTimer.Tick
        kyoshiroWaitTimer.Stop()

        PictureBox5.Image = My.Resources.kyoshiroIdle2
        kyoshiroReturnTimer.Interval = 50
        kyoshiroReturnTimer.Start()
    End Sub

    Private Sub kyoshiroReturnTimer_Tick(sender As Object, e As EventArgs) Handles kyoshiroReturnTimer.Tick
        If Math.Abs(PictureBox3.Left - kyoshiroStartX) > kyoshiroMoveSpeed Then
            PictureBox3.Left += kyoshiroMoveSpeed
            PictureBox5.Left += kyoshiroMoveSpeed

            If PictureBox5.Left > 163 Then
                PictureBox5.Visible = True
            End If
        Else
            PictureBox3.Left = kyoshiroStartX - 60
            PictureBox5.Left = kyoshiroStartX - 60
            kyoshiroReturnTimer.Stop()

            PictureBox3.Image = My.Resources.kyoshiroIdle2
        End If
    End Sub

    Private Sub breakTimer_Tick(sender As Object, e As EventArgs) Handles breakTimer.Tick
        breakTimer.Stop()

        If rimururuLife > 0 Then
            PictureBox1.Image = My.Resources.rimururuIdle
        End If
        PictureBox3.Image = My.Resources.kyoshiroIdle2
        PictureBox4.Visible = False

        flickerTimer.Start()
    End Sub

    Private Sub rimururuDeathTimer_Tick(sender As Object, e As EventArgs) Handles rimururuDeathTimer.Tick
        rimururuDeathTimer.Stop()

        flickerTimer.Stop()
        answerTimer.Stop()
        breakTimer.Stop()
        projectileTimer.Stop()
        projectileDelayTimer.Stop()
        kyoshiroAttackTimer.Stop()
        kyoshiroReturnTimer.Stop()
        kyoshiroWaitTimer.Stop()
        rimururuIdleTimer.Stop()
        kyoshiroAttackDelayTimer.Stop()
        rimururuDeathTimer.Stop()
        kyoshiroHitTimer.Stop()
        rimururuHitDelayTimer.Stop()
        koTimer.Stop()

        Dim nextForm As New Form4()

        If rimururuLife <= 0 Then
            nextForm.BackgroundImage = My.Resources.kyoshiroWin
        ElseIf kyoshiroLife <= 0 Then
            nextForm.BackgroundImage = My.Resources.rimururuWin
        End If

        Me.Hide()
        nextForm.Show()
    End Sub


    Private Sub PictureBox16_Click(sender As Object, e As EventArgs) Handles PictureBox16.Click

    End Sub
End Class