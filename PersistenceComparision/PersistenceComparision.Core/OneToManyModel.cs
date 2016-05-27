using ServiceStack.DataAnnotations;
using System.Collections.Generic;

namespace PersistenceComparision.Core
{
    public class OneModel
    {
        [AutoIncrement]
        public int Id { get; set; }

        public string OneDescription { get; set; }

        public List<ManyModel> Many { get; set; }
    }

    public class ManyModel
    {
        [AutoIncrement]
        public int Id { get; set; }

        public string ManyDescription { get; set; }
    }
}
