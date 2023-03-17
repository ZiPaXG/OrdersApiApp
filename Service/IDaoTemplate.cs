using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service
{
    // Dao - data access object
    // Шаблон CRUD операций

    public interface IDaoTemplate<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T element);
        Task<T> Update(T element);
        Task<bool> Delete(int id);
    }
}