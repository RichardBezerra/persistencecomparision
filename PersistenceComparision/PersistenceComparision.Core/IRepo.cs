using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceComparision.Core
{
    public interface IRepo
    {
        void Create(TinyModel model);

        void Create(OneModel one);

        TinyModel ReadTinyModel(int id);

        OneModel ReadOneModel(int id);

        void Update(TinyModel model);

        void Delete(TinyModel model);
    }
}
