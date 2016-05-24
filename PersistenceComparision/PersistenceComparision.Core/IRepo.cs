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

        TinyModel Read(int id);

        void Update(TinyModel model);

        void Delete(TinyModel model);
    }
}
