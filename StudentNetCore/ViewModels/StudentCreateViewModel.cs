using Microsoft.AspNetCore.Http;
using StudentManagement.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.ViewModels
{
    public class StudentCreateViewModel
    {

        public int id { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "请输入名字"), MaxLength(50, ErrorMessage = "名字的长度不能超过50")]
        public string Name { get; set; }

        [Display(Name = "班级名称")]
        [Required(ErrorMessage = "请选择班级名称")]
        public ClassNameEnum? ClassName { get; set; }


        [Display(Name = "邮箱")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "邮箱的格式不正确")]
        [Required(ErrorMessage = "请输入邮箱地址")]
        public string Email { get; set; }

        [Display(Name = "头像")]
        public List<IFormFile>  Photos { get; set; }



    }
}
