using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.IO;

namespace OrderServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly string schemaPath = Path.Combine(Directory.GetCurrentDirectory(), "Schemas", "orderSchema.json");

        private JSchema LoadSchema()
        {
            if (!System.IO.File.Exists(schemaPath))
            {
                throw new FileNotFoundException($"JSON schema file not found at {schemaPath}");
            }

            string schemaJson = System.IO.File.ReadAllText(schemaPath);
            return JSchema.Parse(schemaJson);
        }

        [HttpPost]
        [Route("validate")]
        public IActionResult ValidateOrder([FromBody] JObject orderData)
        {
            if (orderData == null)
            {
                return BadRequest(new { message = "Ordren kan ikke være tom." });
            }

            try
            {
                // Læs schema fra fil
                JSchema schema = LoadSchema();

                // Valider JSON mod schema
                if (!orderData.IsValid(schema, out IList<string> validationErrors))
                {
                    return BadRequest(new { message = "JSON schema validation failed.", errors = validationErrors });
                }

                return Ok(new { message = "Ordren er valideret og accepteret." });
            }
            catch (FileNotFoundException ex)
            {
                return StatusCode(500, new { message = "Schema-fil ikke fundet.", error = ex.Message });
            }
            catch (JsonReaderException ex)
            {
                return StatusCode(500, new { message = "Fejl ved parsing af schema.", error = ex.Message });
            }
        }
    }
}
