using Microsoft.AspNetCore.Mvc;
using MonitorSpyAPI.Dominio;
using MonitorSpyAPI.Dominio.Log;
using MonitorSpyAPI.Services;
using MonitorSpyAPI.Util.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonitorSpyAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoramentoController : ControllerBase {
        private readonly MonitorService _monitorService;
        private readonly LogErroService _logErroService;

        public MonitoramentoController(MonitorService monitorService, LogErroService logErroService) {
            _monitorService = monitorService;
            _logErroService = logErroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Monitoramento>>> GetMonitors() {
            try {
                List<Monitoramento> monitoramentos = new List<Monitoramento>();

                await Task.Run(() => monitoramentos = _monitorService.Get());

                return Ok(monitoramentos);
            } catch (Exception ex) {
                _logErroService.Insert(new LogFile { Mensagem = ex.Message, DataHora = DateTime.Now, Classe = ExtensionHelper.GetCurrentClass(), Projeto = ExtensionHelper.GetProjectName() });
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Monitoramento>> GetMonitor(string id) {
            try {
                Monitoramento monitor = null;

                await Task.Run(() => monitor = _monitorService.Get(id));

                if (monitor == null)
                    return NotFound();
                
                return Ok(monitor);
            } catch (Exception ex) {
                _logErroService.Insert(new LogFile { Mensagem = ex.Message, DataHora = DateTime.Now, Classe = ExtensionHelper.GetCurrentClass(), Projeto = ExtensionHelper.GetProjectName() });
                return NotFound();
            }
        } 

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Monitoramento monitoramento) {
            try {
                await Task.Run(() => _monitorService.Insert(monitoramento));

                return Ok();
            } catch (Exception ex) {
                _logErroService.Insert(new LogFile { Mensagem = ex.Message, DataHora = DateTime.Now, Classe = ExtensionHelper.GetCurrentClass(), Projeto = ExtensionHelper.GetProjectName() });
                return NotFound();
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Monitoramento monitoramentoIn) {
            try {
                Monitoramento monitor = null;

                await Task.Run(() => monitor = _monitorService.Get(id));

                if (monitor == null)
                    return NotFound();

                await Task.Run(() => _monitorService.Update(id, monitoramentoIn));

                return Ok();
            } catch (Exception ex) {
                _logErroService.Insert(new LogFile { Mensagem = ex.Message, DataHora = DateTime.Now, Classe = ExtensionHelper.GetCurrentClass(), Projeto = ExtensionHelper.GetProjectName() });
                return NotFound();
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) {
            try {
                Monitoramento monitor = null;

                await Task.Run(() => _monitorService.Get(id));

                if (monitor == null)
                    return NotFound();

                await Task.Run(() => _monitorService.Remove(monitor.Id));

                return Ok();
            } catch (Exception ex) {
                _logErroService.Insert(new LogFile { Mensagem = ex.Message, DataHora = DateTime.Now, Classe = ExtensionHelper.GetCurrentClass(), Projeto = ExtensionHelper.GetProjectName() });
                return NotFound();
            }
        }
    }
}
