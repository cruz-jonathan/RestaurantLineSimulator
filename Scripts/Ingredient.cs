using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {
	// Use this for initialization
	public string toString()
	{
		string name = this.name;
		if (!this.CompareTag("Ingredient"))
		{
			return "";
		}

		return name.Remove(0,6);
	}
}
