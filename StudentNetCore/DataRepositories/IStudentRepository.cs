using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public interface IStudentRepository
    {

        /// <summary>
        /// 查找学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student GetStudent(int id);

        /// <summary>
        /// 枚举—学生列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Student> GetStudentsList();

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Student Add(Student student);

        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="updatestudent"></param>
        /// <returns></returns>
        Student Update(Student updatestudent);

        /// <summary>
        /// 删除学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student Delete(int id);



    }
}
