using System.Collections.Generic;
using System.Linq;

namespace TheStillHeron.TestWorkshop.SoccerApi.DataStorage
{
    public interface IDataStore
    {
        void Put(string tableName, IStorable storableItem);

        T Get<T>(string tableName, string id) where T : IStorable;

        IList<T> Get<T>(string tableName) where T : IStorable;
    }

    public class DataStore : IDataStore
    {
        // This is a very rudimentary datbase - it has "tables" and you can store/retrieve things by id
        // We'll use this as a super-simple in-memory database to keep this api streamlined
        private IDictionary<string, IDictionary<string, IStorable>> _database;

        private void PutTable(string tableName)
        {
            if (!_database.ContainsKey(tableName))
            {
                _database.Add(tableName, new Dictionary<string, IStorable>());
            }
        }

        public DataStore()
        {
            _database = new Dictionary<string, IDictionary<string, IStorable>>();
        }

        public void Put(string tableName, IStorable storableItem)
        {
            PutTable(tableName);
            if (!_database[tableName].ContainsKey(storableItem.Id))
            {
                _database[tableName].Add(storableItem.Id, storableItem);
            }
            else
            {
                _database[tableName][storableItem.Id] = storableItem;
            }
        }

        public T Get<T>(string tableName, string id) where T : IStorable
        {
            PutTable(tableName);
            if (!_database[tableName].ContainsKey(id))
            {
                return default(T);
            }
            return (T)_database[tableName][id];
        }

        public IList<T> Get<T>(string tableName) where T : IStorable
        {
            PutTable(tableName);
            return _database[tableName].Values
                .Select(x => (T)x)
                .ToList();
        }
    }
}