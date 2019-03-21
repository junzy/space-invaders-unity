using UnityEngine;
using System.Collections;

public class MysteryAI : MonoBehaviour 
{
	/*Controls behaviour of the mystery ship*/
	public float MoveSpeed;
	void Update () 
	{
		//Movement speed of the ship
		gameObject.rigidbody2D.velocity = new Vector2(MoveSpeed*-1,0);
	}

	//Its mystery time!

}
