using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public void changeMenuScene (string sceneName)
	{ 
		SceneManager.LoadScene (sceneName);
		Time.timeScale = 1.0f;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
	}

	public void quitGame ()
	{
		Application.Quit ();
	}
}
