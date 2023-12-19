using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnion104.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string CreateBy { get; set; } = null!;

        public BaseEntity()
        {
                CreatedAt = DateTime.Now;
            CreateBy = "Yusif.Jalilov";
        }

    }
}
