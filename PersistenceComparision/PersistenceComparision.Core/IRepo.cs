namespace PersistenceComparision.Core
{
    public interface IRepo
    {
        void Create(TinyModel model);

        void Create(OneModel one);

        TinyModel ReadTinyModel(int id);

        OneModel ReadOneModel(int id);

        void Update(TinyModel model);

        void Update(OneModel model);

        void Delete(TinyModel model);

        void Delete(OneModel model);
    }
}
