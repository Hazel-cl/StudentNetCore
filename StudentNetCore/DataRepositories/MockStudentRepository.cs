using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class MockStudentRepository:IStudentRepository
    {

        private List<Student> _studentlist;


        public MockStudentRepository()
        {
            _studentlist = new List<Student>()
            {
                new Student(){id=1,Name="这是第一个学生",ClassName=ClassNameEnum.FirstGrade,Email="C1"},
                new Student(){id=2,Name="这是第二个学生",ClassName=ClassNameEnum.SecondGrade,Email="C2"},
                new Student(){id=3,Name="这是第三个学生",ClassName=ClassNameEnum.ThirdGrade,Email="C3"},

            };
        }




        public Student Add(Student student)
        {
            student.id = _studentlist.Max(s => s.id) + 1;
            _studentlist.Add(student);
            return student;
        }


        public IEnumerable<Student> GetStudentsList()
        {
            return _studentlist;
        }

        public Student GetStudent(int id)
        {
           return _studentlist.FirstOrDefault(a => a.id == id);
        }




        public Student Update(Student updatestudent)
        {
            Student student = _studentlist.FirstOrDefault(s => s.id == updatestudent.id);

            if (student != null)
            {
                student.Name = updatestudent.Name;
                student.Email = updatestudent.Email;
                student.ClassName = updatestudent.ClassName;
            }
            return student;


        }

        public Student Delete(int id)
        {
            Student student = _studentlist.FirstOrDefault(s => s.id == id);
            if(student!=null)
            {
                _studentlist.Remove(student);
            }

            return student;

        }
    }
}
