using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour {

	public GameObject flip;				//gameobject to tell player to flip
    private GameObject flipping;
	public Material cookedBurger;		//Changes the color of the burger once its done
	private float timer;				//timer for the cooking time
	public float cookedTime = 10.0f;	//how long it takes (time = 10s)
	public bool cooked = false;			//Is the burger cooked yet?
	public Burger_Whole wholeBurg;		//Is the whole burger cooked yet?
	private bool displayFlip = false;	//Did the flip instantiate?

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(cooked && !wholeBurg.burgerDone && !displayFlip)
		{
			instantiateFlip();
		}
		
		if (timer >= cookedTime)
		{
			//Changes the color of the burger side
			this.GetComponent<Renderer>().material = cookedBurger;
			//Updates the burger so parent knows its cooked
			cooked = true;
		}

	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("FlatTop"))
		{
			timer += Time.deltaTime;
		}
	}

    void OnTriggerEnter(Collider other) {
        if (cooked) {
            if (other.gameObject.CompareTag("Hands")) {
                Destroy(flipping);
            }
        }
    }

	void instantiateFlip()
	{
		//Creates the Flip signal
		flipping = Instantiate (flip, new Vector3 (this.transform.position.x, this.transform.position.y + 0.1f, this.transform.position.z), Quaternion.identity);
        flipping.transform.parent = this.transform;
		
		//Says that it has flipped the burger
		displayFlip = true;
	}
}
