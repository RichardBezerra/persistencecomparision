using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceComparision.Core
{
    public class ManyModel
    {
        [AutoIncrement]
        public int Id { get; set; }

        public string Many { get; set; }
        
        public int OneModelId { get; set; }
    }
}
