using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour {


    private Rigidbody2D _body;
    public float MoveSpeed { get; set; }
    private float _frequency;  // Speed of sine movement
    private float _magnitude;  // Size of sine movement
    private Vector2 _position;
    private Vector2 _axis;
    private Vector2 _rigthOfScreen;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;

    private bool isDead = false;

    void Start () {
        _animator = GetComponent<Animator>();
        _animator.SetBool("IsDead", false);

        _boxCollider2D = GetComponent<BoxCollider2D>();
       
        _body = GetComponent<Rigidbody2D>();
        _position = transform.position;
        _axis = transform.up;

        //randomize sin wave
        MoveSpeed = Random.Range(3, 6);
        _frequency = Random.Range(10, 15);
        _magnitude = Random.Range(0.3f, 0.5f);

        //set the rigth of the screen
        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _rigthOfScreen = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        CheckIfOffscreen();
	}


    private void Move()
    {
        //adapted from http://answers.unity3d.com/questions/803434/how-to-make-projectile-to-shoot-in-a-sine-wave-pat.html

        if (!isDead)
        {
            _position += (Vector2)(transform.right * Time.deltaTime * MoveSpeed);
            transform.position = _position + _axis * Mathf.Sin(Time.time * _frequency) * _magnitude;
        }
    }

    private void CheckIfOffscreen()
    {
        if (transform.position.x > _rigthOfScreen.x + 10)
        {
            ReplaceChickenToRigthOfScreen();
        }
    }

    private void ReplaceChickenToRigthOfScreen()
    {
        _position.Set(Random.Range(-10, -5), Random.Range(5, 10));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Cannonball(Clone)")
        {
            isDead = true;
            _animator.SetBool("IsDead", true);
            Destroy(collision.gameObject);
            _body.angularVelocity = 0f;
            _body.velocity = Vector2.zero;
        }
    }

    public void DeathAnimationDone()
    {
        _animator.SetBool("IsDead", false);
    }

    public void PokAnimationDone()
    {
        ReplaceChickenToRigthOfScreen();
        isDead = false;
    }


}
