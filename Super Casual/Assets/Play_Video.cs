using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Play_Video : MonoBehaviour {

    public MovieTexture movie1, movie2;
    private AudioSource audios;
    bool playing1 = false;
    bool playing2 = false;
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


            if (movie1 != null && movie1.isPlaying == false && playing1 == true )
            {
                playing1 = false;

                video_ended_img.SetActive(true);
                video_ended_img.GetComponent<Animator>().Play("battle_pegs");

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

            if (movie2 != null && movie2.isPlaying == false && playing2 == true )
            {
                
                playing2 = false;

                video_ended_img.SetActive(true);
            video_ended_img.GetComponent<Animator>().Play("bomblast");

            if (video_revive == true)
                {
                    video_ended_img.GetComponentInChildren<external_link_bt>().set_variables(true, false, video_sorted);
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

    public void solta_a_vinheta_sombra(bool revive, bool activate_pw)
    {
        video_revive = revive;
        video_activate_pw = activate_pw;

        int temp_rand = Random.Range(0, 2);
        video_sorted = temp_rand;

        movie2.Stop();
        movie1.Stop();

        if(temp_rand == 0)
        {
            GetComponent<RawImage>().texture = movie1 as MovieTexture;
            playing2 = false;
            playing1 = true;
            movie1.Play();
        }
        else
        {
            GetComponent<RawImage>().texture = movie2 as MovieTexture;
            playing2 = true;
            playing1 = false;
            movie2.Play();
        }
        
        
         // audios.Play();
    }
    
}
