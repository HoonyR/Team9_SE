using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class ActivityModifier : MonoBehaviour {

  

    public Text DebugText;
    public Dropdown activities_dropdown;
    public Dropdown start_h_dropdown;
    public Dropdown start_m_dropdown;
    public Dropdown duration_h_dropdown;
    public Dropdown duration_m_dropdown;

    public Toggle[] week_toggle = new Toggle[7];
    public ActivityManager activity_manager;
    public DBManager dbmanager;
    ActivityList[] activity_list;
    public AlarmManager alarm_manager;
    
   
    
    public void Initialize(string init_setting)
    {
        if (init_setting == "")
            return;

        activities_dropdown.ClearOptions();
        //레코드를 , 문자로 분리 그리고 드롭다운 메뉴에 추가하기 위해서 
        //activitylist를 레코드수-1만큼 할당. (끝에 ,에 빈 ""가 들어가기 때문에)
        string[] records = init_setting.Split(dbmanager.record_separator);
         activity_list=new ActivityList[records.Length-1];

       
        //레코드는 /문자로 필드를 각각 분리 ,activitylist의 멤버로 할당.
        for (int i=0; i<activity_list.Length;i++)
        {
            activity_list[i] = new ActivityList();
            string[] fields = records[i].Split(dbmanager.field_separator);
            activity_list[i].id = fields[0];
            activity_list[i].activity_type = fields[1];
            activity_list[i].activity_weekday = fields[2];
            activity_list[i].activity_start_h = fields[3];
            activity_list[i].activity_start_m = fields[4];
            activity_list[i].activity_duration_h = fields[5];
            activity_list[i].activity_duration_m = fields[6];
        }
        
        //가장 위의 드롭다운 리스트
        List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
        for (int i = 0; i < activity_list.Length; i++)
        {
            
            list.Add(new Dropdown.OptionData(
                activity_list[i].id+" "+
                activity_manager.FindType(activity_list[i].activity_type) + 
                " " + activity_manager.FindWeekDay(activity_list[i].activity_weekday)+
                " Start:"+activity_list[i].activity_start_h+
                "-"+activity_list[i].activity_start_m+
                " Duration:"+activity_list[i].activity_duration_h +
                "-"+activity_list[i].activity_duration_m));
        }
        activities_dropdown.AddOptions(list);

        list.Clear();
        OnValueChanged(0);
        
    }

    //변경
    public void OnModify()
    {
        string id = activities_dropdown.captionText.text.Split(' ')[0];
        string week_day_value = "";
        for (int i = 0; i < week_toggle.Length; i++)
        {
            if (week_toggle[i].isOn)
            {
                week_day_value += ""+i + dbmanager.week_separator;
            }

        }
        string query = "UPDATE " + dbmanager.table_name + " SET " +
            "week='"+week_day_value+dbmanager.field_separator+"'"+
            ",start_h='" + start_h_dropdown.value + dbmanager.field_separator + "'" +
            ",start_m='" + start_m_dropdown.value + dbmanager.field_separator + "'" +
            ",duration_h='" + duration_h_dropdown.value + dbmanager.field_separator + "'" +
            ",duration_m='" + duration_m_dropdown.value + "'"+
            " where id="+id;
        dbmanager.UpdateQuery(query);
        alarm_manager.Renew();
        
    }
    public void OnValueChanged(int index)
    {
        
        string[] weekdays 
            = activity_list[index].activity_weekday.Split(dbmanager.week_separator) ;

        for (int i = 0; i < week_toggle.Length; i++)
            week_toggle[i].isOn = false;
        for (int i = 0; i < weekdays.Length - 1; i++)
        {
            switch (weekdays[i])
            {
                case "0":
                    week_toggle[0].isOn = true;
                    break;
                case "1":
                    week_toggle[1].isOn = true;
                    break;
                case "2":
                    week_toggle[2].isOn = true;
                    break;
                case "3":
                    week_toggle[3].isOn = true;
                    break;
                case "4":
                    week_toggle[4].isOn = true;
                    break;
                case "5":
                    week_toggle[5].isOn = true;
                    break;
                case "6":
                    week_toggle[6].isOn = true;
                    break;
            }
        }

        start_h_dropdown.value = Int32.Parse(activity_list[index].activity_start_h);
        start_m_dropdown.value = Int32.Parse(activity_list[index].activity_start_m);
        duration_h_dropdown.value = Int32.Parse(activity_list[index].activity_duration_h);
        duration_m_dropdown.value = Int32.Parse(activity_list[index].activity_duration_m);

    }
}
