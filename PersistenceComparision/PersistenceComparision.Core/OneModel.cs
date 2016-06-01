using ServiceStack.DataAnnotations;
using System.Collections.Generic;

namespace PersistenceComparision.Core
{
    public class OneModel : ModelBase
    {
        public OneModel()
        {
            this.Many = new List<ManyModel>();
        }

        public string One { get; set; }

        [Reference]
        public virtual List<ManyModel> Many { get; set; }
    }
}
