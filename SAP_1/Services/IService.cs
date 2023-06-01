namespace SAP_1.Services
{
    public interface IService<T> where T : class
    {
        public void Create(T empregado);
        public void Update(T obj);
        public void Delete(T obj);
        public ICollection<T> FindAll();
        public T Find(T obj);
    }
}
