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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Calendar))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Su = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Mo = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Tu = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.We = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Th = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Fr = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Sa = New System.Windows.Forms.DataGridViewButtonColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Su, Me.Mo, Me.Tu, Me.We, Me.Th, Me.Fr, Me.Sa})
        Me.DataGridView1.EnableHeadersVisualStyles = False
        Me.DataGridView1.Location = New System.Drawing.Point(1, 1)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView1.Size = New System.Drawing.Size(353, 325)
        Me.DataGridView1.TabIndex = 0
        '
        'Su
        '
        Me.Su.FillWeight = 50.0!
        Me.Su.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Su.Frozen = True
        Me.Su.HeaderText = "日"
        Me.Su.Name = "Su"
        Me.Su.ReadOnly = True
        Me.Su.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Su.Width = 50
        '
        'Mo
        '
        Me.Mo.FillWeight = 50.0!
        Me.Mo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Mo.Frozen = True
        Me.Mo.HeaderText = "月"
        Me.Mo.Name = "Mo"
        Me.Mo.ReadOnly = True
        Me.Mo.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Mo.Width = 50
        '
        'Tu
        '
        Me.Tu.FillWeight = 50.0!
        Me.Tu.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Tu.Frozen = True
        Me.Tu.HeaderText = "火"
        Me.Tu.Name = "Tu"
        Me.Tu.ReadOnly = True
        Me.Tu.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Tu.Width = 50
        '
        'We
        '
        Me.We.FillWeight = 50.0!
        Me.We.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.We.Frozen = True
        Me.We.HeaderText = "水"
        Me.We.Name = "We"
        Me.We.ReadOnly = True
        Me.We.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.We.Width = 50
        '
        'Th
        '
        Me.Th.FillWeight = 50.0!
        Me.Th.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Th.Frozen = True
        Me.Th.HeaderText = "木"
        Me.Th.Name = "Th"
        Me.Th.ReadOnly = True
        Me.Th.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Th.Width = 50
        '
        'Fr
        '
        Me.Fr.FillWeight = 50.0!
        Me.Fr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Fr.Frozen = True
        Me.Fr.HeaderText = "金"
        Me.Fr.Name = "Fr"
        Me.Fr.ReadOnly = True
        Me.Fr.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Fr.Width = 50
        '
        'Sa
        '
        Me.Sa.FillWeight = 50.0!
        Me.Sa.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Sa.Frozen = True
        Me.Sa.HeaderText = "土"
        Me.Sa.Name = "Sa"
        Me.Sa.ReadOnly = True
        Me.Sa.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Sa.Width = 50
        '
        'Calendar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(375, 363)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "Calendar"
        Me.Text = "Calendar"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Su As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Mo As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Tu As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents We As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Th As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Fr As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Sa As System.Windows.Forms.DataGridViewButtonColumn

End Class
