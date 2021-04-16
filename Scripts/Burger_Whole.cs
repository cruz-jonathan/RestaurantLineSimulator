using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger_Whole : MonoBehaviour {

	public ParticleSystem smoke;
	public GameObject checkMark;
    private GameObject done;
	public GameObject topBurg;
	public GameObject botBurg;
	private Burger topBurger;
	private Burger botBurger;
	public bool burgerDone = false;

	//Sizzling for the burger
	public AudioSource m_audioSource;
	public bool playSource = false;


	// Use this for initialization
	void Start () {
		topBurger = topBurg.GetComponentInChildren<Burger>();
		botBurger = botBurg.GetComponentInChildren<Burger>();
	}
	
	// Update is called once per frame
	void Update () {
		if (topBurger.cooked && botBurger.cooked)
		{
			transform.gameObject.tag = "Burger";
			
			if (!burgerDone)
			{
			done = Instantiate(checkMark, new Vector3 (this.transform.position.x, this.transform.position.y + 0.1f, this.transform.position.z), Quaternion.identity);
            done.transform.parent = this.transform;	
			}
			burgerDone = true;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("FlatTop"))
		{
			smoke.Play();
			m_audioSource.Play();
		}
        
        if (burgerDone) {
            if (other.gameObject.CompareTag("Hands")) {
                Destroy(done);
            }
        }
	}

    void OnTriggerExit(Collider other) {
      if (other.gameObject.CompareTag("FlatTop")) {
            smoke.Stop();
			m_audioSource.Stop();
        }  
    }
}
