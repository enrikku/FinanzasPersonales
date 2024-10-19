using SQLite;
using FinanzasPersonales.Models;

namespace FinanzasPersonales.DB
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            try
            {
                _database = new SQLiteAsyncConnection(dbPath);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Metodo para inicializar la base de datos
        /// </summary>
        /// <returns></returns>
        public async Task InitializeDatabaseAsync()
        {
            try
            {
                await _database.CreateTableAsync<Transaccion>();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Metodo para obtener los registros de una tabla
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return _database.Table<T>().ToListAsync();
        }

        /// <summary>
        /// Metodo para insertar un nuevo registro en la base de datos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<int> SaveItemAsync<T>(T item) where T : new()
        {
            return _database.InsertAsync(item);
        }

        /// <summary>
        /// Método para eliminar todos los registros de una tabla.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<int> DeleteAllItemsAsync<T>() where T : new()
        {
            return _database.DeleteAllAsync<T>();
        }

        /// <summary>
        /// Método para eliminar un registro de la base de datos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<int> DeleteItemAsync<T>(T item) where T : new()
        {
            return _database.DeleteAsync(item);
        }
    }
}
