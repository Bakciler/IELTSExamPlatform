namespace IELTSExamPlatform.CORE.Entities.Common
{
    public abstract class BaseEntity
    {
        public string Id { get; set; } = Helpers.IdGenerator.GenerateId();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
