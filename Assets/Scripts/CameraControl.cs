using UnityEngine;

public class CameraControl : MonoBehaviour
{

    private Transform _target;
    [SerializeField] private float _distance = 10;
    [SerializeField] private float _xSpeed = 250;
    [SerializeField] private float _ySpeed = 120;
    [SerializeField] private float _yMinLimit = -20;
    [SerializeField] private float _yMaxLimit = 80;

    private float _x = 0f;
    private float _y = 0f;

    private Vector3 _rootPosition;
    public float Distance
    {
        get { return _distance; }
        set { _distance = value; }
    }

    public float XSpeed
    {
        get { return _xSpeed; }
        set { _xSpeed = value; }
    }

    public float YSpeed
    {
        get { return _ySpeed; }
        set { _ySpeed = value; }
    }

    public float YMinLimit
    {
        get { return _yMinLimit; }
        set { _yMinLimit = value; }
    }

    public float YMaxLimit
    {
        get { return _yMaxLimit; }
        set { _yMaxLimit = value; }
    }


    // Use this for initialization
    private void Start ()
	{
	    _target = GameObject.FindGameObjectWithTag("CameraTarget").transform;
	    _rootPosition = transform.position;

	    _x = transform.eulerAngles.y;
	    _y = transform.eulerAngles.x;
	}

    public void LateUpdate () {
	    if (!_target)
        { 
	        return;
	    }
        _x += Input.GetAxis("RightStickHorizontal") * _xSpeed * 0.02f;
        _y += Input.GetAxis("RightStickVertical") * _ySpeed * 0.02f;

        _y = ClampAngle(_y, _yMinLimit, _yMaxLimit);

        var rotation = Quaternion.Euler(_y, _x, 0);

        transform.rotation = rotation;
        transform.position = rotation * new Vector3(0f, 0f, -_distance) + _target.position;
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
        {
            angle += 360f;
        }
        if (angle > 360f)
        {
            angle -= 360f;
        }
        return Mathf.Clamp(angle, min, max);
    }

    public void ReturnCameraRootPosition()
    {
        transform.position = _rootPosition;
    }
}
