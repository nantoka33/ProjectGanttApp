Imports System.Windows
Imports ProjectGanttApp

Class MainWindow
    Private viewModel As New MainViewModel()

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        DataContext = viewModel
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
