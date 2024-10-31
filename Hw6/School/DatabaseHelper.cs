using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite; // 确保已经安装了System.Data.SQLite NuGet包

namespace SchoolService
{
    public static class DatabaseHelper
    {
        // 获取连接字符串
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SchoolConnectionString"].ConnectionString;
        }

        public static DataTable GetAllSchools()
        {
            DataTable dataTable = new DataTable();
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "SELECT * FROM Schools"; // 查询所有学校
                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader); // 将查询结果加载到DataTable
                    }
                }
            }
            return dataTable;
        }

        public static DataTable GetAllClasses()
        {
            DataTable dataTable = new DataTable();
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "SELECT Classes.ClassId, Classes.ClassName, Schools.SchoolName " +
                              "FROM Classes " +
                              "JOIN Schools ON Classes.SchoolId = Schools.SchoolId"; // 假设你想显示每个班级所属的学校名称
                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader); // 将查询结果加载到DataTable
                    }
                }
            }
            return dataTable;
        }


        // 创建数据库和表
        public static void CreateDatabaseAndTables()
        {
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                // 创建学校表
                string createSchoolsTable = @"
                CREATE TABLE IF NOT EXISTS Schools (
                    SchoolId INTEGER PRIMARY KEY AUTOINCREMENT,
                    SchoolName TEXT NOT NULL
                );";
                // 创建班级表
                string createClassesTable = @"
                CREATE TABLE IF NOT EXISTS Classes (
                    ClassId INTEGER PRIMARY KEY AUTOINCREMENT,
                    ClassName TEXT NOT NULL,
                    SchoolId INTEGER,
                    FOREIGN KEY (SchoolId) REFERENCES Schools(SchoolId)
                );";
                // 创建学生表
                string createStudentsTable = @"
                CREATE TABLE IF NOT EXISTS Students (
                    StudentId INTEGER PRIMARY KEY AUTOINCREMENT,
                    StudentName TEXT NOT NULL,
                    ClassId INTEGER,
                    FOREIGN KEY (ClassId) REFERENCES Classes(ClassId)
                );";
                // 创建日志表
                string createLogsTable = @"
                CREATE TABLE IF NOT EXISTS Logs (
                    LogId INTEGER PRIMARY KEY AUTOINCREMENT,
                    OperationType TEXT NOT NULL,
                    OperationDescription TEXT NOT NULL,
                    OperationTime TEXT NOT NULL
                );";

                using (var command = new SQLiteCommand(createSchoolsTable, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createClassesTable, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createStudentsTable, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createLogsTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // 添加学校
        public static int AddSchool(string schoolName)
        {
            int schoolId = 0;
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "INSERT INTO Schools (SchoolName) VALUES (@SchoolName); SELECT last_insert_rowid();";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@SchoolName", schoolName);
                    int result = command.ExecuteNonQuery();
                    schoolId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return schoolId;
        }

        // 添加班级
        public static int AddClass(string className, int schoolId)
        {
            int classId = 0;
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "INSERT INTO Classes (ClassName, SchoolId) VALUES (@ClassName, @SchoolId); SELECT last_insert_rowid();";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ClassName", className);
                    command.Parameters.AddWithValue("@SchoolId", schoolId);
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        classId = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            return classId;
        }
        public static DataTable GetAllStudents()
        {
            DataTable dataTable = new DataTable();
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "SELECT Students.StudentId, Students.StudentName, Classes.ClassName " +
                              "FROM Students " +
                              "JOIN Classes ON Students.ClassId = Classes.ClassId"; // 假设你想显示每个学生所属的班级名称
                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader); // 将查询结果加载到DataTable
                    }
                }
            }
            return dataTable;
        }
        public static int UpdateClassName(int classId, string newClassName)
        {
            int affectedRows = 0;
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "UPDATE Classes SET ClassName = @ClassName WHERE ClassId = @ClassId";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ClassName", newClassName);
                    command.Parameters.AddWithValue("@ClassId", classId);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }
        // 添加学生
        public static int AddStudent(string studentName, int classId)
        {
            int studentId = 0;
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "INSERT INTO Students (StudentName, ClassId) VALUES (@StudentName, @ClassId); SELECT last_insert_rowid();";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@StudentName", studentName);
                    command.Parameters.AddWithValue("@ClassId", classId);
                    int result = command.ExecuteNonQuery();
                    studentId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return studentId;
        }

        public static int DeleteStudent(int studentId)
        {
            int affectedRows = 0;
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "DELETE FROM Students WHERE StudentId = @StudentId";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentId);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }

        public static int UpdateStudentName(int studentId, string newStudentName)
        {
            int affectedRows = 0;
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "UPDATE Students SET StudentName = @StudentName WHERE StudentId = @StudentId";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@StudentName", newStudentName);
                    command.Parameters.AddWithValue("@StudentId", studentId);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }

        // 记录日志
        public static void LogOperation(string operationType, string operationDescription)
        {
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "INSERT INTO Logs (OperationType, OperationDescription, OperationTime) VALUES (@OperationType, @OperationDescription, @OperationTime)";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@OperationType", operationType);
                    command.Parameters.AddWithValue("@OperationDescription", operationDescription);
                    command.Parameters.AddWithValue("@OperationTime", DateTime.Now.ToString());
                    command.ExecuteNonQuery();
                }
            }
        }
        public static int DeleteSchool(int schoolId)
        {
            int affectedRows = 0;
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "DELETE FROM Schools WHERE SchoolId = @SchoolId";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@SchoolId", schoolId);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }
        public static int DeleteClass(int classId)
        {
            int affectedRows = 0;
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "DELETE FROM Classes WHERE ClassId = @ClassId";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ClassId", classId);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }
        public static int UpdateSchoolName(int schoolId, string newSchoolName)
        {
            int affectedRows = 0;
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "UPDATE Schools SET SchoolName = @SchoolName WHERE SchoolId = @SchoolId";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@SchoolName", newSchoolName);
                    command.Parameters.AddWithValue("@SchoolId", schoolId);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }
        public static DataTable GetAllLogs()
        {
            DataTable dataTable = new DataTable();
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "SELECT * FROM Logs"; // 查询所有日志
                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader); // 将查询结果加载到DataTable
                    }
                }
            }
            return dataTable;
        }
        public static void InsertLog(string operationType, string operationDescription)
        {
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                string sql = "INSERT INTO Logs (OperationType, OperationDescription, OperationTime) VALUES (@OperationType, @OperationDescription, @OperationTime)";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@OperationType", operationType);
                    command.Parameters.AddWithValue("@OperationDescription", operationDescription);
                    command.Parameters.AddWithValue("@OperationTime", DateTime.Now); // 使用参数化查询防止SQL注入
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}