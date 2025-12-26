using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentGradeManagement
{
    // Student class to store student information
    class Student
    {
        public string Name { get; set; }
        pub
        public string StudentID { get; set; }
        public Dictionary<string, double> Grades { get; set; }

        public Student(string name, string studentID)
        {
            Name = name;
            StudentID = studentID;
            Grades = new Dictionary<string, double>();
        }
    }

    class Program
    {
        // List to store all students
        static List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
            bool running = true;

            Console.WriteLine("========================================");
            Console.WriteLine("  STUDENT GRADE MANAGEMENT SYSTEM");
            Console.WriteLine("========================================\n");

            // Main program loop - REQUIRED LOOP
            while (running)
            {
                DisplayMenu();
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();

                // Switch statement for menu selection - REQUIRED CONTROL STRUCTURE
                switch (input)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        AddGrade();
                        break;
                    case "3":
                        CalculateAndDisplayAverage();
                        break;
                    case "4":
                        DisplayAllStudents();
                        break;
                    case "5":
                        Console.WriteLine("\nThank you for using the Student Grade Management System!");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("\n❌ Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        // Method to display menu options
        static void DisplayMenu()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("MENU OPTIONS:");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Add New Student");
            Console.WriteLine("2. Add Grade to Student");
            Console.WriteLine("3. Calculate Student Average");
            Console.WriteLine("4. Display All Students");
            Console.WriteLine("5. Exit");
            Console.WriteLine("========================================");
        }

        // Method to add a new student
        static void AddStudent()
        {
            Console.WriteLine("\n--- ADD NEW STUDENT ---");
            Console.Write("Enter student name: ");
            string name = Console.ReadLine();

            Console.Write("Enter student ID: ");
            string studentID = Console.ReadLine();

            // If-else statement to validate student ID - REQUIRED CONTROL STRUCTURE
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(studentID))
            {
                Console.WriteLine(" Error: Name and ID cannot be empty.\n");
                return;
            }

            // Check if student ID already exists
            if (FindStudent(studentID) != null)
            {
                Console.WriteLine(" Error: A student with this ID already exists.\n");
                return;
            }

            Student newStudent = new Student(name, studentID);
            students.Add(newStudent);
            Console.WriteLine($" Student '{name}' with ID '{studentID}' added successfully!\n");
        }

        // Method to add a grade to a student
        static void AddGrade()
        {
            Console.WriteLine("\n--- ADD GRADE TO STUDENT ---");
            
            // If-else to check if students exist
            if (students.Count == 0)
            {
                Console.WriteLine(" No students in the system. Please add a student first.\n");
                return;
            }

            Console.Write("Enter student ID: ");
            string studentID = Console.ReadLine();

            Student student = FindStudent(studentID);

            // If-else statement for validation
            if (student == null)
            {
                Console.WriteLine(" Error: Student not found.\n");
                return;
            }

            Console.Write("Enter subject name: ");
            string subject = Console.ReadLine();

            Console.Write("Enter grade (0-100): ");
            string gradeInput = Console.ReadLine();

            // Try to parse the grade and validate
            if (double.TryParse(gradeInput, out double grade))
            {
                // If-else to validate grade range
                if (grade >= 0 && grade <= 100)
                {
                    student.Grades[subject] = grade;
                    Console.WriteLine($" Grade {grade} added for {subject} to student {student.Name}!\n");
                }
                else
                {
                    Console.WriteLine(" Error: Grade must be between 0 and 100.\n");
                }
            }
            else
            {
                Console.WriteLine(" Error: Invalid grade format.\n");
            }
        }

        // REQUIRED METHOD: Calculate average grade for a student
        static double CalculateAverage(Student student)
        {
            // If-else to check if student has grades
            if (student.Grades.Count == 0)
            {
                return 0;
            }

            double sum = 0;
            
            // For loop to calculate sum - REQUIRED LOOP
            foreach (var grade in student.Grades.Values)
            {
                sum += grade;
            }

            return sum / student.Grades.Count;
        }

        // Method to calculate and display average
        static void CalculateAndDisplayAverage()
        {
            Console.WriteLine("\n--- CALCULATE STUDENT AVERAGE ---");
            
            if (students.Count == 0)
            {
                Console.WriteLine(" No students in the system.\n");
                return;
            }

            Console.Write("Enter student ID: ");
            string studentID = Console.ReadLine();

            Student student = FindStudent(studentID);

            if (student == null)
            {
                Console.WriteLine(" Error: Student not found.\n");
                return;
            }

            if (student.Grades.Count == 0)
            {
                Console.WriteLine($" Student {student.Name} has no grades yet.\n");
                return;
            }

            // Call the CalculateAverage method
            double average = CalculateAverage(student);
            
            Console.WriteLine($"\n Student: {student.Name} (ID: {student.StudentID})");
            Console.WriteLine($"Average Grade: {average:F2}");
            Console.WriteLine();
        }

        // Method to display all students and their grades
        static void DisplayAllStudents()
        {
            Console.WriteLine("\n--- ALL STUDENT RECORDS ---");
            
            // If-else to check if there are students
            if (students.Count == 0)
            {
                Console.WriteLine(" No students in the system.\n");
                return;
            }

            // For loop to display each student - REQUIRED LOOP
            for (int i = 0; i < students.Count; i++)
            {
                Student student = students[i];
                Console.WriteLine($"\n Student #{i + 1}");
                Console.WriteLine($"Name: {student.Name}");
                Console.WriteLine($"ID: {student.StudentID}");
                
                if (student.Grades.Count > 0)
                {
                    Console.WriteLine("Grades:");
                    foreach (var grade in student.Grades)
                    {
                        Console.WriteLine($"  - {grade.Key}: {grade.Value}");
                    }
                    double average = CalculateAverage(student);
                    Console.WriteLine($"Average: {average:F2}");
                }
                else
                {
                    Console.WriteLine("Grades: No grades yet");
                }
                Console.WriteLine("----------------------------------------");
            }
            Console.WriteLine();
        }

        // Helper method to find a student by ID
        static Student FindStudent(string studentID)
        {
            // Loop through students to find matching ID
            foreach (Student student in students)
            {
                if (student.StudentID == studentID)
                {
                    return student;
                }
            }
            return null;
        }
    }
}