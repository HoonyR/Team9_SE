using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ActivityAdder : MonoBehaviour {

    public DBManager dbmanager;
    public AlarmManager alarm_manager;
    public Dropdown activity_type;
    public Dropdown activity_start_h;
    public Dropdown activity_start_m;
    public Dropdown activity_duration_h;
    public Dropdown activity_duration_m;
    public Toggle[] weekdays=new Toggle[7];
    public Text DebugText;
    public void OnOK()
    {
        string week_day_value = "";
        for (int i = 0; i < weekdays.Length; i++)
        {
            if (weekdays[i].isOn)
            {
                week_day_value += ""+i+dbmanager.week_separator;
            }

        }
        dbmanager.Insert(""+dbmanager.field_separator+activity_type.value+dbmanager.field_separator,
            week_day_value + dbmanager.field_separator,
            "" + activity_start_h.value + dbmanager.field_separator,
            "" + activity_start_m.value + dbmanager.field_separator,
            "" + activity_duration_h.value + dbmanager.field_separator,
            ""+ activity_duration_m.value);
        alarm_manager.Renew();
    }
}
