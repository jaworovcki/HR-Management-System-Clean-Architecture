using HR.Leave.Management.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Configuration
{
	public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
	{
		public void Configure(EntityTypeBuilder<LeaveType> builder)
		{
			builder.HasData(new LeaveType
			{
				Id = 1,
				Name = "Annual",
				DateCreated = DateTime.Now,
				DateModified = DateTime.Now
			});

			builder.Property(q => q.Name)
				.IsRequired()
				.HasMaxLength(100);
		}
	}
}
