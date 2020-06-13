using Audit.Core;
using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Mycroft.EntityFrameworkCore.Core;
using Mycroft.EntityFrameworkCore.Core.Models.Admin;
using Mycroft.EntityFrameworkCore.Core.Models.Approval;
using System;
using System.Linq;
using Action = Mycroft.EntityFrameworkCore.Core.Models.Approval.Action;

namespace Mycroft.EntityFrameworkCore.Data
{
    public class DataEngineDbContext : AuditDbContext
    {
        public DataEngineDbContext(DbContextOptions<DataEngineDbContext> options) : base(options)
        {
            //this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.ProxyCreationEnabled = false;
        }

        #region Admin

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AdminLogin> AdminLogins { get; set; }
        public virtual DbSet<AdminRole> AdminRoles { get; set; }

        #endregion Admin

        #region Approval

        public virtual DbSet<ApprovalConfiguration> ApprovalConfigurations { get; set; }
        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<Approval> Approvals { get; set; }

        #endregion Approval

        #region Shared

        public virtual DbSet<AuditLog> AuditLogs { get; set; }

        #endregion Shared

        public override void OnScopeCreated(AuditScope auditScope)
        {
            if (Database.CurrentTransaction == null)
                Database.BeginTransaction();
        }

        public override void OnScopeSaving(AuditScope auditScope)
        {
            try
            {
                // ... custom log saving ...
            }
            catch
            {
                // Rollback call is not mandatory. If exception thrown, the transaction won't get commited
                Database.CurrentTransaction.Rollback();
                throw;
            }
            Database.CurrentTransaction.Commit();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApprovalConfiguration>(entity =>
            {
                entity.HasIndex(e => e.ActionId).IsUnique();
            });

            modelBuilder.Entity<Action>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasIndex(e => e.Code).IsUnique();
            });

            modelBuilder.Entity<AdminLogin>(entity =>
            {
                entity.HasIndex(e => e.username)
                    .IsUnique();
            });

            #region AuditTrail

            Audit.Core.Configuration.Setup()
                .UseEntityFramework(_ => _
                    .AuditTypeMapper(t => typeof(AuditLog))
                    .AuditEntityAction<AuditLog>((ev, entry, entity) =>
                    {
                        string createdby = Environment.UserName;
                        var createdbycolumname = entry.ColumnValues.Keys.SingleOrDefault(x => x == "CreatedBy");
                        if (!string.IsNullOrEmpty(createdbycolumname))
                            createdby = entry.ColumnValues[createdbycolumname].ToString();

                        entity.AuditData = entry.ToJson();
                        entity.EntityType = entry.EntityType.Name;
                        entity.AuditDate = DateTime.Now;
                        entity.AuditUser = createdby; // Environment.UserName;
                        entity.TablePk = entry.PrimaryKey.First().Value.ToString();
                    })
                .IgnoreMatchedProperties(true));

            #endregion AuditTrail
        }
    }
}