using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;  
using System.Linq;
using Thinksquirrel.WordGameBuilder;
using Thinksquirrel.WordGameBuilder.Gameplay;
using Thinksquirrel.WordGameBuilder.ObjectModel;

public class CreateDictionary : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//COLOQUE UMA PALAVRA E MAGICAMENTE GERA UM ARQUIVO AllAnagramsFile.txt NA PASTA RAIZ DO POJETU
		//DESCOMENTE A FUNÇAO ABAIXO PARA USAR ESSA MAGICA INCRIVEL
		verifyWords("UPDATE");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Pega a palavra e imprime todas sub-palavras existentes (ate 8 letras)
	public void verifyWords(string word2verify)
	{
		int i, j, k, l, m, n, o, p;
		int maxlenght = word2verify.Length;
		string tempString;
		
		List<string> wordsletters = new List<string>();
		
		
		
		bool verify;
		
		if (maxlenght > 2) 
		{
			for (i=0; i<maxlenght; i++) 
			{
				for (j=0; j<maxlenght; j++) 
				{
					for (k=0; k<maxlenght; k++)
					{
						if(i!=j && i!=k && j!=k)
						{
							tempString = "";
							tempString = tempString + word2verify [i]; 
							tempString = tempString + word2verify [j];
							tempString = tempString + word2verify [k];
							verify = WordGameLanguage.current.wordSet.Contains (tempString);
							if (verify == true)
							{
								verify = wordsletters.Contains(tempString);
								if (verify == false)
									wordsletters.Add(tempString);
								
							}
							if (maxlenght > 3)
							{
								for (l=0; l<maxlenght; l++)
								{
									if(l!=i && l!=j && l!=k)
									{
										tempString = "";
										tempString = tempString + word2verify [i]; 
										tempString = tempString + word2verify [j];
										tempString = tempString + word2verify [k];
										tempString = tempString + word2verify [l];
										verify = WordGameLanguage.current.wordSet.Contains (tempString);
										if (verify == true)
										{
											verify = wordsletters.Contains(tempString);
											if (verify == false)
												wordsletters.Add(tempString);
											
										}
										
										if (maxlenght > 4)
										{
											for (m=0; m<maxlenght; m++)
											{
												if(m!=i && m!=j && m!=k && m!=l)
												{
													tempString = "";
													tempString = tempString + word2verify [i]; 
													tempString = tempString + word2verify [j];
													tempString = tempString + word2verify [k];
													tempString = tempString + word2verify [l];
													tempString = tempString + word2verify [m];
													verify = WordGameLanguage.current.wordSet.Contains (tempString);
													if (verify == true)
													{
														verify = wordsletters.Contains(tempString);
														if (verify == false)
															wordsletters.Add(tempString);
														
													}
													
													if (maxlenght > 5)
													{
														for (n=0; n<maxlenght; n++)
														{
															if(n!=i && n!=j && n!=k && n!=l && n!=m)
															{
																tempString = "";
																tempString = tempString + word2verify [i]; 
																tempString = tempString + word2verify [j];
																tempString = tempString + word2verify [k];
																tempString = tempString + word2verify [l];
																tempString = tempString + word2verify [m];
																tempString = tempString + word2verify [n];
																verify = WordGameLanguage.current.wordSet.Contains (tempString);
																if (verify == true)
																{
																	verify = wordsletters.Contains(tempString);
																	if (verify == false)
																		wordsletters.Add(tempString);
																	
																}
																
																if (maxlenght > 6)
																{
																	for (o=0; o<maxlenght; o++)
																	{
																		if(o!=i && o!=j && o!=k && o!=l && o!=m && o!=n)  
																		{
																			tempString = "";
																			tempString = tempString + word2verify [i]; 
																			tempString = tempString + word2verify [j];
																			tempString = tempString + word2verify [k];
																			tempString = tempString + word2verify [l];
																			tempString = tempString + word2verify [m];
																			tempString = tempString + word2verify [n];
																			tempString = tempString + word2verify [o];
																			verify = WordGameLanguage.current.wordSet.Contains (tempString);
																			if (verify == true)
																			{
																				verify = wordsletters.Contains(tempString);
																				if (verify == false)
																					wordsletters.Add(tempString);
																				
																			}
																			
																			if (maxlenght > 7)
																			{
																				for (p=0; p<maxlenght; p++)
																				{
																					if(p!=i && p!=j && p!=k && p!=l && p!=m && p!=n && p!=o) 
																					{
																						tempString = "";
																						tempString = tempString + word2verify [i]; 
																						tempString = tempString + word2verify [j];
																						tempString = tempString + word2verify [k];
																						tempString = tempString + word2verify [l];
																						tempString = tempString + word2verify [m];
																						tempString = tempString + word2verify [n];
																						tempString = tempString + word2verify [o];
																						tempString = tempString + word2verify [p];
																						verify = WordGameLanguage.current.wordSet.Contains (tempString);
																						if (verify == true)
																						{
																							verify = wordsletters.Contains(tempString);
																							if (verify == false)
																								wordsletters.Add(tempString);
																							
																						}
																					}		
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
								
							}
						}
					}
				}
			}
		}
		Debug.Log("PRIMINDO PALAVRINHAS MAROTAS");
		StreamWriter outfile = new StreamWriter("All_Anagrams_File.txt");
		foreach (string s in SortByLength(wordsletters))
		{
			Debug.Log(s);
			outfile.WriteLine(s);
		}
		outfile.Close();
		
	}
	
	static IEnumerable<string> SortByLength(IEnumerable<string> e)
	{
		// Use LINQ to sort the array received and return a copy.
		var sorted = from s in e
			orderby s.Length ascending
				select s;
		return sorted;
	}
}
