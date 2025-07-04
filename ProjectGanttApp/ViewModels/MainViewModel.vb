Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class MainViewModel
    Implements INotifyPropertyChanged

    Public Property Tasks As ObservableCollection(Of TaskItem)
    Public Property Projects As ObservableCollection(Of ProjectItem)

    Private _startDate As Date = #2025/07/03#
    Public Property StartDate As Date
        Get
            Return _startDate
        End Get
        Set(value As Date)
            If _startDate <> value Then
                _startDate = value
                OnPropertyChanged()
                OnPropertyChanged(NameOf(DateTicks))
            End If
        End Set
    End Property

    Public ReadOnly Property DateTicks As List(Of Date)
        Get
            Dim result As New List(Of Date)
            Dim current = StartDate
            For i = 0 To 30
                result.Add(current)
                current = current.AddDays(1)
            Next
            Return result
        End Get
    End Property

    Public ReadOnly Property TaskStatuses As Array
        Get
            Return [Enum].GetValues(GetType(TaskStatus))
        End Get
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New()
        Dim dataService As New DataService()
        Tasks = New ObservableCollection(Of TaskItem)(dataService.LoadTasks())
        Projects = New ObservableCollection(Of ProjectItem)(dataService.LoadProjects())
    End Sub

    Protected Sub OnPropertyChanged(<CallerMemberName> Optional prop As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(prop))
    End Sub

    Public Sub AddTask(task As TaskItem)
        task.ID = Tasks.Count + 1
        Tasks.Add(task)
        OnPropertyChanged(NameOf(Tasks))
        OnPropertyChanged(NameOf(DateTicks))
    End Sub
End Class