using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Play_Video : MonoBehaviour {

    public MovieTexture movie;
    private AudioSource audios;

	// Use this for initialization
	void Start () {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        //audios = GetComponent<AudioSource>();
        //audios.clip = movie.audioClip;
        
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            movie.Play();
           // audios.Play();
        }
    }
    
}
