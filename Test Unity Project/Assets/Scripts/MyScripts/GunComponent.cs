using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GunComponent : MonoBehaviour {
	
	public Dictionary<int,GameObject> attachNodes = new Dictionary<int,GameObject>();
	
	// Use this for initialization
	void Start () {
	
		//iterate through nodes and add to attachnode dict
		int counter = 0;
		foreach(Transform child in transform){
			attachNodes.Add(counter, child.gameObject);
			counter++;
		}
	
	}

	public void Attach(GameObject node){
		GameObject newObj;
		//newObj = Instantiate(this, node.transform.position, node.transform.rotation) as GameObject;
		newObj = Instantiate(this, node.transform.position + node.transform.right, node.transform.rotation) as GameObject;
	}
	
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
