using SAP_1.Models;

namespace SAP_1.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        public void Create(T obj);
        public void Update(T obj);
        public void Delete(T obj);
        public ICollection<T> FindAll();
        public T Find(T obj);
    }
}
