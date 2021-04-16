using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject checkmark;
	public GameObject incorrect;
	public TextMesh scoreText;
	public TextMesh timeText;
	
	//Sets the spawn points and the recipes
    public List<GameObject> spawn;
	public List<Recipe> recipes; 

	//Spawn Stuff
	private int spawnCounter = 0;	//Where can i place a chit?

	//Chit Timers
	private float chitTimer;
	public float chitTime;	//How often a chit is punched in

	
	//Submit an order
	public List<Recipe> currentRecipes = new List<Recipe>();
	private int countRecipe = 0;
	private bool tookDish = false;
	
	//Score
	private int playerScore = 0;
	private int pointsPerDish = 500;
	private int firstDishMultiplier = 1;

	//Game
	private float timeLeft = 300.0f;
	public bool gameState = true;

	// Use this for initialization
	void Awake () {
        spawn = GetComponent<SpawnManager>().spawn;
		recipes = GetComponent<RecipeManager>().recipes;
		timeText.text = ""+ timeLeft;
	}

	void Start()
	{
		StartCoroutine("LoseTime");
	}
	
	// Update is called once per frame
	void Update () {
		chitTimer += Time.deltaTime;
		timeText.text = "" + timeLeft;
		scoreText.text = ""+ playerScore;	//Display Score
		
		if (gameState)
		{
			if (chitTimer >= chitTime && spawnCounter != spawn.Count && placeChit())
			{
			Recipe recipe = callRandomChit();
			Recipe spawnedRecipe = Instantiate(recipe,spawn[spawnCounter].transform.position,Quaternion.identity );	//Instantiates a chit at the spawnlocation
			currentRecipes.Add(spawnedRecipe);		//Adds the recipe to the current list of recipes on board
			spawnedRecipe.transform.parent = this.transform;
			spawnCounter ++;
			}
		}

		if (timeLeft <= 0)
		{
			gameState = false;
		}


	}
	
	//Gets a random recipe within the given list
	private Recipe callRandomChit()
	{
		int randomRecipe = Random.Range(0,9);
		return recipes[randomRecipe];

	}

	private bool placeChit()
	{
		int randomChance = Random.Range(0,100);
		chitTimer = 0.0f;
		if (randomChance <= 60 )
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void compareToRecipe(Plates plate)
	{
        if (gameState)
		{
			countRecipe = 0;
			tookDish = false;
			//Compare each recipe
			foreach (Recipe recipe in currentRecipes)
			{	
			if (recipe)
				{//Check if they have the same # of ingredients
				if (plate.recipe.Count==recipe.recipe.Count)
					{
					//Compare each ingredient
					for (int a = 0; a < plate.recipe.Count;a++)
						{
						if (!(plate.recipe[a] == recipe.recipe[a]))
							{
							break;
							}
						else if ((a+1)==plate.recipe.Count && !tookDish)
							{	
							GameObject correct = Instantiate(checkmark,new Vector3 (plate.transform.position.x, plate.transform.position.y + 0.1f, plate.transform.position.z),Quaternion.identity);
							Destroy(correct,3.0f);
							Destroy(currentRecipes[countRecipe].gameObject);
                                currentRecipes.Remove(recipe);
							tookDish=true;	//Stops duplicate chits from being handed in at the same time
							spawnCounter--; //Moves the chit line down by 1
                            //Updates Score
                            if (countRecipe == 0) {
                                    if (firstDishMultiplier != 5)
                                        firstDishMultiplier += 1;
                                }
                            else {
                                    firstDishMultiplier = 1;
                                }
                            playerScore += pointsPerDish * firstDishMultiplier;
                            moveRecipesDown();  //Moves recipes down
                            return;
                            }
						}
					}
				}
				countRecipe++;
			}
			//Subtract Points if the player submits the wrong order
			if (playerScore != 0)
			{
			playerScore -= pointsPerDish;
			}
			GameObject wrongOrder = Instantiate(incorrect, new Vector3 (plate.transform.position.x, plate.transform.position.y + 0.1f, plate.transform.position.z),Quaternion.identity);
			Destroy(wrongOrder,3.0f);
		}
	}

	//After submitting a dish, move the chits down
	public void moveRecipesDown()
	{
   
		for (int a = 0;a < currentRecipes.Count;a++)
		{
		    currentRecipes[a].transform.position = spawn[a].transform.position;
		}
        
	}

	IEnumerator LoseTime()
	{
		while(timeLeft > 0)
		{
		yield return new WaitForSeconds(1);
		timeLeft--;
		}
	}
}
