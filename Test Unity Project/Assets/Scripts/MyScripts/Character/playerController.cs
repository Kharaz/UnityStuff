using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour
{
	//movement value stuff
	float maxVel;
	float gravity = 40f;
	float jump;
	float yVelocity = 0;
	//---------------|
	
	//mouselook stuff
	public enum RotationAxes {MouseXandY = 0, MouseX = 1, MouseY = 2}; 
	public RotationAxes axes = RotationAxes.MouseXandY;
	
	public float sensitivity = 15f;
	
	public float minX = -360f;
	public float maxX = 360f;
	
	public float minY = -60f;
	public float maxY = 60f;
	
	float rotationY;
	//---------------|
	
	
	//GUI stuff
	public bool inventoryOn = false;
	public bool statscreenOn = false;
	//---------------|
	
	//component stuff
	public ActorBehavior actorBehavior;
	public CharacterController controller;
	public GameObject camera;
	//---------------|
	
	void Start ()
	{
		actorBehavior = GetComponent<ActorBehavior>();
		controller = GetComponent<CharacterController>();
		camera = GameObject.Find ("Camera");
		
		if (actorBehavior != null) {
			maxVel = actorBehavior.GetStat(ActorData.StatType.AGILITY);
			jump = actorBehavior.GetStat (ActorData.StatType.STRENGTH);
			actorBehavior.data.name = "Player";
		}
	}
	
	public void UpdateStats() {
		maxVel = actorBehavior.GetStat(ActorData.StatType.AGILITY);
	}
	
	void FixedUpdate()
	{
		UpdateStats ();
	}
	
	void UpdateMovement() {
		float x = Input.GetAxis ("Horizontal");
		float z = Input.GetAxis ("Vertical");
		
		Vector3 moveTo = new Vector3(x,0,z);
		moveTo = transform.TransformDirection(moveTo);
		moveTo *= maxVel;
		
		if(controller.isGrounded){
			yVelocity = 0;
			
			if(Input.GetKey (KeyCode.Space)){
				yVelocity = jump;
			}
		} else {
			yVelocity -= gravity*Time.deltaTime;
		}
		
		moveTo.y = yVelocity;
		controller.Move(moveTo*Time.deltaTime);
		actorBehavior.Move (transform.position.x,transform.position.z);
	}

	void UpdateMouseLook() {
		
		if (inventoryOn){
			Screen.lockCursor = false;
			return;
		} else {
			Screen.lockCursor = true;
		}
		
		float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
		
		rotationY += Input.GetAxis("Mouse Y") * sensitivity;
		rotationY = Mathf.Clamp (rotationY, minY, maxY);
			
		if (axes == RotationAxes.MouseXandY){
			transform.localEulerAngles = new Vector3(0, rotationX, 0);
			camera.transform.localEulerAngles = new Vector3(-rotationY,0,0);	
		}
		else if(axes == RotationAxes.MouseX){
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
		}
		else if(axes == RotationAxes.MouseY){
			camera.transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}		
	}
	
	void UpdateKeyPress() {
		if(Input.GetKeyDown(KeyCode.Tab)){
				//inventoryOn = !inventoryOn;
				//statscreenOn = !statscreenOn;	
				inventoryOn = !inventoryOn;
				statscreenOn = inventoryOn;
		}
		
		if(Input.GetKeyDown(KeyCode.E)){
			Ray ray = camera.camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast (ray, out hit)){
				Debug.Log(hit.transform.name);
				Debug.DrawRay(camera.transform.position, ray.direction, Color.green);
				
				if(hit.transform.GetComponent<item>() != null){
					actorBehavior.PickUpItem(hit.transform.gameObject.GetComponent<item>());
				}
				
				if(hit.transform.GetComponent<Activator>() != null){
					hit.transform.GetComponent<Activator>().Activate(this.gameObject);
				}

				/*
				if(hit.transform.GetComponent<item>() != null){
					Debug.Log ("Is Item");
				}
				*/
				
			}
		}
		
		bool slot1 = actorBehavior.inventory.isEquipped(0);
		bool slot2 = actorBehavior.inventory.isEquipped(1);
		
		//non-firearm stuff. don't like, not flexible, can i clean this up?
		if(slot1 && Input.GetMouseButtonDown(0) && !inventoryOn && !actorBehavior.inventory.Equipped[0].GetComponent<Firearm>()){
			Debug.Log ("Pressed Mouse 0");
			actorBehavior.inventory.Equipped[0].UseItem();
		}
		if(slot2 && Input.GetMouseButtonDown(1) && !inventoryOn){
			Debug.Log ("Pressed Mouse 1");
			actorBehavior.inventory.Equipped[1].UseItem();
		}

		
		//firearm specific stuff, gross
		if(slot1 && actorBehavior.inventory.Equipped[0].GetComponent<Firearm>() && !inventoryOn){
			if(Input.GetKeyDown (KeyCode.R)){
				actorBehavior.inventory.Equipped[0].SendMessage("Reload");
			}
			
			if(Input.GetMouseButton(0)){
				actorBehavior.inventory.Equipped[0].UseItem ();
			}
		}
		
		if(slot2 && actorBehavior.inventory.Equipped[1].GetComponent<Firearm>() && !inventoryOn){
			if(Input.GetKeyDown (KeyCode.R)){
				actorBehavior.inventory.Equipped[1].SendMessage("Reload");
			}
			
			if(Input.GetMouseButton(1)){
				actorBehavior.inventory.Equipped[1].UseItem ();
			}
		}
	
		
	}
	
	void CheckForOutOfBounds() {
		if(transform.position.y < -10){
			transform.position = new Vector3(0,2,0);
		}
	}
	
	void Update()
	{	
		if(actorBehavior.IsAlive()){
			UpdateMovement ();
			UpdateMouseLook ();
			UpdateKeyPress ();
		}
		CheckForOutOfBounds();
	}
}

