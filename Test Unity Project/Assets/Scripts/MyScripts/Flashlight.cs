using UnityEngine;
using System.Collections;

/*
public class Flashlight : ItemFunctionalityTemplate
{
	bool on = false;
	int battery = 100;
	Transform light;
	
	public void Use(item Item){
		Debug.Log("Used item");
		toggleLight(light, Item);
	}
	
	void toggleLight(Transform light, item Item){	
		for(int i = 0; i < Item.transform.childCount; i++){
			if(Item.transform.GetChild(i).name == "Spotlight"){
				light = Item.transform.GetChild(i);
			}
		} 
		on = !on;
		light.gameObject.SetActive(on);
	}
}
*/

public class Flashlight : item {

	bool on = false;
	Transform light;
	
	public override void UseItem()
	{
		for(int i = 0; i < this.transform.childCount; i++){
			if(this.transform.GetChild(i).name == "Spotlight"){
				light = this.transform.GetChild(i);
			}
		}
		
		on = !on;
		light.gameObject.SetActive(on);
	}

}
