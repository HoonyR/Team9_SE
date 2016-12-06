using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ActivityManager : MonoBehaviour {
    public GameObject ActivityAddWindow;
    public GameObject ActivityModifyWindow;
    public ActivityModifier activity_modifier;
    public GameObject ActivityDeleteWindow;
    public ActivityDeleter activity_deleter;
    public GameObject ActivityViewWindow;
    public ActivityViewer activity_viewer;
    public DBManager dbmanager;
    
    public string FindType(string activity_type)
    {
        if (activity_type == "0")
            return "Execercise";
        else if (activity_type == "1")
            return "Eat";
        else if (activity_type == "2")
            return "Sleep";
        return "Error";
    }

    public string FindWeekDay(string activity_week)
    {
        if (activity_week == "")
            return "";
        string[] weekdays = activity_week.Split(dbmanager.week_separator);
        string weekday = "";
        for (int i = 0; i < weekdays.Length - 1; i++)
        {
            switch (weekdays[i])
            {
                case "0":
                    weekday += "Sun/";
                    break;
                case "1":
                    weekday += "Mon/";
                    break;
                case "2":
                    weekday += "Tue/";
                    break;
                case "3":
                    weekday += "Wed/";
                    break;
                case "4":
                    weekday += "Thu/";
                    break;
                case "5":
                    weekday += "Fri/";
                    break;
                case "6":
                    weekday += "Sat/";
                    break;

            }
        }
        return weekday.Substring(0, weekday.Length - 1);
    }


    public void ActivityAddOpen()
    {
        ActivityAddWindow.SetActive(true);
   
    }
    public void ActivityAddClose()
    {
        ActivityAddWindow.SetActive(false);
    }
    public void ActivityModifyOpen()
    {
        string init_setting=dbmanager.Select("SELECT * FROM " + dbmanager.table_name);
        ActivityModifyWindow.SetActive(true);
        activity_modifier.Initialize(init_setting);
    }
    public void ActivityModifyClose()
    {
        ActivityModifyWindow.SetActive(false);
    }
    public void ActivityDeleteOpen()
    {
        ActivityDeleteWindow.SetActive(true);
        string init_setting = dbmanager.Select("SELECT * FROM " + dbmanager.table_name);
        activity_deleter.initialize(init_setting);
    }
    public void ActivityDeleteClose()
    {
        ActivityDeleteWindow.SetActive(false);
    }
    public void ActivityViewOpen()
    {
        ActivityViewWindow.SetActive(true);
        string init_setting=dbmanager.Select("SELECT * FROM "+dbmanager.table_name);
        activity_viewer.Initialize(init_setting);
    }
    public void ActivityViewClose()
    {
        ActivityViewWindow.SetActive(false);
    }
}

//내부 클래스
public class ActivityList
{
    public string id;
    public string activity_type;
    public string activity_weekday;
    public string activity_start_h;
    public string activity_start_m;
    public string activity_duration_h;
    public string activity_duration_m;
}