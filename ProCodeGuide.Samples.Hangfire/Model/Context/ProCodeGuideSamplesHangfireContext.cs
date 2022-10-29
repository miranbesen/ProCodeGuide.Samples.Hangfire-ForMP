using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProCodeGuide.Samples.Hangfire.Model.Context
{
    public partial class ProCodeGuideSamplesHangfireContext : DbContext
    {
        public ProCodeGuideSamplesHangfireContext()
        {
        }

        public ProCodeGuideSamplesHangfireContext(DbContextOptions<ProCodeGuideSamplesHangfireContext> options)
            : base(options)
        {
        }


        public virtual DbSet<TaskInformation> TaskInformations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=MIRANPC\\SQLEXPRESS;initial catalog=ProCodeGuide.Samples.Hangfire;trusted_connection=yes;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TaskInformation>(entity =>
            {
                entity.ToTable("TaskInformation");

                entity.Property(e => e.ServiceUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ToMail)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
