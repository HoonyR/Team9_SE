using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour {

	public void OnStartClick()
    {
        SceneManager.LoadScene("Scenes/SelectModel");
    }
}
