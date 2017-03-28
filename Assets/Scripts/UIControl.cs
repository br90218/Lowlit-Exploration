using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{


    private bool _flashTrigger;

	// Use this for initialization
    private void Start ()
    {
        var reset = Color.white;
        reset.a = 0f;
        GameObject.Find("Panel").GetComponent<UnityEngine.UI.Image>().color = reset;
	    _flashTrigger = false;

	}
	
	// Update is called once per frame
    private void Update () {
	    if (_flashTrigger)
	    {
	        var screen = GameObject.Find("Panel").GetComponent<UnityEngine.UI.Image>().color;
	        screen.a = Mathf.Lerp(screen.a, 0f, 0.4f * Time.deltaTime);
	        if (screen.a < 0.0001f)
	        {
	            _flashTrigger = false;
	        }
	        GameObject.Find("Panel").GetComponent<UnityEngine.UI.Image>().color = screen;
	    }


	}

    public void Flash()
    {
        _flashTrigger = true;
        var flash = GameObject.Find("Panel").GetComponent<UnityEngine.UI.Image>().color;
        flash.a = 1.3f;
        GameObject.Find("Panel").GetComponent<UnityEngine.UI.Image>().color = flash;
    }
}
