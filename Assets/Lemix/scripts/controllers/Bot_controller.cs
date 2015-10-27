using UnityEngine;
using System.Collections;

public class Bot_controller : MonoBehaviour {
	float botGameTime, botPUTime;
	float botDifficulty = 1;
	WController[] wordCTRL;
	PowerUpCtrl[] pwctrl;

	// Use this for initialization
	void Start () {
		if(GLOBALS.Singleton.MP_MODE == 0)
		{
			botGameTime = 6f;
			botPUTime = Random.Range (25f,28f);
			//botPUTime = 2f;
			pwctrl = FindObjectsOfType(typeof(PowerUpCtrl)) as PowerUpCtrl[];
			wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(GLOBALS.Singleton.MP_MODE == 0 && GLOBALS.Singleton.GAME_RUNNING == true)
		{
			try_submit_word();
			try_to_user_power_up();
		}
	}

	void try_submit_word()
	{
		botGameTime -= Time.deltaTime;
		//Debug.Log(botGameTime);
		// verifica se e hora de tentar achar uma palavra
		if (botGameTime <= 0)
		{
			
			// se esta no começo do jogo (30%)
			//Debug.Log(wordCTRL[0].bot_number_of_words_founded());
			//Debug.Log(wordCTRL[0].numberofWords * 0.3f);
			if(wordCTRL[0].bot_number_of_words_founded() < (wordCTRL[0].numberofWords * 0.3f) )
			{
				//Debug.Log("30%");
				botGameTime = Random.Range(4f,6f) * botDifficulty;
				bot_on_init();
			}
			else if(wordCTRL[0].bot_number_of_words_founded() < (wordCTRL[0].numberofWords * 0.6f))
			{
				//Debug.Log("60%");
				botGameTime = Random.Range(8f,10f) * botDifficulty;
				bot_on_middle();
			}
			else
			{
				//Debug.Log("100%");
				botGameTime = Random.Range(10f,13f) * botDifficulty;
				bot_on_the_end();
			}
		}
	}
	
	// se esta no começo do jogo (<30%)
	void bot_on_init()
	{
		int rand;
		rand = Random.Range (0, 100);

		//faixa de palavras com menos letras
		if(rand < 65)
			rand = Random.Range (0, (int) (wordCTRL[0].numberofWords* 0.3f));

		//meio da tabela
		else if(rand < 90)
			rand = Random.Range ((int) (wordCTRL[0].numberofWords* 0.3f),(int) (wordCTRL[0].numberofWords* 0.6f));
		else
			rand = Random.Range ((int) (wordCTRL[0].numberofWords* 0.6f),(int) (wordCTRL[0].numberofWords));

		//tenta submeter a palavra
		wordCTRL[0].bot_try_to_submit_word(rand);
	}

	// se esta no meio começo do jogo (30%-80%)
	void bot_on_middle()
	{
		int rand;	

		rand = Random.Range (0, (int) (wordCTRL[0].numberofWords));
				
		//tenta submeter a palavra
		wordCTRL[0].bot_try_to_submit_word(rand);
	}	

	// se esta no meio pro fim do jogo (>80%)
   // se n acha a palavra tenta de novo
	void bot_on_the_end()
	{
		int rand;	
		
		rand = Random.Range (0, (int) (wordCTRL[0].numberofWords));

		//Try to submit the word again
		if(wordCTRL[0].bot_try_to_submit_word(rand)==false)
			wordCTRL[0].bot_try_to_submit_word(rand);
	}

	void try_to_user_power_up()
	{
		botPUTime -= Time.deltaTime;

		if(botPUTime <= 0)
		{
			botPUTime = Random.Range (25f,27f);
			int rand;	
			rand = Random.Range (0, 3);

			if(rand == 0)
			{
				pwctrl[0].eraseWord(GLOBALS.Singleton.MP_PLAYER);
			}
			else if(rand == 1)
				pwctrl[0].night();
			else if (rand == 2)
				pwctrl[0].freezeLetter();
			else if (rand == 3)
				pwctrl[0].earthquakeReceive();
		}
	}
}
