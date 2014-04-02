using UnityEngine;
using System.Collections;

public class GenPerlinTerrain : MonoBehaviour {
	
	public static int size = 10;
	public int position_offset = 10;
	public GameObject[,] planes = new GameObject[size,size];
	
	float scale = .01f;
	int power = 40;
	
	// Use this for initialization
	void Start () {
		Vector2 sampleStart = new Vector2(Random.Range(0f,1000f), Random.Range (0f,1000f));
		for(int x = 0; x < size; x++){
			for(int y = 0; y < size; y++){
				planes[x,y] = GameObject.CreatePrimitive(PrimitiveType.Plane);
				planes[x,y].name = string.Format("plane{0}{1}",x,y);
				MeshFilter mf = planes[x,y].GetComponent<MeshFilter>();
				MeshCollider mc = planes[x,y].GetComponent<MeshCollider>();
				
				Vector2 v2Sample = new Vector2(scale *(sampleStart.x + x*size), scale * (sampleStart.y + y*size));
				Vector3[] verts = MakeNoise(mf, v2Sample);
				
				mf.mesh.vertices = verts;
				mf.mesh.RecalculateBounds();
				mf.mesh.RecalculateNormals();
				mc.sharedMesh = mf.sharedMesh;
				
				planes[x,y].transform.position = new Vector3(position_offset + x*size,0, position_offset + y*size);
				planes[x,y].transform.parent = transform;
			}
		}
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
	// Update is called once per frame
	void Update () {
	
	}
}
