using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    private Text _loadText;
	// Use this for initialization
    private void Start ()
    {
        _loadText = GameObject.Find("Text").GetComponent<Text>();
    }

	// Update is called once per frame
    private void Update ()
    {
//        StartCoroutine("LoadScene");
        _loadText.color = Color.Lerp(Color.black, Color.white, Mathf.PingPong(Time.time, 1f));
        Invoke("Load", 5f);
    }

    private void Load()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("prevScene") == 0 ? 1 : 0);
    }

//    private IEnumerator LoadScene()
//    {
//        var prevScene = PlayerPrefs.GetInt("prevScene");
//        print(prevScene);
//        yield return new WaitForSeconds(3);
//        print("Loading scene now! BOOM");
//        var async = SceneManager.LoadSceneAsync(prevScene == 0 ? 1 : 0);
//        while (!async.isDone)
//        {
//            var lerp = Mathf.PingPong(Time.time, 1f);
//            _loadText.color = Color.Lerp(Color.black, Color.white, lerp);
//        }
//        yield return async;
//    }

}
