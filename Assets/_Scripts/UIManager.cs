using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text gameOverMessage;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void SetGameOverMessage(string message)
    {
        gameOverMessage.text += message;
    }
}
