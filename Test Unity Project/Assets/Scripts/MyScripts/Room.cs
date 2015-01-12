using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class Room : MonoBehaviour
{

	public Dictionary<Vector2, int> blocks = new Dictionary<Vector2,int>();
	public GameObject Floor;
	public GameObject Roof;

	public GameObject CubeRef;
	
	public Material matRef;
	
	public enum Parts {
		Empty, //0
		Cube //1
	};
	
	// 0 = empty
	// 1 = cube
	
	int width = 5;
	int height = 5;
	
	void Start(){
		InitializeRoom();
	}
	
	public void InitializeRoom() {
		CleanupRoom();
		for(int i = 0; i < width; i++){
			for(int j = 0; j < height; j++){
				blocks[new Vector2(i,j)] = 0;
			}
		}
		
		buildFloor();
	}
	
	public void CleanupRoom(){
		for(int i = 0; i < this.gameObject.transform.childCount; i++){
			Destroy (this.gameObject.transform.GetChild(i).gameObject);
		}
		
		Destroy (Floor);
	}
	
	public int GetPartAt(Vector2 position){
		return blocks[position];
	}
	
	public void PlacePart(Vector2 position, int part){
		if(part != 0){
			Debug.Log ("New part at: " +position);
			blocks[position] = part;
			GameObject newPart = (GameObject)Instantiate (CubeRef, new Vector3(position.x-1, .5f ,position.y-1), Quaternion.identity);
			newPart.transform.parent = this.gameObject.transform;
			Vector3 localPos = new Vector3(position.x+.5f, .5f, position.y+.5f);
			Vector3 worldPos = this.transform.TransformPoint(localPos);
			newPart.transform.position = worldPos;
		}
		//newPart.transform.position = newPart.transform.InverseTransformPoint(new Vector3(position.x+.5f, .5f, position.y+.5f));
		//newPart.transform.position = newPart.transform.TransformPoint(new Vector3(position.x, 0, position.y));
	}
	
	public void RemovePart(Vector2 position, GameObject Part){
		blocks[position] = (int)Parts.Empty;
		if(Part){
			Destroy (Part);
		}
	}
	
	public void buildFloor(){
		Floor = MakePlane((width/2)+.5f, (height/2)+.5f);
		//Floor.transform.position = new Vector3((width/4),0,(height/4));
		Floor.transform.parent = this.gameObject.transform;
		Vector3 relativePosition = new Vector3(width/2f,0,height/2f);
		Floor.transform.position = transform.TransformPoint(relativePosition);
	}
	
	public void buildRoof(){ 
		
	}
	
	private GameObject MakePlane(float width, float height){
		GameObject newPlane = new GameObject();
		newPlane.AddComponent<MeshFilter>();
		newPlane.AddComponent<MeshRenderer>();
		newPlane.AddComponent<MeshCollider>();
		
		MeshFilter mf = newPlane.GetComponent<MeshFilter>();
		MeshRenderer mr = newPlane.GetComponent<MeshRenderer>();
		MeshCollider mc = newPlane.GetComponent<MeshCollider>();
		
		Mesh m = new Mesh();
		
		m.vertices = new Vector3[] {
			new Vector3(-width, 0.01f, height),
			new Vector3(width, 0.01f, height),
			new Vector3(width, 0.01f, -height),
			new Vector3(-width, 0.01f, -height)
		};
		
		m.uv = new Vector2[] {
			new Vector2(0,0),
			new Vector2(0,1),
			new Vector2(1,1),
			new Vector2(1,0)
		};
		
		m.triangles = new int[] {0,1,2,0,2,3};
		m.RecalculateNormals();
		m.RecalculateBounds();
		mf.mesh = m;
		mf.sharedMesh = m;
		mc.sharedMesh = m;
		mr.material = matRef;
		
		return newPlane;
	}
	
	string PrintArray() {
		string array = "";
		Debug.Log ("Blocks:");
		for(int i = 4; i >= 0; i--){
			string line = "";
			for(int j = 0; j < 5; j++){
				line += blocks[new Vector2(j,i)];
			}
			line += "\r\n";
			array += line;
		}
		return array;
	}
	
	public void serializeRoom(){
		//todo: somehow make it do json
		//string finalOutput = "";
		
		/*
		for(int i = 0; i < width; i++){
			for(int j = 0; j < height; j++){
				Vector2 pos = new Vector2(i,j);
				finalOutput += blocks[pos].ToString();
			}
			finalOutput+="\r\n";
		}
		*/
		string otherOutput = PrintArray();
		
		//System.IO.File.WriteAllText ("C:/Users/Zaffron/Desktop/saveroom.txt",finalOutput);
		System.IO.File.WriteAllText ("C:/Users/Zaffron/Desktop/saveroom.txt", otherOutput);
	}
	
	public void deserializeRoom(){
		InitializeRoom();
	
		string line;
		int col = 0;
		
		string File = "C:/Users/Zaffron/Desktop/saveroom.txt";
		
		StreamReader reader = new StreamReader(File, System.Text.Encoding.Default);
		
		Debug.Log ("Reading save file:");
		using(reader) {
			do {
				line = reader.ReadLine();
				
				if(line != null){
					
					Debug.Log (line);
					
					char[] tiles = line.ToCharArray();
					
					for( int i = 0; i < tiles.Length; i++){
						int tileType = int.Parse(char.ToString(tiles[i]));
						Debug.Log ("Adding tile: " + tiles[i] + " of type: " + tileType);
						PlacePart (new Vector2(col, i), tileType);
					}
					col++;
				}
			} while (line != null);
		}
		
		reader.Close();
	
	}
}