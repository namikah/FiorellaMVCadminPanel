using FirstFiorellaMVC.DataAccessLayer;
using FirstFiorellaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ActionResult> Index()
        {
            var blogs = await _dbContext.Blogs.ToListAsync();

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

            var isBlog = await _dbContext.Categories.AnyAsync(x => x.Name.ToLower() == blog.Name.ToLower());
            if (isBlog)
            {
                ModelState.AddModelError("Name", "Already exist this blog");
                return View();
            }

            await _dbContext.Blogs.AddAsync(blog);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
