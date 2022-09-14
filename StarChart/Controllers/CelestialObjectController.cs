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

        /// <summary>
        /// Get elements by name
        /// </summary>
        /// <param name="name">the name element</param>
        /// <returns>the operation result</returns>
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

        /// <summary>
        /// Get all elements
        /// </summary>
        /// <returns>the operation result</returns>
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


        /// <summary>
        /// Create an element
        /// </summary>
        /// <param name="celestialObject">the element to create</param>
        /// <returns>The creation result</returns>
        [HttpPost]
        public IActionResult Create([FromBody] CelestialObject celestialObject)
        {
            _context.CelestialObjects.Add(celestialObject);
            _context.SaveChanges();
            return CreatedAtRoute("GetById", new { id = celestialObject.Id }, celestialObject);

        }
        /// <summary>
        /// Update the element with id specified
        /// </summary>
        /// <param name="id">the specified id</param>
        /// <param name="celestialObject">the updated data</param>
        /// <returns>the update result</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, CelestialObject celestialObject)
        {
            var existingObject = _context.CelestialObjects.Find(id);
            if (existingObject == null)
            {
                return NotFound();
            }
            existingObject.Name = celestialObject.Name;
            existingObject.OrbitalPeriod = celestialObject.OrbitalPeriod;
            existingObject.OrbitedObjectId = celestialObject.OrbitedObjectId;
            _context.CelestialObjects.Update(existingObject);
            _context.SaveChanges();
            return NoContent();
        }


        /// <summary>
        /// Rename object with id and name
        /// </summary>
        /// <param name="id">the element id</param>
        /// <param name="name">the element name</param>
        /// <returns>the rename result</returns>
        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id, string name)
        {
            var existingObject = _context.CelestialObjects.Find(id);
            if (existingObject == null)
            {
                return NotFound();
            }
            existingObject.Name = name;
            _context.CelestialObjects.Update(existingObject);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// The delete action
        /// </summary>
        /// <param name="id">the element id to delete</param>
        /// <returns>the result of the delete operation</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var allObjects = _context.CelestialObjects.Where(e => e.Id == id || e.OrbitedObjectId == id);
            if (!allObjects.Any())
            {
                return NotFound();
            }
            _context.CelestialObjects.RemoveRange(allObjects);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
