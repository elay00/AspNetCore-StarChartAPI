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
        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            CelestialObject celestianObject = null;
            if (celestianObject.Id != id)
            {
                return NotFound("The object is not found with the provided Id");
            }
            else
            {
                celestianObject.Satellites = new System.Collections.Generic.List<CelestialObject> { celestianObject };
                return Ok();
            }
        }
    }
}
