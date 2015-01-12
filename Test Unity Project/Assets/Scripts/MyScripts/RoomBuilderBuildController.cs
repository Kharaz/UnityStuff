using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomBuilderBuildController : MonoBehaviour {

	public Transform tempModel;
	public GameObject roomPrefab;
	public GameObject roomObjectHolder;
	bool deleteMode = false;
	
	public Room room;
	
	//screen placement stuff
	int left = 0;
	int right = Screen.width;
	int middle = Screen.width/2;
	int top = 0;
	int bottom = Screen.height;
	int middleVert = Screen.height/2;

	Transform currentModel = null;
	
	string notificationText = "";
	float eraseTextTime = 0;

	void Start () {
		
	}
	
	string PrintArray() {
		string array = "";
		Debug.Log ("Blocks:");
		for(int i = 4; i >= 0; i--){
			string line = "";
			for(int j = 0; j < 5; j++){
				line += room.blocks[new Vector2(j,i)];
			}
			line += '\n';
			array += line;
		}
		return array;
	}
	
	void Update () {
		checkPlacement();
	}
	
	void OnGUI(){
		drawUI();
		displayModels();
	}
	
	void switchCurrentRoom(GameObject newRoom){
		for(int i = 0; i < room.gameObject.transform.childCount; i++){
			room.gameObject.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.gray;
		}
		//room.gameObject.transform.GetComponent<MeshRenderer>().material.color = Color.white;
		room = newRoom.GetComponent<Room>();
		for(int i = 0; i < room.gameObject.transform.childCount; i++){
			room.gameObject.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.white;
		}
		//room.gameObject.transform.GetComponent<MeshRenderer>().material.color = Color.green;
	}
	
	void makeNewRoom(){
		GameObject tempObj = (GameObject)Instantiate (roomPrefab, new Vector3(0,0,0), Quaternion.identity);
		tempObj.GetComponent<Room>().CubeRef = tempModel.gameObject;
		tempObj.transform.parent = roomObjectHolder.transform;
		switchCurrentRoom(tempObj);
	}
	
	void drawUI(){
		eraseTextTime += 1;
		if((eraseTextTime/1000) > 3){
			eraseTextTime = 0;
			notificationText = "";
		}
		Rect notificationBox = new Rect(right - 300, top, 300, 100);
		GUI.Box(notificationBox, notificationText);
		
		if(deleteMode){
			GUI.color = Color.red;
		}
		
		if(GUI.Button(new Rect(left,top+100,100,100),"Remove")){
			deleteMode = !deleteMode;
		}
		
		if(GUI.Button (new Rect(left,top+200,100,100),"Save")){
			room.serializeRoom();
		}
		
		if(GUI.Button (new Rect(left, top+300, 100, 100), "Load")){
			room.deserializeRoom();
		}
		
		if(GUI.Button (new Rect(left, top+400, 100, 50), "New Floor")){
			makeNewRoom();
		}
		
		if(GUI.Button (new Rect(left, top+450, 100, 50), "Copy Current")){
			room.serializeRoom();
			makeNewRoom();
			room.deserializeRoom();
			Debug.Log ("Copied room: " + PrintArray());
		}
		
		Rect upArrow = new Rect(right - 100, bottom - 100, 25, 25);
		Rect downArrow = new Rect(right - 100, bottom - 50, 25, 25);
		Rect rightArrow = new Rect(right - 75, bottom - 75, 25 ,25);
		Rect leftArrow = new Rect(right - 125, bottom - 75, 25, 25);
		
		Rect moveUp = new Rect(right - 50, bottom - 110, 25,25);
		Rect moveDown = new Rect(right - 50, bottom - 40, 25 ,25);
		
		if(GUI.Button (upArrow, "^")){
			room.transform.position += new Vector3(0,0,1);
		}
		if(GUI.Button (downArrow, "v")){
			room.transform.position += new Vector3(0,0,-1);
		}
		if(GUI.Button (rightArrow, ">")){
			room.transform.position += new Vector3(1,0,0);
		}
		if(GUI.Button (leftArrow, "<")){
			room.transform.position += new Vector3(-1,0,0);
		}
		
		
		if(GUI.Button (moveUp, "up")){
			room.transform.position += new Vector3(0,1,0);
		}
		if(GUI.Button (moveDown, "dn")){
			room.transform.position += new Vector3(0,-1,0);
		}
		
		
	}
	
	void checkPlacement(){
		if(Input.GetMouseButtonDown(0)){
			
			RaycastHit rayHit;
			Ray ray = this.camera.ScreenPointToRay(Input.mousePosition);
		
			if(Physics.Raycast(ray, out rayHit)){
				if(rayHit.collider.gameObject.transform.parent.transform.GetComponent<Room>()){
					if(rayHit.collider.gameObject.transform.parent.transform != room.gameObject.transform){
						switchCurrentRoom(rayHit.collider.gameObject.transform.parent.gameObject);
						notificationText += "Selected different room\n";
						return;	
					}
				}
				
			
				if(!currentModel){
					notificationText += "No part selected, can't place object\n";
				} else {
					Debug.Log ((int)rayHit.point.x + " " + (int)rayHit.point.z);
					//Vector3 hit = room.transform.TransformPoint(new Vector3(rayHit.point.x, 0, rayHit.point.z));
					Vector3 hit = room.transform.InverseTransformPoint(rayHit.point);
					
					Debug.Log ("Regular Point: "+ rayHit.point);
					Debug.Log ("transformed point: "  + hit);
					hit.x = (int)hit.x;
					hit.z = (int)hit.z;
					
					int part = room.GetPartAt(new Vector2((int)hit.x, (int)hit.z));
					
					if(part != 0 && !deleteMode){
						notificationText += "Already a part there\n";
					} else {
						int newX = (int)hit.x;
						int newZ = (int)hit.z;
						Vector3 newPoint = new Vector3(newX, 0, newZ);
						
						if(deleteMode){
							removePartOnHit(rayHit, new Vector2(newX,newZ));
							//Destroy(rayHit.collider.gameObject);
							//room.PlacePart (new Vector2(newX, newZ), 0);
						} else {
							placePartOnHit(new Vector2(newX, newZ));
							//Instantiate(currentModel, newPoint, Quaternion.identity);
							//room.PlacePart(new Vector2(newX,newZ), 1);
						}
					}
				}
			}
			Debug.Log (PrintArray());
		}
	}
	
	void removePartOnHit(RaycastHit rayHit, Vector2 pos){
		if(rayHit.collider.gameObject.name == "New Game Object"){
			notificationText+="Will not delete room floor\n";
			room.RemovePart(pos, null);
			return;
		}
		room.RemovePart(pos, rayHit.collider.gameObject);
	}
	
	void placePartOnHit(Vector2 pos){
		room.PlacePart(pos, 1);
	}
	
	
	void selectPart(Transform part){
		currentModel = part;
	}
	
	void displayModels(){
		if(tempModel){
			if(GUI.Button(new Rect(left, top, 100, 100), "Box")){
				selectPart (tempModel);
			}
			
		}
	}
	

	
}
