namespace BloggingAPI.v1
{
    public class BaseModel
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public int VersionNo { get; set; } = 1;

        public bool IsActive { get; set; } = true;

    }
}
