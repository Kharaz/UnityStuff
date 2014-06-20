using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenPerlinTerrain : MonoBehaviour {
	
	public static int size = 10;
	public int position_offset = 10;
	
	public int seed = 1;
	public Vector2 sampleStart;
	
	public int renderRadius = 5;
	
	public GameObject[,] planes = new GameObject[size,size];
	public List<GameObject> Planes = new List<GameObject>();

	public float scale = .01f;
	int power = 40;

	GameObject Player;
	GameObject world;	
	
	// Use this for initialization
	void Start () {
		
		world = GameObject.Find("worldManager");
	
		Player = GameObject.Find ("Character");
	
		Vector2 sampleStart = new Vector2(seed, seed);
		//Vector2 sampleStart = new Vector2(Random.Range(0f, 1000f), Random.Range(0f, 1000f));
		/*
		for(int x = 0; x < size; x++){
			for(int y = 0; y < size; y++){
				planes[x,y] = GameObject.CreatePrimitive(PrimitiveType.Plane);
				planes[x,y].name = string.Format("plane{0}{1}",x,y);
				MeshFilter mf = planes[x,y].GetComponent<MeshFilter>();
				MeshCollider mc = planes[x,y].GetComponent<MeshCollider>();
				
				//fetch new sample and make noise from it
				Vector2 v2Sample = new Vector2(scale *(sampleStart.x + x*10), scale * (sampleStart.y + y*10));
				Vector3[] verts = MakeNoise(mf, v2Sample);
				
				mf.mesh.vertices = verts;
				mf.mesh.RecalculateBounds();
				mf.mesh.RecalculateNormals();
				mc.sharedMesh = mf.sharedMesh;
				
				planes[x,y].transform.position = new Vector3(position_offset + x*10,0, position_offset + y*10);
				planes[x,y].transform.parent = transform;
			}
		}
		*/
	}
	
	Vector3[] MakeNoise(MeshFilter mf, Vector2 v2Sample) {
		Vector3[] vertices = mf.mesh.vertices;
		for(int i = 0; i < vertices.Length; i++){
			float xCoord = v2Sample.x + vertices[i].x * scale;
			float yCoord = v2Sample.y + vertices[i].z * scale;
			vertices[i].y = (Mathf.PerlinNoise(xCoord,yCoord)-.5f)*power;
		}
		return vertices;
	}
	
	
	public void NewPlane(Vector3 pos, Vector2 v2Sample){
		sampleStart = new Vector2(seed, seed);

		GameObject tempPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		
		//tempPlane.name = string.Format("plane{0}",planeList.Count-1);
		tempPlane.name = "TempPlane";
		MeshFilter mf = tempPlane.GetComponent<MeshFilter>();
		MeshCollider mc = tempPlane.GetComponent<MeshCollider>();
		
		
		Vector3[] verts = MakeNoise(mf, v2Sample);
		
		mf.mesh.vertices = verts;	
		mf.mesh.RecalculateBounds();
		mf.mesh.RecalculateNormals();
		mc.sharedMesh = mf.sharedMesh;
		
		tempPlane.transform.position = pos;
		//tempPlane.transform.parent = world.transform;
		Planes.Add(tempPlane);
	}
	
	
	public void FlatPlane(Vector3 pos){
		GameObject tempPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		tempPlane.transform.position = pos;
		Planes.Add(tempPlane);
	}
	
	public GameObject GetPlaneAt(Vector3 pos){
		for(int i = 0; i < Planes.Count; i++){
			if(Planes[i].transform.position == pos){
				return Planes[i];
			}
		}
		return null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
