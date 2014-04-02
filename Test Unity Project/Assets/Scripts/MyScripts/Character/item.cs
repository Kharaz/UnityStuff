using UnityEngine;
using System.Collections;

public class item
{
	string name;
	string description;
	int weight;
	Component functionScript;
	
	public item() {
		name = "default item name";
		description = "this is a description";
		weight = 1;
		Component functionScript = null;
	}
	
	public item(string Name, string Description, int Weight, Component FunctionScript){
		name = Name;
		description = Description;
		weight = Weight;
		FunctionScript = FunctionScript;
		
	}
}

