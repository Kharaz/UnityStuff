using UnityEngine;
using System.Collections;

public class PerlinNoisePlane : MonoBehaviour
{
	public float scale = 1.0f;
	public float power = 3.0f;
	private Vector2 v2Sample = Vector2.zero;

	// Use this for initialization
	void MakeNoise() {
		MeshFilter mf = GetComponent<MeshFilter>();
		MeshCollider mc = GetComponent<MeshCollider>();
		
		Vector3[] vertices = mf.mesh.vertices;
		for(int i = 0; i < vertices.Length; i++){
			float xCoord = v2Sample.x + vertices[i].x * scale;
			float yCoord = v2Sample.y + vertices[i].z * scale;
			vertices[i].y = (Mathf.PerlinNoise(xCoord,yCoord)-.5f)*power;
		}
		
		mf.mesh.vertices = vertices;
		mf.mesh.RecalculateBounds();
		mf.mesh.RecalculateNormals();
		
		mc.sharedMesh = mf.sharedMesh;
	}
	
	void Start ()
	{
		v2Sample = new Vector2(Random.Range(0f,100f),Random.Range(0f,100f));
		MakeNoise();
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		/*
		if(Input.GetKey (KeyCode.Space)){
			v2Sample = new Vector2(Random.Range(0f,100f),Random.Range(0f,100f));
			MakeNoise ();
		}
		*/
	}
}

