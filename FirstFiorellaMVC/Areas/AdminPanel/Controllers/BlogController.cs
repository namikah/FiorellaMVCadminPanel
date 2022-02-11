using FirstFiorellaMVC.DataAccessLayer;
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
            var blog = await _dbContext.Blogs.FindAsync(id);

            return View(blog);
        }
    }
}
