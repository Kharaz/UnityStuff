using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour
{
	public static World currentWorld;
	public int renderDist = 10;
	
	public int planeSize = 10;
	public int seed = 1;
	
	public GenPerlinTerrain PlaneFab;
	
	GameObject player;
	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find("Character");
		PlaneFab = new GenPerlinTerrain();
		currentWorld = this;
		
		if(seed == 0){
			seed = Random.Range(0,int.MaxValue);
		}	
	}
	
	// Update is called once per frame
	void Update ()
	{
		float playerX = player.transform.position.x;
		float playerZ = player.transform.position.z;
	
		for(float x = playerX - renderDist; x < playerX + renderDist; x+=planeSize){
			for(float z = playerZ - renderDist; z < playerZ + renderDist; z+=planeSize){
				x = (int)(x/10)*10;
				z = (int)(z/10)*10;
				
				Vector2 v2Sample = new Vector2(PlaneFab.scale *(PlaneFab.sampleStart.x + x), PlaneFab.scale * (PlaneFab.sampleStart.y + z));
				
				Vector3 pos = new Vector3(x,0,z);
				
				if(PlaneFab.GetPlaneAt(pos) == null){
					PlaneFab.NewPlane(pos, v2Sample);
					PlaneFab.Planes[PlaneFab.Planes.Count - 1].transform.parent = transform;
				}
				
			}
		}
	}
	
}

