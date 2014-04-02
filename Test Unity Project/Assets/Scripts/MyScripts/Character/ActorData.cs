using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ActorData
{
	public enum StatType {
		STRENGTH = 0,
		INTELLIGENCE = 1,
		AGILITY = 2,
		NUM_STATS = 3
	};
	
	public StatData[] stats;
	public StatData health;
	public string name;
	public float x,y;
	
	public ActorData()
	{
		name = "Default Actor Name";
		
		stats = new StatData[(int)StatType.NUM_STATS];
		
		stats[(int)StatType.STRENGTH] = new StatData(1, 10);
		stats[(int)StatType.INTELLIGENCE] = new StatData(1, 10);
		stats[(int)StatType.AGILITY] = new StatData(1, 10);
		
		health = new StatData(0,10);
	}
	
	public bool IsAlive()
	{
		return health.current > 0;
	}
}

