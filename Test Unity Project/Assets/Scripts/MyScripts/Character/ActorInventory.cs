using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorInventory
{
	public Dictionary<int, item> Contents = new Dictionary<int, item>();
	public Dictionary<int, item> Equipped = new Dictionary<int, item>();
	
	public int maxItems = 10;
	public int equipSlots = 4;
	
	public int Count(){
		return Contents.Count;
	}
	
	public item GetItemAt(int index){
		if(Contents.ContainsKey(index)){
			return Contents[index];
		} else {
			return null;
		}
	}
	
	public bool isEquipped(int index){
		if(Equipped.ContainsKey(index)){
			return true;
		} else {
			return false;
		}
	}
	
	public void AddItem(item Item){
		if(Contents.Count < maxItems){
			for(int i = 0; i < maxItems; i++){
				if (!Contents.ContainsKey(i)){
					Contents.Add(i, Item);
					return;
				}
			}
		} else {
			Debug.Log("Max Items Reached");
		}
		DebugPrintInventory();
	}
	
	public void RemoveItem(int index){
		if(Contents.ContainsKey(index)){
			Contents.Remove(index);
			//Contents[index].SpawnItem();
		} else {
			Debug.Log ("Cannot find item at index " +index);
		}
	}
	
	public void EquipItem(item Item){
		for(int i = 0; i < equipSlots; i++){
			if(!Equipped.ContainsKey(i)) 
			{
				Equipped[i] = Item;
				return;
			}
		}
	}
	
	public void UnequipItem(int index){
		if(Equipped[index] != null){
			Equipped.Remove(index);
		} else {
		Debug.Log ("No item at index " + index);
		}
	}
	
	public item GetEquippedItem(int index){
		if(Equipped.ContainsKey(index)) return Equipped[index];
		return null;
	}
	
	public bool CanEquipItem(){
		if(Equipped.Count == equipSlots){
			return false;
		}
		return true;
	}
	
	public void DebugPrintInventory(){
		string invString = "Inventory Contents: ";
		string equipString = "Equipped items: ";
		
		foreach(KeyValuePair<int, item> item in Contents){
			invString += item.Value.Name() + ", ";
		}
		
		foreach (KeyValuePair<int, item> item in Equipped){
			equipString += item.Value.Name () + ", ";
		}
		
		Debug.Log (invString);
		Debug.Log (equipString);
	}
}

