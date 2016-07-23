using UnityEngine;
using System.Collections;

public class MoveOffet : MonoBehaviour {

	private Material currentMaterial;
	public float speed;
	private float offset;

	private GameController gameController;


	// Use this for initialization
	void Start () {
		gameController = FindObjectOfType(typeof(GameController)) as GameController;
		currentMaterial = GetComponent<Renderer>().material;
	
	}
	
	// Update is called once per frame
	void Update () {

		if((gameController.GetCurrentState() != GameStates.INGAME && 
		   gameController.GetCurrentState() != GameStates.MAINMENU &&
			gameController.GetCurrentState() != GameStates.TUTORIAL) ||
		   Time.timeScale != 1)
			return;

		offset += 0.001f;

		currentMaterial.SetTextureOffset("_MainTex", new Vector2(offset*speed, 0));

	}
}
