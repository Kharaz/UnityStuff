using UnityEngine;
using System.Collections;

public class ActorBehavior : MonoBehaviour {	
	[SerializeField] public ActorData data;
	[SerializeField] public ActorInventory inventory = new ActorInventory();
	
	Transform cam;
	
	public Transform[] alignPoints = new Transform[2];
	
	
	public void Start(){
		if(transform.name == "Character"){
			cam = this.transform.GetChild(0);
			Debug.Log (cam);
			Debug.Log(cam.forward);
		}
	}
	
	public void Update()
	{
		if (Time.frameCount % 60 == 0) {
			//Debug.Log ("Am I alive? " + data.IsAlive());
			//Debug.Log (string.Format("I am at {0},{1}", data.x, data.y));
			if(!IsAlive()){
				Death();
			}
		}
		
		//this.transform.position = new Vector3(data.x, 0.0f, data.y);
	}
	
	public void Death(){
		Destroy(this.transform.gameObject);
	}
	
	public bool IsAlive()
	{
		return data.IsAlive();
	}
	
	public long GetStat(ActorData.StatType type) {
		if (data != null)
			return data.stats[(int)type].current;
		return 0;
	}
	
	public void Move(float newX, float newY)
	{
		data.x = newX;
		data.y = newY;
	}
	
	public Vector2 GetPosition() {
		return new Vector2(data.x, data.y);
	}
	
	public void PickUpItem(item Item) {
		inventory.AddItem(Item);
		Item.Despawn();
	}
	
	public void EquipItem(item Item, int slot){
		Item.RespawnEquip(slot);
		
		for(int i = 0; i < transform.childCount; i++){
			Debug.Log (transform.GetChild(i));
			if(transform.GetChild (i).name == "AttachPoints"){
				int numPoints = transform.GetChild (i).childCount;
				
				for(int x = 0; x < numPoints; x++){
					if(transform.GetChild (i).GetChild(x).name == "Point"+slot){
							Transform trans = transform.GetChild(i).GetChild(x).transform;
							Debug.Log("Transform name: " + trans.name);
							Debug.Log("Point"+slot);
							
						
							Transform attachPoint = trans;
							Item.transform.position = attachPoint.position;
							//Item.transform.rotation = Quaternion.LookRotation(trans.GetChild(0).transform.position - trans.position);
							//Item.transform.rotation = attachPoint.rotation;
							
							if(slot == 1 || slot == 2){
								//Item.transform.LookAt(alignPoints[slot-1].transform.position);
								Item.transform.rotation = cam.rotation;
							} else {
								Item.transform.LookAt(trans.GetChild(0).transform.position);
							}
					}
				}
					
			}
				
			
			if(transform.GetChild(i).name == "Camera") Item.transform.parent = transform.GetChild(i);
		}
		
		
		//Item.transform.position = GameObject.Find("AttachPoints").transform.GetChild(slot).position;
		//Item.transform.rotation = this.transform.GetChild(0).rotation;
		//Item.transform.parent = GameObject.Find("Camera").transform;
		
		Item.gameObject.rigidbody.isKinematic = true;
		Item.gameObject.collider.enabled = false;
		//Destroy (Item.gameObject.rigidbody);
		//todo: make this just disable the rigidbody
		//todo: don't use gameobject.find, figure out how it selects which child is which
	}
	
	public void UnequipItem(item Item){
		Item.transform.parent = null;
		Item.gameObject.rigidbody.isKinematic = false;
		Item.gameObject.collider.enabled = true;
		Item.Despawn();
	}
	
	public void DropItem(int index){
		item thing = inventory.GetItemAt(index);
		thing.Respawn(this.transform.position + (cam.forward*2) + cam.up);
		thing.transform.rotation = this.transform.rotation;
		inventory.RemoveItem(index);
	}
	
	public void TakeDamage(int damage){
		Debug.Log ("taking damage");
		if(this.transform.GetComponent<AudioSource>()){
			audio.Play ();
		}
		data.health.current -= damage;
	}
}
