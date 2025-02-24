using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restorunt.Models;

namespace Restorunt.Controllers
{
    public class AuthController : Controller
    {
        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;


        public AuthController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Register()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Fname,Lname,ImagePath,ImageFile")] Customer customer ,string Username ,string Password)
        {
            if (ModelState.IsValid)
            {
                

                // getting the wwwroot path
                string wwwrootPath = _webHostEnvironment.WebRootPath;  // C:\Users\USER\Desktop\MVC_project\Restorunt\wwwroot\


                // encrypt for the image name  ex: fs6565es32s_Pizza.png
                string fileName = Guid.NewGuid().ToString() + "_" + customer.ImageFile.FileName;

                string path = Path.Combine(wwwrootPath + "/Images/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await customer.ImageFile.CopyToAsync(fileStream);
                }


                customer.ImagePath = fileName;

                _context.Add(customer);
                await _context.SaveChangesAsync();

                UserLogin login = new UserLogin();
                login.UserName = Username;
                login.Password = Password;
                login.CustomerId = customer.Id;
                login.RoleId = 3;

                _context.Add(login);
                await _context.SaveChangesAsync();



                return RedirectToAction("Login");
            }
            return View(customer);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([Bind("UserName,Password")] UserLogin userLogin)
        {
           var authUser= _context.UserLogins.Where(obj => obj.UserName == userLogin.UserName && obj.Password == userLogin.Password).SingleOrDefault();

            if (authUser != null) 
            {
                switch (authUser.RoleId)
                {
                    case 1:
                        HttpContext.Session.SetString("AdminName" , userLogin.UserName);
                        return RedirectToAction("Index", "Admin");
                    case 3:
                        return RedirectToAction("Index", "Home");


                }

                



            }
            return View();

        }


        
    }
}
