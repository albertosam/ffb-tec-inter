using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrilhasCertificacao.Domain;
using TrilhasCertificacao.Infrastructure;

namespace TrilhasCertificacao.Infrastructure.Repositories
{
    public class TrilhaRepository : ITrilhaRepository
    {
        private readonly TrilhaContext _context;

        public TrilhaRepository(TrilhaContext context)
        {
            _context = context;
        }

        public async Task<Trilha> AddAsync(Trilha entity)
        {
            await _context.Trilhas.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var ent = await GetAsync(id);
            _context.Remove(ent);
            await _context.SaveChangesAsync();
        }

        public Task<List<Trilha>> GetAllAsync()
        {
            return _context.Trilhas.Include(a => a.Certificacoes).ToListAsync();
        }

        public async Task<Trilha> GetAsync(Guid id)
        {
            return await _context.Trilhas.Include(x => x.Certificacoes)
                                            .ThenInclude(x => x.Certificacao)
                                            .Where(x => x.Id == id)
                                            .FirstOrDefaultAsync();
        }

        public async Task<Trilha> UpdateAsync(Trilha entity)
        {
            var entityDatabase = await GetAsync(entity.Id);
            _context.Entry(entityDatabase).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
