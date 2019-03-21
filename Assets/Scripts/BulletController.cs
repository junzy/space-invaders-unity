using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour 
{


	/*Controls Bullet Scoring and movement*/

	public float ProjectileSpeed;
	public AudioClip Boom;
	string ScoreString = "Score";
	int Score;
	int[] MysteryScore = new int[] {50,100,150,200};

	void Start ()
	{
		PlayerPrefs.Save();
	}
	

	void Update ()
	{
		this.gameObject.rigidbody2D.velocity= new Vector2(0, ProjectileSpeed);
	}

	void OnCollisionEnter2D(Collision2D Target)
	{
		if(Target.gameObject.tag=="Enemy")
		{
			Target.gameObject.SetActive(false);
			audio.PlayOneShot(Boom);
			AudioSource.PlayClipAtPoint(Boom, this.gameObject.transform.position);
			Score=PlayerPrefs.GetInt(ScoreString);

			/*Scoring based on enemy type
			 D is the mystery ship
			 Highscore updation*/
			if(Target.gameObject.GetComponent<EnemyProperties>().EnemyType=="A")
				Score+=10;
			if(Target.gameObject.GetComponent<EnemyProperties>().EnemyType=="B")
				Score+=20;
			if(Target.gameObject.GetComponent<EnemyProperties>().EnemyType=="C")
				Score+=40;
			if(Target.gameObject.GetComponent<EnemyProperties>().EnemyType=="D")
				Score+= MysteryScore[Random.Range(0,4)];

			PlayerPrefs.SetInt(ScoreString, Score);
			//Updating HighScore
			if(Score>PlayerPrefs.GetInt("Highscore",0))
			{

				PlayerPrefs.SetInt("Highscore",Score);	
				PlayerPrefs.SetInt("Score",Score);	
			}


			//Updating the number of enemies shot, to change game speed.
			//Debug.Log(Score+ "score "+ Target.gameObject.name);
			PlayerPrefs.Save();
			GameObject.Find("EnemyManager").GetComponent<EnemyAI>().UpdateSpeed();
		}
		Destroy(this.gameObject);
		CannonController.CanShoot=true;
	}
}
