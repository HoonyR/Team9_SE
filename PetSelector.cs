using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;
public class PetSelector : MonoBehaviour {

    [DllImport("uft")]
    public static extern void dbopen();

    [DllImport("uft")]
    public static extern int exec_query(StringBuilder query);
    [DllImport("uft")]
    public static extern IntPtr exec_select(StringBuilder query);
    
    public GameObject tiger;
    public GameObject cat;
    public Text name_lb;
    public string pet_table_name;
    int index = 0;

    public Text DebugText;
	// Update is called once per frame
    void Start()
    {
        dbopen();
        StringBuilder query = new StringBuilder("CREATE TABLE IF NOT EXISTS " +
            pet_table_name +
            "(id int ,name varchar(15),level varchar(5),exp varchar(5),newexp varchar(5));");
        int ret = exec_query(query);

        query = new StringBuilder("SELECT name FROM " + pet_table_name + " WHERE id=0;");
        DebugText.text = query.ToString();
        IntPtr ptr = exec_select(query);
        string name= Marshal.PtrToStringAnsi(ptr);
        if (name != "")
            SceneManager.LoadScene("Scenes/MainScene");
    }
	void Update () {
	    
	}
    public void LeftArrow()
    {
        cat.SetActive(true);
        tiger.SetActive(false);
        name_lb.text = "CAT";
        index = 0;
        
    }
    public void RightArrow()
    {
        tiger.SetActive(true);
        cat.SetActive(false);
        name_lb.text = "TIGER";
        index = 1;
    }
    public void SelectPet()
    {
        if (index == 0)
        {
            string query = "INSERT INTO " + pet_table_name +
                    " VALUES(0,'CAT,','0,','0.0,','0.0');";
            exec_query(new StringBuilder(query));
            SceneManager.LoadScene("Scenes/MainScene");
        }
        else if (index == 1)
        {
            string query = "INSERT INTO " + pet_table_name +
                " VALUES(0,'TIGER,','0,','0.0,','0.0');";
            exec_query(new StringBuilder(query));
            SceneManager.LoadScene("Scenes/MainScene");
        }
    }
}
