using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatData {
	public long min;
	public long max;
	public long current;

	public StatData(long curr) {
		this.min = 0;
		this.max = -1;
		this.current = curr;
	}
	
	public StatData(long min, long max) {
		this.min = min;
		this.max = max;
		this.current = max;
	}
}
