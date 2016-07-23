using UnityEngine;
using System.Collections;

public enum GameStates{
	START,
	WAITGAME,
	MAINMENU,
	TUTORIAL,
	INGAME,
	GAMEOVER,
	RANKING,
}

public class GameController : MonoBehaviour {
	

	public Transform player;

	private Vector3 startPositionPlayer;

	private GameStates currentState = GameStates.START;

	public TextMesh numberScore;
	public TextMesh shadowScore;

	public float timeToRestart;
	private float currentTimeToRestart;

	private int score;

	public GameObject mainMenu;
	public GameObject tutorial;

	private GameOverController gameOverController;

	private bool canPlay;
	private float currentTimeToPlayAgain;

	// Use this for initialization
	void Start () {
		startPositionPlayer = player.position;

		gameOverController = FindObjectOfType(typeof(GameOverController)) as GameOverController;
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState){
		case GameStates.START:{
			player.position = startPositionPlayer;
			currentState = GameStates.MAINMENU;
			mainMenu.SetActive(true);
			player.gameObject.SetActive(false);

		}
		break;
		case GameStates.MAINMENU:{
			player.position = startPositionPlayer;
						
			
		}
		break;
		case GameStates.TUTORIAL:{

			player.position = startPositionPlayer;

			currentTimeToPlayAgain += Time.deltaTime;

			if(currentTimeToPlayAgain > 0.2f){
				currentTimeToPlayAgain = 0;
				canPlay = true;
			}

			
			
		}
		break;
		case GameStates.WAITGAME:{
			player.position = startPositionPlayer;


		}
		break;
		case GameStates.INGAME:{
			numberScore.text = score.ToString();
			shadowScore.text = score.ToString();
		}
		break;
		case GameStates.GAMEOVER:{

			currentTimeToRestart += Time.deltaTime;

			if(currentTimeToRestart > timeToRestart){
				currentTimeToRestart = 0;
				currentState =  GameStates.RANKING;

				gameOverController.SetGameOver(score);
				canPlay = false;

				SoundController.PlaySound(soundsGame.menu);

			}


		}
		break;
		case GameStates.RANKING:{
			

			numberScore.GetComponent<Renderer>().enabled = false;
			shadowScore.GetComponent<Renderer>().enabled = false;
			
		}
		break;
		}

	
	}

	public void StartGame(){
		currentState =  GameStates.INGAME;
		numberScore.GetComponent<Renderer>().enabled = true;
		shadowScore.GetComponent<Renderer>().enabled = true;
		score = 0;
		gameOverController.HideGameOver();
		tutorial.SetActive(false);
	}

	public GameStates GetCurrentState(){
		return currentState;
	}

	public void CallGameOver(){
		if(currentState != GameStates.GAMEOVER && currentState != GameStates.RANKING){
			SoundController.PlaySound(soundsGame.hit);
			gameOverController.ShowFade();
			currentState =  GameStates.GAMEOVER;
		}



	}

	public void CallTutorial(){

		
		currentState = GameStates.TUTORIAL;
		mainMenu.SetActive(false);
		tutorial.SetActive(true);
		player.gameObject.SetActive(true);
		gameOverController.HideGameOver();
		ResetGame();

		SoundController.PlaySound(soundsGame.menu);
		
		
	}

	public void CallMainMenu(){
		
		
		currentState = GameStates.MAINMENU;
		mainMenu.SetActive(true);
		player.gameObject.SetActive(false);
		gameOverController.HideGameOver();
		ResetGame();

		SoundController.PlaySound(soundsGame.menu);
		
		
	}

	public void ResetGame(){
		player.position = startPositionPlayer;
		player.GetComponent<PlayerBehaviour>().RestartRotation();
		ObstaclesBehaviour[] pipes = FindObjectsOfType(typeof(ObstaclesBehaviour)) as ObstaclesBehaviour[];
		foreach(ObstaclesBehaviour o in pipes){
			o.gameObject.SetActive(false);
		}

		numberScore.GetComponent<Renderer>().enabled = false;
		shadowScore.GetComponent<Renderer>().enabled = false;
		numberScore.text = score.ToString();
		shadowScore.text = score.ToString();
	}

	public void AddScore(){
		score++;
		SoundController.PlaySound(soundsGame.point);
	}

	public bool CanPlay(){
		return canPlay;
	}
	
}
