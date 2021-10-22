using Microsoft.AspNetCore.Mvc;
using MonitorSpyAPI.Dominio;
using MonitorSpyAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonitorSpyAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoramentoController : ControllerBase {
        private readonly MonitorService _monitorService;

        public MonitoramentoController(MonitorService monitorService) {
            _monitorService = monitorService;
        }
        
        [HttpGet]
        public ActionResult<List<Monitoramento>> GetMonitors() => Ok(_monitorService.Get());

        [HttpGet("{id}", Name ="GetMonitor")]        
        public ActionResult<Monitoramento> GetMonitor(string id) {
            var monitor = _monitorService.Get(id);

            if (monitor == null)
                return NotFound();

            return Ok(monitor);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Monitoramento monitoramento) {
            _monitorService.Create(monitoramento);

            return CreatedAtRoute("GetMonitor", new { id = monitoramento.Id.ToString() }, monitoramento);
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Monitoramento monitoramentoIn) {
            var monitoramento = _monitorService.Get(id);

            if (monitoramento == null)
            {
                return NotFound();
            }

            _monitorService.Update(id, monitoramentoIn);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(string id) {
            var monitoramento = _monitorService.Get(id);

            if (monitoramento == null)
                return NotFound();

            _monitorService.Remove(monitoramento.Id);

            return NoContent();
        }
    }
}
