using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceComparision.Core
{
    [Table("TinyModel")]
    public class TinyModel
    {
        [AutoIncrement]
        public int Id { get; set; }

        public string Descricao { get; set; }
    }
}
