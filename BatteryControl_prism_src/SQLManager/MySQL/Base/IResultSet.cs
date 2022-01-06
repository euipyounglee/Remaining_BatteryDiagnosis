using MySql.Data.MySqlClient;

namespace Dibier.mssql
{
    public interface IResultSet
    {
        void Fetch(MySqlDataReader reader);
    }
}
