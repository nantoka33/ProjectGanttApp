Public Class DataService
    Public Function LoadTasks() As List(Of TaskItem)
        Return New List(Of TaskItem) From {
            New TaskItem With {.ID = 1, .Name = "設計", .StartDate = Date.Today, .EndDate = Date.Today.AddDays(3), .ProjectID = 1, .Status = TaskStatus.NotStarted},
            New TaskItem With {.ID = 2, .Name = "実装", .StartDate = Date.Today.AddDays(1), .EndDate = Date.Today.AddDays(5), .ProjectID = 1, .Status = TaskStatus.InProgress},
            New TaskItem With {.ID = 3, .Name = "レビュー", .StartDate = Date.Today.AddDays(4), .EndDate = Date.Today.AddDays(6), .ProjectID = 1, .Status = TaskStatus.Completed}
        }
    End Function

    Public Function LoadProjects() As List(Of ProjectItem)
        Return New List(Of ProjectItem) From {
            New ProjectItem With {.ID = 1, .Name = "開発プロジェクト", .StartDate = Date.Today, .EndDate = Date.Today.AddDays(10)}
        }
    End Function
End Class