using UnityEngine;
using System.Collections;

public class IsoCameraMovement : MonoBehaviour {

	float moveSpeed = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		doKeyPresses();
	}
	
	void doKeyPresses(){
		if(Input.GetKey(KeyCode.W)){
			transform.localPosition += new Vector3(transform.forward.x, 0, transform.forward.z)*Time.deltaTime*moveSpeed;
		}
		if(Input.GetKey(KeyCode.A)){
			transform.localPosition -= transform.right*Time.deltaTime*moveSpeed;
		}
		if(Input.GetKey(KeyCode.S)){
			transform.localPosition -= new Vector3(transform.forward.x, 0, transform.forward.z)*Time.deltaTime*moveSpeed;
		}
		if(Input.GetKey(KeyCode.D)){
			transform.localPosition += transform.right*Time.deltaTime*moveSpeed;
		}
	}
}
