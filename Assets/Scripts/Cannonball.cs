using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private Vector2 _direction;
    public Vector2 Target { get; set; }
    public Vector2 Position { get; set; }
    public float Speed { get; set; }

    private Rigidbody2D _body;


    // Use this for initialization
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        Vector2 direction = (Target - Position).normalized;
        _body.AddForce(direction * (Speed * Time.deltaTime));

        

        StartCoroutine(RotateObject(360, Vector3.forward, 0.10f));
    }
    // Update is called once per frame
    void Update()
    {
        CheckForOutOfScreen();
    }
    private void CheckForOutOfScreen()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y < 0 || screenPosition.x > Screen.width || screenPosition.x < 0)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "chicken")
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator RotateObject(float angle, Vector3 axis, float inTime)
    {
        // calculate rotation speed
        float rotationSpeed = angle / inTime;

        while (true)
        {
            // save starting rotation position
            Quaternion startRotation = transform.rotation;

            float deltaAngle = 0;

            // rotate until reaching angle
            while (deltaAngle < angle)
            {
                deltaAngle += rotationSpeed * Time.deltaTime;
                deltaAngle = Mathf.Min(deltaAngle, angle);

                transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

                yield return null;
            }

            // delay here
            //yield return new WaitForSeconds(1);
        }
    }
}
