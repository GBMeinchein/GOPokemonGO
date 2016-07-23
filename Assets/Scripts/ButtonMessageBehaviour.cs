using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class ButtonMessageBehaviour : MonoBehaviour {

	public GameObject target;
	public string message;

	// Use this for initialization
	void Start () {
		GetComponent<PressGesture>().StateChanged += HandleStateChanged;
	}

	void HandleStateChanged (object sender, TouchScript.Events.GestureStateChangeEventArgs e)
	{
		if(e.State == Gesture.GestureState.Ended){
			target.SendMessage(message, SendMessageOptions.RequireReceiver);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
