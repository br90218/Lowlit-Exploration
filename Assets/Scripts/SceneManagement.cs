using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

    public void LoadScene()
    {
        PlayerPrefs.SetInt("prevScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("LoadSplash");
    }
}
