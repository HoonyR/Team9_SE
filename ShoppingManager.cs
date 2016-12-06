using UnityEngine;
using System.Collections;

public class ShoppingManager : MonoBehaviour {
    public GameObject BuyWindow;
    public GameObject AssetsWindow;
   
    public void BuyOpen()
    {
        BuyWindow.SetActive(true);
    }
    public void BuyClose()
    {
        BuyWindow.SetActive(false);
    }
    public void AssetsOpen()
    {
        AssetsWindow.SetActive(true);
    }
    public void AssetsClose()
    {
        AssetsWindow.SetActive(false);
    }
}
