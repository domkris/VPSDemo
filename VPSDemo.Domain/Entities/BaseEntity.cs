namespace VPSDomain.Domain.Common
{
    /// <summary>
    /// For Demo purpose, entity only has CreationDate,
    /// In real world scenario additional properties such as UpdateDate, CreatedBy and UpdatedBy could be added
    /// </summary>
    public class BaseEntity
    {
        public DateTime CreationDate { get; set; }
    }
}
