using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GunComponent : MonoBehaviour {
	
	public Dictionary<int,GameObject> Nodes = new Dictionary<int,GameObject>();
	
	// Use this for initialization
	void Start () {
	
		//iterate through nodes and add to available node dict
		int counter = 1;
		foreach(Transform child in transform){
			Nodes.Add(counter, child.gameObject);
			counter++;
		}
	}

	public void Attach(int node, GunComponent component){
		/*
		this.transform.parent = node.transform;
		this.transform.position = node.transform.position + (-node.transform.parent.transform.position + node.transform.position);
		*/
	}
	
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
