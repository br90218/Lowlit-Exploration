using UnityEngine;

public class ParticleAura : MonoBehaviour
{

    [SerializeField] private float _rotationSpeed;

    public float RotationSpeed
    {
        get { return _rotationSpeed;}
        set { _rotationSpeed = value; }
    }

    private float _y;

	// Use this for initialization
	void Start ()
	{
	    _y = 0f;
	    GetComponent<ParticleSystem>().Stop();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _y += _rotationSpeed * Time.deltaTime;
	    if (_y > 360) _y -= 360;

	    var rotation = transform.rotation.eulerAngles;
	    rotation.y = _y;

	    transform.rotation = Quaternion.Euler(rotation);
	}
}
