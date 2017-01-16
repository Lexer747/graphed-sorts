<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 412)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 92)
        Me.Label1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(49, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(107, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Sort"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Chart1
        '
        ChartArea2.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend2)
        Me.Chart1.Location = New System.Drawing.Point(162, 0)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Size = New System.Drawing.Size(1000, 500)
        Me.Chart1.TabIndex = 2
        Me.Chart1.Text = "Chart1"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(49, 52)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(107, 21)
        Me.ComboBox1.TabIndex = 3
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(49, 79)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(107, 21)
        Me.ComboBox2.TabIndex = 4
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(50, 106)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(106, 20)
        Me.TextBox1.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label2.Location = New System.Drawing.Point(0, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 12)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Sort Type"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.3!)
        Me.Label3.Location = New System.Drawing.Point(-1, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 12)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Array Type"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label4.Location = New System.Drawing.Point(0, 110)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 12)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Array Size"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Location = New System.Drawing.Point(53, 184)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 9
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label5.Location = New System.Drawing.Point(0, 184)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 12)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Animation?"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(66, 201)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(0, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label7.Location = New System.Drawing.Point(0, 201)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 12)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Time Taken:"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(50, 132)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(106, 20)
        Me.TextBox2.TabIndex = 17
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(-1, 317)
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(156, 45)
        Me.TrackBar1.TabIndex = 19
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label11.Location = New System.Drawing.Point(0, 350)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(24, 12)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Fast"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label12.Location = New System.Drawing.Point(130, 350)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(26, 12)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "Slow"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label13.Location = New System.Drawing.Point(0, 226)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(75, 12)
        Me.Label13.TabIndex = 22
        Me.Label13.Text = "Array Accesses:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label14.Location = New System.Drawing.Point(0, 214)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(89, 12)
        Me.Label14.TabIndex = 23
        Me.Label14.Text = "Comparisons Made:"
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(88, 215)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 11)
        Me.Label15.TabIndex = 24
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(75, 227)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(83, 12)
        Me.Label16.TabIndex = 25
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Label8.Location = New System.Drawing.Point(39, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(115, 20)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "Double click to reset"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(51, 239)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(107, 11)
        Me.Label17.TabIndex = 28
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label18.Location = New System.Drawing.Point(-1, 238)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(52, 12)
        Me.Label18.TabIndex = 27
        Me.Label18.Text = "Overflows:"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label19.Location = New System.Drawing.Point(-1, 266)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(57, 12)
        Me.Label19.TabIndex = 29
        Me.Label19.Text = "Worst Case:"
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(56, 266)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(102, 16)
        Me.Label20.TabIndex = 30
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label21.Location = New System.Drawing.Point(-1, 254)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(58, 12)
        Me.Label21.TabIndex = 31
        Me.Label21.Text = "Performance"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label22.Location = New System.Drawing.Point(0, 278)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(51, 12)
        Me.Label22.TabIndex = 32
        Me.Label22.Text = "Best Case:"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label23.Location = New System.Drawing.Point(0, 291)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(68, 12)
        Me.Label23.TabIndex = 33
        Me.Label23.Text = "Average Case:"
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(50, 279)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(106, 12)
        Me.Label24.TabIndex = 34
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(67, 291)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(91, 16)
        Me.Label25.TabIndex = 35
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(50, 159)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(106, 20)
        Me.TextBox3.TabIndex = 36
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label9.Location = New System.Drawing.Point(23, 136)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(21, 12)
        Me.Label9.TabIndex = 37
        Me.Label9.Text = "Min"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.Label10.Location = New System.Drawing.Point(20, 163)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(24, 12)
        Me.Label10.TabIndex = 38
        Me.Label10.Text = "Max"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1184, 513)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
End Class
