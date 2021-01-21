  
using webapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipalityController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public MunicipalityController(ApiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method shows all Municipalitys
        /// </summary>
        /// <returns></returns>
        ///<remarks>
        /// Sample request
        /// GET/Municipality
        /// </remarks> 
        [HttpGet]
        public object Get()
        {
            return _context.Municipalitys.ToList();
        }

        /// <summary>
        /// This method retrieves a Municipality by id
        /// </summary>
        /// <param id="id"></param>
        /// <returns></returns>
        ///<remarks>
        /// Sample request
        /// GET/Municipality/{id}
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<Municipality>> GetMunicipality(int id)
        {
            var municipality = await _context.Municipalitys.FindAsync(id);

            if (municipality == null)
            {
                return NotFound();
            }

            return municipality;
        }

        /// <summary>
        /// This method retrieves the Municipality tax by Municipality_name and input date
        /// </summary>
        /// <param municipality_name="municipality_name"></param>
        /// <param input_date="input_date"></param>
        /// <returns></returns>
        ///<remarks>
        /// Sample request
        /// GET/Municipality/municipality_name
        /// </remarks>
        [HttpGet("{municipality_name}/{input_date}")]
        public object GetByMunicipality_name(string municipality_name, string input_date)
        {
            
            try {
                var parsedDate = DateTime.Parse(input_date);
                var municipality = _context.Municipalitys.Where(
                    b => b.Municipality_name == municipality_name 
                    && b.Period_start <= parsedDate 
                    && b.Period_end >= parsedDate)
                    .OrderBy(b => b.Daily)
                    .ThenBy(b => b.Weekly)
                    .ThenBy(b => b.Monthly)
                    .ThenBy(b => b.Yearly)
                    .Select((b) => new {
                    Id = b.Id,
                    Municipality_name = b.Municipality_name,
                    Period_start = b.Period_start,
                    Period_end = b.Period_end,
                    Yearly = b.Yearly,
                    Monthly = b.Monthly,
                    Weekly = b.Weekly,
                    Daily = b.Daily
                }).ToList().First();
                if (municipality == null)
                {
                    return NotFound();
                }
                return municipality;
            }
            catch (FormatException) {
                Console.WriteLine("Unable to parse '{0}'", input_date);
                return BadRequest();
            }
        }

        /// <summary>
        /// This method creates the Municipality tax with a json payload
        /// </summary>
        /// <returns></returns>
        ///<remarks>
        /// </remarks>
        // POST: api/Municipality
        [HttpPost]
        public async Task<ActionResult<Municipality>> PostMunicipality(Municipality municipality)
        {
            Console.WriteLine("You're here: PostMunicipality");
            _context.Municipalitys.Add(municipality);
            var entity = await _context.SaveChangesAsync();
            //return CreatedAtAction("api/Municipality",municipality);
            return CreatedAtAction(nameof(GetMunicipality), new { id = municipality.Id }, municipality);
            //return new ObjectResult(entity) { StatusCode = StatusCodes.Status201Created };
        }
    }
}