using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bun : MonoBehaviour {

	// Use this for initialization
	public Material cookedMaterial;     //What color is the object gonna change to?
	public float cookedTime;            //How long does it take to cook?
	public GameObject done;             //Showing the object is done
	private float timer;
    private bool cooked = false;
    private GameObject checkMark;

    public ParticleSystem smoke;
    public GameObject topBun;
    public GameObject botBun;

    public AudioSource m_audioSource;

	// Use this for initialization
	void Start () {

    }

	// Update is called once per frame
	void Update () {

		//Timer to change material when object is done cooking
		if (timer >= cookedTime)
		{
			topBun.GetComponent<Renderer>().material = cookedMaterial;
            botBun.GetComponent<Renderer>().material = cookedMaterial;
			this.transform.tag = "Bun";

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
