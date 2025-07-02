Imports System.Collections.ObjectModel
Imports System.ComponentModel

Public Class MainViewModel
    Implements INotifyPropertyChanged

    Public Property Tasks As ObservableCollection(Of TaskItem)
    Public Property Projects As ObservableCollection(Of ProjectItem)

    Private _showIncompleteOnly As Boolean
    Public Property ShowIncompleteOnly As Boolean
        Get
            Return _showIncompleteOnly
        End Get
        Set(value As Boolean)
            _showIncompleteOnly = value
            OnPropertyChanged(NameOf(ShowIncompleteOnly))
            OnPropertyChanged(NameOf(FilteredTasks))
        End Set
    End Property

    Public ReadOnly Property FilteredTasks As IEnumerable(Of TaskItem)
        Get
            If ShowIncompleteOnly Then
                Return Tasks.Where(Function(t) t.Status <> TaskStatus.Completed)
            Else
                Return Tasks
            End If
        End Get
    End Property

    Private dataService As New DataService()

    Public Sub New()
        Tasks = New ObservableCollection(Of TaskItem)(dataService.LoadTasks())
        Projects = New ObservableCollection(Of ProjectItem)(dataService.LoadProjects())
        ShowIncompleteOnly = False
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Protected Sub OnPropertyChanged(prop As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(prop))
    End Sub

    Public Sub AddTask(task As TaskItem)
        task.ID = Tasks.Count + 1
        Tasks.Add(task)
        OnPropertyChanged(NameOf(FilteredTasks))
    End Sub
End Class