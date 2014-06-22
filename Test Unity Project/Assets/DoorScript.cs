using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	
	//this is for a two door type door
	public Transform Door1;
	public Transform Door2;
	
	int min;
	int max;
	Vector3 pos;

	
	// Use this for initialization
	void Start () {
		//Trigger = this.transform.GetComponent<SphereCollider>();
		min = 0;
		max = 2;	
	
		pos = new Vector3(0, 0, Mathf.Lerp(min, max, 1));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
	
	void OnTriggerEnter(Collider OtherCollider){
		Debug.Log ("Collided with " + OtherCollider.gameObject.name);
		Door1.localPosition += pos;
		Door2.localPosition -= pos;

	}
	
	void OnTriggerExit(Collider OtherCollider){
		Door1.localPosition -= pos;
		Door2.localPosition += pos;
	}
}
