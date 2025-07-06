Imports Microsoft.Data.Sqlite
Imports System.IO

Public Class DataService
    Private ReadOnly dbPath As String =
                                        System.IO.Path.Combine(
                                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                            "ProjectGanttApp",
                                            "tasks.db"
                                        )


    Public Sub New()
        ' AppData内の保存フォルダを確保
        Dim dir = Path.GetDirectoryName(dbPath)
        If Not Directory.Exists(dir) Then
            Directory.CreateDirectory(dir)
        End If

        EnsureDatabase()
    End Sub

    Public Sub EnsureDatabase()
        If Not File.Exists(dbPath) Then
            Using conn = New SqliteConnection($"Data Source={dbPath}")
                conn.Open()
                Dim cmd = conn.CreateCommand()

                ' テーブル1つ目：Tasks
                cmd.CommandText = "
                CREATE TABLE Tasks (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    StartDate TEXT,
                    EndDate TEXT,
                    ProjectID TEXT,
                    Status INTEGER
                );"
                cmd.ExecuteNonQuery()

                ' テーブル2つ目：Projects
                cmd.CommandText = "
                CREATE TABLE Projects (
                    ID TEXT PRIMARY KEY,
                    Name TEXT,
                    StartDate TEXT,
                    EndDate TEXT
                );"
                cmd.ExecuteNonQuery()
            End Using
        End If
    End Sub

    Public Function LoadTasks() As List(Of TaskItem)
        Dim list As New List(Of TaskItem)
        Using conn = New SqliteConnection($"Data Source={dbPath}")
            conn.Open()
            Dim cmd = conn.CreateCommand()
            cmd.CommandText = "SELECT ID, Name, StartDate, EndDate, ProjectID, Status FROM Tasks"
            Using reader = cmd.ExecuteReader()
                While reader.Read()
                    list.Add(New TaskItem With {
                        .ID = reader.GetInt32(0),
                        .Name = reader.GetString(1),
                        .StartDate = Date.Parse(reader.GetString(2)),
                        .EndDate = Date.Parse(reader.GetString(3)),
                        .ProjectID = Guid.Parse(reader.GetString(4)),
                        .Status = CType(reader.GetInt32(5), TaskStatus)
                    })
                End While
            End Using
        End Using
        Return list
    End Function

    Public Function LoadProjects() As List(Of ProjectItem)
        Dim list As New List(Of ProjectItem)
        Using conn = New SqliteConnection($"Data Source={dbPath}")
            conn.Open()
            Dim cmd = conn.CreateCommand()
            cmd.CommandText = "SELECT ID, Name, StartDate, EndDate FROM Projects"
            Using reader = cmd.ExecuteReader()
                While reader.Read()
                    list.Add(New ProjectItem With {
                        .ID = Guid.Parse(reader.GetString(0)),
                        .Name = reader.GetString(1),
                        .StartDate = Date.Parse(reader.GetString(2)),
                        .EndDate = Date.Parse(reader.GetString(3))
                    })
                End While
            End Using
        End Using
        Return list
    End Function

    Public Sub AddTask(task As TaskItem)
        Using conn = New SqliteConnection($"Data Source={dbPath}")
            conn.Open()
            Dim cmd = conn.CreateCommand()
            cmd.CommandText = "INSERT INTO Tasks (Name, StartDate, EndDate, ProjectID, Status) 
                               VALUES ($name, $start, $end, $project, $status)"
            cmd.Parameters.AddWithValue("$name", task.Name)
            cmd.Parameters.AddWithValue("$start", task.StartDate.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("$end", task.EndDate.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("$project", task.ProjectID.ToString())
            cmd.Parameters.AddWithValue("$status", CInt(task.Status))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub UpdateTask(task As TaskItem)
        Using conn = New SqliteConnection($"Data Source={dbPath}")
            conn.Open()
            Dim cmd = conn.CreateCommand()
            cmd.CommandText = "
                                UPDATE Tasks 
                                SET Name = $name,
                                    StartDate = $start,
                                    EndDate = $end,
                                    ProjectID = $project,
                                    Status = $status
                                WHERE ID = $id
                              "
            cmd.Parameters.AddWithValue("$id", task.ID)
            cmd.Parameters.AddWithValue("$name", task.Name)
            cmd.Parameters.AddWithValue("$start", task.StartDate.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("$end", task.EndDate.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("$project", task.ProjectID.ToString())
            cmd.Parameters.AddWithValue("$status", CInt(task.Status))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

End Class
