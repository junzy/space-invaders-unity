using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Gameover : MonoBehaviour {


	public GameObject YourScore, HighScore;
	// Use this for initialization
	void Start () 
	{
		YourScore.gameObject.GetComponent<Text>().text= "Your Score " + PlayerPrefs.GetInt("Score",0);
		HighScore.gameObject.GetComponent<Text>().text= "HighScore " + PlayerPrefs.GetInt("Highscore",0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
