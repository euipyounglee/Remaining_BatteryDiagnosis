using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace Dibier.mssql
{
    [Serializable()]
    public abstract class AResultSet : IResultSet
    {
        public void Fetch(MySqlDataReader reader)
        {
            PropertyInfo[] pis = this.GetType().GetProperties();

            for (int i=0; i<reader.FieldCount; ++i)
            {
                var pi = pis.SingleOrDefault(p => p.Name.Equals(reader.GetName(i)));
                if (pi != null)
                {
                    var value = reader[reader.GetName(i)];
                    if (value is System.DBNull)
                    {

                    }
                    else
                    {
                        pi.SetValue(this, value);
                    }
                }
            }

            //foreach (PropertyInfo pi in pis)
            //{
            //    try
            //    {
            //        var col = reader.GetDataTypeName(0);
            //        if (reader[pi.Name].GetType() != typeof(DBNull))
            //        {
            //            pi.SetValue(this, reader[pi.Name]);
            //        }
            //    }
            //    catch (Exception e) { }
            //} // end foreach
        }

    }
}
