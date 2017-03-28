using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private float _distance = 10f;
    [SerializeField] private float _height = 5f;
    [SerializeField] private float _heightDamping = 2f;
    [SerializeField] private float _rotationDamping = 3f;
    private Transform _target;

    public float Distance
    {
        get { return _distance; }
        set { _distance = value; }
    }

    public float RotationDamping
    {
        get { return _rotationDamping; }
        set { _rotationDamping = value; }
    }

    public float HeightDamping
    {
        get { return _heightDamping; }
        set { _heightDamping = value; }
    }

    public float Height
    {
        get { return _height; }
        set { _height = value; }
    }


    // Use this for initialization
    private void Start ()
    {
        _target = GameObject.FindGameObjectWithTag("CameraTarget").transform;
    }
	
	// Update is called once per frame
    private void Update () {
        if (!_target)
        {
            return;
        }
        var wantedRotationAngle = _target.eulerAngles.y;
        var wantedHeight = _target.position.y + _height;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle,
            _rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position = _target.position;
        transform.position -= currentRotation * Vector3.forward * _distance;

        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        transform.LookAt(_target);


    }
}
