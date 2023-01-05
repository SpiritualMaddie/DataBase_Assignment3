using HSH_Db_Assignment3.Data;
using HSH_Db_Assignment3.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HSH_Db_Assignment3
{
    internal class ClassMethods
    {
        private SqlConnection sqlCon = new SqlConnection("Data Source=BLUEBOX; Initial Catalog=HighSchoolHigh; Integrated Security=true");
        internal SqlDataAdapter? sqlDa;
        internal DataTable? dataT;

        // Get list of all Employees (SQL)
        internal void ViewListOfEmployees()
        {     
            sqlDa = new SqlDataAdapter("Select * From Employees", sqlCon);
            dataT = new DataTable();

            sqlDa.Fill(dataT);

            foreach (DataRow drEmp in dataT.Rows)
            {
                Console.Write(drEmp["FirstName"] + ", ");
                Console.Write(drEmp["LastName"] + " - ");
                Console.Write(drEmp["Titel"]);
                Console.WriteLine();
            }
        }
        // Get list of all Employees with specific title (SQL)
        internal void ViewEmployee()
        {
            Console.Clear();
            Console.WriteLine("Skriv in vilken titel du söker efter:\n(Lärare, Administratör, Rektor, Lokalvårdare, Specialpedagog)\n");
            var inputTitle = NullCheck();
            var firstletter = inputTitle.Substring(0, 1).ToUpper();
            var everthingelse = inputTitle.Substring(1).ToLower();
            var title = firstletter + everthingelse;

            sqlDa = new SqlDataAdapter("Select * From Employees Where Titel ='" + title + "'", sqlCon);
            dataT = new DataTable();
            sqlDa.Fill(dataT);

            Console.Clear();
            if (title == "Lärare" | title == "Administratör" | title == "Rektor"
                | title == "Lokalvårdare" | title == "Specialpedagog")
            {
                Console.WriteLine("Titel \t\tNamn");
                foreach (DataRow drEmpTitel in dataT.Rows)
                {
                    Console.Write(drEmpTitel["Titel"] + "\t\t");
                    Console.Write(drEmpTitel["FirstName"] + ", ");
                    Console.Write(drEmpTitel["LastName"]);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Tyvärr denna yrkestitel finns inte på skolan, försök igen genom att trycka Enter.");
                Console.ReadKey();
                ViewEmployee();
            }
        }

        // Adds new employee to the database (Entity)
        // ********************************************HAR INGEN FELHANTERING ******************************
        internal void AddEmployee()
        {
            Application app = new Application();
            Console.Clear();
            Console.WriteLine("Här kan du lägga till en ny anställd.");
            Console.WriteLine("\nPersonnr(ÅÅÅÅMMDD-NNNN):");
            var socialSecNr = Console.ReadLine();
            Console.WriteLine("Förnamn:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Efternamn:");
            var lastName = Console.ReadLine();
            Console.WriteLine("Titel:");
            var empTitle = Console.ReadLine();

            using (var context = new HSHContext())
            {
                var employee = new Employee
                {
                    SocialSecurityNr = socialSecNr,
                    FirstName = firstName,
                    LastName = lastName,
                    Titel = empTitle
                };

                context.Employees.Add(employee);
                context.SaveChanges();
            }
            Console.WriteLine("\nDu har lagt till en ny anställd till databasen!\nTryck Enter för att komma vidare.");
            Console.ReadKey();
            app.Start();
        }

        // Get list of all students (Entity framwork)
        // Sorted by FirstName Ascending
        internal static void ViewStudentsSortFNameAsc()
        {
            using (var context = new HSHContext())
            {
                var myStudents = context.Students.OrderBy(s => s.FirstName);
                Console.WriteLine("StudentID \t Klasskod \t Namn");
                foreach (var students in myStudents)
                {
                    Console.WriteLine($"{students.StudentId} \t\t {students.FkClassCode} \t\t {students.FirstName}, {students.LastName}");
                }
            }
        }
        // Get list of all students (Entity framwork)
        // Sorted by FirstName Descending
        internal static void ViewStudentsSortFNameDesc()
        {
            using (var context = new HSHContext())
            {
                var myStudents = context.Students.OrderByDescending(s => s.FirstName);
                Console.WriteLine("StudentID \t Klasskod \t Namn");
                foreach (var students in myStudents)
                {
                    Console.WriteLine($"{students.StudentId} \t\t {students.FkClassCode} \t\t {students.FirstName}, {students.LastName}");
                }
            }

        }
        // Get list of all students (Entity framwork)
        // Sorted by LastName Ascending
        internal static void ViewStudentsSortLNameAsc()
        {
            using (var context = new HSHContext())
            {
                var myStudents = context.Students.OrderBy(s => s.FirstName);
                Console.WriteLine("StudentID \t Klasskod \t Namn");
                foreach (var students in myStudents)
                {
                    Console.WriteLine($"{students.StudentId} \t\t {students.FkClassCode} \t\t {students.LastName}, {students.FirstName}");
                }
            }
        }
        // Get list of all students (Entity framwork)
        // Sorted by LastName Descending
        internal static void ViewStudentsSortLNameDesc()
        {
            using (var context = new HSHContext())
            {
                var myStudents = context.Students.OrderByDescending(s => s.LastName);
                Console.WriteLine("StudentID \t Klasskod \t Namn");
                foreach (var students in myStudents)
                {
                    Console.WriteLine($"{students.StudentId} \t\t {students.FkClassCode} \t\t {students.LastName}, {students.FirstName}");
                }
            }
        }

        // Adding new Student (SQL)
        // ********************************************HAR INGEN FELHANTERING ******************************
        internal void AddStudents()
        {
            Application app = new Application();
            Console.Clear();
            Console.WriteLine("Här kan du lägga till en ny student.");
            Console.WriteLine("\nPersonnr(ÅÅÅÅMMDD-NNNN):");
            var socialSecNr = Console.ReadLine();
            Console.WriteLine("Förnamn:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Efternamn:");
            var lastName = Console.ReadLine();
            Console.WriteLine("Klasskod:\n(ES20C - Bild, ES21B - Foto/Film, ES22A - Musik)");
            var studClassCode = Console.ReadLine();

            var newStudent = "INSERT INTO Students (SocialSecurityNr, FirstName, LastName, FK_ClassCode) " +
                "VALUES (@socialSecNr, @firstName, @lastName, @studClassCode)";

            using (SqlCommand sqlCom = new SqlCommand(newStudent, sqlCon))
            {
                sqlCom.Parameters.AddWithValue("@socialSecNr", socialSecNr);
                sqlCom.Parameters.AddWithValue("@firstName", firstName);
                sqlCom.Parameters.AddWithValue("@lastName", lastName);
                sqlCom.Parameters.AddWithValue("@studClassCode", studClassCode);

                sqlCon.Open();
                sqlCom.ExecuteNonQuery();
                sqlCon.Close();
            }
            Console.WriteLine("\nDu har lagt till en ny student till databasen!\nTryck Enter för att komma vidare.");
            Console.ReadKey();
            app.Start();

        }

        // Show list of classes and get all students in specific class (Entity)
        internal void ViewClasses()
        {
            Console.Clear();
            using (var context = new HSHContext())
            {
                Application app = new Application();
                
                Console.WriteLine("Här är en lista med alla klasser i High School High\n");
                var myClasses = context.Classes;

                Console.WriteLine("Klasskod\tKlass");
                foreach (var schoolClass in myClasses)
                {
                    Console.WriteLine($"{schoolClass.ClassCode}\t\t{schoolClass.ClassName}");
                }

                Console.WriteLine("\nVänligen skriv in vilken klasskod du vill se studenter från:");
                var inputClassCode = NullCheck().ToUpper();

                if (inputClassCode == "ES20C" | inputClassCode == "ES21B" | inputClassCode == "ES22A")
                {
                    var myStudentsInClass = context.Students
                                .Where(s => s.FkClassCode == inputClassCode);
                    Console.Clear();
                    Console.WriteLine("\nStudentID \t Klasskod \t Namn");
                    foreach (var studentClass in myStudentsInClass)
                    {
                        Console.WriteLine($"{studentClass.StudentId} \t\t {studentClass.FkClassCode} \t\t {studentClass.FirstName}, {studentClass.LastName}");
                    }
                }
                else
                {
                    Console.WriteLine("\nTyvärr klassen du har valt finns inte, tryck Enter för att försöka igen.");
                    Console.ReadKey();
                    ViewClasses();
                }
                app.PromptAfterList();
                app.PromptClass();
            }
        }

        // Get all grades set in the past month (SQL)
        internal void AllGradesLastMonth()
        {
            var currentDate = DateTime.Now;
            var lastMonth = currentDate.AddMonths(-1);
            var gradeInfo = @"
                        SELECT * FROM GradeLists
                        JOIN Students ON StudentID = GradeLists.FK_StudentID
                        JOIN Courses ON CourseCode = GradeLists.FK_CourseCode
                        WHERE GradeDate > @lastMonth";
            var command = new SqlCommand(gradeInfo, sqlCon);

            command.Parameters.AddWithValue("@lastMonth", lastMonth);
            sqlDa = new SqlDataAdapter(command);
            dataT = new DataTable();

            sqlDa.Fill(dataT);

            Console.Clear();
            Console.WriteLine("\nKurs \t\t\tBetyg \tDatum \t\t\tStudent");
            foreach (DataRow drGrades in dataT.Rows)
            {
                Console.Write(drGrades["CourseName"] + "\t");
                Console.Write(drGrades["Grade"] + "\t");
                Console.Write(drGrades["GradeDate"] + "\t");
                Console.Write(drGrades["FirstName"] + ", ");
                Console.Write(drGrades["LastName"]);
                Console.WriteLine();
            }
        }

        // List of average, highest and lowest grade grouped by course (SQL)
        internal void AverageGradeHighLow()
        {
            Console.Clear();
            var averageGrade = @"
                        SELECT CourseName, ROUND(AVG(CASE 
                                WHEN Grade = 'A' THEN 5
                                WHEN Grade = 'B' THEN 4
                                WHEN Grade = 'C' THEN 3
                                WHEN Grade = 'D' THEN 2
                                WHEN Grade = 'E' THEN 1
                                WHEN Grade = 'F' THEN 0
                                ELSE 0.0 END), 1) AS AverageGrade, 
                        MIN(Grade) AS HighGrade, MAX(Grade) AS LowGrade
                        FROM GradeLists
                        JOIN Students ON StudentID = GradeLists.FK_StudentID
                        JOIN Courses ON CourseCode = GradeLists.FK_CourseCode
                        GROUP BY CourseName, CourseName";
            var command = new SqlCommand(averageGrade, sqlCon);

            sqlDa = new SqlDataAdapter(command);
            dataT = new DataTable();

            sqlDa.Fill(dataT);
            Console.WriteLine(@"
Betygen kommer att numreras för att få fram ett snittvärde
(A = 5, B = 4, C = 3, D = 2, E = 1, F = 0)");
            Console.WriteLine("\nSnittbetyg \tLängsta/högsta betyget \tKursnamn");
            foreach (DataRow drGradesA in dataT.Rows)
            {
                Console.Write(drGradesA["AverageGrade"] + "\t");
                Console.Write(drGradesA["LowGrade"] + "  /  ");
                Console.Write(drGradesA["HighGrade"] + "\t\t\t");
                Console.Write(drGradesA["CourseName"]);
                Console.WriteLine();
            }
        }

        internal static string NullCheck()
        {
            while (true)
            {
                var userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput) | string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Du måste skriva något, försök igen");
                }
                else
                {
                    return userInput;
                }
            }
        }
        internal void Exit()
        {
            Console.Clear();
            Console.WriteLine("Du har valt att avsluta \nAppen stängs.");
            Thread.Sleep(2000);
            Environment.Exit(0);
        }
    }
}
