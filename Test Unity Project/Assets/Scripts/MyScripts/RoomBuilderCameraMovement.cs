using UnityEngine;
using System.Collections;

public class RoomBuilderCameraMovement : MonoBehaviour {
	
	int moveSpeed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		checkMovementKeys();
	}
	
	void checkMovementKeys(){
		float horiz = Input.GetAxis("Horizontal");
		float vert = Input.GetAxis("Vertical");
		
		this.gameObject.transform.position += new Vector3(horiz, 0, vert) * Time.deltaTime * moveSpeed;
		
		if(Input.GetKey(KeyCode.Space)){
			this.gameObject.transform.position += new Vector3(0,moveSpeed/2,0) * Time.deltaTime;
			Debug.Log("pressed space");
		}
		if(Input.GetKey(KeyCode.LeftControl)){
			this.gameObject.transform.position -= new Vector3(0,moveSpeed/2,0) * Time.deltaTime;
		}
	}
}
