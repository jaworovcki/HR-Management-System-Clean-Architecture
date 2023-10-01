using HR.Leave.Management.Application.Contracts.Persistence;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly HrDataContext _context;

		public GenericRepository(HrDataContext context)
        {
			_context = context;
		}

        public async Task<T> CreateAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			await _context.SaveChangesAsync();

			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			_context.Set<T>().Remove(entity);

			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			var entities = await _context.Set<T>()
				.AsNoTracking()
				.ToListAsync();

			return entities;
		}

		public async Task<T> GetByIdAsync(int id)
		{
			var entity = await _context.Set<T>()
				.FindAsync(id);

			return entity;
		}

		public async Task UpdateAsync(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;

			await _context.SaveChangesAsync();
		}
	}
}
