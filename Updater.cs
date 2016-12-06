using UnityEngine;
using System.Collections;

public class Updater : MonoBehaviour {

    public PetManager petmanager;
    public ShoppingManager shopmanager;
    void Start()
    {
        OnUpdate();
    }
    public void OnUpdate()
    {
        petmanager.UpdateState();
        shopmanager.UpdateAssets();
    }
}
