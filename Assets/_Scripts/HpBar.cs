using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour {
    public GameObject bar;
    private Vector3 maxScale;//local scale for max hp
    private Vector3 maxPosition; //local position for max hp
	// Use this for initialization
	void Start () {
        maxScale = bar.transform.localScale;
        maxPosition = bar.transform.localPosition;
	}
	
    public void setHP(float percent)
    {
        bar.transform.localScale = new Vector3(percent * maxScale.x, maxScale.y, maxScale.z);
      //  bar.
    }
	// Update is called once per frame
	void Update () {

	}
}
