using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.Concrete;
using DocumentFormat.OpenXml.Spreadsheet;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Reflection;



namespace CoreDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddDbContext<Context>();           //Identity

            builder.Services.AddIdentity<AppUser, AppRole>(x =>
            {
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<Context>();             //Identity

            builder.Services.AddControllersWithViews();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login/Index";
            });

            builder.Services.AddMvc(config =>                               //Proje seviyesinde Authorize iþlemi kullanabilecez. 
            {                                                                    //Yani tüm sayfalarý görebilmek için giriþ yapmalý. Hiçbir sayfayý göremiyoruz
                var policy = new AuthorizationPolicyBuilder()                          // AllowAnonymous komutu sayesinde sayfalarý görebiliyoruz
                                     .RequireAuthenticatedUser()
                                     .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });


            builder.Services.ConfigureApplicationCookie(options => 
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                options.AccessDeniedPath = new PathString("/Login/AccessDenied");
                options.LoginPath = "/Login/Index";
                options.SlidingExpiration = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1", "?code={0}");  // urlye kayýtlý olmayan bir sayfa yazýldýðýnda 404 hatasý ve hatadan dolayý gösterilecek sayfanýn dönmesine yarýyor

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                        name: "areas",
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                      );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=blog}/{action=Index}/{id?}");



            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

                var roles = new[] { "Admin", "Writer", "Member", "Moderator" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new AppRole
                        {
                            Name = role
                        });
                    }
                }
            }


            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                string email = "admin@admin.com";
                string password = "Umut.2001";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new AppUser();
                    user.UserName = "admin";
                    user.Email = email;
                    user.NameSurname = "Umut Buðra TER";
                    user.ImageUrl = "43b6388e-1b16-46ba-a5d7-58d0fe5fce27.jpeg";
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Admin");
                }

                string email2 = "test@hotmail.com";
                string password2 = "123456a";
                if (await userManager.FindByEmailAsync(email2) == null)
                {
                    var user = new AppUser();
                    user.UserName = "test";
                    user.Email = email2;
                    user.NameSurname = "Eren Utku";
                    user.ImageUrl = "58c6e06c-927d-492c-a369-4b7d33babf86.jpg";
                    await userManager.CreateAsync(user, password2);
                    await userManager.AddToRoleAsync(user, "Writer");
                }
            }
            SeedData.EnsurePopulated(app);

            app.Run();

        }
    }
}


