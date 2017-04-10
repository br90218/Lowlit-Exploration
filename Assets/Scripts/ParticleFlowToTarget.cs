using UnityEngine;

public class ParticleFlowToTarget : MonoBehaviour {

	// Use this for initialization
    void awake()
    {

    }
	void Start () {
	    GetComponent<ParticleSystem>().Stop();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
	}
}
