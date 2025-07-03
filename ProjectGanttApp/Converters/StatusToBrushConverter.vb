Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Media

Public Class StatusToBrushConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        Select Case CType(value, TaskStatus)
            Case TaskStatus.NotStarted
                Return Brushes.MediumOrchid
            Case TaskStatus.InProgress
                Return Brushes.SteelBlue
            Case TaskStatus.Completed
                Return Brushes.LightGreen
        End Select
        Return Brushes.Gray
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class