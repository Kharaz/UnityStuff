using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	//todo: kill self for how terrible this entire script is
	
	//this is for a two door type door
	public Transform Door1;
	public Transform Door2;
	
	int min;
	int max;
	
	Vector3 Door1Open;
	Vector3 Door2Open;
	Vector3 Door1Closed;
	Vector3 Door2Closed;
	
	
	float moveTime = 0.5f;
	public float t = 0f;
	
	
	public bool moving;
	public bool open;
	
	// Use this for initialization
	void Start () {
		//Trigger = this.transform.GetComponent<SphereCollider>();
		min = 0;
		max = 2;
		moving = false;
		open = false;
		Door1Open = Door1.position - new Vector3(0,0,1);
		Door2Open = Door2.position + new Vector3(0,0,1);
		Door1Closed = Door1.position;
		Door2Closed = Door2.position;
	}
	
	void FixedUpdate(){
		if (moving && t < 1){
			t += Time.deltaTime/moveTime;
		} else {
			t = 0;
			moving = false;
		}
		if(open){
			//t += Time.deltaTime/moveTime;
			Door1.position = Vector3.Lerp(Door1.position, Door1Open, t);
			Door2.position = Vector3.Lerp(Door2.position, Door2Open, t);
		
		} else if (!open){
			//t += Time.deltaTime/moveTime;
			Door1.position = Vector3.Lerp(Door1.position, Door1Open + new Vector3(0,0,1), t);
			Door2.position = Vector3.Lerp(Door2.position, Door2Open - new Vector3(0,0,1), t);
		
		}
	}
	
	
	
	IEnumerator MoveFromTo(Transform transform, Vector3 end, float time){
		if(!moving){
			moving = true;
			float t = 0f;
			while(t < 1.0f){
			t += Time.deltaTime / moveTime;
			transform.position = Vector3.Lerp (transform.position, end, t);
			yield return 0;
			}
		}
		moving = false;
		yield return 0;
	}
	
	
	void OnTriggerEnter(Collider OtherCollider){
		 //StartCoroutine(MoveFromTo(Door1, Door1.position + new Vector3(0,0,2), moveTime));
		 //StartCoroutine(MoveFromTo(Door2, Door2.position - new Vector3(0,0,2), moveTime));
		moving = true;
		open = true;
	}
	
	void OnTriggerExit(Collider OtherCollider){
		//StartCoroutine(MoveFromTo(Door1, Door1.position - new Vector3(0,0,2), moveTime));
		//StartCoroutine(MoveFromTo(Door2, Door2.position + new Vector3(0,0,2), moveTime));
		moving = true;
		open = false;
	}
}
