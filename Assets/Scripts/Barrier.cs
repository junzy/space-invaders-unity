using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour 
{
	//Controls the state of the barriers on being damaged
	private int State;
	SpriteRenderer WallRender;
	public Sprite[] WallStates;// Contains the destroyed wall sprites
	void Start () 
	{
		State=1;
		//1-4 valid. 5 is destroyed.
		WallRender = gameObject.GetComponent<SpriteRenderer>();
	}

	void OnCollisionEnter2D(Collision2D Target)
	{
		if(Target.gameObject.tag=="Bullet" || Target.gameObject.tag=="PlayerBullet")
		{
			if(State==4)
				Destroy(this.gameObject);
			WallRender.sprite = WallStates[State++];
		}
	}
}
