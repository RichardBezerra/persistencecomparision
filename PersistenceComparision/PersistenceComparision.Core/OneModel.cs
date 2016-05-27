using System.Collections.Generic;

namespace PersistenceComparision.Core
{
    public class OneModel
    {
        public OneModel()
        {
            this.Many = new List<ManyModel>();
        }

        public int Id { get; set; }

        public string One { get; set; }

        public virtual ICollection<ManyModel> Many { get; set; }
    }
}
