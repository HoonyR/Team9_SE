using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DBManager : MonoBehaviour {

    [DllImport("uft")]
    public static extern void dbopen();
    [DllImport("uft")]
    public static extern int createtable();
    [DllImport("uft")]
    public static extern void insert(StringBuilder type,StringBuilder start_h,
        StringBuilder start_m, StringBuilder end_h, StringBuilder end_m);
    [DllImport("uft")]
    public static extern IntPtr tizensharedlibrary40();
    [DllImport("uft")]
    public static extern void deletefrom(StringBuilder con);
    [DllImport("uft")]
    public static extern int init_alarm();

    public Text debugText;
    public Text ActivityViewer;
    // Use this for initialization
    void Start () {

        DBopen();
        CreateTable();
        int a=init_alarm();
        if (a==0)
            debugText.text = "ALARM_ERROR_NONE";
        else if(a==1)
            debugText.text = "ALARM_ERROR_INVALID_PARAMETER";
        else if(a==2)
            debugText.text = "ALARM_ERROR_INVALID_DATE";
        else if(a==3)
            debugText.text = "ALARM_ERROR_CONNECTION_FAIL";
        else if(a==4)
            debugText.text = "ALARM_ERROR_PERMISSION_DENIED";
       

    }
    public void DBopen()
    {
        dbopen();
    }
    public void CreateTable()
    {   
        createtable();  
    }
    public void Insert(string type,string start_h,string start_m,
        string end_h,string end_m)
    {
        insert(new StringBuilder(type), new StringBuilder(start_h),
            new StringBuilder(start_m), new StringBuilder(end_h),
            new StringBuilder(end_m));
    }
    public void Select()
    {
        
        IntPtr ptr = tizensharedlibrary40();
        ActivityViewer.text = Marshal.PtrToStringAnsi(ptr);
    }

    public void DeleteFrom(String con)
    {
        deletefrom(new StringBuilder(con));
    }
}
