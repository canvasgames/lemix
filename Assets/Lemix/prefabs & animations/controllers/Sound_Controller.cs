using UnityEngine;
using System.Collections;

public class Sound_Controller : MonoBehaviour {
	public static Sound_Controller sController = null;

	public AudioSource efxSource, efxSource2, efxSource3;             
	public AudioSource musicSource; 

	public AudioClip NewPW, AudioAlready, AudioFound, AudioError;


	void Awake ()
	{
		//sController = this;
		if (sController == null)
			sController = this;
		else if (sController != this)
			Destroy (gameObject);
		DontDestroyOnLoad(gameObject);
		musicSource.Play();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void PlayPowerUp ()
	{
		if(efxSource.volume > 0)
			PlaySingle(NewPW);
	}
	
	public void AudAlready ()
	{
		if(efxSource.volume > 0)
			PlaySingle(AudioAlready);
	}

	public void AudFound ()
	{
		if(efxSource.volume > 0)
			PlaySingle(AudioFound);
	}

	public void AudError ()
	{
		if(efxSource.volume > 0)
			PlaySingle(AudioError);
	}
	//#####################################################
	void PlaySingle(AudioClip clip)
	{
		if(efxSource.isPlaying == false)
		{
			//Set the clip of our efxSource audio source to the clip passed in as a parameter.
			efxSource.clip = clip;
		
			//Play the clip.
			efxSource.PlayOneShot(clip);
		}
		else if(efxSource2.isPlaying == false)
		{
			//Set the clip of our efxSource audio source to the clip passed in as a parameter.
			efxSource2.clip = clip;
			
			//Play the clip.
			efxSource2.PlayOneShot(clip);
		}
		else
		{
			//Set the clip of our efxSource audio source to the clip passed in as a parameter.
			efxSource3.clip = clip;
			
			//Play the clip.
			efxSource3.PlayOneShot(clip);
		}

	}

	public void muteSFX()
	{
		efxSource.volume = 0;
		efxSource2.volume = 0;
		efxSource3.volume = 0;
	}

	public void unmuteSFX()
	{
		efxSource.volume = 1;
		efxSource2.volume = 1;
		efxSource3.volume = 1;
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
