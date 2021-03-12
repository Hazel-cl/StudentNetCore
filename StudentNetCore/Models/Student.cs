using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    /// <summary>
    /// 学生信息
    /// </summary>
    public class Student
    {

        public int id { get; set; }

        [Display(Name ="姓名")]
        [Required(ErrorMessage ="请输入名字"),MaxLength(50,ErrorMessage ="名字的长度不能超过50")]
        public string Name { get; set; }

        [Display(Name = "班级名称")]
        [Required(ErrorMessage = "请选择班级名称")]
        public ClassNameEnum? ClassName { get; set; }


        [Display(Name = "邮箱")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "邮箱的格式不正确")]
        [Required(ErrorMessage = "请输入邮箱地址")]
        public string Email { get; set; }


        public string PhptoPath { get; set; }

       



    }
}
