using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OrderService.Models;

namespace OrderServiceAPI.Controllers
{
    [Route("api/order/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
       
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _shippingServiceUrl = "http://shipping-service/api/shipping"; // Konfigurerbar i miljø

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("validate")]
        public async Task<IActionResult> PostOrder([FromBody] Order order)
        {
            if (order == null)
                return BadRequest("Ordre er tom");

            // Indlæs JSON Schema
            var schemaText = System.IO.File.ReadAllText("order-schema.json");
            JSchema schema = JSchema.Parse(schemaText);

            // Konverter ordre til JSON
            JObject orderJson = JObject.Parse(JsonConvert.SerializeObject(order));

            // Valider mod JSON Schema
            if (!orderJson.IsValid(schema, out IList<string> errors))
            {
                return BadRequest(new { error = "Ordren er ikke valid", details = errors });
            }
            else
            {
                return Ok("Ordren er valid");
            }

            //// Send ordre til ShippingService
            //var client = _httpClientFactory.CreateClient();
            //var response = await client.PostAsync(
            //    _shippingServiceUrl,
            //    new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json"));

            //if (response.IsSuccessStatusCode)
            //{
            //    return Ok("Ordren er valideret og sendt til ShippingService");
            //}
            //else
            //{
            //    return StatusCode((int)response.StatusCode, "Fejl ved afsendelse til ShippingService");
            //}
        }
    }
}
