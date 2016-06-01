using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceComparision.Core
{
    [Table("TinyModel")]
    public class TinyModel : ModelBase
    {
        public string Descricao { get; set; }
    }
}
