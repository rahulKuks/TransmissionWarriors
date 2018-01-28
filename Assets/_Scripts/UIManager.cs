using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private Text gameOverMessage;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void SetupGameOverMessage(string message)
    {
        gameOverScreen.SetActive(true);
        gameOverMessage.text = message;
    }
}
