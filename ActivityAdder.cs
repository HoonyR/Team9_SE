using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ActivityAdder : MonoBehaviour {

    public DBManager dbmanager;
    public Dropdown activity_type;
    public Dropdown activity_start_h;
    public Dropdown activity_start_m;
    public Dropdown activity_end_h;
    public Dropdown activity_end_m;
    public void OnOK()
    {
        dbmanager.Insert("" + activity_type.value,
            "" + activity_start_h.value,
            "" + activity_start_m.value,
            "" + activity_end_h.value,
            ""+activity_end_m.value);
    }
}
