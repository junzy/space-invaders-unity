using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour 
{

	/*Controls Cannon Move Speed and constraints, along with gameover scenes
	 * */



	public GameObject Bullet, GameOverPanel;
	public float MoveSpeed;
	public static bool CanShoot;
	float TempTimer;
	public static int Lives;
	public Sprite Destroyed;
	public CanvasRenderer[] LivesImage;
	Sprite OriginalState;
	private bool Alive;
	public AudioClip Lazer, Death;
	void Start()
	{
		//TempTimer=ShootInterval;
		CanShoot=true;
		Lives=3;
		Alive=true;
	
	}


	void Update () 
	{
		/* Shooting Controls
		 */
		TempTimer-=Time.deltaTime;
		if(Input.GetKeyDown("space") && CanShoot)
		{
			Instantiate(Bullet, new Vector2(this.gameObject.transform.position.x,this.gameObject.transform.position.y+0.8f), Quaternion.identity);
			audio.PlayOneShot(Lazer);
			CanShoot=false;
		}

		/*Movement controls and constraints*/

		if(Alive==true && Input.GetKey("left") && this.gameObject.transform.position.x>= GameObject.Find("LeftBoundary").GetComponent<Transform>().position.x)
		{
			this.gameObject.transform.position = new Vector3(Mathf.Lerp(transform.position.x,transform.position.x-1.0f,MoveSpeed), transform.position.y);
		}
		else if(Alive==true && Input.GetKey("right") && this.gameObject.transform.position.x<= GameObject.Find("RightBoundary").GetComponent<Transform>().position.x)
		{
			this.gameObject.transform.position = new Vector3(Mathf.Lerp(transform.position.x,transform.position.x+1.0f,MoveSpeed) , transform.position.y);
		}

		if(Alive==false)
		{
			if(Input.GetKeyDown("space"))
			{
				gameObject.GetComponent<SpriteRenderer>().sprite= OriginalState;
				Alive=true;
			}
		}
	}
	/*Destruction is called on enemy bullet impact*/
	public void Destruction()
	{
		audio.PlayOneShot(Death);
		OriginalState = gameObject.GetComponent<SpriteRenderer>().sprite;
		LivesImage[Lives-1].gameObject.SetActive(false);
		Lives--;
		gameObject.GetComponent<SpriteRenderer>().sprite= Destroyed;
		Alive=false;
		/*Calling GameOver Screen*/
		if(Lives==0)
		{
			GameObject.Find("GameManager").GetComponent<GUIManager>().GameOver();
		}
	}
	/*End game Condition*/
	void OnCollisionEnter2D(Collision2D Target)
	{
		if(Target.gameObject.tag=="Enemy")
		{
			Alive=false;
			GameObject.Find("GameManager").GetComponent<GUIManager>().GameOver();
		}
	}
}
