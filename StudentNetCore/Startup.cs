using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StudentManagement.DataRepositories;
using StudentManagement.Models;

namespace StudentManagement
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

       public  Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContextPool<AppDbContext>(
                options=>options.UseSqlServer(_configuration.GetConnectionString("StudentDBconnection"))
                );

            services.AddMvc().AddXmlSerializerFormatters();

            services.AddScoped<IStudentRepository, SqlStudentRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();
                developerExceptionPageOptions.SourceCodeLineCount = 10;

                app.UseDeveloperExceptionPage();
            }


           

            //添加静态文件中间件
            app.UseStaticFiles();


            app.UseMvc(routes =>
            {

                routes.MapRoute("default", "{controller}/{action}/{id?}");

            });





            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("52app.html");

            //app.UseFileServer(fileServerOptions);
            //app.UseDefaultFiles();



            app.Run(async (context) =>
            {
                //throw new Exception("Error");

                await context.Response.WriteAsync("Hello world");

                //logger.LogInformation("M3");

                //await context.Response.WriteAsync("M3");


            });
        }
    }
}
