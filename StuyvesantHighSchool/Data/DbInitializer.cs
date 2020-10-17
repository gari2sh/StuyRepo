using StuyvesantHighSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuyvesantHighSchool.Data
{
    public class DbInitializer
    {
        public static void Initialize(StuyDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{FirstName="Shreya",LastName="Shukla",EnrollmentDate=DateTime.Parse("2020-01-01")},
            new Student{FirstName="Bobby",LastName="Smith",EnrollmentDate=DateTime.Parse("2020-01-01")},
            new Student{FirstName="John",LastName="Doe",EnrollmentDate=DateTime.Parse("2020-01-01")},
            new Student{FirstName="Tom",LastName="Mathew",EnrollmentDate=DateTime.Parse("2019-01-01")},
            new Student{FirstName="Dan",LastName="Pauli",EnrollmentDate=DateTime.Parse("2016-01-01")},
            new Student{FirstName="Joe",LastName="Gregor",EnrollmentDate=DateTime.Parse("2017-01-01")},
            new Student{FirstName="Laura",LastName="Newman",EnrollmentDate=DateTime.Parse("2019-01-01")},
            new Student{FirstName="Nina",LastName="Thomas",EnrollmentDate=DateTime.Parse("2018-01-01")}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var teachers = new Teacher[]
           {
                new Teacher { FirstName = "Sally",     LastName = "Xu",
                    JoinDate = DateTime.Parse("2001-01-11") },
                new Teacher { FirstName = "Lara",    LastName = "Bose",
                    JoinDate = DateTime.Parse("2002-05-16") },
                new Teacher { FirstName = "Diana",   LastName = "Heden",
                    JoinDate = DateTime.Parse("2010-01-08") },
                new Teacher { FirstName = "Raj", LastName = "Patel",
                    JoinDate = DateTime.Parse("2015-02-10") },
                new Teacher { FirstName = "Roger",   LastName = "Lee",
                    JoinDate = DateTime.Parse("2008-01-09") }
           };

            foreach (Teacher i in teachers)
            {
                context.Teachers.Add(i);
            }
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name = "English",  TeacherID  = teachers.Single( i => i.LastName == "Xu").TeacherID },
                new Department { Name = "Mathematics", TeacherID  = teachers.Single( i => i.LastName == "Bose").TeacherID },
                new Department { Name = "Engineering", TeacherID  = teachers.Single( i => i.LastName == "Heden").TeacherID },
                new Department { Name = "Biology", TeacherID  = teachers.Single( i => i.LastName == "Patel").TeacherID },
                new Department { Name = "Botany", TeacherID  = teachers.Single( i => i.LastName == "Lee").TeacherID }
            };

            foreach (Department d in departments)
            {
                context.Departments.Add(d);
            }
            context.SaveChanges();


            var subjects = new Subject[]
            {
                new Subject {SubjectID = 1050, Title = "Chemistry",      Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Engineering").DepartmentID
                },
                new Subject {SubjectID = 4022, Title = "Micro Biology", Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Biology").DepartmentID
                },
                new Subject {SubjectID = 4041, Title = "Life Sciences", Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Biology").DepartmentID
                },
                new Subject {SubjectID = 1045, Title = "Calculus",       Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
                },
                new Subject {SubjectID = 3141, Title = "Algebra2",   Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
                },
                new Subject {SubjectID = 2021, Title = "Creative Writng",    Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
                },
                new Subject {SubjectID = 2042, Title = "Literature",     Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
                },
            };

            foreach (Subject c in subjects)
            {
                context.Subjects.Add(c);
            }
            context.SaveChanges();

            var roomAssignments = new RoomAssignment[]
            {
                new RoomAssignment {
                    TeacherID = teachers.Single( i => i.LastName == "Xu").TeacherID,
                    Room = "Room 101" },
                new RoomAssignment {
                    TeacherID = teachers.Single( i => i.LastName == "Bose").TeacherID,
                    Room = "Room 102" },
                new RoomAssignment {
                    TeacherID = teachers.Single( i => i.LastName == "Heden").TeacherID,
                    Room = "Room 305" },
                new RoomAssignment {
                    TeacherID = teachers.Single( i => i.LastName == "Patel").TeacherID,
                    Room = "Room 202" },
                new RoomAssignment {
                    TeacherID = teachers.Single( i => i.LastName == "Lee").TeacherID,
                    Room = "Room 301" },
            };

            foreach (RoomAssignment o in roomAssignments)
            {
                context.RoomAssignments.Add(o);
            }
            context.SaveChanges();

            var subjectAssignments = new SubjectAssignment[]
            {
                new SubjectAssignment {
                    SubjectID = subjects.Single(c => c.Title == "Chemistry" ).SubjectID,
                    TeacherID = teachers.Single(i => i.LastName == "Patel").TeacherID
                    },
                new SubjectAssignment {
                    SubjectID = subjects.Single(c => c.Title == "Chemistry" ).SubjectID,
                    TeacherID = teachers.Single(i => i.LastName == "Heden").TeacherID
                    },
                new SubjectAssignment {
                    SubjectID = subjects.Single(c => c.Title == "Micro Biology" ).SubjectID,
                    TeacherID = teachers.Single(i => i.LastName == "Lee").TeacherID
                    },
                new SubjectAssignment {
                    SubjectID = subjects.Single(c => c.Title == "Life Sciences" ).SubjectID,
                    TeacherID = teachers.Single(i => i.LastName == "Lee").TeacherID
                    },
                new SubjectAssignment {
                    SubjectID = subjects.Single(c => c.Title == "Calculus" ).SubjectID,
                    TeacherID = teachers.Single(i => i.LastName == "Bose").TeacherID
                    },
                new SubjectAssignment {
                    SubjectID = subjects.Single(c => c.Title == "Algebra2" ).SubjectID,
                    TeacherID = teachers.Single(i => i.LastName == "Bose").TeacherID
                    },
                new SubjectAssignment {
                    SubjectID = subjects.Single(c => c.Title == "Creative Writng" ).SubjectID,
                    TeacherID = teachers.Single(i => i.LastName == "Xu").TeacherID
                    },
                new SubjectAssignment {
                    SubjectID = subjects.Single(c => c.Title == "Literature" ).SubjectID,
                    TeacherID = teachers.Single(i => i.LastName == "Xu").TeacherID
                    },
            };

            foreach (SubjectAssignment ci in subjectAssignments)
            {
                context.SubjectAssignments.Add(ci);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
           {
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Shukla").StudentID,
                    SubjectID = subjects.Single(c => c.Title == "Chemistry" ).SubjectID,
                    Score = 95
                },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Shukla").StudentID,
                    SubjectID = subjects.Single(c => c.Title == "Micro Biology" ).SubjectID,
                    Score = 90
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Shukla").StudentID,
                    SubjectID = subjects.Single(c => c.Title == "Life Sciences" ).SubjectID,
                    Score = 88
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Smith").StudentID,
                    SubjectID = subjects.Single(c => c.Title == "Calculus" ).SubjectID,
                    Score = 96
                    },
                    new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Smith").StudentID,
                    SubjectID = subjects.Single(c => c.Title == "Algebra2" ).SubjectID,
                    Score = 78
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Smith").StudentID,
                    SubjectID = subjects.Single(c => c.Title == "Creative Writng" ).SubjectID,
                    Score = 86
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Doe").StudentID,
                    SubjectID = subjects.Single(c => c.Title == "Chemistry" ).SubjectID,
                    Score = 85
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Doe").StudentID,
                    SubjectID = subjects.Single(c => c.Title == "Micro Biology").SubjectID,
                    Score = 77
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Mathew").StudentID,
                    SubjectID = subjects.Single(c => c.Title == "Chemistry").SubjectID,
                    Score = 88
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Pauli").StudentID,
                    SubjectID = subjects.Single(c => c.Title == "Creative Writng").SubjectID,
                    Score = 94
                    },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Newman").StudentID,
                    SubjectID = subjects.Single(c => c.Title == "Literature").SubjectID,
                    Score = 83
                    }
           };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                            s.Student.StudentID == e.StudentID &&
                            s.Subject.SubjectID == e.SubjectID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();

        }
    }
}