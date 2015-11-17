using UnityEngine;
using System.Collections;

public class DialogOnTop : MonoBehaviour {

	// Use this for initialization
	void OnEnable()
	{
		transform.SetAsLastSibling();
	}
}
