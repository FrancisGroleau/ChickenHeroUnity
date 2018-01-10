using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMuzzleFlash : MonoBehaviour {

    private SpriteRenderer _renderer;
    private Animator _animator;
    // Use this for initialization
    void Start () {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if(_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            _renderer.enabled = false;
        }
        else
        {
            _renderer.enabled = true;
        }

        if(Input.touchCount > 0)
        {
            Touch t = Input.touches[0];
            if(t.phase == TouchPhase.Began)
            {
                GetComponent<Animator>().SetTrigger("Shooting");
            }
        }



        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<Animator>().SetTrigger("Shooting");
        }
    }
}
