using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StorageActivator : Activator
{
	ActorInventory inventory = new ActorInventory();
	int maxItems = 10;
	int equipSlots = 0;
	public string name;
		
	public bool dispContents = false;
	
	
	//temporary pasting of variables from guicontroller, find a way to remove it
	public int Left;
	public int HorizMiddle;
	public int Right;
	public int HorizQuarter;
	
	public int Top;
	public int VertMiddle;
	public int Bottom;
	public int VertQuarter;
	//----||
	
	GameObject activeUser;
	
	
	// Use this for initialization
	void Start ()
	{	
		name = this.gameObject.name;
		toggle = true;
		active = false;
		
		inventory.maxItems = maxItems;
		inventory.equipSlots = equipSlots;
		
		//temporary variables, remove later/sooner
		Left = 0;
		Right = Screen.width;
		HorizMiddle = Screen.width/2;
		HorizQuarter = HorizMiddle/2;
		
		Top = 0;
		Bottom = Screen.height;
		VertMiddle = Screen.height/2;
		VertQuarter = VertMiddle/2;
		//----||
		
	}
	
	void OnGUI(){
		if(active){
			int maxCounter = inventory.Contents.Count;
			int counter = 0;
			
			//Rect inventoryBackground = new Rect(Left+HorizQuarter/4,Top+VertQuarter/4, 300, (maxCounter*20)+30);
			Rect inventoryBackground = new Rect(Left+HorizQuarter/4,Top+VertMiddle, 300, (maxCounter*20)+30);
			GUI.Box(inventoryBackground, name+"'s Contents");
			
			foreach(KeyValuePair<int, item> item in inventory.Contents){
				Rect LabelBox = new Rect(inventoryBackground.x + 10, inventoryBackground.y + (counter*20) + 25, 100, 20);
				GUI.Label(LabelBox, item.Value.Name());
				counter++;
			}
			
			
			if(!activeUser.GetComponent<playerController>().inventoryOn){
				Deactivate(activeUser);
			}	
		}
	}
	
	public override void Activate(GameObject activationSource)
	{
		Debug.Log ("Opened Chest");
		active = true;
		activeUser = activationSource;
		activationSource.GetComponent<playerController>().inventoryOn = true;
		activationSource.GetComponent<GuiController>().inExtInv = true;
	}
	
	public override void Deactivate(GameObject deactivationSource)
	{
		Debug.Log("Closed Chest");
		active = false;
		activeUser = null;
		deactivationSource.GetComponent<playerController>().inventoryOn = false;
		deactivationSource.GetComponent<GuiController>().inExtInv = false;
	}
}

