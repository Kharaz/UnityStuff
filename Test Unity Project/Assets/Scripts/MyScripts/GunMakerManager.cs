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
			Debug.Log ("Clicked button");
			//selectedNode = null;

			RaycastHit rayHit;
			Ray ray = this.camera.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out rayHit)){
				Debug.Log(rayHit.transform.name);

				if(selectedPiece != null){
					selectedPiece.renderer.material.color = Color.white;
					selectedComponent = null;
				}

				selectedPiece = rayHit.transform.gameObject;
				selectedComponent = selectedPiece.GetComponent<GunComponent>();

				selectedPiece.renderer.material.color = Color.red;
				Debug.Log (selectedComponent.attachNodes[0].name);
			}

		}
	}
	void OnGUI() {
		if(selectedPiece != null){
			for(int i = 0; i < selectedComponent.attachNodes.Count; i++) {
				GameObject currNode = selectedComponent.attachNodes[i];

				Vector3 cameraPoint = camera.WorldToScreenPoint(currNode.transform.position);
				Rect currButton = new Rect(cameraPoint.x - 12, cameraPoint.y - 12, 25, 25);

				if(GUI.Button(currButton,"+")){
					selectedNode = currNode;
					//displayAttachMenu(new Vector2(currButton.x+12, currButton.y));
				}
			}
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
		//Debug.Log ("Displaying menu");

		foreach(KeyValuePair<int, GameObject> attachment in AvailableComponents) {
			Rect buttonRect = new Rect(background.x + 10,background.y + (attachment.Key * 20) + 10, 110, 20);
			if(GUI.Button (buttonRect,attachment.Value.name)){
				Debug.Log("add "+attachment.Value.name+" component here");
				attachment.Value.GetComponent<GunComponent>().Attach(selectedNode);
			}
		}
	}
	
}
