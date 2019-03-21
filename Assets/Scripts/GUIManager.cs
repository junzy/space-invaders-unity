using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour 
{
	//Controls all GUI related features of the game
	public GameObject ScoreGUI, GameOverPanel;
	public GameObject YourScore, HighScore;

	void Start () 
	{
		PlayerPrefs.SetInt("Score",0);//Initialize the game score to 0
	}

	void Update () 
	{
		ScoreGUI.GetComponent<Text>().text = "SCORE "+PlayerPrefs.GetInt("Score",0); //Score GUI updation
	}

	public void GameOver()
	{
		Time.timeScale=0;// game is paused
		GameOverPanel.gameObject.SetActive(true);//gameover screen is brought up
		YourScore.GetComponent<Text>().text= "Your Score " + PlayerPrefs.GetInt("Score",0);
		HighScore.GetComponent<Text>().text= "HighScore " + PlayerPrefs.GetInt("Highscore",0);
	}

	public void Reload()
	{
		Application.LoadLevel("GameScene");
		GameOverPanel.gameObject.SetActive(false);
		Time.timeScale=1;
	}

	public void Quit()
	{
		Application.Quit();
	}

}
