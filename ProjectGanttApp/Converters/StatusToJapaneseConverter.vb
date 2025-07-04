Imports System.Globalization
Imports System.Windows.Data

Public Class StatusToJapaneseConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        Dim status = CType(value, TaskStatus)
        Select Case status
            Case TaskStatus.NotStarted
                Return "未着手"
            Case TaskStatus.InProgress
                Return "進行中"
            Case TaskStatus.Completed
                Return "完了"
            Case Else
                Return "不明"
        End Select
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        ' コンボボックスで逆変換したい場合はこちらも実装する
        Select Case value.ToString()
            Case "未着手"
                Return TaskStatus.NotStarted
            Case "進行中"
                Return TaskStatus.InProgress
            Case "完了"
                Return TaskStatus.Completed
            Case Else
                Return TaskStatus.NotStarted
        End Select
    End Function
End Class
