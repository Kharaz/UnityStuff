using UnityEngine;
using System.Collections;

public class DoorScript2 : MonoBehaviour
{
	public bool isOpen = false;
	public bool moving = false;
	
	public Transform Door1;
	public Transform Door2;
	
	Vector3 startpos;
	Vector3 endpos;
	
	Vector3 moveVec;
	
	float moveSpeed = 1;
	float moveTo = 1;
	
	public void Start(){
		startpos = Door1.localPosition;
		endpos = Door1.localPosition - new Vector3(0,0,moveTo);
		moveVec = new Vector3(0,0,moveTo);
	}
	
	public void OnTriggerEnter(){
		isOpen = true;
	}
	
	public void OnTriggerExit(){
		isOpen = false;
	}	
	
	public void Update(){
		if(isOpen && Door1.localPosition.z > endpos.z){
			//Door1.localPosition = Vector3.Lerp(startpos, endpos, 0.1f);
			Door1.Translate(-moveVec * Time.deltaTime);
			Door2.Translate (moveVec * Time.deltaTime);
		}
		
		if(!isOpen && Door1.localPosition.z < startpos.z){
			Door1.Translate (moveVec * Time.deltaTime);
			Door2.Translate (-moveVec* Time.deltaTime);
		}
	}
	
}

