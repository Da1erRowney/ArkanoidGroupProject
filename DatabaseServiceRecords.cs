using Microsoft.VisualBasic.ApplicationServices;
using SQLite;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WinFormsApp1;

namespace Recordes
{
    public class DatabaseServiceRecords
    {
        private SQLiteConnection _connection;

        public DatabaseServiceRecords(string databasePath)
        {
            _connection = new SQLiteConnection(databasePath);
            _connection.CreateTable<Record>();
        }

        public void InsertRecord(Record data)
        {
            _connection.Insert(data);
        }

        public List<Record> GetAllRecord()
        {
            return _connection.Table<Record>().ToList();
        }

        public void UpdateRecord(Record data)
        {
            _connection.Update(data);
        }
        public Record GetNickName(string nick)
        {
            return _connection.Table<Record>().FirstOrDefault(u => u.NickName == nick);
        }
        public void CloseConnection()
        {
            _connection?.Close();
        }
    }

    public class Record
    {
        [PrimaryKey]
        public string NickName { get; set; }
        public int RecordUser { get; set; }


    }
}

