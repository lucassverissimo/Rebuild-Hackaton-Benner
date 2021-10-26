using Microsoft.AspNetCore.Mvc;
using MonitorSpyAPI.Dominio;
using MonitorSpyAPI.Dominio.Log;
using MonitorSpyAPI.Services;
using MonitorSpyAPI.Util.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonitorSpyAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoramentoClickController : ControllerBase {
        private readonly MonitorClickService _monitorClickService;
        private readonly LogErroService _logErroService;

        public MonitoramentoClickController(MonitorClickService monitorClickService, LogErroService logErroService) {
            _monitorClickService = monitorClickService;
            _logErroService = logErroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonitoramentoClick>>> Get() {
            try {
                List<MonitoramentoClick> monitoramentoClicks = new List<MonitoramentoClick>();

                await Task.Run(() => monitoramentoClicks = _monitorClickService.Get());

                return Ok(monitoramentoClicks);
            } catch (Exception ex) {
                _logErroService.Insert(new LogFile { Mensagem = ex.Message, DataHora = DateTime.Now, Classe = ExtensionHelper.GetCurrentClass(), Projeto = ExtensionHelper.GetProjectName() });
                return NotFound();
            }
        }

        [HttpGet("{id}", Name = "GetMonitorClick")]
        public async Task<ActionResult<Monitoramento>> Get(string id) {
            try {
                MonitoramentoClick monitoramentoClick = null;

                await Task.Run(() => monitoramentoClick = _monitorClickService.Get(id));

                if (monitoramentoClick == null)
                    return NotFound();

                return Ok(monitoramentoClick);
            } catch (Exception ex) {
                _logErroService.Insert(new LogFile { Mensagem = ex.Message, DataHora = DateTime.Now, Classe = ExtensionHelper.GetCurrentClass(), Projeto = ExtensionHelper.GetProjectName() });
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MonitoramentoClick monitorClick) {
            try {
                await Task.Run(() => _monitorClickService.Insert(monitorClick));

                return Ok();
            } catch (Exception ex) {
                _logErroService.Insert(new LogFile { Mensagem = ex.Message, DataHora = DateTime.Now, Classe = ExtensionHelper.GetCurrentClass(), Projeto = ExtensionHelper.GetProjectName() });
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] MonitoramentoClick monitorClickIn) {
            try {
                MonitoramentoClick monitorClick = null;

                await Task.Run(() => monitorClick = _monitorClickService.Get(id));

                if (monitorClick == null)
                    return NotFound();

                await Task.Run(() => _monitorClickService.Update(id, monitorClickIn));

                return Ok();
            } catch (Exception ex) {
                _logErroService.Insert(new LogFile { Mensagem = ex.Message, DataHora = DateTime.Now, Classe = ExtensionHelper.GetCurrentClass(), Projeto = ExtensionHelper.GetProjectName() });
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id) {
            try {
                MonitoramentoClick monitorClick = null;

                await Task.Run(() => monitorClick = _monitorClickService.Get(id));

                if (monitorClick == null)
                    return NotFound();

                await Task.Run(() => _monitorClickService.Remove(monitorClick.Id));

                return Ok();
            } catch (Exception ex) {
                _logErroService.Insert(new LogFile { Mensagem = ex.Message, DataHora = DateTime.Now, Classe = ExtensionHelper.GetCurrentClass(), Projeto = ExtensionHelper.GetProjectName() });
                return NotFound();
            }
        }
    }
}
