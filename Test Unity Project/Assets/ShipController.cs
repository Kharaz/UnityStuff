using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipController : MonoBehaviour {

	[SerializeField]
	Dictionary<KeyCode, List<Transform>> componentDict = new Dictionary<KeyCode, List<Transform>>();
	public Transform leftEngine;
	public Transform rightEngine;
	public Transform bottomEngine;
	public Transform topEngine;
	
	public Transform qEngine;
	public Transform eEngine;
	
	
	
	// Use this for initialization
	void Start () {
		Vector3 fwd = this.transform.forward;
		Vector3 up = this.transform.up;
		Vector3 right = this.transform.right;
		
		componentDict.Add(KeyCode.D, new List<Transform>());
		componentDict.Add(KeyCode.A, new List<Transform>());
		
		//componentDict[KeyCode.LeftShift] = new List<Transform>();
		
		componentDict.Add(KeyCode.Space, new List<Transform>());
		componentDict.Add(KeyCode.LeftShift, new List<Transform>());
	
		for(int i = 0; i < this.transform.childCount; i++) {
			Vector3 heading = this.transform.GetChild(i).transform.position - this.transform.position;
			
			float dirNumLR = getRelativeDirection(fwd, heading, this.transform.up);
			float dirNumUD = getRelativeDirection(right, heading, -fwd);
			
			Transform childTrans = this.transform.GetChild(i).transform;
			
			if(dirNumLR == 1f){
				componentDict[KeyCode.A].Add(childTrans);
				if(childTrans.name == "Engine1"){
					Debug.Log ("Added engine1 to keycode a");
				}
			} else if (dirNumLR == -1f){
				componentDict[KeyCode.D].Add(childTrans);
				if(childTrans.name == "Engine1"){
					Debug.Log ("Added engine1 to keycode d");
				}
			} else {
				
				Debug.Log ("Object " + childTrans.name + " is neither left nor right");
			}
			
			if(dirNumUD == 1f){
				componentDict[KeyCode.Space].Add(childTrans);
				if(childTrans.name == "Engine1"){
					Debug.Log ("Added engine1 to keycode space");
				}
			} else if (dirNumUD == -1f){
				//componentDict[KeyCode.LeftShift].Add(childTrans);
				
				if(childTrans.name == "Engine1"){
					Debug.Log ("Added engine1 to keycode shift");
					componentDict[KeyCode.LeftShift] = new List<Transform>() {childTrans};
				}
			} else {
				Debug.Log ("Object " + childTrans.name + " is neither up nor down");
			}
			
		}
		
		Debug.Log (componentDict);
	}
	
	// Update is called once per frame
	void Update () {
		//doKeyPresses();
		DictionaryKeyPresses();
	}
	
	void DictionaryKeyPresses(){
		foreach(KeyValuePair<KeyCode, List<Transform>> entry in componentDict){
			if(Input.GetKeyDown (entry.Key) || Input.GetKeyUp(entry.Key)){
				for(int i = 0; i < entry.Value.Count; i++){
					Debug.Log ("Attempting use of " + entry.Value[0].name + " with keycode " + entry.Key);
					///why the fuck doesnt shift work
					entry.Value[i].GetComponent<EngineComponent>().Use();
				}
			}
		}
	}
	
	float getRelativeDirection(Vector3 fwd, Vector3 targetDir, Vector3 up){
		Vector3 perp = Vector3.Cross (fwd, targetDir);
		float dir = Vector3.Dot(perp, up);
		
		if(dir > 0f){
			return 1f; //to the right
		} else if (dir < 0f) {
			return -1f; //to the left
		} else {
			return 0f; //front or behind
		}
	}
	
	void OnGUI(){
		drawUI();
	}
	
	void drawUI(){
		Vector3 speed = this.transform.rigidbody.velocity;
		GUI.Box (new Rect(0,0, 300, 50), "Velocity: " + speed);
	}
	
	void doKeyPresses(){
		if(Input.GetKey(KeyCode.W)){
			
		}
		if(Input.GetKey(KeyCode.S)){
			
		}
		if(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyUp(KeyCode.Q)){
			qEngine.GetComponent<EngineComponent>().Use();
		}
		if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyUp(KeyCode.E)){
			eEngine.GetComponent<EngineComponent>().Use();
		}
		
		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.A)){
			rightEngine.GetComponent<EngineComponent>().Use();
		}
		if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.D) ){
			leftEngine.GetComponent<EngineComponent>().Use();
		}
		
		if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift)){
			topEngine.GetComponent<EngineComponent>().Use();
		}
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(KeyCode.Space)){
			bottomEngine.GetComponent<EngineComponent>().Use();
		}
	}
	/*
	void bindComponents(){
		for(int i = 0; i < this.gameObject.transform.childCount; i++){
			GameObject currentChild = this.gameObject.transform.GetChild(i).gameObject;
			if(currentChild.name == "Engine"){
				
				int Right = isRight (currentChild);
				int Up = isUp (currentChild);
				
				if(Right == 1){
					componentKeys[KeyCode.D].Add(currentChild);
				}
				 
				if(Right == -1){
					componentKeys[KeyCode.A].Add(currentChild);
				}
				 
				if(Up == -1) {
					componentKeys[KeyCode.LeftShift].Add(currentChild);
				}
				
				if(Up == 1){
					componentKeys[KeyCode.Space].Add(currentChild);
				}
				
			}
		}
	}
	
	int isRight(GameObject obj){
		Vector3 objLocaltoWorld = obj.transform.TransformPoint(obj.transform.position);
		int outNum = Mathf.RoundToInt(Vector3.Dot(objLocaltoWorld, this.transform.position));
		
		Debug.Log ("Object " + obj.name + " is " + outNum  + " of " + this.gameObject.name);
		return outNum;
	}
	
	int isUp(GameObject obj){
		Vector3 objLocaltoWorld = obj.transform.TransformPoint(obj.transform.position);
		int outNum = Mathf.RoundToInt(Vector3.Dot(objLocaltoWorld, this.transform.position));
		return outNum;
	}
	
	int isForward(GameObject obj) {
		Vector3 objLocaltoWorld = obj.transform.TransformPoint(obj.transform.position);
		int outNum = Mathf.RoundToInt(Vector3.Dot(objLocaltoWorld, this.transform.position));
		return outNum;
	}
	*/
}
