using IELTSExamPlatform.CORE.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IELTSExamPlatform.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Reading> Readings { get; set; }
        public DbSet<ReadingPassage> ReadingPassages { get; set; }
        public DbSet<ReadingParagrahs> ReadingParagrahs { get; set; }
        public DbSet<Heading> Headings { get; set; }
        public DbSet<ChoiceQuestion> ChoicesQuestions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<BooleanQuestion> BooleanQuestions { get; set; }
        public DbSet<MatchHeadingsQuestion> MatchHeadingsQuestions { get; set; }
        public DbSet<FillInTheBlank> FillInTheBlanks { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<Blank> Blanks { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
