using UnityEngine;
using System.Collections;

public class ActivityDeleter : MonoBehaviour {

    public DBManager dbmanager;
    public void OnDelete()
    {
        dbmanager.DeleteFrom("A");
    }
}
