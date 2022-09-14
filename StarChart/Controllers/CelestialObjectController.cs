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
            CelestialObject celestianObject = new CelestialObject { Id = 12345, Name = "abcdef" };
            if (celestianObject.Id != id)
            {
                return NotFound();
            }
            else if(celestianObject.Id == id)
            {
                celestianObject.Satellites = new System.Collections.Generic.List<CelestialObject> { celestianObject };
                return Ok(celestianObject);
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet("{name}", Name = "GetByName")]
        public IActionResult GetByName(string name)
        {
            CelestialObject celestianObject = new CelestialObject { Name = "name45896" };
            if (celestianObject.Name != name)
            {
                return NotFound();
            }
            else if(celestianObject.Name == name)
            {
                celestianObject.Satellites = new System.Collections.Generic.List<CelestialObject> { celestianObject };
                return Ok(celestianObject);
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet(Name = "GetAll")]
        public IActionResult GetAll()
        {
            CelestialObject[] celestianObjects = new CelestialObject[2];

            celestianObjects[0] = new CelestialObject { Id = 1, Name = "1" };
            celestianObjects[0].Satellites = new System.Collections.Generic.List<CelestialObject>()
            {
                  new CelestialObject() {Id =11, Name = "1.1"},
                  new CelestialObject() {Id =12, Name = "1.2"}
            };
            celestianObjects[1] = new CelestialObject { Id = 2, Name = "2" };
            celestianObjects[1].Satellites = new System.Collections.Generic.List<CelestialObject>()
            {
                  new CelestialObject() {Id =21, Name = "2.1"},
                  new CelestialObject() {Id =22, Name = "2.2"}
            };
            return Ok(celestianObjects);
        }
    }
}
