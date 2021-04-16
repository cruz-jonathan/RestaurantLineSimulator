using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour {


	public Recipe recipe1;
	public Recipe recipe2;
	public Recipe recipe3;
	public Recipe recipe4;
	public Recipe recipe5;
	public Recipe recipe6;
	public Recipe recipe7;
	public Recipe recipe8;
	public Recipe recipe9;
	public Recipe recipe10;

	public List<Recipe> recipes = new List<Recipe>();

	// Use this for initialization
	void Awake () {
		initializeRecipeList();
	}
	
	void initializeRecipeList()
	{
		recipes.Add(recipe1);
		recipes.Add(recipe2);
		recipes.Add(recipe3);
		recipes.Add(recipe4);
		recipes.Add(recipe5);
		recipes.Add(recipe6);
		recipes.Add(recipe7);
		recipes.Add(recipe8);
		recipes.Add(recipe9);
		recipes.Add(recipe10);
	}
}
