try:
    import sqlite3
except ImportError:
    import clr
    clr.AddReference("IronPython.SQLite")
    # clr.AddReferenceToFile("System.Data.SQLite.DLL")
    clr.AddReference("System.Data.SQLite")
    from System.Data.SQLite import *

def createDB():
    try:
        # sqConnection = SQLiteConnection('Data Source=mydatabase.db;Version=3;')
        sqConnection = SQLiteConnection('Data Source=mybase.db;Version=3;')

        sqConnection.Open()
        sql = "CREATE TABLE IF NOT EXISTS create table A(id int); insert into A values(1);"
        cmd = SQLiteCommand(sql, sqConnection)
        cmd.ExecuteNonQuery()
        sqConnection.Close()

    except :
        return False
    return True

def createDB1():
    try:
        conn = sqlite3.connect('sqlite3.db', isolation_level=None)
        c = conn.cursor()
        c.execute("CREATE TABLE IF NOT EXISTS users(id INTEGER PRIMARY KEY, username TEXT, email TEXT, phone TEXT, regist_date TEXT)")
    except :
        return False
    return True
    

if __name__ == "__main__":
    createDB()