Imports System.Data.SqlClient
Imports System.ComponentModel

'------------------------------------------------------------
'-                File Name : frmMain.vb                    - 
'-                Part of Project: Assign4                  -
'------------------------------------------------------------
'-                Written By: Tajbid Hasib                  -
'-                Written On: 10/15/2019                    -
'------------------------------------------------------------
'- File Purpose:                                            -
'- This is main form for Turbo Auto Service
'------------------------------------------------------------
'- Program Purpose:                                         -
'-                                                          -
'- This program can be used to manage an auto service store
'- by communicating with databases
'------------------------------------------------------------
'- Global Variable Dictionary:             -
'- dsMechanics - dataset for mechanics
'- dsVehicles - dataset for vehicles
'- dsCustomers - dataset for customers
'- dsServices - dataset for services
'- dsShedule - dataset for schedule
'- customerID - incrementing ID for new customer
'- vehicleID - incrementing ID for new vehicle
'- scheduleID - incrementing ID for new appointment
'- DBConn - SQL Connection Object 
'- DBCmd - SQL Command Object
'- strSQLCmd - String containing SQL command
'- strDBPath - string containing path to DB
'- strConnection - String containing DB connection
'- myConn - SQL Connection Object
'- DBAdaptMechanics - SQL Adapter for mechanics
'- DBAdaptCustomers - SQL Adapter for customers
'- DBAdaptServices - SQL Adapter for services
'- DBAdaptSchedule - SQL Adapter for schedule
'- DBAdaptVehicles - SQL Adapter for vehicles
'- arrMechanics - Array containing mechanics
'------------------------------------------------------------

Public Class frmMain

    Const fileName As String = "frmMain.vb"

    'Create a dataset to point to each table -- do it here so that we don't
    'have to keep passing things around
    Dim dsMechanics As New DataSet
    Dim dsVehicles As New DataSet
    Dim dsCustomers As New DataSet
    Dim dsServices As New DataSet
    Dim dsSchedule As New DataSet

    'Beginning IDs
    Dim customerID As Integer = 1
    Dim vehicleID As Integer = 1
    Dim scheduleID As Integer = 1

    'Frequently used variables 
    Dim DBConn As SqlConnection
    Dim DBCmd As SqlCommand = New SqlCommand()
    Dim strSQLCmd As String

    'Here's the connection string related pieces - the same as in the module
    'A smarter way would be to declare them once, but I wanted the code to
    'be as simple as possible
    Const strDBNAME As String = "Assignment4" 'Name of database

    'Name of the database server
    Const strSERVERNAME As String = "(localdb)\MSSQLLocalDB"

    'Path to database in executable
    Dim strDBPATH As String = My.Application.Info.DirectoryPath &
                                  "\" & strDBNAME & ".mdf"

    'This is the full connection string
    Dim strCONNECTION As String = "SERVER=" & strSERVERNAME & ";DATABASE=" &
                 strDBNAME & ";Integrated Security=SSPI;AttachDbFileName=" &
                 strDBPATH

    'We'll also create a SqlConnection object since we will execute some
    'straight SQL rather than relying on the DBAdapters...
    Dim myConn As New SqlConnection(strCONNECTION)

    'Likewise create three data adapters so we don't mess stuff up
    'trying to be cute with one adapter
    Dim DBAdaptMechanics As SqlDataAdapter
    Dim DBAdaptSchedule As SqlDataAdapter
    Dim DBAdaptCustomers As SqlDataAdapter
    Dim DBAdaptVehicles As SqlDataAdapter
    Dim DBAdaptServices As SqlDataAdapter

    Dim arrMechanics() As Mechanic

    '------------------------------------------------------------
    '-      Subprogram Name: frmMain_Load            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine initiates the program.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- sender – Identifies which particular control raised the  –
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- strSQLCmd - String containing SQL command
    '- mechanics - array containing Mechanic objects
    '------------------------------------------------------------
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim strSQLCmd As String

        'Load up all Mechanics and Services since they will never change while the program runs
        strSQLCmd = "Select * From Mechanics"
        DBAdaptMechanics = New SqlDataAdapter(strSQLCmd, strCONNECTION)
        DBAdaptMechanics.Fill(dsMechanics, "Mechanics")



        'Create mechanic objects
        Dim mechanics(dsMechanics.Tables(0).Rows.Count) As Mechanic

        For index As Integer = 0 To (dsMechanics.Tables(0).Rows.Count - 1)
            Dim mID As String = dsMechanics.Tables(0).Rows(index).Item("TUID").ToString
            Dim mName As String = dsMechanics.Tables(0).Rows(index).Item("Name").ToString
            Dim mRate As Integer = CInt(dsMechanics.Tables(0).Rows(index).Item("Rate").ToString)

            mechanics(index) = New Mechanic(mID, mName, mRate)

            cmbSdlMechanic.Items.Add(dsMechanics.Tables(0).Rows(index).Item("Name").ToString)

        Next

        arrMechanics = mechanics

        'Load up all Mechanics and Services since they will never change while the program runs
        strSQLCmd = "Select * From Services"
        DBAdaptServices = New SqlDataAdapter(strSQLCmd, strCONNECTION)
        DBAdaptServices.Fill(dsServices, "Services")

        For index As Integer = 0 To (dsServices.Tables(0).Rows.Count - 1)
            cmbAptService.Items.Add(dsServices.Tables(0).Rows(index).Item("ServiceName").ToString)
        Next


    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: btnImportFile_Click            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine is called when btnImportFile is clicked
    '- in order to import a file
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- sender – Identifies which particular control raised the  –
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- fileLocation - string containing file location
    '- splitLine - array containing words from each line of file
    '- customerName - string containing customer name
    '- serviceName - string containing service name
    '- serviceTimeRequired - double containing service time
    '- vehicleName - string containing vehicle name
    '------------------------------------------------------------
    Private Sub btnImportFile_Click(sender As Object, e As EventArgs) Handles btnImportFile.Click

        Dim fileLocation As String
        fileLocation = InputBox("Enter the location of .txt file to import:", "File Import")

        If System.IO.File.Exists(fileLocation) Then

            'Now try to open up a connection to the database
            DBConn = New SqlConnection(strCONNECTION)
            DBConn.Open()

            For Each line As String In System.IO.File.ReadLines(fileLocation)


                If line.Chars(0) = "C" Then

                    Dim splitLine() As String = Split(line, vbTab)
                    Dim customerName As String = Trim(splitLine(1))

                    addCustomer(customerName)

                ElseIf line.Chars(0) = "V" Then

                    Dim splitLine() As String = Split(line, vbTab)
                    Dim currCustomerName As String = splitLine(1)
                    Dim vehicleName As String = splitLine(2)

                    addVehicle(currCustomerName, vehicleName)

                ElseIf line.Chars(0) = "S" Then

                    Dim splitLine() As String = Split(line, vbTab)
                    Dim currCustomerName As String
                    Dim currVehicleName As String
                    Dim serviceName As String = ""
                    Dim serviceTimeRequired As Double

                    'Find ID of Customer
                    strSQLCmd = "Select * From Customers WHERE Name = '" & splitLine(1) & "'"
                    DBAdaptCustomers = New SqlDataAdapter(strSQLCmd, strCONNECTION)
                    dsCustomers.Clear()
                    DBAdaptCustomers.Fill(dsCustomers, "Customers")

                    currCustomerName = dsCustomers.Tables(0).Rows(0).Item("Name").ToString

                    'Find ID of Vehicle 
                    strSQLCmd = "Select * From Vehicles WHERE Vehicle_Description = '" & splitLine(2) & "'"
                    DBAdaptVehicles = New SqlDataAdapter(strSQLCmd, strCONNECTION)
                    dsVehicles.Clear()
                    DBAdaptVehicles.Fill(dsVehicles, "Vehicles")

                    currVehicleName = dsVehicles.Tables(0).Rows(0).Item("Vehicle_Description").ToString

                    'Find Service ID and time required for service
                    For index As Integer = 0 To (dsServices.Tables(0).Rows.Count - 1)
                        If splitLine(3) = dsServices.Tables(0).Rows(index).Item("ServiceName").ToString Then
                            serviceName = dsServices.Tables(0).Rows(index).Item("ServiceName").ToString
                            serviceTimeRequired = CDbl(dsServices.Tables(0).Rows(index).Item("TimeRequired"))
                            Exit For
                        End If
                    Next

                    addAppointment(currCustomerName, currVehicleName, serviceName, serviceTimeRequired)


                End If

            Next

            DBConn.Close()

            refreshDgvs()
            refreshWeeks()

            MessageBox.Show("Import Successful.")

        Else
            MessageBox.Show("File Does Not Exist!", "Error")
        End If


    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: btnAddCustomer_Click            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine attempts to add new customer to DB.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- sender – Identifies which particular control raised the  –
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- customerName - String containing new customer name
    '------------------------------------------------------------
    Private Sub btnAddCustomer_Click(sender As Object, e As EventArgs) Handles btnAddCustomer.Click

        If txtCustomerName.Text.Length < 3 Then
            MessageBox.Show("Customer name must be at least 3 characters.")

        Else
            DBConn = New SqlConnection(strCONNECTION)

            DBConn.Open()

            Dim customerName As String = txtCustomerName.Text
            addCustomer(customerName)

            DBConn.Close()

            refreshDgvs()

            MessageBox.Show("Added Customer Successfully")
        End If


    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: btnAddVehicle_Click            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine attempts to add new vehicle to DB.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- sender – Identifies which particular control raised the  –
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- currCustomerName - String containing new customer name
    '- vehicleName - String containing new vehicle name
    '------------------------------------------------------------
    Private Sub btnAddVehicle_Click(sender As Object, e As EventArgs) Handles btnAddVehicle.Click

        If txtVehicleName.Text.Length < 3 Then
            MessageBox.Show("Vehicle Make/Model must be at least 3 characters.")
        ElseIf IsNothing(cmbVehicleCustomer.SelectedItem) Then
            MessageBox.Show("Please select a customer.")

        Else
            DBConn = New SqlConnection(strCONNECTION)

            DBConn.Open()

            Dim currCustomerName As String = cmbVehicleCustomer.SelectedItem
            Dim vehicleName As String = txtVehicleName.Text

            addVehicle(currCustomerName, vehicleName)

            DBConn.Close()

            refreshDgvs()
            refreshVehicles()


            MessageBox.Show("Added Vehicle Successfully")

        End If


    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: btnAddAppointment_Click            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine attempts to add new new appointment to DB.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- sender – Identifies which particular control raised the  –
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- currCustomerName - String containing customer name
    '- currVehicleName - String containing vehicle name
    '- serviceName - String containing service name
    '- serviceTimeRequired - double containing time required
    '------------------------------------------------------------
    Private Sub btnAddAppointment_Click(sender As Object, e As EventArgs) Handles btnAddAppointment.Click

        If IsNothing(cmbAptCustomer.SelectedItem) Then
            MessageBox.Show("Please select a customer.")
        ElseIf IsNothing(cmbAptService.SelectedItem) Then
            MessageBox.Show("Please select a service.")
        ElseIf IsNothing(cmbAptVehicle.SelectedItem) Then
            MessageBox.Show("Please select a vehicle.")
        Else


            DBConn = New SqlConnection(strCONNECTION)

            DBConn.Open()

            Dim currCustomerName As String
            Dim currVehicleName As String
            Dim serviceName As String = ""
            Dim serviceTimeRequired As Double

            'Find ID of Customer
            strSQLCmd = "Select * From Customers WHERE Name = '" & cmbAptCustomer.SelectedItem & "'"
            DBAdaptCustomers = New SqlDataAdapter(strSQLCmd, strCONNECTION)
            dsCustomers.Clear()
            DBAdaptCustomers.Fill(dsCustomers, "Customers")

            currCustomerName = dsCustomers.Tables(0).Rows(0).Item("Name").ToString

            'Find ID of Vehicle 
            strSQLCmd = "Select * From Vehicles WHERE Vehicle_Description = '" & cmbAptVehicle.SelectedItem & "'"
            DBAdaptVehicles = New SqlDataAdapter(strSQLCmd, strCONNECTION)
            dsVehicles.Clear()
            DBAdaptVehicles.Fill(dsVehicles, "Vehicles")

            currVehicleName = dsVehicles.Tables(0).Rows(0).Item("Vehicle_Description").ToString

            'Find Service ID and time required for service
            For index As Integer = 0 To (dsServices.Tables(0).Rows.Count - 1)
                If cmbAptService.SelectedItem = dsServices.Tables(0).Rows(index).Item("ServiceName").ToString Then
                    serviceName = dsServices.Tables(0).Rows(index).Item("ServiceName").ToString
                    serviceTimeRequired = CDbl(dsServices.Tables(0).Rows(index).Item("TimeRequired"))
                    Exit For
                End If
            Next

            addAppointment(currCustomerName, currVehicleName, serviceName, serviceTimeRequired)

            DBConn.Close()

            refreshDgvs()
            refreshWeeks()

            MessageBox.Show("Added New Appointment")
        End If


    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: btnViewSchedule_Click            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine shows weekly schedule for employee.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- sender – Identifies which particular control raised the  –
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- name - String containing employee name
    '- rate - double containing employee pay rate
    '- hoursWorked - double containing weekly hours worked
    '- estPay - double containing estimated pay for employee
    '- week - integer containing week number
    '------------------------------------------------------------
    Private Sub btnViewSchedule_Click(sender As Object, e As EventArgs) Handles btnViewSchedule.Click

        If IsNothing(cmbSdlMechanic.SelectedItem) Then
            MessageBox.Show("Please select a mechanic.")
        ElseIf IsNothing(cmbSdlWeek.SelectedItem) Then
            MessageBox.Show("Please select a week.")
        Else

            DBConn = New SqlConnection(strCONNECTION)
            DBConn.Open()

            strSQLCmd = "SELECT *
                    FROM Schedule 
                    WHERE Mechanic = '" & cmbSdlMechanic.SelectedItem & "' " &
                        "AND Week = '" & cmbSdlWeek.SelectedItem & "'" &
                        "ORDER BY CASE Appointment_Day 
                                        WHEN 'Monday' THEN '1'
                                        WHEN 'Tuesday' THEN '2'
                                        WHEN 'Wednesday' THEN '3'
                                        WHEN 'Thursday' THEN '4'
                                        WHEN 'Friday' THEN '5'
                                        END"
            DBAdaptSchedule = New SqlDataAdapter(strSQLCmd, strCONNECTION)
            dsSchedule.Clear()
            DBAdaptSchedule.Fill(dsSchedule, "Schedule")

            'Refresh the DataGridView showing the schedule so that it's accurate

            frmEmpSchedule.dgvSchedule.DataSource = dsSchedule.Tables("Schedule")

            frmEmpSchedule.dgvSchedule.Columns(0).Visible = False
            frmEmpSchedule.dgvSchedule.Columns(1).Visible = False
            frmEmpSchedule.dgvSchedule.Columns(2).Visible = False

            frmEmpSchedule.dgvSchedule.Refresh()

            Dim name As String = ""
            Dim rate As Double = 0
            Dim hoursWorked As Double = 0
            Dim estPay As Double = 0
            Dim week As Integer = CInt(cmbSdlWeek.SelectedItem)

            If arrMechanics(0).getName = cmbSdlMechanic.SelectedItem Then

                name = arrMechanics(0).getName
                rate = arrMechanics(0).getRate
                hoursWorked = arrMechanics(0).getHoursWorked(week - 1)
                estPay = CDbl(rate) * hoursWorked

            ElseIf arrMechanics(1).getName = cmbSdlMechanic.SelectedItem Then

                name = arrMechanics(1).getName
                rate = arrMechanics(1).getRate
                hoursWorked = arrMechanics(1).getHoursWorked(week - 1)
                estPay = CDbl(rate) * hoursWorked

            End If

            frmEmpSchedule.lblMechanic.Text = "Week " & cmbSdlWeek.SelectedItem & " Schedule For " & name
            frmEmpSchedule.lblRate.Text = rate.ToString("$0.00")
            frmEmpSchedule.lblHours.Text = hoursWorked.ToString
            frmEmpSchedule.lblPay.Text = estPay.ToString("$0.00")

            frmEmpSchedule.ShowDialog()

            DBConn.Close()
        End If


    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: addCustomer            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine adds new customer to DB.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- customerName - name of new customer
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- cmd - SQL Command for executing query
    '- result - result of SQL Query
    '------------------------------------------------------------
    Public Sub addCustomer(ByVal customerName As String)

        Dim cmd As New SqlCommand("Select Name From Customers WHERE Name = '" & customerName & "'", DBConn)
        Dim result = cmd.ExecuteScalar()

        If result = "" Then
            'Use SQL to insert a new row into Customers
            DBCmd.CommandText = "INSERT INTO Customers (TUID, Name) VALUES ('" &
                                customerID.ToString & "','" & customerName & "')"
            DBCmd.Connection = DBConn
            DBCmd.ExecuteNonQuery()  'Since it's a non-SELECT statement

            'Add Customer to combo box
            cmbVehicleCustomer.Items.Add(customerName)
            cmbAptCustomer.Items.Add(customerName)

            customerID += 1
        Else    'Customer exists
            MessageBox.Show("Customer " & customerName & " already exists in system.")
        End If

    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: addVehicle            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine adds new vehicle to DB.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- customerName - name of customer
    '- vehicleName - name of new vehicle
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- customerID - string containing customer id
    '- cmd - SQL Command for executing query
    '- result - result of SQL Query
    '------------------------------------------------------------
    Public Sub addVehicle(ByVal customerName As String, ByVal vehicleName As String)

        'Find ID of Customer
        strSQLCmd = "Select * From Customers WHERE Name = '" & customerName & "'"
        DBAdaptCustomers = New SqlDataAdapter(strSQLCmd, strCONNECTION)
        dsCustomers.Clear()
        DBAdaptCustomers.Fill(dsCustomers, "Customers")

        Dim customerID As String = dsCustomers.Tables(0).Rows(0).Item("TUID").ToString

        'Check if this vehicle already exists
        Dim cmd As New SqlCommand("Select TUID From Vehicles WHERE Customer_TUID = '" & customerID &
                                  "' AND Vehicle_Description = '" & vehicleName & "'", DBConn)
        Dim result = cmd.ExecuteScalar()

        If result = "" Then
            'Use SQL to insert a new row into Registration
            DBCmd.CommandText = "INSERT INTO Vehicles (TUID, Customer_TUID, Vehicle_Description) VALUES ('" &
                    vehicleID.ToString & "','" & customerID & "', '" & vehicleName & "')"
            DBCmd.Connection = DBConn
            DBCmd.ExecuteNonQuery()  'Since it's a non-SELECT statement

            'Add Customer to combo box
            vehicleID += 1

        Else
            MessageBox.Show("Vehicle " & vehicleName & " belonging to " & customerName &
                            " already exists in system.")
        End If



    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: addAppointment            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine adds new appointment to DB.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- currCustomerName - name of customer
    '- currVehicleName - name of new vehicle
    '- serviceName - name of service
    '- serviceTimeRequired - time for service
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- cmd - SQL Command for executing query
    '- result - result of SQL Query
    '- mechanicName - name of mechanic
    '- week - week number
    '- day - week day
    '- startTime - appointment start time
    '- endTime - appointment end time
    '- arrDayTimes - array containing week, day and times
    '------------------------------------------------------------
    Public Sub addAppointment(ByVal currCustomerName As String,
                              ByVal currVehicleName As String,
                              ByVal serviceName As String,
                              ByVal serviceTimeRequired As Double)

        'Check if this appointment already exists
        Dim cmd As New SqlCommand("Select TUID From Schedule WHERE Customer = '" & currCustomerName &
                                  "' AND Vehicle = '" & currVehicleName &
                                  "' And Service = '" & serviceName & "'", DBConn)
        Dim result = cmd.ExecuteScalar()

        'If result = "" Then
        Dim mechanicName As String = ""
            Dim week As String
            Dim day As String
            Dim startTime As String
            Dim endTime As String

            Dim arrDayTimes(3) As String

            Dim arrMech1DayTime() As Double = arrMechanics(0).getAvailableDayTimeWeek(serviceTimeRequired)
            Dim arrMech2DayTime() As Double = arrMechanics(1).getAvailableDayTimeWeek(serviceTimeRequired)

            'Find Mechanic ID, Available Day and Time for both mechanics

            'Compare weeks
            If arrMech1DayTime(2) = arrMech2DayTime(2) Then

                'Compare Days
                If arrMech1DayTime(0) < arrMech2DayTime(0) Then
                    mechanicName = dsMechanics.Tables(0).Rows(0).Item("Name")
                    arrDayTimes = arrMechanics(0).addToSchedule(serviceTimeRequired)

                ElseIf arrMech1DayTime(0) > arrMech2DayTime(0) Then
                    mechanicName = dsMechanics.Tables(0).Rows(1).Item("Name")
                    arrDayTimes = arrMechanics(1).addToSchedule(serviceTimeRequired)

                ElseIf arrMech1DayTime(0) = arrMech2DayTime(0) Then

                    'Compare CurrentTimes
                    If arrMech1DayTime(1) = arrMech2DayTime(1) Then
                        mechanicName = dsMechanics.Tables(0).Rows(0).Item("Name")
                        arrDayTimes = arrMechanics(0).addToSchedule(serviceTimeRequired)

                    ElseIf arrMech1DayTime(1) < arrMech2DayTime(1) Then
                        mechanicName = dsMechanics.Tables(0).Rows(0).Item("Name")
                        arrDayTimes = arrMechanics(0).addToSchedule(serviceTimeRequired)

                    Else
                        mechanicName = dsMechanics.Tables(0).Rows(1).Item("Name")
                        arrDayTimes = arrMechanics(1).addToSchedule(serviceTimeRequired)

                    End If
                End If

                'Check which mechanic has Earlier week available:
            ElseIf arrMech1DayTime(2) < arrMech2DayTime(2) Then
                mechanicName = dsMechanics.Tables(0).Rows(0).Item("Name")
                arrDayTimes = arrMechanics(0).addToSchedule(serviceTimeRequired)

            Else
                mechanicName = dsMechanics.Tables(0).Rows(1).Item("Name")
                arrDayTimes = arrMechanics(1).addToSchedule(serviceTimeRequired)

            End If

            day = arrDayTimes(0)
            startTime = arrDayTimes(1)
            endTime = arrDayTimes(2)
            week = arrDayTimes(3)


            'Use SQL to insert a new row into Registration
            DBCmd.CommandText = "INSERT INTO Schedule (TUID, Week, Mechanic, Customer, 
                                          Vehicle, Service, Appointment_Day,
                                          Start_Time, End_Time) VALUES ('" & scheduleID.ToString &
                                  "','" & week & "', '" & mechanicName & "', '" & currCustomerName & "', '" &
                                  currVehicleName & "', '" & serviceName & "', '" &
                                  day & "', '" & startTime & "', '" & endTime & "')"
            DBCmd.Connection = DBConn
            DBCmd.ExecuteNonQuery()  'Since it's a non-SELECT statement

            scheduleID += 1

        'Else
        'MessageBox.Show(serviceName & " Service is already scheduled for Customer " &
        'currCustomerName & " on Vehicle " & currVehicleName)
        'End If

    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: cmbMechanic_SelectedIndexChanged            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine calls refreshWeeks subroutine.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- sender – Identifies which particular control raised the  –
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- (none)
    '------------------------------------------------------------
    Private Sub cmbMechanic_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSdlMechanic.SelectedIndexChanged

        refreshWeeks()

    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: cmbAptCustomer_SelectedIndexChanged            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine calls refreshVehicles subroutine.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- sender – Identifies which particular control raised the  –
    '-          click event                                     - 
    '- e – Holds the EventArgs object sent to the routine       -
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- (none)
    '------------------------------------------------------------
    Private Sub cmbAptCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAptCustomer.SelectedIndexChanged

        refreshVehicles()

    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: refreshWeeks            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine fills weeks combo box
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (none)
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- cmd - SQL Command string
    '- result - SQL Command result
    '------------------------------------------------------------
    Public Sub refreshWeeks()
        DBConn = New SqlConnection(strCONNECTION)
        DBConn.Open()

        Dim cmd As New SqlCommand("Select MAX(Week) From Schedule WHERE Mechanic = '" & cmbSdlMechanic.SelectedItem & "'", DBConn)

        cmbSdlWeek.Items.Clear()
        Dim result = cmd.ExecuteScalar()

        If Not TypeName(result) = "DBNull" Then

            'Add week numbers to combo box
            For i As Integer = 1 To CInt(result)
                cmbSdlWeek.Items.Add(i)
            Next
        End If

        DBConn.Close()
    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: refreshDgvs            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine refreshes datagrid views
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (none)
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- (none)
    '------------------------------------------------------------
    Public Sub refreshDgvs()
        'Refresh dataset
        strSQLCmd = "Select * From Customers"
        DBAdaptCustomers = New SqlDataAdapter(strSQLCmd, strCONNECTION)
        dsCustomers.Clear()
        DBAdaptCustomers.Fill(dsCustomers, "Customers")

        strSQLCmd = "Select * From Vehicles"
        DBAdaptCustomers = New SqlDataAdapter(strSQLCmd, strCONNECTION)
        dsVehicles.Clear()
        DBAdaptCustomers.Fill(dsVehicles, "Vehicles")

        strSQLCmd = "Select * From Schedule"
        DBAdaptSchedule = New SqlDataAdapter(strSQLCmd, strCONNECTION)
        dsSchedule.Clear()
        DBAdaptSchedule.Fill(dsSchedule, "Schedule")

        dgvSchedule.DataSource = dsSchedule.Tables("Schedule")
        dgvSchedule.Refresh()
    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: refreshVehicles            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine fills vehicles combo box
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (none)
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- cmd1 - SQL Command string
    '- cmd2 - SQL Command string
    '- cID - SQL Command execution

    '------------------------------------------------------------
    Private Sub refreshVehicles()

        'Clear 
        cmbAptVehicle.Items.Clear()

        'Retrieve customer ID
        DBConn = New SqlConnection(strCONNECTION)
        DBConn.Open()

        Dim cmd1 As New SqlCommand("Select TUID From Customers WHERE Name = '" & cmbAptCustomer.SelectedItem & "'", DBConn)
        Dim cID = cmd1.ExecuteScalar()

        Dim cmd2 As New SqlCommand("Select Vehicle_Description From Vehicles WHERE Customer_TUID = '" & cID & "'", DBConn)
        Dim reader As SqlDataReader = cmd2.ExecuteReader
        While reader.Read()
            cmbAptVehicle.Items.Add(reader.Item(0))
        End While
        reader.Close()

        DBConn.Close()
    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: txtCustomerName_KeyPress            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine restricts keypresses in customer TB
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- sender - object sending 
    '- e - KeyPressEventArgs argument
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- allowedChars - string containing allowed characters
    '------------------------------------------------------------
    Private Sub txtCustomerName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCustomerName.KeyPress

        If Not (Asc(e.KeyChar) = 8) Then
            Dim allowedChars As String = "abcdefghijklmnopqrstuvwxyz1234567890-' "
            If Not allowedChars.Contains(e.KeyChar.ToString.ToLower) Then
                e.KeyChar = ChrW(0)
                e.Handled = True
            End If
        End If

    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: txtCustomerName_KeyPress            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine restricts keypresses in vehicle TB
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- sender - object sending 
    '- e - KeyPressEventArgs argument
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- allowedChars - string containing allowed characters
    '------------------------------------------------------------
    Private Sub txtVehicleName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVehicleName.KeyPress
        If Not (Asc(e.KeyChar) = 8) Then
            Dim allowedChars As String = "abcdefghijklmnopqrstuvwxyz1234567890-' "
            If Not allowedChars.Contains(e.KeyChar.ToString.ToLower) Then
                e.KeyChar = ChrW(0)
                e.Handled = True
            End If
        End If
    End Sub


End Class
