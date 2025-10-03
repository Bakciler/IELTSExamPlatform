using IELTSExamPlatform.CORE.Entities;
using IELTSExamPlatform.CORE.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace IELTSExamPlatform.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Reading> Readings { get; set; }
        public DbSet<ReadingPassage> ReadingPassages { get; set; }
        public DbSet<ReadingParagraphs> ReadingParagraphs { get; set; }
        public DbSet<Heading> Headings { get; set; }
        public DbSet<ChoiceQuestion> ChoiceQuestions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<BooleanQuestion> BooleanQuestions { get; set; }
        public DbSet<MatchHeadingsQuestion> MatchHeadingsQuestions { get; set; }
        public DbSet<FillInTheBlank> FillInTheBlanks { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<Blank> Blanks { get; set; }
        public DbSet<ReadingQuestion> ReadingQuestions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ReadingQuestion>().ToTable("ReadingQuestions");
            builder.Entity<ChoiceQuestion>().ToTable("ChoiceQuestions");
            builder.Entity<BooleanQuestion>().ToTable("BooleanQuestions");
            builder.Entity<MatchHeadingsQuestion>().ToTable("MatchHeadingsQuestions");
            builder.Entity<FillInTheBlank>().ToTable("FillInTheBlanks");
            
            base.OnModelCreating(builder);
        }
    }
}
