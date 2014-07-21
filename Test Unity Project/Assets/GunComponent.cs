using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GunComponent : MonoBehaviour {
	
	Dictionary<int,GameObject> attachNodes = new Dictionary<int,GameObject>():
	
	// Use this for initialization
	void Start () {
	
		//iterate through nodes and add to attachnode dict
		for(int i = 0; i < this.gameObject.transform.childCount; i++){
			if(this.gameObject.transform.GetChild(i).name == "Node"){
				attachNodes.Add(i, this.gameObject.transform.GetChild(i));
			}
		}
	
	}
	
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
