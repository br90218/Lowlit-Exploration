using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeControl : MonoBehaviour {

    //Singleton Property
    public static NarrativeControl Instance { get; private set; }

    private const float Audiorate = 44100f;

    [SerializeField] private AudioClip _bgmClipFirst;
    [SerializeField] private AudioClip _bgmClipSecond;
    [SerializeField] private AudioClip[] _narrativeClips;
    [SerializeField] private GameObject[] _envSource;

    public AudioClip[] NarrativeClips
    {
        get { return _narrativeClips; }
        set { _narrativeClips = value; }
    }

    public GameObject[] EnvSource
    {
        get { return _envSource; }
        set { _envSource = value; }
    }

    public AudioClip BgmClipFirst
    {
        get { return _bgmClipFirst; }
        set { _bgmClipFirst = value; }
    }

    public AudioClip BgmClipSecond
    {
        get { return _bgmClipSecond; }
        set { _bgmClipSecond = value; }
    }

    private int _index;
    private AudioSource _narrativeSource;
    private AudioSource _bgmSource;
    private AudioSource _bgmSecond;
    private List<List<SubtitleKey>> _subtitleCollection;
    private void Awake()
    {
        if (Instance != null & Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

	// Use this for initialization
    private void Start ()
    {
        //initialize audio index variable
        _index = 0;

        // Setting up audio source
        _narrativeSource = GameObject.Find("Narrative").GetComponent<AudioSource>();
        _bgmSource = GameObject.Find("BGM").GetComponent<AudioSource>();
        _bgmSecond = GameObject.Find("BGM2").GetComponent<AudioSource>();
        _bgmSource.clip = _bgmClipFirst;
        _bgmSecond.clip = _bgmClipSecond;

        // Parsing subtitle files
        var file = Resources.Load("Subtitles/subs_test") as TextAsset;
        var fileLines = file.text.Split('\n');
        _subtitleCollection = new List<List<SubtitleKey>>();
        var subtitles = new List<SubtitleKey>();
        foreach (var line in fileLines)
        {
            if (line.Contains("<p>"))
            {
                subtitles = new List<SubtitleKey>();
            }
            else if (line.Contains("</p>"))
            {
                _subtitleCollection.Add(subtitles);
            }
            else if (line.Contains("<time/>"))
            {

                var timeStamp = float.Parse(line.Substring(7, 7));
                var subtitle = line.Substring(15);
                subtitle = subtitle.Replace("<br>", "\n");
                subtitles.Add(new SubtitleKey(timeStamp, subtitle));
            }
            else
            {
                Debug.LogWarning("Invalid tag detected in subtitle file");
            }
        }

        if (_narrativeClips.Length != _subtitleCollection.Count)
        {
            if (_narrativeClips.Length > _subtitleCollection.Count)
            {
                Debug.LogWarning("There are more audio clips than subtitles. May cause error.");
            }
            else
            {
                Debug.LogWarning("There are more subtitles than audio clips. May cause error.");
            }
        }
        else if (_narrativeClips.Length == _subtitleCollection.Count)
        {
            Debug.Log("Everything looks good!");
        }

    }
	
	// Update is called once per frame
    private void Update () {
		
	}


    public void InvokeDialogue()
    {
        //tells the text component to display subtitles

        GameObject.Find("Subtitles").GetComponent<SubtitleDisplay>().PlaySubtitles(_subtitleCollection[_index]);

        //plays the audio clip
        //checks if special events must occur at the same time

        //DON'T ENABLE THIS -- DEBUG USE ONLY
//        if (_index == 0)
//        {
//            GameObject.Find("Cutscene Manager").GetComponent<Animator>().SetTrigger("EnterEnding");
//            _bgmSource.clip = _bgmClips[1];
//            _bgmSource.Play();
//            foreach (var envSource in _envSource)
//            {
//
//                envSource.GetComponent<AudioSource>().volume = 0f;
//            }
//        }

        if (_index == 15)
        {
            _bgmSource.Play();
            foreach (var envSource in _envSource)
            {
                StartCoroutine(AdjustVolume(envSource.GetComponent<AudioSource>(), 0.1f));
            }
        }
        else if (_index == 17)
        {
            GameObject.Find("Cutscene Manager").GetComponent<Animator>().SetTrigger("EnterEnding");
            StartCoroutine(AdjustVolume(_bgmSource, 0.5f));
            _bgmSecond.Play();
        }
        _narrativeSource.clip = _narrativeClips[_index];
        _narrativeSource.Play();
        _index++;
    }

    public float GetCurrentAudioTime()
    {
        if (!_narrativeSource.isPlaying) return 0f;
        else return _narrativeSource.timeSamples / Audiorate;
    }

    private IEnumerator AdjustVolume(AudioSource source, float speed)
    {
        while (source.volume > 0.02f)
        {
            source.volume = Mathf.Lerp(source.volume, 0f, speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        source.Stop();
    }
}
