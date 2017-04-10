using System.Collections;
using UnityEngine;

public class OrbFloat : MonoBehaviour
{
    private Vector3 _rootPosition;
    private bool _touched;

    [SerializeField] private AnimationCurve _xCurve;
    [SerializeField] private AnimationCurve _yCurve;
    [SerializeField] private AnimationCurve _zCurve;


    public AnimationCurve YCurve
    {
        get { return _yCurve; }
        set { _yCurve = value; }
    }

    public AnimationCurve XCurve
    {
        get { return _xCurve; }
        set { _xCurve = value; }
    }

    public AnimationCurve ZCurve
    {
        get { return _zCurve; }
        set { _zCurve = value; }
    }

    // Use this for initialization
    private void Start()
    {
        _rootPosition = transform.position;
        _touched = false;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(_rootPosition.x + _xCurve.Evaluate(Time.time),
            _rootPosition.y + _yCurve.Evaluate((Time.time)), _rootPosition.z + _zCurve.Evaluate(Time.time));

        if (_touched)
        {
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, 0f, 0.5f * Time.deltaTime);

        }
    }

    public void Interact()
    {
        transform.Find("Orb_Float").gameObject.GetComponent<ParticleSystem>().Play();
        _touched = true;
        StartCoroutine("Destroy");
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(7f);
        GameObject.Find("Cutscene Manager").GetComponent<Animator>().SetBool("Flash", true);
        Invoke("TurnFlashTriggerOff", 0.1f);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<ParticleSystem>().Stop();
        while (GetComponent<ParticleSystem>().IsAlive())
        {
            yield return new WaitForFixedUpdate();
        }
        GameObject.Destroy(transform.parent.gameObject);
        GameObject.Find("Orb Locations").GetComponent<OrbManager>().InstantiateNextOrb();
    }

    private void TurnFlashTriggerOff()
    {
        GameObject.Find("Cutscene Manager").GetComponent<Animator>().SetBool("Flash", false);
    }

}