using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{

    public GameObject _pokPrefab;
    public GameObject _lootFactoryPrefab;

    private Rigidbody2D _body;
    public float MoveSpeed { get; set; }
    private float _frequency;  // Speed of sine movement
    private float _magnitude;  // Size of sine movement
    private Vector2 _position;
    private Vector2 _axis;
    private Vector2 _rigthOfScreen;
    private Animator _animator;

    private ChickenLootFactory _lootFactory;

    private bool isDead = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("IsDead", false);

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

        Instantiate(_lootFactoryPrefab);

        _lootFactory = _lootFactoryPrefab.GetComponent<ChickenLootFactory>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (collision.gameObject.tag == "cannonball")
        {
            isDead = true;
            _animator.SetBool("IsDead", true);
            _body.angularVelocity = 0f;
            _body.velocity = Vector2.zero;

            Storage.Instance.LootPosition = this.transform.position;
            _lootFactory.CreateChickenLoot();
        }
    }


    public void DeathAnimationDone()
    {
        _animator.SetBool("IsDead", false);
        Instantiate(_pokPrefab, this.transform.position, Quaternion.identity);
        isDead = false;
        ReplaceChickenToRigthOfScreen();
    }

}
