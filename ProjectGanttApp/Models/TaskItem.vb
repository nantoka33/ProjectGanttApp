Public Enum TaskStatus
    NotStarted
    InProgress
    Completed
End Enum

Public Class TaskItem
    Public Property ID As Integer
    Public Property Name As String
    Public Property StartDate As Date
    Public Property EndDate As Date
    Public Property ProjectID As Guid
    Public Property Status As TaskStatus = TaskStatus.NotStarted
End Class