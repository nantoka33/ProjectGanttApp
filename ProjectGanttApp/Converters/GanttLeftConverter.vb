Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows

Public Class GanttLeftConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        Dim task = TryCast(value, TaskItem)
        If task IsNot Nothing Then
            Return (task.StartDate - Date.Today).TotalDays * 20
        End If
        Return 0
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class