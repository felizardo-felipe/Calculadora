using Calculadora.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Calculo>().Wait();
        }

        public Task<List<Calculo>> GetCalculosAsync()
        {
            return _database.Table<Calculo>().ToListAsync();
        }

        public Task<int> SalvaCalculoAsync(Calculo calculo)
        {
            return _database.InsertAsync(calculo);
        }
    }
}
