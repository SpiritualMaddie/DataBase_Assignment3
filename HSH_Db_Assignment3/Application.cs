using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSH_Db_Assignment3
{
    internal class Application
    {
        internal void Start()
        {
            Console.Title = "High School High - app";
            string prompt = $"Välkommen till High School Highs applikation.\n(Använd pilarna för att navigera menyn och gör val med Enter.)\n";
            string[] options = {
                "Visa personal", "Lägg till personal", "Visa studenter", "Lägg till student", "Visa klasser",
                "Alla betyg satta senaste månaden", "Snittbetyg och högsta vs lägsta", "Avsluta" };
            Menu menu = new Menu(prompt, options);
            int menuSelect = menu.Run();
            ClassMethods classMethods = new ClassMethods();

            switch (menuSelect)
            {
                case 0:
                    ViewEmployees();
                    break;
                case 1:
                    classMethods.AddEmployee();
                    break;
                case 2:
                    ViewStudents();
                    break;
                case 3:
                    classMethods.AddStudents();
                    break;
                case 4:
                    classMethods.ViewClasses();
                    break;
                case 5:
                    classMethods.AllGradesLastMonth();
                    PromptAfterList();
                    Start();
                    break;
                case 6:
                    classMethods.AverageGradeHighLow();
                    PromptAfterList();
                    Start();
                    break;
                case 7:
                    classMethods.Exit();
                    break;
            }
        }
        internal void ViewStudents()
        {
            string prompt = $"Välj om du vill sortera studenterna efter för eller efternamn i stigande eller fallande ordning.\n";
            string[] options = {
                "Förnamn (A-Ö)", "Förnamn (Ö-A)",
                "Efternamn (A-Ö)", "Efternamn (Ö-A)", "Huvudmeny" };
            Menu menu = new Menu(prompt, options);
            int menuSelect = menu.Run();

            switch (menuSelect)
            {
                case 0:
                    Console.Clear();
                    ClassMethods.ViewStudentsSortFNameAsc();
                    PromptAfterList();
                    PromptSort();
                    break;
                case 1:
                    Console.Clear();
                    ClassMethods.ViewStudentsSortFNameDesc();
                    PromptAfterList();
                    PromptSort();
                    break;
                case 2:
                    Console.Clear();
                    ClassMethods.ViewStudentsSortLNameAsc();
                    PromptAfterList();
                    PromptSort();
                    break;
                case 3:
                    Console.Clear();
                    ClassMethods.ViewStudentsSortLNameDesc();
                    PromptAfterList();
                    PromptSort();
                    break;
                case 4:
                    Start();
                    break;
            }
        }
        internal void ViewEmployees()
        {
            string prompt = $"Välj om du vill se alla anställda eller en viss kategori\n";
            string[] options = {
                "Alla anställda", "Välj titel", "Huvudmeny" };
            Menu menu = new Menu(prompt, options);
            int menuSelect = menu.Run();
            ClassMethods classMethods = new ClassMethods();

            switch (menuSelect)
            {
                case 0:
                    Console.Clear();
                    classMethods.ViewListOfEmployees();
                    PromptAfterList();
                    ViewEmployees();
                    break;
                case 1:
                    Console.Clear();
                    classMethods.ViewEmployee();
                    PromptAfterList();
                    ViewEmployees();
                    break;
                case 2:
                    Start();
                    break;
            }
        }
        internal void PromptSort()
        {
            string prompt = $"Vill du se andra sorteringar?\n(Väljer du Nej skickas du tillbaka till huvudmenyn.)\n";
            string[] options = { "Ja", "Nej" };
            Menu menu = new Menu(prompt, options);
            int menuSelect = menu.Run();

            switch (menuSelect)
            {
                case 0:
                    ViewStudents();
                    break;
                case 1:
                    Start();
                    break;
            }
        }
        internal void PromptClass()
        {
            ClassMethods classM = new ClassMethods();
            string prompt = $"Vill du se andra klasser?\n(Väljer du Nej skickas du tillbaka till huvudmenyn.)\n";
            string[] options = { "Ja", "Nej" };
            Menu menu = new Menu(prompt, options);
            int menuSelect = menu.Run();

            switch (menuSelect)
            {
                case 0:
                    classM.ViewClasses();
                    break;
                case 1:
                    Start();
                    break;
            }
        }
        internal void PromptAfterList()
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Tryck Enter för att komma vidare.");
            Console.ReadKey();
        }
    }
}
