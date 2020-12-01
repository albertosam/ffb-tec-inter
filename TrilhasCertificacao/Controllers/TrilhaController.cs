using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrilhasCertificacao.Domain;
using TrilhasCertificacao.Model;

namespace TrilhasCertificacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrilhaController : ControllerBase
    {
        private readonly ITrilhaRepository _trilhaRepository;
        private readonly ICertificadoRepository _certificadoRepository;

        public TrilhaController(ITrilhaRepository trilhaRepository, ICertificadoRepository certificadoRepository)
        {
            _trilhaRepository = trilhaRepository;
            _certificadoRepository = certificadoRepository;
        }

        [HttpGet]
        public Task<List<Trilha>> GetAll()
        {
            return _trilhaRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public Task<Trilha> Get([FromRoute] Guid id)
        {
            return _trilhaRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task Post([FromBody] TrilhaCreate novo)
        {
            var trilha = new Trilha(novo.Descricao, Convert.ToInt32(novo.Ano), novo.Ativo, novo.Notificar);

            int seq = 0;
            foreach (var item in novo.Certificacoes)
            {
                seq++;
                var cert = await _certificadoRepository.GetAsync(item.CertificacaoId);
                trilha.Certificacoes.Add(new TrilhaCertificacao { CertificacaoId = cert.Id, Sequencia = seq });
            }
            await _trilhaRepository.AddAsync(trilha);
        }

        [HttpPut("{id}")]
        public async Task<Trilha> Put([FromRoute] Guid id, [FromBody] TrilhaUpdate update)
        {
            var trilha = await _trilhaRepository.GetAsync(id);

            trilha.Ano = Convert.ToInt32(update.Ano);
            trilha.Descricao = update.Descricao;
            trilha.Notificar = update.Notificar;

            return await _trilhaRepository.UpdateAsync(trilha);
        }



        [HttpDelete("{id}")]
        public Task Delete([FromRoute] Guid id)
        {
            return _trilhaRepository.DeleteAsync(id);
        }
    }
}
