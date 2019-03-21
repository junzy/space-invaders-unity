using UnityEngine;
using System.Collections;

public class EnemyBulletController : MonoBehaviour 
{

	/*Controls interaction of enemy bullets with environment and other gameobjects*/

	public int Bounces;
	void Start()
	{
		Bounces=3;
	}

	void OnCollisionEnter2D(Collision2D Target)
	{
		if(Target.gameObject.tag=="Player")
		{
			//Lose Life
			GameObject.Find("cannon").GetComponent<CannonController>().Destruction();
			Destroy(this.gameObject);
		}
		if(Target.gameObject.tag=="PlayerBullet")
		{
			Destroy(Target.gameObject);
			Destroy(this.gameObject);
		}
		Bounces--;
		if(Bounces<=0)
		{
			Destroy(this.gameObject);
		}
	}
}
