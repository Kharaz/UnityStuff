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
	public Vector2 mouseAbs;
	public Vector2 smoothMouse;
	
	public Vector2 sensitivity = new Vector2(2,2); 
	public Vector2 smoothing = new Vector2(3,3);
	
	public Vector2 clampDeg = new Vector2(360, 180);
	public bool lockCursor;
	
	public Vector2 targetDir;
	//public Vector2 targetCharDir;
	public GameObject charBody;
	//---------------|
	
	//component stuff
	public ActorBehavior actorBehavior;
	public CharacterController controller;
	//---------------|
	
	void Start ()
	{
		actorBehavior = GetComponent<ActorBehavior>();
		controller = GetComponent<CharacterController>();
		
		targetDir = controller.transform.localRotation.eulerAngles;
		
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
		Screen.lockCursor = lockCursor;
		
		var targetOrientation = Quaternion.Euler (targetDir);
		
		var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
		mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y*smoothing.y));
		
		smoothMouse.x = Mathf.Lerp(smoothMouse.x, mouseDelta.x, 1/smoothing.x);
		smoothMouse.y = Mathf.Lerp(smoothMouse.y, mouseDelta.y, 1/smoothing.y);
		
		mouseAbs += smoothMouse;
		
		if(clampDeg.x < 360) {
			mouseAbs.x = Mathf.Clamp(mouseAbs.x, -clampDeg.x * 0.5f, clampDeg.x * 0.5f);
		}
		
		 var xRot = Quaternion.AngleAxis(-mouseAbs.y, targetOrientation * Vector3.right);
        transform.rotation = xRot;

		
		if(clampDeg.y < 360) {
			mouseAbs.y = Mathf.Clamp(mouseAbs.y, -clampDeg.y * 0.5f, clampDeg.y * 0.5f);
		}
	
        var yRot = Quaternion.AngleAxis(mouseAbs.x, transform.InverseTransformDirection(Vector3.up));
        transform.rotation *= targetOrientation;
		transform.rotation *= yRot;
	}
	
	void CheckForOutOfBounds() {
		if(transform.position.y < -10){
			transform.position = new Vector3(0,2,0);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{	
		if(actorBehavior.IsAlive()){
			UpdateMovement ();
			Debug.Log (Input.GetAxisRaw("Mouse X"));
		}
		UpdateMouseLook ();
		CheckForOutOfBounds();
	}
}

