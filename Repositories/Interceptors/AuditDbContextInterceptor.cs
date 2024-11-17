using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Repositories.Interceptors
{
	public class AuditDbContextInterceptor : SaveChangesInterceptor
	{
		private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviours = new()
		{
			{ EntityState.Added, AddBehaviour },
			{ EntityState.Modified, ModifiedBehaviour }
		};
		private static void AddBehaviour(DbContext context, IAuditEntity auditEntity)
		{
			auditEntity.Created = DateTime.Now;
			context.Entry(auditEntity).Property(x => x.Updated).IsModified = false; //we don't want to change the updated date
		}
		private static void ModifiedBehaviour(DbContext context, IAuditEntity auditEntity)
		{
			auditEntity.Updated = DateTime.Now;
			context.Entry(auditEntity).Property(x => x.Created).IsModified = false; //we don't want to change the created date
		}
		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
			CancellationToken cancellationToken = new CancellationToken()) //these are hooks
		{
			foreach (var entityEntry in eventData.Context.ChangeTracker.Entries().ToList())
			{
				if (entityEntry.Entity is not IAuditEntity auditEntity) continue;
				Behaviours[entityEntry.State](eventData.Context, auditEntity);

			}
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}
	}
}
