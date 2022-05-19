using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class SQLiteDatabase : MonoBehaviour
{ 
    bool isTableCreated = false;
    bool isDataInserted = false;
 
    public void ScoreHandling()
    {
        CreateDataTable();
        if (isTableCreated)
        {
            InsertData();
        }
        if (isDataInserted)
        {

            ReadTableData();
        }
    }
    public void ScoreHandlingStart()
    {
         ReadTableData();
    }
    private void CreateDataTable()
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "My_Database";
        Debug.Log( connection );
        IDbConnection dbcon = new SqliteConnection( connection );
        dbcon.Open();
        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        string q_createTable = "CREATE TABLE IF NOT EXISTS my_table (id INTEGER PRIMARY KEY autoincrement, val INTEGER )";
        dbcmd.CommandText = q_createTable;
        dbcmd.ExecuteReader();
        dbcon.Close();
        isTableCreated = true;
    }
    public void DropDataTable()
    {
        //if (TableExists())
        //{
            string connection = "URI=file:" + Application.persistentDataPath + "/" + "My_Database";
            Debug.Log( connection );
            IDbConnection dbcon = new SqliteConnection( connection );
            dbcon.Open();
            IDbCommand dbcmd;
            dbcmd = dbcon.CreateCommand();
            string q_createTable = "DROP TABLE my_table";
            dbcmd.CommandText = q_createTable;
            dbcmd.ExecuteReader();
            dbcon.Close();
            isTableCreated = true;
        //}
        //else
        //{
        //    Debug.Log( "Table does not exit to drop..." );
        //}
    }
    private void InsertData()
    {
 
        isTableCreated = false;
        if (ScoreValues.scoreVal != null)
        {
            string connection = "URI=file:" + Application.persistentDataPath + "/" + "My_Database";
            IDbConnection dbcon = new SqliteConnection( connection );
            dbcon.Open();

            IDbCommand cmnd = dbcon.CreateCommand();

            cmnd.CommandText = $"INSERT INTO my_table (val) VALUES ({ScoreValues.scoreVal})";
            cmnd.ExecuteNonQuery();

            dbcon.Close();
            isDataInserted = true;
        }
    }
    public bool TableExists()
    {
        string con = "URI=file:" + Application.persistentDataPath + "/" + "My_Database";
        IDbConnection dbcon = new SqliteConnection( con);
        dbcon.Open();
        const string cmdText = "SELECT name FROM sqlite_master WHERE type='table' AND name='my_table'";
        IDbCommand cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = cmdText;
        // var cmd = con.CreateCommand( cmdText , typeof( T ).Name );
        var result = cmnd_read.ExecuteReader();
        bool showResult = result.Read();
        Debug.Log( "hasTable " + showResult);
        dbcon.Close();      
        return showResult;
    }
    private void ReadTableData()
    {
        isDataInserted = false;
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "My_Database";
        Debug.Log( connection );
        IDbConnection dbcon = new SqliteConnection( connection );
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT *, (SELECT COUNT(*) FROM my_table) as total_count FROM my_table";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            //Debug.Log( reader[0].ToString() + reader[1].ToString() + reader[2].ToString() );
            if (!ScoreValues.storeData.ContainsKey( reader[0].ToString() ))
            {
                ScoreValues.storeData.Add( reader[0].ToString() , reader[1].ToString() );
            }
            if (reader[2].ToString() != null)
            {
                ScoreValues.scoreCount = reader[2].ToString();
            }
        }
        dbcon.Close();
    }
}
