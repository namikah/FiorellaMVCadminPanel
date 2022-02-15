using FirstFiorellaMVC.DataAccessLayer;
using FirstFiorellaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFiorellaMVC.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            ViewBag.ProductCounts = await _dbContext.Products.CountAsync();
            ViewBag.CurrentPage = page;

            var products = await _dbContext.Products.Include(x=>x.Images).Include(x=>x.Category).Include(x => x.Campaign).OrderByDescending(x => x.Id).Skip((page - 1) * 4).Take(4).ToListAsync();

            return View(products);
        }

        public async Task<ActionResult> Detail(int? id)
        {
            if (id == null)
                return BadRequest();

            var product =await _dbContext.Products.Include(x=>x.Images).Include(x=>x.Category).Include(x=>x.Campaign).Where(x=>x.Id == id).FirstOrDefaultAsync();
            if (product == null)
                return NotFound();

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isProduct = await _dbContext.Products.AnyAsync(x => x.Name.ToLower() == product.Name.ToLower());
            if (isProduct)
            {
                ModelState.AddModelError("Name", "Already exist");
                return View();
            }

            var isCategory = await _dbContext.Categories.AnyAsync(x => x.Id == product.CategoryId);
            if (!isCategory)
            {
                ModelState.AddModelError("CategoryId", "Not found category");
                return View();
            }

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
