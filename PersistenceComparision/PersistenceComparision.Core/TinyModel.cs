using ServiceStack.DataAnnotations;

namespace PersistenceComparision.Core
{
    public class TinyModel
    {
        [AutoIncrement]
        public int Id { get; set; }

        public string Descricao { get; set; }
    }
}
