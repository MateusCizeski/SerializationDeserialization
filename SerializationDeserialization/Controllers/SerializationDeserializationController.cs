using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Text;

namespace SerializationDeserialization.Controllers
{
    public class XmlRequest
    {
        public string Xml { get; set; }
    }

    public class JsonRequest
    {
        public string Json { get; set; }
    }

    [Route("api/v1/SerializationDeserialization")]
    [ApiController]
    public class SerializationDeserializationController : Controller
    {
        private static readonly List<Veiculo> veiculos = new()
        {
            new Veiculo { Marca = "Toyota", Modelo = "Corolla", Ano = 2023, Preco = 120000 },
            new Veiculo { Marca = "Honda", Modelo = "Civic", Ano = 2022, Preco = 110000 }
        };

        [HttpGet]
        [Route("serializar/xml")]
        public IActionResult SerializarXml()
        {
            try
            {
                var xml = XmlService.Serializar(veiculos);
                return Content(xml, "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao serializar para XML: {ex.Message}");
            }
        }

        [HttpPost("desserializar/xml")]
        public IActionResult DesserializarXml([FromBody] XmlRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Xml))
                {
                    return BadRequest("XML vazio ou inválido.");
                }

                var veiculosDesserializados = XmlService.Desserializar(request.Xml);

                return Ok(veiculosDesserializados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao desserializar XML: {ex.Message}");
            }
        }

        [HttpGet("serializar/json")]
        public IActionResult SerializarJson()
        {
            try
            {
                var json = JsonService.Serializar(veiculos);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao serializar para JSON: {ex.Message}");
            }
        }

        [HttpPost("desserializar/json")]
        public IActionResult DesserializarJson([FromBody] JsonRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Json))
                {
                    return BadRequest("JSON vazio ou inválido.");
                }

                var veiculosDesserializados = JsonService.Desserializar(request.Json);

                return Ok(veiculosDesserializados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao desserializar JSON: {ex.Message}");
            }
        }

        [HttpGet("serializar/csv")]
        public IActionResult SerializarCsv()
        {
            try
            {
                var csv = CsvService.Serializar(veiculos);
                return File(Encoding.UTF8.GetBytes(csv), "text/csv", "veiculos.csv");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao serializar para CSV: {ex.Message}");
            }
        }

        [HttpPost("desserializar/csv")]
        public IActionResult DesserializarCsv(IFormFile arquivo)
        {
            try
            {
                if (arquivo == null || arquivo.Length == 0)
                {
                    return BadRequest("Arquivo não enviado.");
                }

                using var stream = arquivo.OpenReadStream();
                var veiculosDesserializados = CsvService.Desserializar(stream);

                return Ok(veiculosDesserializados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao desserializar CSV: {ex.Message}");
            }
        }

        [HttpGet("serializar/soap")]
        public IActionResult SerializarSoap()
        {
            try
            {
                var soap = SoapService.Serializar(veiculos);
                return Content(soap, "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao serializar para SOAP: {ex.Message}");
            }
        }

        [HttpPost("desserializar/soap")]
        public IActionResult DesserializarSoap(IFormFile arquivo)
        {
            try
            {
                if (arquivo == null || arquivo.Length == 0)
                {
                    return BadRequest("Arquivo não enviado.");
                }

                using var stream = arquivo.OpenReadStream();
                var veiculosDesserializados = SoapService.Desserializar<List<Veiculo>>(stream); // Desserializa o SOAP

                return Ok(veiculosDesserializados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao desserializar SOAP: {ex.Message}");
            }
        }
    }
}
