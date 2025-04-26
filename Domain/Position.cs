using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Position : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? NumPoints { get; set; }
        public Guid PointPriceId {  get; set; }
        public virtual PointPrice PointPrice { get; set; }
    }
}
