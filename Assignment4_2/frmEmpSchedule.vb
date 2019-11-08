
'------------------------------------------------------------
'-                File Name : frmEmpSchedule.vb                    - 
'-                Part of Project: Assign4                  -
'------------------------------------------------------------
'-                Written By: Tajbid Hasib                  -
'-                Written On: 10/15/2019                    -
'------------------------------------------------------------
'- File Purpose:                                            -
'- This is schedule form for Turbo Auto Service
'------------------------------------------------------------
'- Global Variable Dictionary:             -
'- (none)
'------------------------------------------------------------
'------------------------------------------------------------
Public Class frmEmpSchedule
    Const fileName As String = "frmEmpSchedule.vb"
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        PrintForm1.PrintAction = Printing.PrintAction.PrintToPreview
        PrintForm1.Print()

    End Sub

End Class