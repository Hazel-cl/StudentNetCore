using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(

               new Student
               {
                   id = 1,
                   Name = "学生测试",
                   Email = "11@qq.com",
                   ClassName = ClassNameEnum.FirstGrade,
               },
                new Student
                {
                    id = 2,
                    Name = "学生测试2",
                    Email = "22@qq.com",
                    ClassName = ClassNameEnum.SecondGrade,
                },
                new Student
                {
                    id = 3,
                    Name = "学生测试3",
                    Email = "33@qq.com",
                    ClassName = ClassNameEnum.SecondGrade,
                }

               );
        }


    }
}
