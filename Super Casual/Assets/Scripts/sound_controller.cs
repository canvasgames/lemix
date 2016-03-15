using UnityEngine;
using System.Collections;

public class sound_controller : MonoBehaviour
{
    public static sound_controller s = null;

    public AudioSource efxSource, efxSource2, efxSource3;
    public AudioSource musicSource;

    public AudioClip Jump, Explosion;

    public GameObject bt_sound;

    bool can_play_jump = true;

    void Awake()
    {
        //sController = this;
        if (s == null)
            s = this;
        else if (s != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        int temp_sound = PlayerPrefs.GetInt("sound_state_0on_1off", 0);

        musicSource.Play();

        if(temp_sound == 1)
        {
            muteMusic();
            muteSFX();

            if(bt_sound != null)
            {
                bt_sound.GetComponent<Animator>().Play("bt_sound_off");
            }
            
        }
    }
    // Use this for initialization
    void Start()
    {
        can_play_jump = true;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlayJump()
    {
        if (efxSource.volume > 0 && can_play_jump == true)
        {
            can_play_jump = false;
            PlaySingle(Jump);
            Invoke("can_play_jump_again", 0.3f);
        }
            
    }

    void can_play_jump_again()
    {
        can_play_jump = true;
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
        PlayerPrefs.SetInt("sound_state_0on_1off", 1);
        efxSource.volume = 0;
        efxSource2.volume = 0;
        efxSource3.volume = 0;
    }

    public void unmuteSFX()
    {
        PlayerPrefs.SetInt("sound_state_0on_1off", 0);
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
