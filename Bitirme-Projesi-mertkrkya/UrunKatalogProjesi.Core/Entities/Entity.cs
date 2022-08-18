using System;

namespace UrunKatalogProjesi.Core.Entities
{
    public class Entity : CommonEntity
    {
        public int Id { get; set; }
    }

    public class CommonEntity
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedById { get; set; }
    }
}
