using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
namespace HUDLL.Models
{
    public class HistoryContext : DbContext
    {
        public HistoryContext(DbContextOptions<HistoryContext> options)
            : base(options) { }

        public DbSet<TimePeriod> TimePeriods { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Figure> Figures { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<PostFigure> PostFigures { get; set; }
        public DbSet<PostEventType> PostEventTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostFigure>()
                .HasKey(bc => new { bc.PostId, bc.FigureId });
            modelBuilder.Entity<PostFigure>()
                .HasOne(bc => bc.Post)
                .WithMany(b => b.PostFigures)
                .HasForeignKey(bc => bc.PostId);
            modelBuilder.Entity<PostFigure>()
                .HasOne(bc => bc.Figure)
                .WithMany(c => c.PostFigures)
                .HasForeignKey(bc => bc.FigureId);

            modelBuilder.Entity<PostEventType>()
                .HasKey(bc => new { bc.PostId, bc.EventTypeId });
            modelBuilder.Entity<PostEventType>()
                .HasOne(bc => bc.Post)
                .WithMany(b => b.PostEventTypes)
                .HasForeignKey(bc => bc.PostId);
            modelBuilder.Entity<PostEventType>()
                .HasOne(bc => bc.EventType)
                .WithMany(c => c.PostEventTypes)
                .HasForeignKey(bc => bc.EventTypeId);
        }
    }

    public class TimePeriod
    {
        public int TimePeriodId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Name { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string MediaContent { get; set; }
        public DateTime PostDate { get; set; }

        public int TimePeriodId { get; set; }
        public TimePeriod TimePeriod { get; set; }

        public ICollection<PostFigure> PostFigures { get; set; }
        public ICollection<PostEventType> PostEventTypes { get; set; }
    }

    public class Figure
    {
        public int FigureId { get; set; }
        public string FigureType { get; set; }

        public ICollection<PostFigure> PostFigures { get; set; }
    }

    public class EventType
    {
        public int EventTypeId { get; set; }
        public string EventTypeName { get; set; }

        public ICollection<PostEventType> PostEventTypes { get; set; }
    }

    public class PostFigure
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int FigureId { get; set; }
        public Figure Figure { get; set; }
    }

    public class PostEventType
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }
    }
}
