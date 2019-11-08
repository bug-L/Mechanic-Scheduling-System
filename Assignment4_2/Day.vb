'------------------------------------------------------------
'-                File Name : Day.vb                    - 
'-                Part of Project: Assign4                  -
'------------------------------------------------------------
'-                Written By: Tajbid Hasib                  -
'-                Written On: 10/15/2019                    -
'------------------------------------------------------------
'- File Purpose:                                            -
'- This is the Day class that holds information about
'- each mechanic
'------------------------------------------------------------
'- Global Variable Dictionary:             -
'- (none)
'------------------------------------------------------------

Public Class Day

    Const fileName As String = "Day.vb"
    Private strDayName As String
    Private dblHoursRemaining As Double
    Private dblCurrentTime As Double
    Private dictTime As Dictionary(Of Double, String)
    Private weekNumber As Integer

    Public Sub New()
        setDayName("")
        setHoursReamining(0)
        dblCurrentTime = 0  'Clock Time
        initializeDictionary()
        weekNumber = 0
    End Sub

    Public Sub New(ByVal newDayName As String, ByVal newWeekNumber As Integer)
        strDayName = newDayName
        setHoursReamining(8)
        dblCurrentTime = 8 'Clock Time
        initializeDictionary()
        weekNumber = newWeekNumber
    End Sub

    '------------------------------------------------------------
    '-      Function Name: addJob            -
    '------------------------------------------------------------
    '- Function Returns:                                      -
    '-                                                          -
    '- This function adds job to day.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- inputHours - hours for job
    '------------------------------------------------------------
    Public Function addJob(ByVal inputHours As Double)
        dblHoursRemaining = dblHoursRemaining - inputHours

        Dim strTimes(3) As String

        'Day number
        strTimes(0) = strDayName

        'start time
        strTimes(1) = dictTime.Item(dblCurrentTime)

        dblCurrentTime += inputHours

        'end time
        strTimes(2) = dictTime.Item(dblCurrentTime)
        'IF end time is 1 PM, make it 12PM
        If strTimes(2) = "01.00PM" Then
            strTimes(2) = "12.00PM"
        End If

        strTimes(3) = weekNumber + 1

        Return strTimes
    End Function

    '------------------------------------------------------------
    '-      Subprogram Name: initializeDictionary          -
    '------------------------------------------------------------
    '- Subprogram Purpose:                                      -
    '-                                                          -
    '- This subroutine initalizes dictionary.
    '------------------------------------------------------------
    '- Parameter Dictionary (in parameter order):               -
    '- (none)
    '------------------------------------------------------------
    Public Sub initializeDictionary()

        dictTime = New Dictionary(Of Double, String)
        ' Add entries to dictionary.
        dictTime.Add(8.0, "08.00AM")
        dictTime.Add(8.5, "08.30AM")
        dictTime.Add(9.0, "09.00AM")
        dictTime.Add(9.5, "09.30AM")
        dictTime.Add(10.0, "10.00AM")
        dictTime.Add(10.5, "10.30AM")
        dictTime.Add(11.0, "11.00AM")
        dictTime.Add(11.5, "11.30AM")
        dictTime.Add(12.0, "01.00PM")
        dictTime.Add(12.5, "01.30PM")
        dictTime.Add(13.0, "02.00PM")
        dictTime.Add(13.5, "02.30PM")
        dictTime.Add(14.0, "03.00PM")
        dictTime.Add(14.5, "03.30PM")
        dictTime.Add(15.0, "04.00PM")
        dictTime.Add(15.5, "04.30PM")
        dictTime.Add(16.0, "05.00PM")

    End Sub

    '------------------------------------------------------------
    '-      SubProgram Group: Setters and getters-
    '------------------------------------------------------------
    '- Purpose:                                      -
    '-                                                          -
    '- Set attributes
    '- return attributes
    '------------------------------------------------------------

    Public Sub setDayName(ByVal inputDayName As String)
        strDayName = inputDayName
    End Sub

    Public Function getDayName()
        Return strDayName
    End Function

    Public Sub setHoursReamining(ByVal inputHoursRemaining As Double)
        dblHoursRemaining = inputHoursRemaining
    End Sub

    Public Function getHoursRemaining()
        Return dblHoursRemaining
    End Function

    Public Sub setCurrentTime(ByVal inputCurrentTime As Double)
        dblCurrentTime = inputCurrentTime
    End Sub

    Public Function getCurrentTime()
        Return dblCurrentTime
    End Function


End Class
