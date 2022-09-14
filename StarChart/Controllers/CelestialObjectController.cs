using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    /// <summary>
    /// CeletiaObjectController classe
    /// </summary>
    [Route("")]
    [ApiController]
    public class CelestialObjectController : Controller
    {
        ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance <see cref="CelestialObjectController"/>
        /// </summary>
        /// <param name="context">the database context</param>
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Index Action
        /// </summary>
        /// <returns>The IActionResult View</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
