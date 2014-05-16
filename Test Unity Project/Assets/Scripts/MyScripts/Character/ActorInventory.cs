using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorInventory : MonoBehaviour
{
	
	List<item> Contents;
	
	public int maxItems = 10;
	
	// Use this for initialization
	void Start ()
	{
		Contents = new List<item>();
	
	}
	
	void AddItem(item Item){
	
		if(Contents.Count >= maxItems){
			Contents.Add(Item);
		}
		else{
			Debug.Log("Max Items Reached");
		}
	}
	
	void RemoveItem(item Item){
		Contents.Remove(Item);
	}
	
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

