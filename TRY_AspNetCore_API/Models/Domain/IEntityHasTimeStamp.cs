namespace TRY_AspNetCore_API.Models.Domain
{
    public interface IEntityHasTimeStamp
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
