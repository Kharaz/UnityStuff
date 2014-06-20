using UnityEngine;
using System.Collections;

public class item : MonoBehaviour
{
	public string name = "default item name";
	string description = "default description";
	int weight = 1;
	
	public ItemFunctionalityTemplate functionScript;
	
	public bool equipped = false;
	
	public void Start(){
		name = this.gameObject.name;
		//functionScript = new MeleeWeapon();
	}

	public void Despawn(){
		this.transform.gameObject.SetActive(false);
	}
	
	public void Respawn(Vector3 pos){
		this.transform.position = pos;
		this.transform.gameObject.SetActive(true);
	}
	
	public void RespawnEquip(int slot){
		this.transform.gameObject.SetActive (true);
	}
	/*	
	public item() {
		name = "default item name";
		description = "this is a description";
		weight = 1;
	}
	
	public item(string Name) {
		name = Name;
		description = "this is a description";
		weight = 1;
	}
	
	public item(string Name, string Description, int Weight, ItemFunctionalityTemplate FunctionScript){
		name = Name;
		description = Description;
		weight = Weight;
		//functionScript = FunctionScript;
		
	}
	*/

	public string Name(){
		return name;
	}
	
	public void setName(string name){
		this.name = name;
	}

	public virtual void UseItem(){
		Debug.Log ("I got used :(");
	}
}

