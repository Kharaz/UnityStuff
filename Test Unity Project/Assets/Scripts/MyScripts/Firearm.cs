using UnityEngine;
using System.Collections;


public class Firearm : item {
	public Transform firePoint;
	public int damage = 2;
	
	public int currentAmmo;
	public int maxAmmo;
	
	public bool isAuto = false;
	bool hasShot = false;
	
	public int reloadTime;
	float timeWaited;
	bool reloading;
	
	float timeSinceLastShot = 0;
	public float shotCooldown;
	
	AudioClip bang; 
	AudioClip click;
	
	bool canShoot(){
		if(isAuto && timeSinceLastShot > shotCooldown){
			return true;
		}
		if(!reloading && currentAmmo > 0 && timeSinceLastShot > shotCooldown){
			return true;
		}
		return false;
	}
	
	public override void UseItem() {
		//audio.clip = null;
		if(!reloading && currentAmmo > 0 && timeSinceLastShot > shotCooldown && !hasShot){
			hasShot = true;
			audio.clip = bang;
			timeSinceLastShot = 0;
		
			currentAmmo -= 1;
			RaycastHit hit;
			Ray shootRay = new Ray(firePoint.position, this.transform.forward);
			
			if(Physics.Raycast(shootRay, out hit)){
				if(hit.transform.GetComponent<ActorBehavior>()){
					hit.transform.GetComponent<ActorBehavior>().TakeDamage(damage);
				}
				
				if(hit.transform.GetComponent<Rigidbody>()){
					hit.transform.rigidbody.AddForce(this.transform.forward*damage*100);
				}
				
				firePoint.GetComponent<LineRenderer>().SetPosition(1,hit.point);
				firePoint.GetComponent<LineRenderer>().SetPosition(0,firePoint.position);
				Debug.Log(hit.transform.name);
			}
			
			Debug.DrawRay(firePoint.position, this.transform.forward, Color.red, 0.25f);
			audio.Play();
		} else if (reloading || currentAmmo == 0) {
			audio.clip = click;
			//Reload ();
			audio.Play();
		}
	}
	
	public void Start(){
		bang = Resources.Load("bang") as AudioClip;
		click = Resources.Load("click") as AudioClip;
		name = this.transform.name;
	}
	
	public void Update(){
		timeSinceLastShot += Time.deltaTime;
		timeSinceLastShot = Mathf.Clamp(timeSinceLastShot, 0, 40);
		
		if(timeSinceLastShot > shotCooldown && isAuto){
			hasShot = false;
		}
		
		if(!isAuto){
			hasShot = Input.GetMouseButton(0);
		}
	
		if(Time.frameCount % 60 == 0){
			firePoint.GetComponent<LineRenderer>().SetPosition(0,Vector3.zero);
			firePoint.GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
		}
		
		if(reloading){
			timeWaited += Time.deltaTime;
			if(timeWaited > reloadTime){
				reloading = false;
				currentAmmo = maxAmmo;
				timeWaited = 0;
			}
		} 
	}
	
	public void Reload() {
		reloading = true;
		
	}
}

