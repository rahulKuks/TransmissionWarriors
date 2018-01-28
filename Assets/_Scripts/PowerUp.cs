using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float healthUpgrade;

    public float HealthUpgrade { private set { healthUpgrade = value;  } get { return healthUpgrade; } }

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
