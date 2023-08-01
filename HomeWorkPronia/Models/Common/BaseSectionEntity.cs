namespace HomeWorkPronia.Models.Common
{
    public class BaseSectionEntity : BaseEntity
    {
        public bool IsDeleted {  get; set; }
        public  DateTime CreatedDate { get; set; }
        public  string CreatedBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdatedBy { get; set; }
    }
}
