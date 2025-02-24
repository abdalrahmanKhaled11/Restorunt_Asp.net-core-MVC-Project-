using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restorunt.Models;

namespace Restorunt.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ModelContext _context;

        //6- IWebHostEnvironment

        private readonly IWebHostEnvironment _webHostEnvironment;



        public CategoriesController(ModelContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
              return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Categories'  is null.");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }


            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        //4- change imagePath to imageFile 

        //5- Create images folder in the wwwroot


        public async Task<IActionResult> Create([Bind("Id,CategoryName,ImageFile")] Category category)
        {
            if (ModelState.IsValid)
            {

                // 7- preparing the image path before adding the object

                // getting the wwwroot path
                string wwwrootPath = _webHostEnvironment.WebRootPath;  // C:\Users\USER\Desktop\MVC_project\Restorunt\wwwroot\


                // encrypt for the image name  ex: fs6565es32s_Pizza.png
                string fileName = Guid.NewGuid().ToString() + "_" + category.ImageFile.FileName;

                string path = Path.Combine(wwwrootPath + "/Images/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await category.ImageFile.CopyToAsync(fileStream);
                }


                category.ImagePath = fileName;

            


                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,CategoryName,ImageFile")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // getting the wwwroot path
                string wwwrootPath = _webHostEnvironment.WebRootPath;  // C:\Users\USER\Desktop\MVC_project\Restorunt\wwwroot\


                // encrypt for the image name  ex: fs6565es32s_Pizza.png
                string fileName = Guid.NewGuid().ToString() + "_" + category.ImageFile.FileName;

                string path = Path.Combine(wwwrootPath + "/Images/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await category.ImageFile.CopyToAsync(fileStream);
                }


                category.ImagePath = fileName;

                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ModelContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(decimal id)
        {
          return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
