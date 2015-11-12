using UnityEngine;
using System.Collections;

public class Sound_Controller : MonoBehaviour {
	public static Sound_Controller sController = null;

	public AudioSource efxSource;             
	public AudioSource musicSource; 


	void Awake ()
	{
		//sController = this;
		if (sController == null)
			sController = this;
		else if (sController != this)
			Destroy (gameObject);
		DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySingle(AudioClip clip)
	{
		//Set the clip of our efxSource audio source to the clip passed in as a parameter.
		efxSource.clip = clip;
		
		//Play the clip.
		efxSource.Play ();

	}

	public void muteSFX()
	{
		efxSource.volume = 0;
	}

	public void unmuteSFX()
	{
		efxSource.volume = 1;
	}

	public void muteMusic()
	{
		musicSource.volume = 0;
	}
	
	public void unmuteMusic()
	{
		musicSource.volume = 1;
	}

}
