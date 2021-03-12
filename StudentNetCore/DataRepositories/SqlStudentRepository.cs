using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.DataRepositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly AppDbContext context;

        public SqlStudentRepository(AppDbContext context)
        {
            this.context = context;
        }




        public Student Add(Student student)
        {
            context.students.Add(student);

            context.SaveChanges();

            return student;
        }

        public Student Delete(int id)
        {
            Student student = context.students.Find(id);

            if(student!=null)
            {
                context.students.Remove(student);
                context.SaveChanges();
            }

            return student;
        }

        public Student GetStudent(int id)
        {
            return context.students.Find(id);
        }

        public IEnumerable<Student> GetStudentsList()
        {
            return context.students;
        }

        public Student Update(Student updatestudent)
        {
            var student = context.students.Attach(updatestudent);

            student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();

            return updatestudent;



        }
    }
}
