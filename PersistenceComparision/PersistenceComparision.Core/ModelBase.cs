using ServiceStack.DataAnnotations;

namespace PersistenceComparision.Core
{
    public class ModelBase
    {
        [AutoIncrement]
        public int Id { get; set; }
    }
}
