using UnityEngine;
using System.Collections;

public enum soundsGame{
	die,
	hit,
	menu,
	point,
	wing
}

public class SoundController : MonoBehaviour {

	public AudioClip soundDie;
	public AudioClip soundHit;
	public AudioClip soundMenu;
	public AudioClip soundPoint;
	public AudioClip soundWing;

    public static SoundController instance;

	// Use this for initialization
	void Start () {
		instance = this;
    }
	
	public static void PlaySound(soundsGame currentSound){
		switch(currentSound){
		case soundsGame.die:{
			instance.GetComponent<AudioSource>().PlayOneShot(instance.soundDie);
		}
			break;
		case soundsGame.hit:{
			instance.GetComponent<AudioSource>().PlayOneShot(instance.soundHit);
			instance.Invoke("PlaySoundDie", 0.3f);
		}
			break;
		case soundsGame.menu:{
			instance.GetComponent<AudioSource>().PlayOneShot(instance.soundMenu);
		}
			break;
		case soundsGame.point:{
			instance.GetComponent<AudioSource>().PlayOneShot(instance.soundPoint);
		}
			break;
		case soundsGame.wing:{
			instance.GetComponent<AudioSource>().PlayOneShot(instance.soundWing);
		}
			break;
		}
	}

	private void PlaySoundDie(){
		PlaySound(soundsGame.die);
	}
}
