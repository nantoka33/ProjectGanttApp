Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Shapes
Imports System.Windows.Media

Class MainWindow
    Private viewModel As New MainViewModel()
    Private Const DayWidth As Double = 20.0

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        DataContext = viewModel
        AddHandler viewModel.PropertyChanged, AddressOf OnViewModelPropertyChanged
        DrawGanttChart()
    End Sub

    Private Sub OnViewModelPropertyChanged(sender As Object, e As ComponentModel.PropertyChangedEventArgs)
        If e.PropertyName = NameOf(viewModel.FilteredTasks) Then
            DrawGanttChart()
        End If
    End Sub

    Private Sub DrawGanttChart()
        GanttArea.Children.Clear()
        Dim rowHeight As Double = 30
        Dim i As Integer = 0
        For Each task In viewModel.FilteredTasks
            Dim left = (task.StartDate - Date.Today).TotalDays * DayWidth
            Dim width = (task.EndDate - task.StartDate).TotalDays * DayWidth

            Dim fillBrush As Brush = Brushes.SkyBlue
            Select Case task.Status
                Case TaskStatus.NotStarted
                    fillBrush = Brushes.MediumOrchid
                Case TaskStatus.InProgress
                    fillBrush = Brushes.SteelBlue
                Case TaskStatus.Completed
                    fillBrush = Brushes.LightGreen
            End Select

            Dim rect As New Rectangle With {
                .Width = width,
                .Height = rowHeight - 5,
                .Fill = fillBrush,
                .Stroke = Brushes.Black,
                .StrokeThickness = 1,
                .Tag = task
            }
            Canvas.SetLeft(rect, left)
            Canvas.SetTop(rect, i * rowHeight)
            GanttArea.Children.Add(rect)

            Dim label As New TextBlock With {
                .Text = task.Status.ToString(),
                .Foreground = Brushes.Black
            }
            Canvas.SetLeft(label, left + 5)
            Canvas.SetTop(label, i * rowHeight)
            GanttArea.Children.Add(label)

            i += 1
        Next
    End Sub

    Private Sub AddTask_Click(sender As Object, e As RoutedEventArgs)
        Dim newTask As New TaskItem With {
            .Name = "新タスク",
            .StartDate = Date.Today.AddDays(1),
            .EndDate = Date.Today.AddDays(3),
            .ProjectID = 1,
            .Status = TaskStatus.NotStarted
        }
        viewModel.AddTask(newTask)
    End Sub
End Class
