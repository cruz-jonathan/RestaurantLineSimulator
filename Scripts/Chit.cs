using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chit : MonoBehaviour {

	//Puts it on text
	public TextMesh ingredient1 = null;
	public TextMesh ingredient2 = null;
	public TextMesh ingredient3 = null;
	public TextMesh ingredient4 = null;
	public TextMesh ingredient5 = null;
	public TextMesh ingredient6 = null;
	public TextMesh ingredient7 = null;
	private List<TextMesh> written = new List<TextMesh>();

	//List of the ingredients in the recipe
	public Ingredient in_1;
	public Ingredient in_2;
	public Ingredient in_3;
	public Ingredient in_4;
	public Ingredient in_5;
	public Ingredient in_6;
	public Ingredient in_7;
	private List<Ingredient> ingredients = new List<Ingredient>();

	public List<GameObject> spawns;
	
	//Used for comparison purposes
	public List<Ingredient> recipe;	//Contains gameObjects
	
	// Use this for initialization
	void Start () {
		recipe = GetComponent<Recipe>().recipe;
		spawns = GetComponent<SpawnManager>().spawn;
		
		organizeText();

		organizeList();

		organizeChit();

	}

	//Allows to easily create prefabs and create a recipe list to allow easier cmoparison
	void organizeList()
	{
		ingredients.Add(in_1);
		ingredients.Add(in_2);
		ingredients.Add(in_3);
		ingredients.Add(in_4);
		ingredients.Add(in_5);
		ingredients.Add(in_6);
		ingredients.Add(in_7);

		foreach(Ingredient ingredient in ingredients)
		{
			if (ingredient)
			{
				recipe.Add(ingredient);
			}
		}
	}

	void organizeText()
	{
		//Initializes to blank
		ingredient1.text = "";
		ingredient2.text = "";
		ingredient3.text = "";
		ingredient4.text = "";
		ingredient5.text = "";
		ingredient6.text = "";
		ingredient7.text = "";

		//Add to text list for easier access
		written.Add(ingredient1);
		written.Add(ingredient2);
		written.Add(ingredient3);
		written.Add(ingredient4);
		written.Add(ingredient5);
		written.Add(ingredient6);
		written.Add(ingredient7);
	}

	void organizeChit()
	{
		for (int a = 0; a < recipe.Count; a ++)
		{
			written[a].text = recipe[a].GetComponent<Ingredient>().toString();
			Ingredient ing = Instantiate(recipe[a], spawns[a].transform.position, Quaternion.identity);
			ing.transform.parent = this.transform;
		}
	}
}
