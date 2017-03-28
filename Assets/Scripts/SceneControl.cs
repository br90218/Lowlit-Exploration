using UnityEngine;

public class SceneControl : MonoBehaviour {

	// Update is called once per frame
    private void Update () {
        if (Input.GetButtonDown("Interact"))
        {
            GameObject.FindGameObjectWithTag("SceneManagement").GetComponent<SceneManagement>().LoadScene();
        }
	}
}
