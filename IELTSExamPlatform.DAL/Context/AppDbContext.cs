using IELTSExamPlatform.CORE.Entities;
using IELTSExamPlatform.CORE.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IELTSExamPlatform.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Reading> Readings { get; set; }
        public DbSet<ReadingPassage> ReadingPassages { get; set; }
        public DbSet<ReadingParagraphs> ReadingParagraphs { get; set; }

        public DbSet<ReadingQuestion> ReadingQuestions { get; set; }
        public DbSet<BooleanQuestion> BooleanQuestions { get; set; }
        public DbSet<ChoiceQuestion> ChoiceQuestions { get; set; } // ✅ düzəldildi
        public DbSet<FillInTheBlank> FillInTheBlanks { get; set; }
        public DbSet<MatchHeadingsQuestion> MatchHeadingsQuestions { get; set; }

        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<Blank> Blanks { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Heading> Headings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TPH mapping: ReadingQuestion base class
            modelBuilder.Entity<ReadingQuestion>()
                .HasDiscriminator<string>("QuestionType")
                .HasValue<BooleanQuestion>("Boolean")
                .HasValue<ChoiceQuestion>("Choice")
                .HasValue<FillInTheBlank>("FillInTheBlank")
                .HasValue<MatchHeadingsQuestion>("MatchHeading");

            // Sentence → ReadingQuestion (TPH olduğuna görə base class-a bağlanır)
            modelBuilder.Entity<Sentence>()
                .HasOne<ReadingQuestion>()
                .WithMany()
                .HasForeignKey(s => s.FillInTheBlankId)
                .OnDelete(DeleteBehavior.Cascade);

            // Blank → Sentence
            modelBuilder.Entity<Blank>()
                .HasOne(b => b.Sentence)
                .WithMany(s => s.Blanks)
                .HasForeignKey(b => b.SentenceId)
                .OnDelete(DeleteBehavior.Cascade);

            // QuestionOption → ChoiceQuestion
            modelBuilder.Entity<QuestionOption>()
                .HasOne(q => q.ChoiceQuestion)
                .WithMany(c => c.QuestionOptions)
                .HasForeignKey(q => q.ChoiceQuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<ReadingQuestion>()
                         .Where(e => e.State == EntityState.Added))
            {
                var passageId = entry.Entity.ReadingPassageId;
                var maxOrder = ReadingQuestions
                    .Where(q => q.ReadingPassageId == passageId)
                    .Max(q => (int?)q.Order) ?? 0;

                entry.Entity.Order = maxOrder + 1;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}