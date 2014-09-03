<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Calendar
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Calendar))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ButtonNext = New System.Windows.Forms.Button()
        Me.ComboBox_year = New System.Windows.Forms.ComboBox()
        Me.ComboBox_month = New System.Windows.Forms.ComboBox()
        Me.ButtonPre = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonToday = New System.Windows.Forms.Button()
        Me.Su = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tu = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.We = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Th = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sa = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Su, Me.Mo, Me.Tu, Me.We, Me.Th, Me.Fr, Me.Sa})
        Me.DataGridView1.EnableHeadersVisualStyles = False
        Me.DataGridView1.Location = New System.Drawing.Point(3, 31)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView1.Size = New System.Drawing.Size(351, 325)
        Me.DataGridView1.TabIndex = 0
        '
        'ButtonNext
        '
        Me.ButtonNext.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.ButtonNext.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.ButtonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonNext.Location = New System.Drawing.Point(308, 5)
        Me.ButtonNext.Name = "ButtonNext"
        Me.ButtonNext.Size = New System.Drawing.Size(45, 22)
        Me.ButtonNext.TabIndex = 7
        Me.ButtonNext.Text = ">>"
        Me.ButtonNext.UseVisualStyleBackColor = False
        '
        'ComboBox_year
        '
        Me.ComboBox_year.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox_year.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ComboBox_year.FormattingEnabled = True
        Me.ComboBox_year.Location = New System.Drawing.Point(80, 6)
        Me.ComboBox_year.MaxLength = 4
        Me.ComboBox_year.Name = "ComboBox_year"
        Me.ComboBox_year.Size = New System.Drawing.Size(51, 21)
        Me.ComboBox_year.TabIndex = 2
        '
        'ComboBox_month
        '
        Me.ComboBox_month.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox_month.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ComboBox_month.FormattingEnabled = True
        Me.ComboBox_month.Location = New System.Drawing.Point(158, 6)
        Me.ComboBox_month.MaxLength = 2
        Me.ComboBox_month.Name = "ComboBox_month"
        Me.ComboBox_month.Size = New System.Drawing.Size(40, 21)
        Me.ComboBox_month.TabIndex = 4
        '
        'ButtonPre
        '
        Me.ButtonPre.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.ButtonPre.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.ButtonPre.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonPre.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonPre.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonPre.Location = New System.Drawing.Point(4, 5)
        Me.ButtonPre.Name = "ButtonPre"
        Me.ButtonPre.Size = New System.Drawing.Size(45, 22)
        Me.ButtonPre.TabIndex = 1
        Me.ButtonPre.Text = "<<"
        Me.ButtonPre.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(135, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 12)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "年"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(201, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 12)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "月"
        '
        'ButtonToday
        '
        Me.ButtonToday.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ButtonToday.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.ButtonToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonToday.Location = New System.Drawing.Point(224, 5)
        Me.ButtonToday.Name = "ButtonToday"
        Me.ButtonToday.Size = New System.Drawing.Size(50, 22)
        Me.ButtonToday.TabIndex = 6
        Me.ButtonToday.Text = "今日"
        Me.ButtonToday.UseVisualStyleBackColor = False
        '
        'Su
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(242, Byte), Integer))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Red
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Red
        Me.Su.DefaultCellStyle = DataGridViewCellStyle2
        Me.Su.FillWeight = 50.0!
        Me.Su.Frozen = True
        Me.Su.HeaderText = "日"
        Me.Su.Name = "Su"
        Me.Su.ReadOnly = True
        Me.Su.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Su.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Su.Width = 50
        '
        'Mo
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Mo.DefaultCellStyle = DataGridViewCellStyle3
        Me.Mo.FillWeight = 50.0!
        Me.Mo.Frozen = True
        Me.Mo.HeaderText = "月"
        Me.Mo.Name = "Mo"
        Me.Mo.ReadOnly = True
        Me.Mo.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Mo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Mo.Width = 50
        '
        'Tu
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Tu.DefaultCellStyle = DataGridViewCellStyle4
        Me.Tu.FillWeight = 50.0!
        Me.Tu.Frozen = True
        Me.Tu.HeaderText = "火"
        Me.Tu.Name = "Tu"
        Me.Tu.ReadOnly = True
        Me.Tu.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Tu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tu.Width = 50
        '
        'We
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.We.DefaultCellStyle = DataGridViewCellStyle5
        Me.We.FillWeight = 50.0!
        Me.We.Frozen = True
        Me.We.HeaderText = "水"
        Me.We.Name = "We"
        Me.We.ReadOnly = True
        Me.We.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.We.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.We.Width = 50
        '
        'Th
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Th.DefaultCellStyle = DataGridViewCellStyle6
        Me.Th.FillWeight = 50.0!
        Me.Th.Frozen = True
        Me.Th.HeaderText = "木"
        Me.Th.Name = "Th"
        Me.Th.ReadOnly = True
        Me.Th.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Th.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Th.Width = 50
        '
        'Fr
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Fr.DefaultCellStyle = DataGridViewCellStyle7
        Me.Fr.FillWeight = 50.0!
        Me.Fr.Frozen = True
        Me.Fr.HeaderText = "金"
        Me.Fr.Name = "Fr"
        Me.Fr.ReadOnly = True
        Me.Fr.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Fr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Fr.Width = 50
        '
        'Sa
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(187, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(230, Byte), Integer))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Blue
        Me.Sa.DefaultCellStyle = DataGridViewCellStyle8
        Me.Sa.FillWeight = 50.0!
        Me.Sa.Frozen = True
        Me.Sa.HeaderText = "土"
        Me.Sa.Name = "Sa"
        Me.Sa.ReadOnly = True
        Me.Sa.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Sa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Sa.Width = 50
        '
        'Calendar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(357, 357)
        Me.Controls.Add(Me.ButtonToday)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox_month)
        Me.Controls.Add(Me.ComboBox_year)
        Me.Controls.Add(Me.ButtonNext)
        Me.Controls.Add(Me.ButtonPre)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Calendar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Calendar"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonPre As System.Windows.Forms.Button
    Friend WithEvents ButtonNext As System.Windows.Forms.Button
    Friend WithEvents ComboBox_year As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox_month As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonToday As System.Windows.Forms.Button
    Friend WithEvents Su As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Mo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tu As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents We As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Th As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sa As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
