namespace StudentSystem.ConsoleClient
{
    using System;

    using StudentSystem.Model;
    using StudentSystem.Data;

    public class StudentSystemConsoleClient
    {
        static void Main()
        {
            // Using c0de first approach, create database for student system with the following tables:
            // Students (with Id, Name, Number, etc.).
            // Courses (Name, Description, Materials, etc.).
            // StudentsInCourses (many-to-many relationship).
            // Homework (one-to-many relationship with students and courses), fields: Content, TimeSent.
            // Annotate the data models with the appropriate attributes and enable code first migrations.
            // Write a console application that uses the data.
            // Seed the data with random values.

            var db = new StudentSystemDbContext();
            db.Students.Add(new Student
            {
                Name = "Penka",
                Number = "124457"
            });

            db.SaveChanges();

            foreach (var student in db.Students)
            {
                Console.WriteLine(student.Name + " " + student.Number);
            }
        }
    }
}