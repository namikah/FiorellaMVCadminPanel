using FirstFiorellaMVC.DataAccessLayer;
using FirstFiorellaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFiorellaMVC.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            ViewBag.BlogCounts = await _dbContext.Blogs.CountAsync();
            ViewBag.CurrentPage = page;

            var blogs = await _dbContext.Blogs.Skip((page - 1) * 4).Take(4).ToListAsync();

            return View(blogs);
        }

        public async Task<ActionResult> Detail(int? id)
        {
            if (id == null)
                return BadRequest();

            var blog = await _dbContext.Blogs.FindAsync(id);
            if (blog == null)
                return NotFound();

            return View(blog);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isBlog = await _dbContext.Blogs.AnyAsync(x => x.Name.ToLower() == blog.Name.ToLower());
            if (isBlog)
            {
                ModelState.AddModelError("Name", "Already exist this blog");
                return View();
            }

            await _dbContext.Blogs.AddAsync(blog);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _dbContext.Blogs.FindAsync(id);
            if (blog == null)
                return NotFound();

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExistBlog = await _dbContext.Blogs.FindAsync(blog.Id);
            if (isExistBlog == null)
            {
                ModelState.AddModelError("Name", "Note found");
                return View(isExistBlog);
            }

            var isExistBlogName = await _dbContext.Blogs.AnyAsync(x => x.Name == blog.Name);
            if (isExistBlogName)
            {
                ModelState.AddModelError("Name", "Already exist this blog");
                return View(isExistBlog);
            }
            isExistBlog.Name = blog.Name;
            isExistBlog.Description = blog.Description;
            isExistBlog.Datetime = blog.Datetime;
            isExistBlog.Context = blog.Context;
            isExistBlog.Image = blog.Image;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
