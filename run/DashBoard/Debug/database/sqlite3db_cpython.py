try:
    import sqlite3
except ImportError:
    import clr
    clr.AddReference("IronPython.SQLite")
    import IronPython.SQLite as sqlite3


def createDB():
    try:
        conn = sqlite3.connect('sqlite3.db', isolation_level=None)
        c = conn.cursor()
        c.execute("CREATE TABLE IF NOT EXISTS users(id INTEGER PRIMARY KEY, username TEXT, email TEXT, phone TEXT, regist_date TEXT)")
    except :
        return False
    return True
    

if __name__ == "__main__":
    createDB()