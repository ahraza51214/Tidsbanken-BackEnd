using System;
namespace Tidsbanken_BackEnd.Services
{
    // Generic interface for CRUD (Create, Read, Update, Delete) methods on entities of type T with an ID of type ID.
    public interface ICrudService<T, ID>
    {
        // Asynchronously get all entities of type T.
        Task<IEnumerable<T>> GetAllAsync();

        // Asynchronously get an entity of type T by its ID.
        Task<T?> GetByIdAsync(ID id);

        // Asynchronously add a new entity of type T to the database.
        Task<T> AddAsync(T obj);

        // Asynchronously update an existing entity of type T in the database.
        Task<T> UpdateAsync(T obj);

        // Asynchronously delete an entity of type T by its ID from the database.
        Task DeleteAsync(ID id);
    }
}