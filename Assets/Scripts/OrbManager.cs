using UnityEngine;
using UnityEngine.UI;

public class OrbManager : MonoBehaviour {

    [SerializeField] private Transform[] _orbLocations;
    [SerializeField] private GameObject _orb;
    [SerializeField] private GameObject _firstTutorialOrb;
    [SerializeField] private GameObject _secondTutorialOrb;

    private int _index;

    public Transform[] OrbLocations
    {
        get { return _orbLocations; }
        set { _orbLocations = value; }
    }

    public GameObject Orb
    {
        get { return _orb; }
        set { _orb = value; }
    }

    public GameObject FirstTutorialOrb
    {
        get { return _firstTutorialOrb; }
        set { _firstTutorialOrb = value; }
    }

    public GameObject SecondTutorialOrb
    {
        get { return _secondTutorialOrb; }
        set { _secondTutorialOrb = value; }
    }


    // Use this for initialization
    private void Start ()
    {
        _index = 0;
        Instantiate(_firstTutorialOrb, _orbLocations[0].position, _orbLocations[0].rotation);
        GameObject.Find("Orb Count").GetComponent<Text>().text = "Orbs Found: " + _index + "0/20";
    }
	
	// Update is called once per frame
    private void Update () {
		
	}

    public void InstantiateNextOrb()
    {
        if (_index + 1 >= _orbLocations.Length)
        {
            Debug.Log("Reached end of locations, returning");
        }
        else
        {
            Instantiate(_index + 1 == 1 ? _secondTutorialOrb : _orb, _orbLocations[++_index].position,
                Quaternion.identity);
        }

        GameObject.Find("Orb Count").GetComponent<Text>().text = "Orbs Found: " + _index + "/20";
    }

    
}
