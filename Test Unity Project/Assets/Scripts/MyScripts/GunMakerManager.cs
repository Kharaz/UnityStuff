using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunMakerManager : MonoBehaviour {

	GameObject selectedPiece;
	GunComponent selectedComponent;
	GameObject selectedNode;
	Dictionary<int, GameObject> AvailableComponents = new Dictionary<int, GameObject>();

	// Use this for initialization
	void Start () {
		AvailableComponents.Add(0, GameObject.Find("GenericComponent"));
	}
	
	// Update is called once per frame
	void Update () {
		
		doMouseClicks();
		
	}

	void doMouseClicks(){
		if(Input.GetMouseButtonDown(0)){
			RaycastHit rayHit;
			Ray ray = this.camera.ScreenPointToRay(Input.mousePosition);
			
			if(Physics.Raycast(ray, out rayHit)){
				if(rayHit.transform.gameObject.GetComponent<GunComponent>() != null){
					selectPiece (rayHit);
				} else {
				
				}
				
			}
		
		
			/*
			if(selectedPiece != null){
				selectedPiece.renderer.material.color = Color.white;
				selectedPiece = null;
				selectedComponent = null;
			} else {
				
			}
			*/
		}
	}
	
	void deselectPiece()
	{
		selectedPiece.renderer.material.color = Color.white;
		selectedPiece = null;
		selectedComponent = null;
	}
	
	void selectPiece(RaycastHit select) {
		if(selectedPiece != null){
			deselectPiece();
		}
	
		Debug.Log("selected " +select.transform.name);
		selectedPiece = select.transform.gameObject;
		selectedComponent = selectedPiece.GetComponent<GunComponent>();
		
		selectedPiece.renderer.material.color = Color.red;
	}
	
	void OnGUI() {
		if(selectedPiece != null){
		
			//need to redesign system to use some weird specialization
			//of octrees so yeah this is kinda useless
			//shifting focus elsewhere
			/*	
			Debug.Log(selectedComponent.attachNodes[2]);
			foreach(KeyValuePair<int, GameObject> attachNode in selectedComponent.attachNodes){
				GameObject currNode = attachNode.Value;
				
				Vector3 cameraPoint = camera.WorldToScreenPoint(currNode.transform.position);
				Rect currButton = new Rect(cameraPoint.x - 12, cameraPoint.y - 12, 25, 25);
				
				if (GUI.Button(currButton, attachNode.Key.ToString())){
					Debug.Log ("selected node " + currNode.transform.name);
					selectedNode = currNode;
				}
			}
			*/
		
			/*
			for(int i = 0; i < selectedComponent.attachNodes.Count; i++) {
				GameObject currNode = selectedComponent.attachNodes[i];
					
				Vector3 cameraPoint = camera.WorldToScreenPoint(currNode.transform.position);
				Rect currButton = new Rect(cameraPoint.x - 12, cameraPoint.y - 12, 25, 25);

				if(currNode.transform.childCount != 0){
					if(GUI.Button(currButton, "-")){
						Destroy (currNode.transform.GetChild(0));
						selectedComponent.attachNodes[i] = null;
					}
				} else {
					if(GUI.Button(currButton,"+")){
						selectedNode = currNode;
						Debug.Log(selectedNode.name + " is clicked");
						//displayAttachMenu(new Vector2(currButton.x+12, currButton.y));
					}
				}
			}
			*/
		}

		if(selectedNode != null){
			Vector3 cameraPoint = camera.WorldToScreenPoint(selectedNode.transform.position);
			Vector2 attachMenuPos = new Vector2(cameraPoint.x, cameraPoint.y);
			displayAttachMenu(attachMenuPos);
		}
	}

	void displayAttachMenu(Vector2 pos) {
		Rect background = new Rect(pos.x, pos.y, 140, (AvailableComponents.Count)*20 + 20);
		GUI.Box(background, "");

		foreach(KeyValuePair<int, GameObject> attachment in AvailableComponents) {
			Rect buttonRect = new Rect(background.x + 10,background.y + (attachment.Key * 20) + 10, 110, 20);
			if(GUI.Button (buttonRect,attachment.Value.name)){
				Debug.Log("add "+attachment.Value.name+" component here");
				
				GameObject newObj = Instantiate(attachment.Value) as GameObject;
				GunComponent newObjScript = newObj.GetComponent<GunComponent>();
				newObjScript.Attach(1, newObjScript); //temporary, todo: change inputs to something that is right
				
				selectedNode = null;
				selectedComponent = null;
				selectedPiece = null;
			}
		}
	}
	
}
