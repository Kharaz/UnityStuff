using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class InteriorGenerator : MonoBehaviour {
	
	
	Dictionary<int, GameObject> partsList = new Dictionary<int, GameObject>();
	
	int snap = 2;
	
	void Start () {
		partsList.Add(0,null);
		partsList.Add(1,Resources.LoadAssetAtPath("Assets/Prefabs/Custom Prefabs/Interior/Modular/floor_edge2.prefab",typeof(GameObject)) as GameObject);
		partsList.Add(2,Resources.LoadAssetAtPath("Assets/Prefabs/Custom Prefabs/Interior/Modular/FloorCorner1.prefab",typeof(GameObject)) as GameObject);
		partsList.Add(3,Resources.LoadAssetAtPath("Assets/Prefabs/Custom Prefabs/Interior/Modular/FloorTile1.prefab",typeof(GameObject)) as GameObject);
		partsList.Add(4,Resources.LoadAssetAtPath("Assets/Prefabs/Custom Prefabs/Interior/Modular/Hall2.prefab",typeof(GameObject)) as GameObject);
		
		//GameObject newThing = (GameObject)Instantiate(partsList[1]);
		//newThing.transform.position = new Vector3(0,0,0);
		
		GenLevel("Assets/Textfiles/leveltest.txt");
	}
	
	void GenLevel(string file){
		string line;
		int col = 0;
		
		StreamReader reader = new StreamReader(file, Encoding.Default);
		
		using(reader){
			do{
				line = reader.ReadLine();
				col++;
				
				if( line != null){
					string[] tiles = line.Split(',');
					
					for(int i = 0; i < tiles.Length; i++){
						int tile = int.Parse(tiles[i]);
						
						try
						{
							GameObject tempObj = (GameObject)Instantiate (partsList[tile]);
							tempObj.transform.position = this.transform.position + new Vector3(col*snap, 0, i*snap);
							tempObj.transform.parent = this.transform;
							
							switch(tile){
								case(1):
									tempObj.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
									break;
								default:
									break;
							}
						
						} catch {
						
						}
					}
					
				}
			} while (line != null);
			
			reader.Close ();
		}
	}
	
}
