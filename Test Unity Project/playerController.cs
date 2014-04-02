using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour
{
	float xvel;
	float yvel;
	float maxvel;
	
	float acc;
	
	Component Base = GetComponent("baseChar.cs");
	
	// Use this for initialization
	void Start ()
	{
		//Base = GetComponent("baseChar.cs");
		xvel = 0;
		yvel = 0;
		acc = 0.5f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Base.IsAlive()){
			//allow for movement and stuff
			/* key movement */
			if(Input.GetKeyDown(KeyCode.W)){
				if(Mathf.Abs(xvel) >= maxvel){
					xvel = maxvel;
				} else {
					xvel += acc;
				}
			}
			if(Input.GetKeyDown (KeyCode.S)){
				if(Mathf.Abs(xvel) >= maxvel){
					xvel = -maxvel;
				} else {
					xvel -= acc;
				}
			}
			
			if(Input.GetKeyDown(KeyCode.A)){
				if(Mathf.Abs(xvel) >= maxvel){
					yvel = maxvel;
				} else {
					yvel += acc;
				}
			}
			if(Input.GetKeyDown (KeyCode.D)){
				if(Mathf.Abs(xvel) >= maxvel){
					yvel = -maxvel;
				} else {
					yvel -= acc;
				}
			}
			/* end key movement */
			
			
			//apply new velocities
			transform.position.x += xvel * Time.deltaTime;
			transform.position.y += yvel * Time.deltaTime; 
		}
	}
}

