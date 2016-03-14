using UnityEngine;
using System.Collections;

public class sound_controller : MonoBehaviour
{
    public static sound_controller s = null;

    public AudioSource efxSource, efxSource2, efxSource3;
    public AudioSource musicSource;

    public AudioClip Jump, Explosion;


    void Awake()
    {
        //sController = this;
        if (s == null)
            s = this;
        else if (s != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        musicSource.Play();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlayJump()
    {
        if (efxSource.volume > 0)
            PlaySingle(Jump);
    }
    public void PlayExplosion()
    {
        if (efxSource.volume > 0)
            PlaySingle(Explosion);
    }

    //#####################################################
    void PlaySingle(AudioClip clip)
    {
        if (efxSource.isPlaying == false)
        {
            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            efxSource.clip = clip;

            //Play the clip.
            efxSource.PlayOneShot(clip);
        }
        else if (efxSource2.isPlaying == false)
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
