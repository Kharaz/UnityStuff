using UnityEngine;
using System.Collections;

public class GuiController : MonoBehaviour
{
	
	public playerController controller;
	public ActorBehavior behavior;
	
	void Start ()
	{
		controller = GetComponent<playerController>();
		behavior = GetComponent<ActorBehavior>();
	}
	
	void OnGUI()
	{
		if (controller.inventoryOn == true){
			displayInventory();
		}
	}
	
	void displayInventory()
	{
		GUI.Box(new Rect(50,50,100,150), "Inventory");
		
		int counter = 0;
		
		Debug.Log(behavior.inventory.Count ());
		
		while (counter < behavior.inventory.Count())
		{
			GUI.Label(new Rect(60, 50+(counter*20),100,20), "Item");
			counter++;
		}
		
		GUI.Label(new Rect(60, 50+(counter*20)+20,100,20), "EndOfInventory");
		
		/*
		for(int i = 0; i < behavior.inventory.currentItemCount; i++){
			GUI.Label(new Rect(60, 50+(i*20),100,20), "Item");
		}
		*/
	}
	
	
	// Use this for initialization

	// Update is called once per frame
	void Update ()
	{
	
	}
}

