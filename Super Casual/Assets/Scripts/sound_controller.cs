using UnityEngine;
using System.Collections;
using DG.Tweening;

public enum MusicStyle{
	Eletro,
	Rock,
	Pop,
	Reggae,
	Classic,
}

public class sound_controller : MonoBehaviour
{
    public static sound_controller s = null;

    public AudioSource efxSource, efxSource2, efxSource3;
    public AudioSource musicSource, musicSource2, musicSource3, musicSource4, musicSource5, curFadeIn, curFadeOut;

    public AudioClip Jump, Explosion, Collect, Alert;
    public AudioClip[]  Jumps;

    public GameObject bt_sound;
	public MusicLayers[] musics;

    bool can_play_jump = true;
	MusicStyle curMusic;

    int music_playing = 1;

	#region ===== Init =======
    void Awake()
    {
        //muteMusic();
        //sController = this;
        if (s == null)
            s = this;
        else if (s != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        int temp_sound = PlayerPrefs.GetInt("sound_state_0on_1off", 0);

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
		change_music (MusicStyle.Rock);

    }

    // Update is called once per frame
    void Update()
    {
        if (curFadeIn != null) {
            if (curFadeIn.volume < 1) {
				if (music_playing > 1) {
					curFadeIn.volume += 0.7f * Time.deltaTime;
					if(curFadeOut != null) curFadeOut.volume -= 0.7f * Time.deltaTime;
				}/* else {
					curFadeIn.volume += 0.25f * Time.deltaTime;
					curFadeOut.volume -= 0.25f * Time.deltaTime;
				}*/
            }
            else { 
				curFadeIn.volume = 1;
				if(curFadeOut != null)
					curFadeOut.volume = 0;
                curFadeIn = null;
                curFadeOut = null;
            }
        }
    }

	#endregion

	#region ===== Music Change =====
	public void change_music(MusicStyle style){
		foreach (MusicLayers mus in musics) {
			if (mus.myStyle == style) {
				curMusic = style;

				musicSource.Stop ();
				musicSource2.Stop ();
				musicSource3.Stop ();
				musicSource4.Stop ();
				musicSource5.Stop ();

				musicSource = mus.layer1;
				musicSource2 = mus.layer2;
				musicSource3 = mus.layer3;
				musicSource4 = mus.layer4;
				musicSource5 = mus.layer5;

				play_music ();

			}
		}
	}

	#endregion

    public void play_alert() {
        PlaySingle(Alert);
    }

    public void special_event() {
        //PlaySingle(Collect);
        PlaySingle(Jumps[Random.Range(0, 7)]);
    }
    public void play_collect_pw() {
        PlaySingle(Collect);
        //PlaySingle(Jumps[Random.Range(0, 7)]);
    }
    public void update_music() {
        if(USER.s.SOUND_MUTED == 0) update_music2();
       /* music_playing++;
        if (music_playing == 2) {
            //musicSource.Stop();
            //musicSource2.volume = 1;
            curFadeIn = musicSource2;
            curFadeOut = musicSource;
        }
        else if (music_playing == 3) {
            musicSource2.Stop();
            musicSource3.volume = 1;
        }

        else if (music_playing == 4) {
            musicSource3.Stop();
            musicSource4.volume = 1;
        }
        else if (music_playing == 5) {
            musicSource4.Stop();
            musicSource5.volume = 1;
        }
        */
    }

    public void update_music2() {
        music_playing++;
        if (music_playing == 2) {
            //musicSource.Stop();
            //musicSource2.volume = 1;
            curFadeIn = musicSource2;
            curFadeOut = musicSource;
        }
        else if (music_playing == 3) {
            curFadeIn = musicSource3;
            curFadeOut = musicSource2;
        }

        else if (music_playing == 4) {
			if (curMusic != MusicStyle.Rock) {
				curFadeIn = musicSource4;
				curFadeOut = musicSource3;
			} else {
				curFadeIn = musicSource4;
			}
        }
        else if (music_playing == 5) {

			if (curMusic != MusicStyle.Rock) {
				curFadeIn = musicSource5;
				curFadeOut = musicSource4;
			} else {
				curFadeIn = musicSource5;
				curFadeOut = musicSource3;
			}
           
        }
    }

    public void PlayJump()
    {
        if (efxSource.volume > 0 && can_play_jump == true)
        {
            can_play_jump = false;

            PlaySingle(Jump);
            //PlaySingle(Jumps[Random.Range(0,7)]);
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
    public void play_music()
    {
		musicSource.Play();
		musicSource2.Play();
		musicSource3.Play();
		musicSource4.Play();
        musicSource5.Play();
		musicSource.volume = 1;
		music_playing = 1;
    }
    public void stop_music()
    {
        musicSource.Stop();
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
