using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Play_Video : MonoBehaviour {

    public MovieTexture movie1, movie2, movie3;
    private AudioSource audios;
    bool playing = false;

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
                hud_controller.si.watched_the_video();
            }
        }

    }

    public void solta_a_vinheta_sombra()
    {
        int temp_rand = Random.Range(0, 4);

        GetComponent<RawImage>().texture = movie1 as MovieTexture;
        movie1.Stop();
        movie1.Play();
        playing = true;
         // audios.Play();
    }
    
}
