using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class ActivityDeleter : MonoBehaviour {

    public DBManager dbmanager;
    public Text DebugText;
    public Dropdown activities_dropdown;
    public ActivityList[] activity_list;
    public ActivityManager activity_manager;
    public AlarmManager alarm_manager;

    public void initialize(string init_setting)
    {
        if (init_setting == "")
            return;

        activities_dropdown.ClearOptions();
        //레코드를 , 문자로 분리 그리고 드롭다운 메뉴에 추가하기 위해서 
        //activitylist를 레코드수-1만큼 할당. (끝에 ,에 빈 ""가 들어가기 때문에)
        string[] records = init_setting.Split(dbmanager.record_separator);
        activity_list = new ActivityList[records.Length - 1];
        DebugText.text = "length:" + activity_list.Length;

        //레코드는 /문자로 필드를 각각 분리 ,activitylist의 멤버로 할당.
        for (int i = 0; i < activity_list.Length; i++)
        {
            activity_list[i] = new ActivityList();
            //DebugText.text = "records:" + records[i]+'\n';
            string[] fields = records[i].Split(dbmanager.field_separator);
            activity_list[i].id = fields[0];            
            activity_list[i].activity_type = fields[1];            
            activity_list[i].activity_weekday = fields[2];            
            activity_list[i].activity_start_h = fields[3];            
            activity_list[i].activity_start_m = fields[4];            
            activity_list[i].activity_duration_h = fields[5];            
            activity_list[i].activity_duration_m = fields[6];            
        }
        DebugText.text += " " + activity_list.Length;
        //가장 위의 드롭다운 리스트
        List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
        for (int i = 0; i < activity_list.Length; i++)
        {

            list.Add(new Dropdown.OptionData(
                activity_list[i].id + " " +
                activity_manager.FindType(activity_list[i].activity_type) +
                " " + activity_manager.FindWeekDay(activity_list[i].activity_weekday) +
                " Start:" + activity_list[i].activity_start_h +
                "-" + activity_list[i].activity_start_m +
                " Duration:" + activity_list[i].activity_duration_h +
                "-" + activity_list[i].activity_duration_m));
        }
        activities_dropdown.AddOptions(list);
        
        list.Clear();
    }
    public void OnDelete()
    {
        string id = activities_dropdown.captionText.text.Split(' ')[0];
        string query = "DELETE FROM " + dbmanager.table_name + " where id=" + id;
        dbmanager.DeleteFrom(query);
        alarm_manager.Renew();
    }
}
