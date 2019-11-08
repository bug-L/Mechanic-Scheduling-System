
'------------------------------------------------------------
'-                File Name : Mechanic.vb                    - 
'-                Part of Project: Assign4                  -
'------------------------------------------------------------
'-                Written By: Tajbid Hasib                  -
'-                Written On: 10/15/2019                    -
'------------------------------------------------------------
'- File Purpose:                                            -
'- This is the mechanic class that holds information about
'- each mechanic
'------------------------------------------------------------
'- Global Variable Dictionary:             -
'- (none)
'------------------------------------------------------------

Public Class Mechanic
    Const fileName As String = "Mechanic.vb"

    Private strID As String
    Private strName As String
    Private intRate As Integer
    Private intWeekNumber As Integer
    Private dblHoursWorked(51) As Double
    Private dayMon(51) As Day
    Private dayTue(51) As Day
    Private dayWed(51) As Day
    Private dayThu(51) As Day
    Private dayFri(51) As Day

    '------------------------------------------------------------
    '-      Subprogram Name: New            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine creates new mechanic object.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (none)
    '------------------------------------------------------------

    Public Sub New()
        setID("")
        setName("")
        setRate(0)
        intWeekNumber = 0
        setHoursWorked(intWeekNumber, 0)
        addWeek(intWeekNumber)

    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: New            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine creates new mechanic object.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- newID - ID of mechanic
    '- newName - name of mechanic
    '- newRate - hourly rate of mechanic
    '------------------------------------------------------------
    Public Sub New(ByVal newID As String, ByVal newName As String, ByVal newRate As Integer)
        setID(newID)
        setName(newName)
        setRate(newRate)
        intWeekNumber = 0
        setHoursWorked(intWeekNumber, 0)
        addWeek(intWeekNumber)

    End Sub

    '------------------------------------------------------------
    '-      Subprogram Name: addWeek            -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine adds days to a week.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- inputWeekNumber - week number
    '------------------------------------------------------------
    Private Sub addWeek(inputWeekNumber)
        dayMon(inputWeekNumber) = New Day("Monday", inputWeekNumber)
        dayTue(inputWeekNumber) = New Day("Tuesday", inputWeekNumber)
        dayWed(inputWeekNumber) = New Day("Wednesday", inputWeekNumber)
        dayThu(inputWeekNumber) = New Day("Thursday", inputWeekNumber)
        dayFri(inputWeekNumber) = New Day("Friday", inputWeekNumber)
    End Sub

    '------------------------------------------------------------
    '-      Function Name: getAvailableDayTimeWeek            -
    '------------------------------------------------------------
    '- Function Returns:                                      -
    '-                                                          -
    '- This function returns available day and time.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- inputHours - hours for job
    '------------------------------------------------------------
    Public Function getAvailableDayTimeWeek(ByVal inputHours As Double)

        Dim arrDayTime(2) As Double

        For i As Integer = 0 To intWeekNumber
            If dayMon(i).getHoursRemaining >= inputHours Then
                arrDayTime(0) = 1
                arrDayTime(1) = dayMon(i).getCurrentTime()
                arrDayTime(2) = i
                Exit For

            ElseIf dayTue(i).getHoursRemaining >= inputHours Then
                arrDayTime(0) = 2
                arrDayTime(1) = dayTue(i).getCurrentTime()
                arrDayTime(2) = i
                Exit For

            ElseIf dayWed(i).getHoursRemaining >= inputHours Then
                arrDayTime(0) = 3
                arrDayTime(1) = dayWed(i).getCurrentTime()
                arrDayTime(2) = i
                Exit For

            ElseIf dayThu(i).getHoursRemaining >= inputHours Then
                arrDayTime(0) = 4
                arrDayTime(1) = dayThu(i).getCurrentTime()
                arrDayTime(2) = i
                Exit For

            ElseIf dayFri(i).getHoursRemaining >= inputHours Then
                arrDayTime(0) = 5
                arrDayTime(1) = dayFri(i).getCurrentTime()
                arrDayTime(2) = i
                Exit For


            Else
                'NO Available time this week, check if the next week exist, otherwise create one
                If dayMon(i + 1) Is Nothing Then
                    intWeekNumber += 1
                    addWeek(intWeekNumber)
                    'Since new week is created, first available day is 1
                    arrDayTime(0) = 1
                    arrDayTime(1) = dayMon(intWeekNumber).getCurrentTime
                    arrDayTime(2) = intWeekNumber

                End If

                'next week exists, so do nothing 

            End If
        Next


        Return arrDayTime

    End Function

    '------------------------------------------------------------
    '-      Function Name: addToSchedule            -
    '------------------------------------------------------------
    '- Function Returns:                                      -
    '-                                                          -
    '- This function returns array of day, week, times.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- inputHours - hours for job
    '------------------------------------------------------------
    Public Function addToSchedule(ByVal inputHours As Double)

        Dim arrDayTimes(3) As String

        For i As Integer = 0 To intWeekNumber
            If dayMon(i).getHoursRemaining >= inputHours Then
                arrDayTimes = dayMon(i).addJob(inputHours)
                dblHoursWorked(i) += inputHours
                Exit For
            ElseIf dayTue(i).getHoursRemaining >= inputHours Then
                arrDayTimes = dayTue(i).addJob(inputHours)
                dblHoursWorked(i) += inputHours
                Exit For
            ElseIf dayWed(i).getHoursRemaining >= inputHours Then
                arrDayTimes = dayWed(i).addJob(inputHours)
                dblHoursWorked(i) += inputHours
                Exit For
            ElseIf dayThu(i).getHoursRemaining >= inputHours Then
                arrDayTimes = dayThu(i).addJob(inputHours)
                dblHoursWorked(i) += inputHours
                Exit For
            ElseIf dayFri(i).getHoursRemaining >= inputHours Then
                arrDayTimes = dayFri(i).addJob(inputHours)
                dblHoursWorked(i) += inputHours
                Exit For
            End If
        Next

        Return arrDayTimes

    End Function

    '------------------------------------------------------------
    '-      SubProgram Group: Setters and getters-
    '------------------------------------------------------------
    '- Purpose:                                      -
    '-                                                          -
    '- Set attributes
    '- return attributes
    '------------------------------------------------------------
    Public Sub setID(ByVal inputID As String)
        strID = inputID
    End Sub

    Public Function getID()
        Return strID
    End Function

    Public Sub setName(ByVal inputName As String)
        strName = inputName
    End Sub

    Public Function getName()
        Return strName
    End Function

    Public Sub setRate(ByVal inputRate As Integer)
        intRate = inputRate
    End Sub

    Public Function getRate()
        Return intRate
    End Function

    Public Sub setHoursWorked(ByVal weekNumber As Integer, ByVal inputHoursWorked As Double)
        dblHoursWorked(weekNumber) = inputHoursWorked
    End Sub

    Public Function getHoursWorked(ByVal weekNumber As Integer)
        Return dblHoursWorked(weekNumber)
    End Function

End Class
