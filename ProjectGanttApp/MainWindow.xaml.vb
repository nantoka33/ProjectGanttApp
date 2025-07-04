Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Shapes
Imports System.Windows.Media

Class MainWindow
    Private viewModel As New MainViewModel()
    Private Const DayWidth As Double = 40.0
    Private Const RowHeight As Double = 30.0

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        DataContext = viewModel
        DrawDateTicks()
        DrawGanttBars()
    End Sub

    Private Sub DatePicker_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs)
        If StartDatePicker.SelectedDate.HasValue Then
            viewModel.StartDate = StartDatePicker.SelectedDate.Value
            DrawDateTicks()
            DrawGanttBars()
        End If
    End Sub

    Private Sub DrawDateTicks()
        If TickCanvas Is Nothing Then Return
        TickCanvas.Children.Clear()

        Dim left As Double = 0

        For Each dt As Date In viewModel.DateTicks
            Dim border As New Border With {
                .Width = DayWidth,
                .Height = 30,
                .BorderBrush = Brushes.Gray,
                .BorderThickness = New Thickness(0.5)
            }

            Dim label As New TextBlock With {
                .Text = dt.ToString("M/d"),
                .FontSize = 10,
                .HorizontalAlignment = HorizontalAlignment.Center,
                .VerticalAlignment = VerticalAlignment.Center
            }

            border.Child = label
            Canvas.SetLeft(border, left)
            TickCanvas.Children.Add(border)

            left += DayWidth
        Next
    End Sub

    Private Sub DrawGanttBars()
        If GanttCanvas Is Nothing Then Return
        GanttCanvas.Children.Clear()

        Dim tasks = viewModel.Tasks.ToList()
        For i = 0 To tasks.Count - 1
            Dim task = tasks(i)
            Dim startOffset = (task.StartDate - viewModel.StartDate).TotalDays
            Dim duration = (task.EndDate - task.StartDate).TotalDays + 1

            Dim barLeft = startOffset * DayWidth
            Dim barWidth = Math.Max(duration * DayWidth, 10)

            Dim color As Brush = Brushes.LightGray
            Select Case task.Status
                Case TaskStatus.NotStarted
                    color = Brushes.MediumOrchid
                Case TaskStatus.InProgress
                    color = Brushes.SteelBlue
                Case TaskStatus.Completed
                    color = Brushes.LightGreen
            End Select

            Dim rect As New Rectangle With {
                .Width = barWidth,
                .Height = RowHeight - 4,
                .Fill = color,
                .Stroke = Brushes.Black,
                .StrokeThickness = 0.5
            }

            Canvas.SetLeft(rect, barLeft)
            Canvas.SetTop(rect, i * RowHeight)
            GanttCanvas.Children.Add(rect)

            Dim label As New TextBlock With {
                .Text = task.Name,
                .FontSize = 10,
                .Foreground = Brushes.Black
            }
            Canvas.SetLeft(label, barLeft + 4)
            Canvas.SetTop(label, i * RowHeight + 4)
            GanttCanvas.Children.Add(label)
        Next

        GanttCanvas.Height = RowHeight * tasks.Count
    End Sub

    Private Sub UpdateButton_Click(sender As Object, e As RoutedEventArgs)
        DrawDateTicks()
        DrawGanttBars()
    End Sub

End Class
