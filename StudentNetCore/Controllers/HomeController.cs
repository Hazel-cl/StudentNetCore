using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.ViewModels;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        // readonly 防止误操作分配其他值 比如null
        private readonly IStudentRepository _studentRepository;
        private readonly HostingEnvironment hostingEnvironment;


        //构造函数注入
        public HomeController(IStudentRepository studentRepository, HostingEnvironment hostingEnvironment)
        {
            _studentRepository = studentRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 展示学生列表
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public IActionResult Index()
        {
            IEnumerable<Student> students = _studentRepository.GetStudentsList();
            return View(students);
        }


        /// <summary>
        /// 展示学生详情
        /// </summary>
        /// <param name="id">学生id</param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Student = _studentRepository.GetStudent(id),
                PageTitle = "StudentDetails"
            };

            return View(homeDetailsViewModel);


        }

        /// <summary>
        /// 展示添加学生页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(StudentCreateViewModel student)
        {

            if (ModelState.IsValid)
            {

                string uniqueFileName = null;

                //if (student.Photos != null)
                //{
                //    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "image");
                //    uniqueFileName = Guid.NewGuid().ToString() + "_" + student.Photos.FileName;

                //    string filePath = Path.Combine(uploadsFolder +"\\"+ uniqueFileName);

                //    student.Photos.CopyTo(new FileStream(filePath, FileMode.Create));
                //}

                if (student.Photos != null && student.Photos.Count > 0)
                {
                    uniqueFileName = ProcessUploadedFile(student);

                }


                Student newstudent = new Student
                {
                    Name = student.Name,
                    Email = student.Email,
                    ClassName = student.ClassName,
                    PhptoPath = uniqueFileName
                };

                _studentRepository.Add(newstudent);

                return RedirectToAction("Details", new { id = newstudent.id });
            }

            return View();

        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Student student = _studentRepository.GetStudent(id);


            StudentEditVidewModel studentEditVidew = new StudentEditVidewModel
            {
                Id = student.id,
                Name = student.Name,
                Email = student.Email,
                ClassName = student.ClassName,
                ExistingPhotoPath = student.PhptoPath
            };

            return View(studentEditVidew);

            //   throw new Exception("查询不到这个学生信息");       


        }



        [HttpPost]
        public IActionResult Edit(StudentEditVidewModel model)
        {

            //检查提供的数据是否有效，如果没有通过验证，需要重新编辑学生信息
            //这样用户就可以更正并重新提交编辑表单
            if (ModelState.IsValid)
            {
                Student student = _studentRepository.GetStudent(model.Id);
                student.Email = model.Email;
                student.Name = model.Name;
                student.ClassName = model.ClassName;

                if (model.Photos.Count > 0)
                {

                    if (model.ExistingPhotoPath != null)
                    {
                        for (int i = 0; i < model.ExistingPhotoPath.Split("◇").Count(); i++)
                        {
                            string filePahth = Path.Combine(hostingEnvironment.WebRootPath, "image", model.ExistingPhotoPath.Split("◇")[i]);
                            System.IO.File.Delete(filePahth);
                        }
                    }

                    student.PhptoPath = ProcessUploadedFile(model);

                }

                Student updateStudent = _studentRepository.Update(student);


                return RedirectToAction("Index");

            }

            return View(model);



        }







        /// <summary>
        /// 将照片保存到指定的路径中，并返回唯一的文件名
        /// </summary>
        /// <returns></returns>
        private string ProcessUploadedFile(StudentCreateViewModel model)
        {
            string TuniqueFileName = null;

            if (model.Photos.Count > 0)
            {
                string uniqueFileName = null;


                foreach (var photo in model.Photos)
                {
                    //必须将图像上传到wwwroot中的images文件夹
                    //而要获取wwwroot文件夹的路径，我们需要注入 ASP.NET Core提供的HostingEnvironment服务
                    //通过HostingEnvironment服务去获取wwwroot文件夹的路径
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "image");
                    //为了确保文件名是唯一的，我们在文件名后附加一个新的GUID值和一个下划线

                    uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    //因为使用了非托管资源，所以需要手动进行释放
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        //使用IFormFile接口提供的CopyTo()方法将文件复制到wwwroot/images文件夹
                        photo.CopyTo(fileStream);
                    }

                    if (TuniqueFileName == null)
                    {
                        TuniqueFileName = uniqueFileName;
                    }
                    else
                    {
                        TuniqueFileName = TuniqueFileName + "◇" + uniqueFileName;
                    }
                }
            }




            return TuniqueFileName;

        }



    }
}
