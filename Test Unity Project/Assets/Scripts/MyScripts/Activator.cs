using UnityEngine;
using System.Collections;

public class Activator : MonoBehaviour
{
	//activator base class
	//activators include anything that the player interacts with
	//that cannot be picked up, such as:
		//light switches
		//storage cabinets/chests
		//computer consoles
		//elevators(maybe special case like door), elevator buttons
		//etc
	
	public bool toggle = true;	
	public bool active = false;
	
	void Start(){
	
	}
	
	void Update(){
	
	}
	
	public virtual void Activate(GameObject activationSource){
		Debug.Log("Generic Activator Activated");
		active = true;
		//do stuff here
		
		
		if(!toggle){ //deactivate right after doing stuff if its not a toggle switch
			Deactivate(this.gameObject);
		}
	}
	
	public virtual void Deactivate(GameObject deactivationSource){
		Debug.Log ("Generic Activator Deactivated");
		active = false;
	}

}