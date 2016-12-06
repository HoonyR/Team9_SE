using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AlarmManager : MonoBehaviour {

    [DllImport("alarmset")]
    public static extern int callalarm(int type, int[] weeks,int week_size, int start_h, int start_m, 
        int duration_h, int duration_m);
    [DllImport("alarmset")]
    public static extern void alarmallcancel();
    [DllImport("alarmset")]
    public static extern int search_device();
    public Text DebugText;
    public DBManager dbmanager;
    ActivityList[] activity_list;


    //알람을 새로 갱신하는 메소드
    public void Renew(){
        string result = dbmanager.Select("SELECT * FROM " + dbmanager.table_name);
        if (result == "")
            return;
        alarmallcancel();
        string[] records = result.Split(dbmanager.record_separator);
        activity_list = new ActivityList[records.Length - 1];

        //레코드는 /문자로 필드를 각각 분리 ,activitylist의 멤버로 할당.
        for (int i = 0; i < activity_list.Length; i++)
        {
            activity_list[i] = new ActivityList();
            string[] fields = records[i].Split(dbmanager.field_separator);
            activity_list[i].id = fields[0];
            activity_list[i].activity_type = fields[1];
            int activity_type = Int32.Parse(activity_list[i].activity_type);
            activity_list[i].activity_weekday = fields[2];
            activity_list[i].activity_start_h = fields[3];
            int activity_start_h = Int32.Parse(activity_list[i].activity_start_h);
            activity_list[i].activity_start_m = fields[4];
            int activity_start_m = Int32.Parse(activity_list[i].activity_start_m);
            activity_list[i].activity_duration_h = fields[5];
            int activity_duration_h = Int32.Parse(activity_list[i].activity_duration_h);
            activity_list[i].activity_duration_m = fields[6];
            int activity_duration_m = Int32.Parse(activity_list[i].activity_duration_m) ;
            string[] weeks_str =
                activity_list[i].activity_weekday.Split(dbmanager.week_separator);
            int[] week_days = new int[weeks_str.Length - 1];
            for (int j = 0; j < week_days.Length; j++)
                week_days[j] = Int32.Parse(weeks_str[j]);

            callalarm(activity_type, week_days, week_days.Length,
                activity_start_h, activity_start_m, activity_duration_h, activity_duration_m);

        }

    }

    public int SearchDevice()
    {
         return search_device();
    }


}
