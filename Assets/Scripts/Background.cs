using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {


    public int _direction;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_direction == 1)
        {
            transform.Translate(Time.deltaTime * -1, 0, 0);
        }
        else if(_direction == 2)
        {
            transform.Translate(Time.deltaTime, 0, 0);
        }
	}
}
