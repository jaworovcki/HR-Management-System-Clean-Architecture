using HR.Leave.Management.Domain;
using HR.Leave.Management.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.DatabaseContext
{
	public class HrDataContext : DbContext
	{
        public HrDataContext(DbContextOptions<HrDataContext> options)
            : base(options)
        {
            
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }

        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDataContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State is EntityState.Added || q.State is EntityState.Modified))
            {
                entry.Entity.DateModified = DateTime.Now;

                if (entry.State is EntityState.Added)
                {
					entry.Entity.DateCreated = DateTime.Now;
				}
            }

			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
