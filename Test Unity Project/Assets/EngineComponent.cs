using UnityEngine;
using System.Collections;

public class EngineComponent : MonoBehaviour {

	public Transform thrustPoint;
	public Transform emitter;

	bool isActive = false;
	public float force = 1;
	
	Vector3 thrustVector;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isActive){
			this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
			thrustVector = thrustPoint.transform.position - this.transform.position;
			this.transform.parent.rigidbody.AddForceAtPosition(-force * thrustVector, thrustPoint.position);
		}
	}
	void OnDrawGizmos(){
	if(isActive){
			Gizmos.DrawLine(thrustPoint.transform.position,this.transform.position + 5*(thrustPoint.transform.position- this.transform.position));
			if(emitter){
				emitter.GetComponent<ParticleSystem>().Play();
			}
		}
	}
	
	public void Use(){
		isActive = !isActive;
		
		
		if(!isActive){
			this.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
			if(emitter){
				emitter.GetComponent<ParticleSystem>().Stop();
			}
		}
	}
}
