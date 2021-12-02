using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using IronPython;
using IronPython.SQLite;



namespace BatteryDashBoard
{
    class ClassSqlite
    {
        string currentRoot = System.IO.Directory.GetCurrentDirectory();
        SQLiteConnection _sqliteConn = null;
        webServer _wServer = null;
        public ClassSqlite()
        {

        }

        public ClassSqlite(webServer wServer)
        {
            _wServer = wServer;
        }

         ~ClassSqlite()
        {
            // _sqliteConn.Close();
            Console.WriteLine("_sqliteConn ...");
        }

        public bool CreateDataSqlite(string root)
        {
            bool bResult = false;
            string _DbFile = "MyDatabase.db";
            if (root.Length > 0) {
                currentRoot = root;
            }
            _DbFile = string.Format("{0}\\{1}", currentRoot, _DbFile);

            string ConnectionString = string.Format("Data Source={0};Version=3;", _DbFile);
            try
            {
                if (!System.IO.File.Exists(_DbFile))
                {
                    SQLiteConnection.CreateFile(_DbFile);  // SQLite DB 생성
                    bResult = true;
                    MessageBox.Show("DB 생성되었습니다");
                }


                // 테이블 생성 코드
                 _sqliteConn = new SQLiteConnection(ConnectionString);
                _sqliteConn.Open();

#if false
                string strsql = "CREATE TABLE IF NOT EXISTS scores (name varchar(20), score int)";

                SQLiteCommand cmd = new SQLiteCommand(strsql, _sqliteConn);
                cmd.ExecuteNonQuery();
#endif

                crateTbl_pg_user_view();
                ClassPython py = new ClassPython(_wServer);
                py.CallFileView("database", "sqlite3db.py");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
             //   return false;
            }


            return bResult;// true;

        }

        public bool crateTbl_pg_user_view()
        {
            bool bResult = true;
            string strsql = "";
            strsql = "CREATE TABLE IF NOT EXISTS `pg_user_view` (";
            strsql += "`USER_ID` varchar(30)  ,";//사용자 ID
            strsql += "`USER_NM` varchar(80)   DEFAULT NULL ,";//사용자 명
                     strsql += "`USER_PW` varchar(4000)   DEFAULT NULL   ,";//사용자 비밀번호
                     strsql += "`EMAIL` varchar(50)   DEFAULT NULL  ,";//이메일
                     strsql += "`TELNO` varchar(30)   DEFAULT NULL  ,";//전화번호
                     strsql += "`ROLE` varchar(30)   DEFAULT NULL  ,";//역할
                     strsql += "`EVAL_QUICK_AT` varchar(1)   DEFAULT NULL    ,";//잔존가치평가 빠른검사 권한 여부
                     strsql += "`EVAL_STD_AT` varchar(1)   DEFAULT NULL ,";// 잔존가치평가 표준검사 권한 여부
                     strsql += "`EVAL_DETAIL_AT` varchar(1)   DEFAULT NULL ,";//잔존가치평가 정밀검사 권한 여부
                     strsql += "`SAFE_AT` varchar(1)   DEFAULT NULL   ,";// 안전검사 권한 여부
                     strsql += "`OCV_AT` varchar(1)   DEFAULT NULL  ,";//OCV 권한 여부
                     strsql += "PRIMARY KEY(`USER_ID`)";
                     strsql += ")";


            try
            {
                SQLiteCommand cmd = new SQLiteCommand(strsql, _sqliteConn);
                cmd.ExecuteNonQuery();
            }catch(Exception ex)
            {
                bResult = false;
                MessageBox.Show(ex.Message.ToString(), "Error");
            }

            return bResult;

        }

    }
}
