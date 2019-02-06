using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour {

	public Text startText;

	public void StartGame(){
		startText.text = "Setting up Stage...";
		SceneManager.LoadScene (1);
	}

	public void Quit(){
		Application.Quit ();
	}

}
