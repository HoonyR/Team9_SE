using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {
    public GameObject PetSubMenu;
    public GameObject ActivitySubMenu;
    public GameObject ShoppingSubMenu;
    private GameObject CurrentSelected;
    // Use this for initialization
    void Start () {
        CurrentSelected = null;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPetMenuTouched()
    {
        ActivateSubmenu(PetSubMenu);
    }
    public void OnActivityMenuTouched()
    {
        ActivateSubmenu(ActivitySubMenu);
    }
    public void OnShoppingMenuTouched()
    {
        ActivateSubmenu(ShoppingSubMenu);
    }

    //메뉴가 눌렸을때 하위 메뉴를 보여준다. 
    //하위메뉴가 이미 active한 상태에서 한번더 메뉴 버튼을 누르면
    //하위메뉴는 inactive한 상태가 된다.
    void ActivateSubmenu(GameObject SelectedMenu)
    {
        PetSubMenu.SetActive(false);
        ActivitySubMenu.SetActive(false);
        ShoppingSubMenu.SetActive(false);
        if (CurrentSelected != null && CurrentSelected == SelectedMenu)
        {
            SelectedMenu.SetActive(false);
            CurrentSelected = null;
        }
        else
        {
            SelectedMenu.SetActive(true);
            CurrentSelected = SelectedMenu;
        }
    }
}
