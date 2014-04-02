using UnityEngine;
using System.Collections;

public class ActorBehavior : MonoBehaviour {	
	[SerializeField] public ActorData data;
	
	public void Awake()
	{
	}
	
	public void Update()
	{
		if (Time.frameCount % 60 == 0) {
			Debug.Log ("Am I alive? " + data.IsAlive());
			Debug.Log (string.Format("I am at {0},{1}", data.x, data.y));
		}
		
		this.transform.position = new Vector3(data.x, 0.0f, data.y);
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
}
