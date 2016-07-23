using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	public Transform mesh;
	public float forceFly;
	private Animator animatorPlayer;

	private float currentTimeToAnim;
	private bool inAnim = true;
	private GameController gameController;
	private PauseController pauseController;

	// Use this for initialization
	void Start () {
		animatorPlayer = mesh.GetComponent<Animator>();
		gameController = FindObjectOfType(typeof(GameController)) as GameController;
		pauseController = FindObjectOfType(typeof(PauseController)) as PauseController;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0) && gameController.GetCurrentState() == GameStates.INGAME && 
		   gameController.GetCurrentState() != GameStates.GAMEOVER && 
		   !pauseController.IsPaused()){

			inAnim = true;

			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1)*forceFly);

			SoundController.PlaySound(soundsGame.wing);

		}
		else if(Input.GetMouseButtonDown(0) && gameController.GetCurrentState() == GameStates.TUTORIAL){
			if(gameController.CanPlay()){
				Restart();
			}
		}

		animatorPlayer.SetBool("callFly", inAnim);

		Vector3 positionPlayer = transform.position;

		if(positionPlayer.y > 5){
			positionPlayer.y = 5;
			transform.position = positionPlayer;
		}

		if(gameController.GetCurrentState() == GameStates.TUTORIAL){
			inAnim = true;
		}

		if(gameController.GetCurrentState() != GameStates.INGAME && 
		   gameController.GetCurrentState() != GameStates.GAMEOVER){
			GetComponent<Rigidbody2D>().gravityScale = 0;
			return;
		}
		else{
			GetComponent<Rigidbody2D>().gravityScale = 1;
		}

		if(inAnim && gameController.GetCurrentState() != GameStates.TUTORIAL){
			currentTimeToAnim += Time.deltaTime;

			if(currentTimeToAnim > 0.4f){
				currentTimeToAnim = 0;
				inAnim = false;

			}
		}




		if(gameController.GetCurrentState() == GameStates.INGAME){


			if(GetComponent<Rigidbody2D>().velocity.y < 0){
				mesh.eulerAngles -= new Vector3(0,0,5f);
				if(mesh.eulerAngles.z < 270 && mesh.eulerAngles.z > 30)
					mesh.eulerAngles = new Vector3(0,0,270);
			}
			else if(GetComponent<Rigidbody2D>().velocity.y > 0){
				mesh.eulerAngles += new Vector3(0,0,2f);

				if(mesh.eulerAngles.z > 30)
					mesh.eulerAngles = new Vector3(0,0,30);
			}

		}
		else if(gameController.GetCurrentState() == GameStates.GAMEOVER){
			mesh.eulerAngles -= new Vector3(0,0,5f);
			if(mesh.eulerAngles.z < 270 && mesh.eulerAngles.z > 30)
				mesh.eulerAngles = new Vector3(0,0,270);
		}
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
			gameController.CallGameOver();	
	}	

	void OnTriggerEnter2D(Collider2D coll) {
			gameController.CallGameOver();	
	}

	public void RestartRotation(){
		mesh.eulerAngles = new Vector3(0,0,0);
	}

	public void Restart(){
		if(gameController.GetCurrentState() != GameStates.GAMEOVER){
			gameController.ResetGame();
			gameController.StartGame();
		}
	}
	
}
