using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {

	public Texture2D pauseImage;
	public Texture2D playImage;

	private GameController gameController;

	public float sizeButton;

	private bool isPaused;

	// Use this for initialization
	void Start () {
		gameController = FindObjectOfType(typeof(GameController)) as GameController;
	}
	
	void OnGUI(){
		if(gameController.GetCurrentState() == GameStates.INGAME){
			if(!isPaused){
				if(GUI.Button(new Rect(0,0,sizeButton, sizeButton), pauseImage, GUIStyle.none)){
					isPaused = true;
					Time.timeScale = 0;
				}
			}
			else{
				if(GUI.Button(new Rect(0,0,sizeButton, sizeButton), playImage, GUIStyle.none)){
					isPaused = false;
					Time.timeScale = 1;
				}
			}
		}
		
	}

	public bool IsPaused(){
		return isPaused;
	}
}
