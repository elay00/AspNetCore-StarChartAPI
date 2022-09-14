using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    /// <summary>
    /// CeletiaObjectController classe
    /// </summary>
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        /// <summary>
        /// ApplicationDbContext field
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance <see cref="CelestialObjectController"/>
        /// </summary>
        /// <param name="context">the database context</param>
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets Celestian object by Id
        /// </summary>
        /// <param name="id">the Id of the celestian object to get</param>
        /// <returns>an action result</returns>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = _context.CelestialObjects.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            result.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == id).ToList();
            return Ok(result);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var result = _context.CelestialObjects.Where(e => e.Name == name).ToList();
            if (!result.Any())
            {
                return NotFound();
            }
            foreach (var item in result)
            {
                item.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == item.OrbitedObjectId).ToList();

            }
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _context.CelestialObjects.ToList();
            foreach (var item in result)
            {
                item.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == item.OrbitedObjectId).ToList();
            }
            return Ok(result);
        }
    }
}
