using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

	public TextMesh score;
	public TextMesh bestScore;
	public TextMesh scoreShadow;
	public TextMesh bestScoreShadow;
	public Renderer[] medals;

	public GameObject content;
	public GameObject title;
	public GameObject newScore;

	private float currentTimeGameOver;

	public GameObject fadeObject;

	// Use this for initialization
	void Start () {
		HideGameOver();
	}
	
	// Update is called once per frame
	void Update () {
		if(content.activeSelf && content.GetComponent<Animator>().GetBool("CallGameOver")){
			currentTimeGameOver += Time.deltaTime;

			if(currentTimeGameOver > 1){
				content.GetComponent<Animator>().SetBool("CallGameOver", false);
				title.GetComponent<Animator>().SetBool("CallGameOver", false);
				currentTimeGameOver = 0;
			}
		}
	}

	public void SetGameOver(int scoreInGame){

		if(scoreInGame > PlayerPrefs.GetInt("Score")){
			PlayerPrefs.SetInt("Score",scoreInGame);
			newScore.SetActive(true);
		}
		else{
			newScore.SetActive(false);
		}

		score.text = scoreInGame.ToString();
		scoreShadow.text = score.text;

		bestScore.text = PlayerPrefs.GetInt("Score").ToString();
		bestScoreShadow.text = bestScore.text;

		if(scoreInGame >= 10 && scoreInGame <= 24){
			medals[0].enabled = true;
		}
		else if(scoreInGame >= 25  && scoreInGame <= 34){
			medals[1].enabled = true;
		}
		else if(scoreInGame >= 35  && scoreInGame <= 49){
			medals[2].enabled = true;
		}
		else if(scoreInGame >= 50){
			medals[3].enabled = true;
		}

		content.SetActive(true);
		title.SetActive(true);

		content.GetComponent<Animator>().SetBool("CallGameOver", true);
		title.GetComponent<Animator>().SetBool("CallGameOver", true);




	}

	public void HideGameOver(){



		content.SetActive(false);
		title.SetActive(false);
		foreach(Renderer m in medals){
			m.enabled = false;
		}
		if(fadeObject.activeSelf)
			fadeObject.GetComponent<Animator>().SetBool("StartFade", false);
		fadeObject.SetActive(false);

	}

	public void ShowFade(){
		fadeObject.SetActive(true);
		fadeObject.GetComponent<Animator>().SetBool("StartFade", true);
	}

}
