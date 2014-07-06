using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuiController : MonoBehaviour
{
	
	public playerController controller;
	public ActorBehavior behavior;
	
	public int Left;
	public int HorizMiddle;
	public int Right;
	public int HorizQuarter;
	
	public int Top;
	public int VertMiddle;
	public int Bottom;
	public int VertQuarter;
	
	public bool inExtInv;
	
	//rect(left, top, width, height)
	void Start ()
	{
		controller = GetComponent<playerController>();
		behavior = GetComponent<ActorBehavior>();
		
		Left = 0;
		Right = Screen.width;
		HorizMiddle = Screen.width/2;
		HorizQuarter = HorizMiddle/2;
		
		Top = 0;
		Bottom = Screen.height;
		VertMiddle = Screen.height/2;
		VertQuarter = VertMiddle/2;
	}
	
	void OnGUI()
	{
		if (controller.inventoryOn == true){
			displayInventory();
			displayGear();
		}
		
		if (controller.statscreenOn == true){
			displayStats();
		}
	}
	
	void displayInventory()
	{
		int counter = 0;
		int maxcounter = behavior.inventory.Count ();
		
		Rect inventoryBackground = new Rect(Left+HorizQuarter/4,Top+VertQuarter/4, 300, (maxcounter*20)+30);
		GUI.Box(inventoryBackground, "Inventory");
	
		List<int> toRemove = new List<int>();
		
		foreach(KeyValuePair<int, item> item in behavior.inventory.Contents){
		
			Rect LabelBox = new Rect(inventoryBackground.x + 10, inventoryBackground.y + (counter*20) + 25, 100, 20);
			GUI.Label(LabelBox, item.Value.Name());
			
			Rect ItemButton = new Rect(inventoryBackground.x + 80, inventoryBackground.y + (counter*20) + 25, 60,20);
			Rect EquipButton = new Rect(inventoryBackground.x + 160, inventoryBackground.y +(counter*20) + 25, 60, 20);
			
			
			if(!behavior.inventory.GetItemAt(item.Key).equipped){
				if(GUI.Button (ItemButton,"Remove")) {
					toRemove.Add(item.Key);
					item.Value.equipped = false;
				}
			}
			
			if(!item.Value.equipped && behavior.inventory.CanEquipItem()){
				if(GUI.Button (EquipButton, "Equip")) {
					behavior.inventory.EquipItem(item.Value);
					behavior.EquipItem(item.Value, behavior.inventory.Equipped.Count);
					item.Value.equipped = true;
				}
			}
			
			counter++;
		}
		
		for(int i = 0; i < toRemove.Count; i++){
			behavior.DropItem(toRemove[i]);
		}
	}
	
	void displayStats()
	{
		Rect statsBackground = new Rect(Right-HorizQuarter+(HorizQuarter/4),Top+VertQuarter/4, 100,80);
		Vector2 statsContent = new Vector2(statsBackground.x+10, statsBackground.y+20);
		
		GUI.Box(statsBackground, behavior.data.name);
		
		for(int i = 0; i < behavior.data.stats.Length; i++){
			Rect numberlabelPosition = new Rect(statsContent.x+70, statsContent.y + i*15, 40, 20);
			Rect namelabelPosition = new Rect(statsContent.x, statsContent.y+i*15, 70, 20);
		
			GUI.Label(numberlabelPosition, behavior.data.stats[i].current.ToString());
			GUI.Label (namelabelPosition, "StatName");
		}
	}
	
	void  displayGear()
	{
		Rect gearBackground = new Rect(Right - HorizQuarter - (HorizQuarter/4), Top + VertQuarter, 210, 110);
		//Vector2 gearContentPos = new Vector2(Right - HorizQuarter + (HorizQuarter/4), Top + VertQuarter);
		Vector2 gearContentPos = new Vector2(gearBackground.x + 20, gearBackground.y);
		
		GUI.Box (gearBackground, "Gear");

		for(int i = 0; i < behavior.inventory.equipSlots; i++){
			string slotLabel = "Slot " + i + ": ";
			string slotContent = "Empty";
			
			Rect slotRect = new Rect(gearContentPos.x, gearContentPos.y + (i*20) + 20, gearBackground.width, 20);
			Rect slotRectUnequipButton = new Rect( slotRect.x + 120, slotRect.y, 60, 20);
			
			/*
			if(behavior.inventory.GetEquippedItem(i) && behavior.inventory.Contents.ContainsValue(behavior.inventory.GetEquippedItem(i))){
				slotContent = behavior.inventory.Equipped[i].Name();
				if (GUI.Button (slotRectUnequipButton, "Unequip")){
					behavior.inventory.GetEquippedItem(i).equipped = false;
					behavior.inventory.UnequipItem(i);
				}
			}
			*/
			
			if(behavior.inventory.GetEquippedItem(i)){
				slotContent = behavior.inventory.Equipped[i].Name();
				if (GUI.Button (slotRectUnequipButton, "Unequip")){
						behavior.inventory.GetEquippedItem(i).equipped = false;
						behavior.UnequipItem(behavior.inventory.GetEquippedItem(i));
						behavior.inventory.UnequipItem(i);
					}
			}
			
			GUI.Label (slotRect, slotLabel+ slotContent);
			
		}
	}
	
	
	// todo: make generic "displayInventory" method that just draws an inventory
	// regardless of whose it is since, like, they're kinda all the same
	/*
	public void displayExternalInventory(ActorInventory extInv)
	{
		int maxCounter = extInv.Contents.Count;
		int counter = 0;
		inExtInv = true;
	
		Rect inventoryBackground = new Rect(Left+HorizQuarter/4,Top+VertQuarter/4, 300, (maxCounter*20)+30);
		GUI.Box(inventoryBackground, "Inventory");
		
		foreach(KeyValuePair<int, item> item in extInv.Contents){
			Rect LabelBox = new Rect(inventoryBackground.x + 10, inventoryBackground.y + (counter*20) + 25, 100, 20);
			GUI.Label(LabelBox, item.Value.Name());
			counter++;
		}	
	}
	*/

	void Update ()
	{
	
	}
}

