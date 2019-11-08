<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.cmbVehicleCustomer = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbAptVehicle = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbAptService = New System.Windows.Forms.ComboBox()
        Me.btnImportFile = New System.Windows.Forms.Button()
        Me.dgvSchedule = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnAddCustomer = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnAddVehicle = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtVehicleName = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmbAptCustomer = New System.Windows.Forms.ComboBox()
        Me.btnAddAppointment = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cmbSdlWeek = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbSdlMechanic = New System.Windows.Forms.ComboBox()
        Me.btnViewSchedule = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        CType(Me.dgvSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbVehicleCustomer
        '
        Me.cmbVehicleCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVehicleCustomer.FormattingEnabled = True
        Me.cmbVehicleCustomer.Location = New System.Drawing.Point(199, 68)
        Me.cmbVehicleCustomer.Name = "cmbVehicleCustomer"
        Me.cmbVehicleCustomer.Size = New System.Drawing.Size(246, 24)
        Me.cmbVehicleCustomer.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(55, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Customer Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(310, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 17)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Vehicle"
        '
        'cmbAptVehicle
        '
        Me.cmbAptVehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAptVehicle.FormattingEnabled = True
        Me.cmbAptVehicle.Location = New System.Drawing.Point(313, 47)
        Me.cmbAptVehicle.Name = "cmbAptVehicle"
        Me.cmbAptVehicle.Size = New System.Drawing.Size(193, 24)
        Me.cmbAptVehicle.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(575, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(117, 17)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Service Required"
        '
        'cmbAptService
        '
        Me.cmbAptService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAptService.FormattingEnabled = True
        Me.cmbAptService.Location = New System.Drawing.Point(578, 47)
        Me.cmbAptService.Name = "cmbAptService"
        Me.cmbAptService.Size = New System.Drawing.Size(193, 24)
        Me.cmbAptService.TabIndex = 8
        '
        'btnImportFile
        '
        Me.btnImportFile.Location = New System.Drawing.Point(25, 21)
        Me.btnImportFile.Name = "btnImportFile"
        Me.btnImportFile.Size = New System.Drawing.Size(131, 44)
        Me.btnImportFile.TabIndex = 11
        Me.btnImportFile.Text = "Import File"
        Me.btnImportFile.UseVisualStyleBackColor = True
        '
        'dgvSchedule
        '
        Me.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSchedule.Location = New System.Drawing.Point(28, 37)
        Me.dgvSchedule.Name = "dgvSchedule"
        Me.dgvSchedule.ReadOnly = True
        Me.dgvSchedule.RowTemplate.Height = 24
        Me.dgvSchedule.Size = New System.Drawing.Size(980, 355)
        Me.dgvSchedule.TabIndex = 13
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnAddCustomer)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtCustomerName)
        Me.GroupBox1.Location = New System.Drawing.Point(25, 82)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(506, 183)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Add A Customer"
        '
        'btnAddCustomer
        '
        Me.btnAddCustomer.Location = New System.Drawing.Point(211, 101)
        Me.btnAddCustomer.Name = "btnAddCustomer"
        Me.btnAddCustomer.Size = New System.Drawing.Size(150, 41)
        Me.btnAddCustomer.TabIndex = 2
        Me.btnAddCustomer.Text = "Add Customer"
        Me.btnAddCustomer.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(55, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 17)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Name:"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.Location = New System.Drawing.Point(143, 47)
        Me.txtCustomerName.MaxLength = 100
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(302, 22)
        Me.txtCustomerName.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnAddVehicle)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txtVehicleName)
        Me.GroupBox2.Controls.Add(Me.cmbVehicleCustomer)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(555, 82)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(506, 183)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Add A Vehicle"
        '
        'btnAddVehicle
        '
        Me.btnAddVehicle.Location = New System.Drawing.Point(249, 115)
        Me.btnAddVehicle.Name = "btnAddVehicle"
        Me.btnAddVehicle.Size = New System.Drawing.Size(150, 41)
        Me.btnAddVehicle.TabIndex = 2
        Me.btnAddVehicle.Text = "Add Vehicle"
        Me.btnAddVehicle.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(55, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 17)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Make/Model:"
        '
        'txtVehicleName
        '
        Me.txtVehicleName.Location = New System.Drawing.Point(199, 28)
        Me.txtVehicleName.MaxLength = 100
        Me.txtVehicleName.Name = "txtVehicleName"
        Me.txtVehicleName.Size = New System.Drawing.Size(246, 22)
        Me.txtVehicleName.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbAptCustomer)
        Me.GroupBox3.Controls.Add(Me.btnAddAppointment)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.cmbAptVehicle)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.cmbAptService)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Location = New System.Drawing.Point(25, 286)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1036, 102)
        Me.GroupBox3.TabIndex = 15
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Create New Appointment"
        '
        'cmbAptCustomer
        '
        Me.cmbAptCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAptCustomer.FormattingEnabled = True
        Me.cmbAptCustomer.Location = New System.Drawing.Point(58, 47)
        Me.cmbAptCustomer.Name = "cmbAptCustomer"
        Me.cmbAptCustomer.Size = New System.Drawing.Size(193, 24)
        Me.cmbAptCustomer.TabIndex = 6
        '
        'btnAddAppointment
        '
        Me.btnAddAppointment.Location = New System.Drawing.Point(825, 36)
        Me.btnAddAppointment.Name = "btnAddAppointment"
        Me.btnAddAppointment.Size = New System.Drawing.Size(150, 41)
        Me.btnAddAppointment.TabIndex = 2
        Me.btnAddAppointment.Text = "Add Appointment"
        Me.btnAddAppointment.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(55, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(109, 17)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Customer Name"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cmbSdlWeek)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.cmbSdlMechanic)
        Me.GroupBox4.Controls.Add(Me.btnViewSchedule)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Location = New System.Drawing.Point(236, 411)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(651, 100)
        Me.GroupBox4.TabIndex = 16
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "View Employee Weekly Schedule"
        '
        'cmbSdlWeek
        '
        Me.cmbSdlWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSdlWeek.FormattingEnabled = True
        Me.cmbSdlWeek.Location = New System.Drawing.Point(258, 50)
        Me.cmbSdlWeek.Name = "cmbSdlWeek"
        Me.cmbSdlWeek.Size = New System.Drawing.Size(124, 24)
        Me.cmbSdlWeek.TabIndex = 8
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(255, 30)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(102, 17)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Week Number:"
        '
        'cmbSdlMechanic
        '
        Me.cmbSdlMechanic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSdlMechanic.FormattingEnabled = True
        Me.cmbSdlMechanic.Location = New System.Drawing.Point(76, 50)
        Me.cmbSdlMechanic.Name = "cmbSdlMechanic"
        Me.cmbSdlMechanic.Size = New System.Drawing.Size(124, 24)
        Me.cmbSdlMechanic.TabIndex = 6
        '
        'btnViewSchedule
        '
        Me.btnViewSchedule.Location = New System.Drawing.Point(428, 36)
        Me.btnViewSchedule.Name = "btnViewSchedule"
        Me.btnViewSchedule.Size = New System.Drawing.Size(150, 41)
        Me.btnViewSchedule.TabIndex = 2
        Me.btnViewSchedule.Text = "View Schedule"
        Me.btnViewSchedule.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(73, 30)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 17)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Mechanic:"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.dgvSchedule)
        Me.GroupBox5.Location = New System.Drawing.Point(25, 532)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(1036, 420)
        Me.GroupBox5.TabIndex = 17
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "All Appointments"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1086, 975)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnImportFile)
        Me.Name = "frmMain"
        Me.Text = "Turbo Auto Service"
        CType(Me.dgvSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbVehicleCustomer As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbAptVehicle As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbAptService As ComboBox
    Friend WithEvents btnImportFile As Button
    Friend WithEvents dgvSchedule As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnAddCustomer As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents txtCustomerName As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents btnAddVehicle As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents txtVehicleName As TextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents btnAddAppointment As Button
    Friend WithEvents cmbAptCustomer As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents cmbSdlMechanic As ComboBox
    Friend WithEvents btnViewSchedule As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents cmbSdlWeek As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents GroupBox5 As GroupBox
End Class
