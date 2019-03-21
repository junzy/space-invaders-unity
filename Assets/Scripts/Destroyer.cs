using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour 
{
	//Destroys escaping bullets and ninjas
	void OnCollisionEnter2D(Collision2D Target)
	{
		Debug.Log(Target.gameObject.name+"Destroyed");
		if(Target.gameObject.tag == "PlayerBullet")
			CannonController.CanShoot=true;
		Destroy(Target.gameObject);
	}
}
