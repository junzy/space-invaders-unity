using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour 
{
	/*MainAI controller for Enemy Wave*/

	public bool MoveRight=true;
	public float MoveInterval, MoveSize, SpeedIncrease;
	public GameObject LeftBoundary, RightBoundary, EnemyA,EnemyB, EnemyC, Bullet;
	public GameObject[,] objects;
	public int Rows, Columns;
	GameObject EnemyToSpawn;
	float TimerTemp, MoveIntervalTemp;
	int LeftLimit, RightLimit;

	void Start () 
	{
		MoveIntervalTemp=MoveInterval;//SEtting up for later stages in the game.
		objects = new GameObject[Rows,Columns];
		EnemyToSpawn=EnemyA;
		Spawn();
		TimerTemp=MoveIntervalTemp;
	}

	void Spawn()
	{
		for(int i=0;i<Rows;i++)
		{
			for(int j=0;j<Columns;j++)
			{
				if(i==Rows-2 || i==Rows-3)
					EnemyToSpawn=EnemyB;
				if(i==Rows-1)
					EnemyToSpawn=EnemyC;
				objects[i,j]= (GameObject)Instantiate(EnemyToSpawn, new Vector2(this.gameObject.transform.position.x+j*1.2f,this.gameObject.transform.position.y+i*0.8f),Quaternion.identity);
			}
		}
	}

	void Update () 
	{
		//if its time to move & shoot
		if(TimerTemp<=0)
		{
			Move();//move the aliens laterally
			TimerTemp=MoveIntervalTemp;
			Shoot();
		}
		TimerTemp-=Time.deltaTime;
		CheckGameOver();//Gameover check


	}

	void CheckGameOver(){

		/*Spawns a new wave of aliens if the current set is done*/

		int Flag=0;
		for(int i=0;i<Rows;i++)
		{
			for(int j=0;j<Columns;j++)
			{
				if(objects[i,j].activeSelf==true)
				{
					Flag=1;
				}

			}
		}
		if(Flag==0)//if all aliens dead
		{
			Spawn();
			MoveIntervalTemp=MoveInterval;
			if(SpeedIncrease<=0.03)
				SpeedIncrease+=0.005f;
			Flag=1;
		}
	}

	public void UpdateSpeed()
	{
		//Game Speed Changes
		MoveIntervalTemp-= SpeedIncrease;
	}

	void MoveDown()
	{
		//Moves all the aliens one level down
		for(int i=0;i<Rows;i++)
		{
			for(int j=0;j<Columns;j++)
			{
				objects[i,j].gameObject.transform.position = new Vector2(objects[i,j].gameObject.transform.position.x, objects[i,j].gameObject.transform.position.y-0.25f);
			}
		}
	}

	void Shoot()
	{
		//Chooses a random column and the instantiates a bullet at the lowest row alive alien.

		int RandomCol = Random.Range(0,Columns),i=0;
		if(objects[Rows-1,RandomCol].gameObject.activeSelf==true)
		//Debug.Log(RandomCol);
		for(i=0;i<Rows;i++)
		{
			if(objects[i,RandomCol].gameObject.activeSelf==true)
			{
				Instantiate(Bullet, new Vector2(objects[i,RandomCol].gameObject.transform.position.x, objects[i,RandomCol].gameObject.transform.position.y-0.4f), Quaternion.identity);
				break;
			}
		}
	}
	
	void Move()
	{
		//Controls the lateral motion of the aliens
		if(MoveRight)
		{
			for(int i=0;i<Rows;i++)
			{
				for(int j=0;j<Columns;j++)
				{
					objects[i,j].gameObject.transform.position = new Vector2(objects[i,j].gameObject.transform.position.x+MoveSize, objects[i,j].gameObject.transform.position.y);

				}
			}
		}
		else
		{
			for(int i=0;i<Rows;i++)
			{
				for(int j=0;j<Columns;j++)
				{
					objects[i,j].gameObject.transform.position = new Vector2(objects[i,j].gameObject.transform.position.x-MoveSize, objects[i,j].gameObject.transform.position.y);
				}
			}
		}
		SetLimit();
		CheckCrash();
	}

	void SetLimit()
	{
		//Sets the left and right limits for the aliens lateral movement
		for(int j=0;j<Columns;j++)
		{
			if(objects[Rows-1,j].gameObject.activeSelf== true)
			{
				LeftLimit=j;
				Debug.Log(LeftLimit);
				break;
			}
		}
		for(int j=Columns-1;j>=0;j--)
		{
			if(objects[Rows-1,j].gameObject.activeSelf== true)
			{
				RightLimit=j;
				Debug.Log(RightLimit);
				break;
			}
		}
	}

	void CheckCrash()
	{
		//checks if the aliens direction of movement has to be changed or not
		if(objects[3,LeftLimit].transform.position.x - MoveSize<= LeftBoundary.transform.position.x || objects[3,RightLimit].transform.position.x + MoveSize >= RightBoundary.transform.position.x)
		{
			MoveDown();
			MoveRight=!MoveRight;
		}
	}






	
}
