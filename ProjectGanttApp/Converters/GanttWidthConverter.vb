Imports System.Globalization
Imports System.Windows.Data

Public Class GanttWidthConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        Dim task = TryCast(value, TaskItem)
        If task IsNot Nothing Then
            Return Math.Max((task.EndDate - task.StartDate).TotalDays * 20, 40)
        End If
        Return 40
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class