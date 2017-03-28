using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI = UnityEngine.UI;

public class SubtitleDisplay : MonoBehaviour {

    private const float Audiorate = 44100f;

    private UI.Text _text;

    private bool _playing;

    private List<SubtitleKey> _subtitles;
    // Use this for initialization
    private void Start ()
    {
        _text = GetComponent<UI.Text>();
        _text.text = "";
        _playing = false;
    }
	
	// Update is called once per frame
    private void Update () {
		
	}

    public void PlaySubtitles(List<SubtitleKey> subtitles)
    {
        if (_playing)
        {
            StopCoroutine("Play");
        }
        _subtitles = subtitles;
        StartCoroutine("Play");
        _playing = true;
    }

    private IEnumerator Play()
    {
        for (var index = 0; index < _subtitles.Count; index++)
        {
            if (_subtitles[index].GetSubtitle().Contains("</off>"))
            {
                _text.text = "";
                break;
            }
            _text.text = _subtitles[index].GetSubtitle();
            while (index + 1 < _subtitles.Count && GameObject.Find("Sounds")
                       .GetComponent<NarrativeControl>()
                       .GetCurrentAudioTime() < _subtitles[index + 1].GetTimeStamp())
            {
                yield return new WaitForFixedUpdate();
            }
        }
        _playing = false;
    }
}
