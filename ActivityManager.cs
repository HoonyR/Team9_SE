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
    public GameObject ActivityDeleteWindow;
    public GameObject ActivityViewWindow;
    public DBManager dbmanager;   

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
        ActivityModifyWindow.SetActive(true);
    }
    public void ActivityModifyClose()
    {
        ActivityModifyWindow.SetActive(false);
    }
    public void ActivityDeleteOpen()
    {
        ActivityDeleteWindow.SetActive(true);
    }
    public void ActivityDeleteClose()
    {
        ActivityDeleteWindow.SetActive(false);
    }
    public void ActivityViewOpen()
    {
        ActivityViewWindow.SetActive(true);
        dbmanager.Select();
    }
    public void ActivityViewClose()
    {
        ActivityViewWindow.SetActive(false);
    }
}
