﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace Persistencia.Models
{
    public partial class cursosbasesContext : IdentityDbContext<Usuario>
    {
        public cursosbasesContext()
        {
        }

        public cursosbasesContext(DbContextOptions<cursosbasesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comentario> Comentarios { get; set; }
        public virtual DbSet<Curso> Cursos { get; set; }
        public virtual DbSet<Cursoinstructor> Cursoinstructors { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Precio> Precios { get; set; }
        

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseMySQL("server=10.1.112.11; Uid=monty; Password=berninet2013; Database=cursosbases; Port=3306");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //crear los archivos de migración
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
            modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));
            
            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.Idcomentario)
                    .HasName("PRIMARY");

                entity.ToTable("comentario");

                entity.HasIndex(e => e.CursoId, "fkCurso_idx");

                entity.Property(e => e.Idcomentario).HasColumnName("idcomentario");

                entity.Property(e => e.AlumnoName).HasMaxLength(155);

                entity.Property(e => e.Comentario1)
                    .HasMaxLength(255)
                    .HasColumnName("Comentario");

                entity.Property(e => e.CursoId).HasColumnName("cursoId");

                entity.HasOne(d => d.Curso)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.CursoId)
                    .HasConstraintName("fkComentario");
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasKey(e => e.Idcurso)
                    .HasName("PRIMARY");

                entity.ToTable("curso");

                entity.Property(e => e.Idcurso).HasColumnName("idcurso");

                entity.Property(e => e.Descripcion).HasMaxLength(45);

                entity.Property(e => e.Fotoportada)
                    .HasMaxLength(255)
                    .HasColumnName("fotoportada");

                entity.Property(e => e.Nombre).HasMaxLength(255);
            });

            modelBuilder.Entity<Cursoinstructor>(entity =>
            {
                entity.HasKey(e => new { e.Idcurso, e.Idinstructor })
                    .HasName("PRIMARY");

                entity.ToTable("cursoinstructor");

                entity.HasIndex(e => e.Idinstructor, "fkInstructor_idx");

                entity.Property(e => e.Idcurso).HasColumnName("idcurso");

                entity.Property(e => e.Idinstructor).HasColumnName("idinstructor");

                entity.HasOne(d => d.IdcursoNavigation)
                    .WithMany(p => p.Cursoinstructors)
                    .HasForeignKey(d => d.Idcurso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCurso");

                entity.HasOne(d => d.IdinstructorNavigation)
                    .WithMany(p => p.Cursoinstructors)
                    .HasForeignKey(d => d.Idinstructor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkInstructor");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.HasKey(e => e.Idinstructor)
                    .HasName("PRIMARY");

                entity.ToTable("instructor");

                entity.Property(e => e.Idinstructor).HasColumnName("idinstructor");

                entity.Property(e => e.Apellidos).HasMaxLength(150);

                entity.Property(e => e.Foto).HasMaxLength(255);

                entity.Property(e => e.Grado).HasMaxLength(150);

                entity.Property(e => e.Nombre).HasMaxLength(150);
            })
                ;

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
