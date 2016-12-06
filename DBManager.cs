using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

public class DBManager : MonoBehaviour {

    [DllImport("uft")]
    public static extern void dbopen();
    
    [DllImport("uft")]
    public static extern int exec_query(StringBuilder query);
    [DllImport("uft")]
    public static extern IntPtr exec_select(StringBuilder query);
    

    public string table_name;
    public string pet_table_name;
    public string assets_table_name;
    public string inventory_table_name;
    public string bluetooth_table_name;
    public char field_separator = '/';
    public char week_separator = '|';
    public char record_separator = ',';
    private int activity_auto_increment;
    private int pet_auto_increment;
    public Text debugText;
    public Text ActivityViewer;
    public AlarmManager alarm_manager;
    // Use this for initialization
    void Start () {

        DBopen();
        CreateTable();
        string last_id=Select("SELECT id FROM " + table_name+" order by id desc limit 1");
        if (last_id == ""|| last_id == ",")
            activity_auto_increment = 0;
        else
            activity_auto_increment = 
                Int32.Parse(last_id.Substring(0,last_id.Length-1))+1;
        

        last_id = Select("SELECT id FROM " + pet_table_name + " order by id desc limit 1");
        if (last_id == ""|| last_id == ",")
        {
            StringBuilder query =
                new StringBuilder("INSERT INTO " + pet_table_name +
                " VALUES(0,'noname,','0,','0.0,','0.0');");
            
            int ret=exec_query(query);
        }

        last_id = Select("SELECT id FROM " + assets_table_name + " order by id desc limit 1");
        if (last_id == ""|| last_id == ",")
        {
            StringBuilder query =
                new StringBuilder("INSERT INTO " + assets_table_name +
                " VALUES(0,'0','0');");
            int ret = exec_query(query);
        }

        last_id = Select("SELECT id FROM " + inventory_table_name + " order by id desc limit 1");
        if (last_id == ""|| last_id == ",")
        {
            StringBuilder query =
                new StringBuilder("INSERT INTO " + inventory_table_name +
                " VALUES(0,'0','0','0');");
            int ret = exec_query(query);
        }

        last_id = Select("SELECT id FROM " + bluetooth_table_name + " order by id desc limit 1");
        string addr = Select("SELECT addr FROM " + bluetooth_table_name + " WHERE id=0;");
        debugText.text = last_id;
        if (last_id == ""||addr==""||addr=="0,")
        {
            StringBuilder query = new StringBuilder("DELETE FROM " + bluetooth_table_name);
            int ret = exec_query(query);
            query =new StringBuilder(
                "INSERT INTO "+bluetooth_table_name+" VALUES(0,'0','0');");
            ret= exec_query(query);
            debugText.text = "Insert ret:" + ret;
            ret=alarm_manager.SearchDevice();
            debugText.text = "Search Device " + ret+" last_id:"+last_id+
                ",addr="+addr;
            //SceneManager.LoadScene("Scenes/StartScene");
        }
        else
        {
            addr = Select("SELECT addr FROM " + bluetooth_table_name + " WHERE id=0;");
            debugText.text = addr;
            alarm_manager.Renew();
        }
  
    }

    public void DBopen()
    {
        dbopen();
    }
    public void CreateTable()
    {
        StringBuilder query = new StringBuilder("CREATE TABLE IF NOT EXISTS "+table_name+
        "(id int " +
        ",activity_type varchar(2),"+
        "week varchar(18),start_h varchar(3),"+
        "start_m varchar(3),duration_h varchar(3),"+
        "duration_m varchar(3));");
        int ret=exec_query(query);
        

        query = new StringBuilder("CREATE TABLE IF NOT EXISTS " + pet_table_name +
            "(id int ,name varchar(15),level varchar(5),exp varchar(5),newexp varchar(5));");
        ret=exec_query(query);
        

        query = new StringBuilder("CREATE TABLE IF NOT EXISTS " + assets_table_name +
            "(id int,asset varchar(10),newasset varchar(10));");
        ret = exec_query(query);
        debugText.text = "Create Table " + ret + ", table_name:" + assets_table_name;

        query = new StringBuilder("CREATE TABLE IF NOT EXISTS " + inventory_table_name +
            "(id int,feed varchar(3),gum varchar(3),ball varchar(3));");
        ret = exec_query(query);
        debugText.text = " Create Table " + ret + ",table _name:" + inventory_table_name;

        query = new StringBuilder("CREATE TABLE IF NOT EXISTS " + bluetooth_table_name +
            "(id int,name varchar(32),addr varchar(32));");
        ret = exec_query(query);
        debugText.text = " CREATE TABLE " + ret + ",TABLE_NAME:" + bluetooth_table_name;

    }
    public void Insert(string type,string week,string start_h,string start_m,
        string duration_h, string duration_m)
    {
        StringBuilder query = 
            new StringBuilder("INSERT INTO "+ table_name +
            "(id,activity_type,week,start_h,start_m,duration_h,duration_m) " +
            "VALUES ("+(activity_auto_increment++)+
            ",'" + type + "','" + week + "','" + start_h + "','" + start_m + 
            "','"+ duration_h + "','" + duration_m + "');");
        int ret=exec_query(query);
        debugText.text = "Insert" + ret+" query:"+query.ToString();
    }

    public void UpdatePet(string name,string lv,string exp)
    {
        StringBuilder query =
            new StringBuilder("UPDATE " + pet_table_name +
            " SET name='" + name + record_separator + "',level='" + lv + record_separator +
            "',exp='" + exp + record_separator+"',newexp='0.0' where id=0;");
        int ret = exec_query(query);
        //debugText.text = "UPDATE " + ret + " query:" + query.ToString();
    }
    public string Select(string select_query)
    {
        StringBuilder query = new StringBuilder(select_query);
        IntPtr ptr = exec_select(query);
        return Marshal.PtrToStringAnsi(ptr);
        
        
    }
    public void UpdateQuery(String update_query)
    {
        StringBuilder query = new StringBuilder(update_query);
        int ret=exec_query(query);
        //debugText.text = " update :" + ret+" query:"+update_query;
    }
    public void DeleteFrom(String delete_query)
    {
        StringBuilder query = new StringBuilder(delete_query);
        exec_query(query);
    }
}
