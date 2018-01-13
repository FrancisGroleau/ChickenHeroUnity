using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {


    public GameObject _chickenPrefab;
    public GameObject _projectilePrefab;
    public GameObject _cannonPrefab;
    public GameObject _muzzleFlashPrefab;
    public List<GameObject> _projectiles = new List<GameObject>();
    public List<GameObject> _chikens = new List<GameObject>();
    private float _angle;

	// Use this for initialization
	void Start () {

   
        int numberOfChikens = Random.Range(20, 30);
        for (int i = 0; i < numberOfChikens; i++)
        {
            Vector2 position = new Vector2(Random.Range(-10, 0), Random.Range(5, 10));
            GameObject clone = Instantiate(_chickenPrefab, position, Quaternion.identity);
            BoxCollider2D bc1 = clone.GetComponent<BoxCollider2D>();

            foreach(GameObject g in _chikens)
            {
                BoxCollider2D bc2 = g.GetComponent<BoxCollider2D>();
                Physics2D.IgnoreCollision(bc1, bc2);
            }

            _chikens.Add(clone);
        }

        Instantiate(_cannonPrefab, new Vector2(0, -4.10f), Quaternion.identity);
        Instantiate(_muzzleFlashPrefab, new Vector2(0, -3.3f), Quaternion.identity);

		
	}
	
	// Update is called once per frame
	void Update () {
        CheckForFireCanon();

    }

    private void CheckForFireCanon()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.touches[0];
            if(t.phase == TouchPhase.Began)
            {
                Vector2 position = new Vector2(0, -4);
                Vector2 target = Camera.main.ScreenToWorldPoint(t.position);

                GameObject projectile = Instantiate(_projectilePrefab, position, Quaternion.identity);
                projectile.GetComponent<Cannonball>().Position = position;
                projectile.GetComponent<Cannonball>().Target = target;
                projectile.GetComponent<Cannonball>().Speed = 60f;
                _projectiles.Add(projectile);

                //_angle = Vector2.Angle(position.normalized, target.normalized);
                //Vector2 direction = target - position;

                bool anticlockwise = false;
                float x = target.x - position.x;
                if (x < 0)
                {
                    x *= -1;
                    anticlockwise = true;
                }

                float y = target.y - position.y;
                if (y < 0)
                    y *= -1;

                _angle = Mathf.Atan2(y, x);

                if (anticlockwise)
                    _angle *= -1;

                _angle = _angle * Mathf.Rad2Deg;

                Assets.Scripts.Storage.Instance.Angle = Mathf.RoundToInt(_angle);
                Assets.Scripts.Storage.Instance.Target = target;
                Assets.Scripts.Storage.Instance.ActualCannonAngle = 0f;

                _cannonPrefab.GetComponent<Cannnon>().RotateTowardsTarget(); 
            }
        }
    }
}
