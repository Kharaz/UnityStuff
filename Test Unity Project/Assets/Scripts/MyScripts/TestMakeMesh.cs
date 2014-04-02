using UnityEngine;
using System.Collections;

public class TestMakeMesh : MonoBehaviour
{

	// Use this for initialization
	float size = 1;
	
	void Awake() {
		for(int x = 0; x < size; x++){
			for(int y = 0; y < size; y++){
				Mesh newMesh = CreateMesh();
				
			}
		}
	}
	
	Mesh CreateMesh() {
		Mesh m = new Mesh();
		MeshFilter f = GetComponent<MeshFilter>();
		MeshRenderer r = GetComponent<MeshRenderer>();
		
		m.name = "test_mesh";		
		Vector3[] vertices = new Vector3[4];
	
		vertices[0] = new Vector3(0,0,0);
		vertices[1] = new Vector3(1,0,0);
		vertices[2] = new Vector3(0,0,-1);
		vertices[3] = new Vector3(1,0,-1);
		
		m.vertices = vertices;
		
		int[] triangles = new int[6]{0,1,2,3,2,1};
		m.triangles = triangles;
		
		Vector2[] uvs = new Vector2[4];
		uvs[0] = new Vector2(0,0);
		uvs[1] = new Vector2(1,0);
		uvs[2] = new Vector2(0,1);
		uvs[3] = new Vector2(1,1);
		
		m.uv = uvs;
	
		m.RecalculateNormals();
		
		f.mesh = m;
		r.material = new Material(Shader.Find("Diffuse"));
		
		return m;
	}
	
	
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

