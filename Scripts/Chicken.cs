using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour {

    public Material cookedMaterial1;
    public Material cookedMaterial2;
    public float cookedTime;
    public GameObject done;
    private float timer;
    private bool cooked = false;
    public ParticleSystem smoke;
    private GameObject checkMark;
    public GameObject chicken1;
    public GameObject chicken2;
    public GameObject chicken3;
    public GameObject chicken4;
    public GameObject chicken5;
    public GameObject chicken6;

    public AudioSource m_audioSource;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (timer >= cookedTime)
        {
            this.transform.tag = "Chicken";

            chicken1.GetComponent<Renderer>().material = cookedMaterial1;
            chicken2.GetComponent<Renderer>().material = cookedMaterial1;
            chicken3.GetComponent<Renderer>().material = cookedMaterial1;
            chicken4.GetComponent<Renderer>().material = cookedMaterial2;
            chicken5.GetComponent<Renderer>().material = cookedMaterial2;
            chicken6.GetComponent<Renderer>().material = cookedMaterial2;
            if (!cooked)
            {
                checkMark = Instantiate(done, new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, this.transform.position.z), Quaternion.identity);
                checkMark.transform.parent = this.transform;
            }
            cooked = true;
        }
	}

    void OnTriggerStay(Collider other)
    {
        //If Object is touching the flattop, start cooking it
        if (other.gameObject.CompareTag("FlatTop"))
        {
            timer += Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FlatTop"))
        {
            smoke.Play();
            m_audioSource.Play();
        }

        if (cooked) {
            if (other.gameObject.CompareTag("Hands")) {
                Destroy(checkMark);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FlatTop"))
        {
            smoke.Stop();
            m_audioSource.Stop();
        }
    }
}
