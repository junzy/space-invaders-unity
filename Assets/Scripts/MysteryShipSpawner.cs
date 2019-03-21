using UnityEngine;
using System.Collections;

public class MysteryShipSpawner : MonoBehaviour {

	public GameObject MysteryShip;
	float timer;
	public float MysteryInterval;
	void Start () 
	{
		timer = MysteryInterval;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer-=Time.deltaTime;
		if(timer<=0)
		{
			MysteryTime();
			timer=MysteryInterval;
		}
	}

	void MysteryTime()
	{
		Instantiate(MysteryShip,transform.position,Quaternion.identity);
		
	}
}
