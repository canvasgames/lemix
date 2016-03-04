using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Play_Video : MonoBehaviour {

    public MovieTexture movie1, movie2, movie3;
    private AudioSource audios;
    bool playing = false;
    bool video_revive = false;
    bool video_activate_pw = false;
    int video_sorted = 0;

    public GameObject video_ended_img;

	// Use this for initialization
	void Awake () {
        
        //audios = GetComponent<AudioSource>();
        //audios.clip = movie.audioClip;
        
    }


    void Update()
    {
        if(movie1 != null)
        {
            if (movie1.isPlaying == false && playing == true)
            {
                playing = false;
                video_ended_img.SetActive(true);

                if (video_revive == true)
                {
                    video_ended_img.GetComponentInChildren<external_link_bt>().set_variables(true,false, video_sorted);
                    video_ended_img.GetComponentInChildren<button_close_video_ended>().set_variables(true, false);

                }
                else if (video_activate_pw == true)
                {

                    video_ended_img.GetComponentInChildren<external_link_bt>().set_variables(false, true, video_sorted);
                    video_ended_img.GetComponentInChildren<button_close_video_ended>().set_variables(false, true);
                }

                video_revive = false;
                video_activate_pw = false;
            }
        }

    }

    public void solta_a_vinheta_sombra(bool revive, bool activate_pw)
    {
        video_revive = revive;
        video_activate_pw = activate_pw;

        int temp_rand = Random.Range(0, 4);
        video_sorted = temp_rand;

        GetComponent<RawImage>().texture = movie1 as MovieTexture;
        movie1.Stop();
        movie1.Play();
        playing = true;
         // audios.Play();
    }
    
}
