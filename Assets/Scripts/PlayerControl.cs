using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private float _rotSpeed = 10;
    public float RotSpeed
    {
        get { return _rotSpeed; }
        set { _rotSpeed = value; }
    }

    // Use this for initialization
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        var targetPos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        targetPos.y = transform.position.y;
        var targetDir = Quaternion.LookRotation(-(targetPos - transform.position));


        if (Vector3.Dot(GameObject.FindGameObjectWithTag("MainCamera").transform.forward, transform.forward) < -0.9f)
        {
            _animator.SetFloat("HSpeed", -Input.GetAxis("Horizontal"));
            _animator.SetFloat("VSpeed", -Input.GetAxis("Vertical"));
        }
        else
        {
            _animator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
            _animator.SetFloat("VSpeed", Input.GetAxis("Vertical"));
            if (_animator.GetFloat("HSpeed") > 0.1f || _animator.GetFloat("VSpeed") > 0.1f || _animator.GetFloat("HSpeed") < -0.1f || _animator.GetFloat("VSpeed") < -0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetDir, _rotSpeed * Time.deltaTime);
            }
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetDir, _rotSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.parent.CompareTag("Tutorial1"))
        {
            return;
        }
        other.enabled = false;
        GameObject.FindGameObjectWithTag("UI").GetComponent<UIControl>().ShowInteraction(false);
        _animator.SetBool("Pray", true);
        other.transform.parent.FindChild("Orb").gameObject.GetComponent<OrbFloat>().Interact();
        Invoke("DisablePray", 0.1f);
        transform.Find("SpiralSymbolEffect").GetComponent<ParticleSystem>().Play();

    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("OrbInteractiveArea"))
        {
            return;
        }
        var orbControl = other.transform.parent.FindChild("Orb").gameObject.GetComponent <OrbFloat>();
		GameObject.FindGameObjectWithTag ("UI").GetComponent<UIControl> ().ShowInteraction (true);
        if (Input.GetButtonDown("Interact"))
        {
			other.enabled = false;
			GameObject.FindGameObjectWithTag("UI").GetComponent<UIControl>().ShowInteraction (false);
            _animator.SetBool("Pray", true);
            Invoke("DisablePray", 0.1f);
            orbControl.Interact();
            if (other.name.Equals("NoInteract"))
            {
                return;
            }
            GameObject.Find("Sounds").GetComponent<NarrativeControl>().InvokeDialogue();
            transform.Find("SpiralSymbolEffect").GetComponent<ParticleSystem>().Play();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("OrbInteractiveArea"))
        {
            GameObject.FindGameObjectWithTag("UI").GetComponent<UIControl>().ShowInteraction (false);
        }
    }



    private void DisablePray()
    {
        _animator.SetBool("Pray", false);
    }
}