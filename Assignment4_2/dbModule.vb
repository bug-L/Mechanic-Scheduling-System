Imports System.Data.SqlClient

'------------------------------------------------------------
'-                File Name : dbModule.vb                    - 
'-                Part of Project: Assign4                  -
'------------------------------------------------------------
'-                Written By: Tajbid Hasib                  -
'-                Written On: 10/15/2019                    -
'------------------------------------------------------------
'- File Purpose:                                            -
'- This module creates and sets up databases
'------------------------------------------------------------
'- Program Purpose:                                         -
'-                                                          -
'- This program can be used to manage an auto service store
'- by communicating with databases
'------------------------------------------------------------
'- Global Variable Dictionary:             -
'- (none)
'------------------------------------------------------------
Module dbModule

    Const fileName = "dbModule.vb"

    '------------------------------------------------------------
    '-      Subprogram Name: Main            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine initiates the module.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (none)
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- strDBPATH - string path to DB
    '- strCONNECTION - DB Connection string
    '------------------------------------------------------------
    Sub Main()

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

        DeleteDatabase(strSERVERNAME, strDBNAME)

        'If the database doesn't exist, create it
        If Not (System.IO.File.Exists(strDBPATH)) Then
            CreateDatabase(strSERVERNAME, strDBNAME, strDBPATH, strCONNECTION)
        End If

        'Make sure all tables are cleaned out each time we run this

        CleanOutMechanicsTable(strCONNECTION)
        CleanOutCustomersTable(strCONNECTION)
        CleanOutScheduleTable(strCONNECTION)
        CleanOutVehiclesTable(strCONNECTION)
        CleanOutServicesTable(strCONNECTION)

        'Put some data into the tables
        PopulateMechanicsTable(strCONNECTION)  '1 student record
        PopulateServicesTable(strCONNECTION)   '6 courses
        'DeleteDatabase(strSERVERNAME, strDBNAME)

        frmMain.ShowDialog()


    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: CreateDatabase            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine initiates the program.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- strSERVERNAME – string server name- 
    '- strDBNAME - string DB name -
    '- strDBPATH - string DB Path
    '- strCONNECTION - string connection to DB
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- strSQLCmd - String containing SQL command
    '- mechanics - array containing Mechanic objects
    '------------------------------------------------------------
    Sub CreateDatabase(ByVal strSERVERNAME As String, ByVal strDBNAME As String,
                       ByVal strDBPATH As String, ByVal strCONNECTION As String)

        'Let's build a SQL Server database from scratch
        Dim DBConn As SqlConnection
        Dim strSQLCmd As String
        Dim DBCmd As SqlCommand

        'All we need to do initially is just point at the server
        DBConn = New SqlConnection("Server=" & strSERVERNAME)

        'Let's write a SQL DDL Command to build the database
        'There are a lot of other parameters but we can let them default
        'All we need are these three
        strSQLCmd = "CREATE DATABASE " & strDBNAME & " On " &
                    "(NAME = '" & strDBNAME & "', " &
                    "FILENAME = '" & strDBPATH & "')"

        DBCmd = New SqlCommand(strSQLCmd, DBConn)

        Try
            'Open the connection and try running the command
            DBConn.Open()
            DBCmd.ExecuteNonQuery()
            'MessageBox.Show("Database was successfully created", "",
            'MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            'If we can't build the database, we are dead in the water so bail...
            'MessageBox.Show(ex.ToString())
            'MessageBox.Show("Cannot build database!  Closing program down...")
            End
        End Try

        'We are currently pointing at the [MASTER] database, so we
        'need to close the connection and reopen it pointing at the
        'Registration database...
        If (DBConn.State = ConnectionState.Open) Then
            DBConn.Close()
        End If

        'Now we need to use the full connection string with the Integrated 
        'Security line, et cetera
        DBConn = New SqlConnection(strCONNECTION)
        DBConn.Open()

        'Build the Customers Table
        DBCmd.CommandText = "CREATE TABLE Mechanics(" &
                                     "TUID varchar(6), " &
                                     "Name varchar(50)," &
                                     "Rate varchar(2))"
        DBCmd.Connection = DBConn
        Try
            DBCmd.ExecuteNonQuery()
            'MessageBox.Show("Created Mechanics Table")
        Catch Ex As Exception
            'MessageBox.Show("Mechanics Table Already Exists")
        End Try


        'Build the Customers Table
        DBCmd.CommandText = "CREATE TABLE Customers(" &
                                     "TUID varchar(6), " &
                                     "Name varchar(50))"
        DBCmd.Connection = DBConn
        Try
            DBCmd.ExecuteNonQuery()
            'MessageBox.Show("Created Customers Table")
        Catch Ex As Exception
            'MessageBox.Show("Customers Table Already Exists")
        End Try

        'Build the Registration Table
        DBCmd.CommandText = "CREATE TABLE Vehicles(" &
                                     "TUID varchar(6), " &
                                     "Customer_TUID varchar(6)," &
                                     "Vehicle_Description varchar(100))"
        DBCmd.Connection = DBConn
        Try
            DBCmd.ExecuteNonQuery()
            'MessageBox.Show("Created Vehicles Table")
        Catch Ex As Exception
            'MessageBox.Show("Vehicles Table Already Exists")
        End Try

        'Build the Registration Table
        DBCmd.CommandText = "CREATE TABLE Services(" &
                                     "TUID varchar(6), " &
                                     "ServiceName varchar(50)," &
                                     "TimeRequired varchar(6))"
        DBCmd.Connection = DBConn
        Try
            DBCmd.ExecuteNonQuery()
            'MessageBox.Show("Created Services Table")
        Catch Ex As Exception
            'MessageBox.Show("Services Table Already Exists")
        End Try

        'Build the Registration Table
        DBCmd.CommandText = "CREATE TABLE Schedule(" &
                                     "TUID varchar(6), " &
                                     "Week varchar(6)," &
                                     "Mechanic varchar(100)," &
                                     "Appointment_Day varchar(10)," &
                                     "Customer varchar(100)," &
                                     "Vehicle varchar(100)," &
                                     "Service varchar(100)," &
                                    "Start_Time varchar(10)," &
                                     "End_Time varchar(10))"

        DBCmd.Connection = DBConn
        Try
            DBCmd.ExecuteNonQuery()
            'MessageBox.Show("Created Schedule Table")
        Catch Ex As Exception
            'MessageBox.Show("Schedule Table Already Exists")
        End Try

        'We can check to see if we're open before trying to
        'issue a connection close
        If (DBConn.State = ConnectionState.Open) Then
            DBConn.Close()
        End If
    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: CleanOutMechanicsTable            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine clears mechanics table from DB.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- strConn - string containing DB Connection
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- DBConn – SQL Connection Object- 
    '- DBCmd - SQL Command object -
    '------------------------------------------------------------
    Sub CleanOutMechanicsTable(ByVal strConn As String)
        Dim DBConn As SqlConnection
        Dim DBCmd As SqlCommand = New SqlCommand()

        'Now try to open up a connection to the database
        DBConn = New SqlConnection(strConn)
        DBConn.Open()

        'Use SQL DML to zap the contents of the table
        DBCmd.CommandText = "DELETE FROM Mechanics"
        DBCmd.Connection = DBConn
        DBCmd.ExecuteNonQuery()
        'MessageBox.Show("Deleted Everything In Mechanics")

        DBConn.Close()
    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: CleanOutCustomersTable            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine clears customers table from DB.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- strConn - string containing DB Connection
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- DBConn – SQL Connection Object- 
    '- DBCmd - SQL Command object -
    '------------------------------------------------------------
    Sub CleanOutCustomersTable(ByVal strConn As String)
        Dim DBConn As SqlConnection
        Dim DBCmd As SqlCommand = New SqlCommand()

        'Now try to open up a connection to the database
        DBConn = New SqlConnection(strConn)
        DBConn.Open()

        'Use SQL DML to zap the contents of the table
        DBCmd.CommandText = "DELETE FROM Customers"
        DBCmd.Connection = DBConn
        DBCmd.ExecuteNonQuery()
        'MessageBox.Show("Deleted Everything In Customers")

        DBConn.Close()
    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: CleanOutScheduleTable            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine clears schedule table from DB.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- strConn - string containing DB Connection
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- DBConn – SQL Connection Object- 
    '- DBCmd - SQL Command object -
    '------------------------------------------------------------
    Sub CleanOutScheduleTable(ByVal strConn As String)
        Dim DBConn As SqlConnection
        Dim DBCmd As SqlCommand = New SqlCommand()

        'Now try to open up a connection to the database
        DBConn = New SqlConnection(strConn)
        DBConn.Open()

        'Use SQL DML to zap the contents of the table
        DBCmd.CommandText = "DELETE FROM Schedule"
        DBCmd.Connection = DBConn
        DBCmd.ExecuteNonQuery()
        'MessageBox.Show("Deleted Everything In Schedule")

        DBConn.Close()
    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: CleanOutVehiclesTable            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine clears vehicles table from DB.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- strConn - string containing DB Connection
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- DBConn – SQL Connection Object- 
    '- DBCmd - SQL Command object -
    '------------------------------------------------------------
    Sub CleanOutVehiclesTable(ByVal strConn As String)
        Dim DBConn As SqlConnection
        Dim DBCmd As SqlCommand = New SqlCommand()

        'Now try to open up a connection to the database
        DBConn = New SqlConnection(strConn)
        DBConn.Open()

        'Use SQL DML to zap the contents of the table
        DBCmd.CommandText = "DELETE FROM Vehicles"
        DBCmd.Connection = DBConn
        DBCmd.ExecuteNonQuery()
        'MessageBox.Show("Deleted Everything In Vehicles")

        DBConn.Close()
    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: CleanOutServicesTable            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine clears services table from DB.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- strConn - string containing DB Connection
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- DBConn – SQL Connection Object- 
    '- DBCmd - SQL Command object -
    '------------------------------------------------------------
    Sub CleanOutServicesTable(ByVal strConn As String)
        Dim DBConn As SqlConnection
        Dim DBCmd As SqlCommand = New SqlCommand()

        'Now try to open up a connection to the database
        DBConn = New SqlConnection(strConn)
        DBConn.Open()

        'Use SQL DML to zap the contents of the table
        DBCmd.CommandText = "DELETE FROM Services"
        DBCmd.Connection = DBConn
        DBCmd.ExecuteNonQuery()
        'MessageBox.Show("Deleted Everything In Services")

        DBConn.Close()
    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: PopulateServicesTable            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine populates services table from DB.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- strConn - string containing DB Connection
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- DBConn – SQL Connection Object- 
    '- DBCmd - SQL Command object -
    '------------------------------------------------------------
    Sub PopulateServicesTable(ByVal strConn As String)
        Dim DBConn As SqlConnection
        Dim DBCmd As SqlCommand = New SqlCommand()

        'Now try to open up a connection to the database
        DBConn = New SqlConnection(strConn)
        DBConn.Open()

        'Add a student using SQL DML
        DBCmd.CommandText = "INSERT INTO Services(TUID, ServiceName, TimeRequired) " &
                            "VALUES ('1','Oil Change','0.5')"
        DBCmd.Connection = DBConn
        DBCmd.ExecuteNonQuery()

        DBCmd.CommandText = "INSERT INTO Services(TUID, ServiceName, TimeRequired) " &
                            "VALUES ('2','Tire Replacement','1')"
        DBCmd.Connection = DBConn
        DBCmd.ExecuteNonQuery()

        DBCmd.CommandText = "INSERT INTO Services(TUID, ServiceName, TimeRequired) " &
                            "VALUES ('3','Brakes','3')"
        DBCmd.Connection = DBConn
        DBCmd.ExecuteNonQuery()

        DBCmd.CommandText = "INSERT INTO Services(TUID, ServiceName, TimeRequired) " &
                            "VALUES ('4','Transmission Filter Replacement','2')"
        DBCmd.Connection = DBConn
        DBCmd.ExecuteNonQuery()

        DBCmd.CommandText = "INSERT INTO Services (TUID, ServiceName, TimeRequired) " &
                            "VALUES ('5','Cooling System Cleaning','4')"
        DBCmd.Connection = DBConn
        DBCmd.ExecuteNonQuery()

        DBConn.Close()

        ' MessageBox.Show("Services Table Populated")

    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: PopulateMechanicsTable            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine populates mechanics table from DB.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- strConn - string containing DB Connection
    '------------------------------------------------------------
    '- Local Variable Dictionary -
    '- DBConn – SQL Connection Object- 
    '- DBCmd - SQL Command object -
    '------------------------------------------------------------
    Sub PopulateMechanicsTable(ByVal strConn As String)
        Dim DBConn As SqlConnection
        Dim DBCmd As SqlCommand = New SqlCommand()

        'Now try to open up a connection to the database
        DBConn = New SqlConnection(strConn)
        DBConn.Open()

        'Add student registration using SQL DML
        DBCmd.CommandText = "INSERT INTO Mechanics(TUID, Name, Rate) " &
                            "VALUES ('1','Sue', '10')"
        DBCmd.Connection = DBConn
        DBCmd.ExecuteNonQuery()

        DBCmd.CommandText = "INSERT INTO Mechanics(TUID, Name, Rate) " &
                            "VALUES ('2','Steve', '9')"
        DBCmd.Connection = DBConn
        DBCmd.ExecuteNonQuery()

        DBConn.Close()
        'MessageBox.Show("Mechanics Table Populated")
    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: DeleteDatabase            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine deletes entire DB.
    '------------------------------------------------------------
    Sub DeleteDatabase(ByVal strSERVERNAME As String, ByVal strDBNAME As String)

        'This routine shows how to delete a database completely
        'from code.  It does not consider deleting the data from
        'the tables nor dropping the tables -- it just zaps the
        'database completely

        Dim DBConn As SqlConnection
        Dim strSQLCmd As String
        Dim DBCommand As SqlCommand

        'We need to point back at the [Master] database itself
        DBConn = New SqlConnection("Server=" & strSERVERNAME)

        'Try to force single ownership of the database so that we have the
        'permissions to delete it
        strSQLCmd = "ALTER DATABASE [" & strDBNAME & "] SET " &
                    "SINGLE_USER WITH ROLLBACK IMMEDIATE"

        DBCommand = New SqlCommand(strSQLCmd, DBConn)

        Try
            DBConn.Open()
            DBCommand.ExecuteNonQuery()
            'MessageBox.Show("Database set for exclusive use", "",
            'MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

        If (DBConn.State = ConnectionState.Open) Then
            DBConn.Close()
        End If

        'Now drop the database
        strSQLCmd = "DROP DATABASE " & strDBNAME
        DBCommand = New SqlCommand(strSQLCmd, DBConn)

        Try
            DBConn.Open()
            DBCommand.ExecuteNonQuery()
            'MessageBox.Show("Database has been deleted", "", MessageBoxButtons.OK,
            'MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

        If (DBConn.State = ConnectionState.Open) Then
            DBConn.Close()
        End If
    End Sub
End Module
