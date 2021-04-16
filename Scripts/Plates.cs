using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Plates : MonoBehaviour {

	//Plate will spawn the ingredient on the plate once the ingredient touches the plate
	public Ingredient bacon;
	public Ingredient bun;
	public Ingredient cookedBurger;		//Burger needs to be cooked
	public Ingredient cheese;
	public Ingredient cookedChicken;	//Chicken needs to be cooked
	public Ingredient jalapeno;
	public Ingredient lettuce;
	public Ingredient pickle;
	public Ingredient tomato;

    public SteamVR_Behaviour_Pose rightHand;
    public SteamVR_Behaviour_Pose leftHand;
	public GameObject player;

	public GameManager gameManager;

	//Make a list of what is on the list
	//NOTE: IF LIST IS EMPTY AND INGREDIENT != BUN, THEN DO NOT ADD
	public List<Ingredient> recipe;

	//Position of where food goes on plate
	public float foodCoord = 0.008f;

	//Timer for submit
	private float submitTimer;
	private float submitTime = 5.0f;

	// Use this for initialization
	void Awake()
	{
		recipe = GetComponent<Recipe>().recipe;	
	}
	
	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		player = GameObject.Find("Player");
		rightHand = player.transform.Find("[CameraRig]").transform.Find("Controller (right)").GetComponent<SteamVR_Behaviour_Pose>();
		leftHand = player.transform.Find("[CameraRig]").transform.Find("Controller (left)").GetComponent<SteamVR_Behaviour_Pose>();
	}
	// Update is called once per frame
	void Update () {
		if (submitTimer >= submitTime)
		{
			gameManager.compareToRecipe(this.GetComponent<Plates>());
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
			//Instantiates the food onto the plate
			
			//Bacon
			if (other.gameObject.CompareTag("Bacon"))
			{
            updateHands(other);
            Destroy(other.gameObject);
			spawnIngredient(bacon);
			}
			
			//Bun
			if (other.gameObject.CompareTag("Bun"))
			{
            updateHands(other);
            Destroy(other.gameObject);
			spawnIngredient(bun);

			}
			
			//Cooked Burger
			if (other.gameObject.CompareTag("Burger"))
			{
            updateHands(other);
            Destroy(other.gameObject);
			spawnIngredient(cookedBurger);
			}

			
			//Cheese
			if (other.gameObject.CompareTag("Cheese"))
			{
            updateHands(other);
            Destroy(other.gameObject);
			spawnIngredient(cheese);
			}
			
			//Chicken
			if (other.gameObject.CompareTag("Chicken"))
			{
            updateHands(other);
            Destroy(other.gameObject);
			spawnIngredient(cookedChicken);
			}
			
			//Jalapeno
			if (other.gameObject.CompareTag("Jalapeno"))
			{
            updateHands(other);
            Destroy(other.gameObject);
			spawnIngredient(jalapeno);
			}
			
			//Lettuce
			if (other.gameObject.CompareTag("Lettuce"))
			{
            updateHands(other);
            Destroy(other.gameObject);
			spawnIngredient(lettuce);
			}
			
			//Pickles
			if (other.gameObject.CompareTag("Pickles"))
			{
            updateHands(other);
            Destroy(other.gameObject);
			spawnIngredient(pickle);
			}
			
			//Tomato
			if (other.gameObject.CompareTag("Tomato"))
			{
            updateHands(other);
            Destroy(other.gameObject);
			spawnIngredient(tomato);
			}
	}

	//If the plate is in the submit area
	void OnTriggerStay(Collider other)
	{
			//If the object is the submit area
			if (other.GetComponent<GameManager>())
			{
				submitTimer += Time.deltaTime;
			}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<GameManager>())
		{
			submitTimer = 0;
		}
	}


	void spawnIngredient(Ingredient x)
	{
        //Instatiates the desired food
        Ingredient ingredient = Instantiate(x,transform.position + new Vector3 (0f, foodCoord,0f),Quaternion.identity);
		ingredient.transform.parent = this.transform;
		//Adds the ingredient to the recipe list
		recipe.Insert(0,x);
		foodCoord += 0.01f;
	}

    //Method to delete the object from the list of their respective hands
    void updateHands (Collider other) {
        if (other.GetComponent<Interactable>().m_ActiveHand.GetComponent<SteamVR_Behaviour_Pose>().inputSource == rightHand.inputSource) {
            rightHand.GetComponent<HandInteraction>().OnTriggerExit(other);
        }
        else if (other.GetComponent<Interactable>().m_ActiveHand.GetComponent<SteamVR_Behaviour_Pose>().inputSource == leftHand.inputSource) {
            leftHand.GetComponent<HandInteraction>().OnTriggerExit(other);
        }
    }
}
