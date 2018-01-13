using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{

    public float Points { get; set; }
    public Rigidbody2D Body { get; set; }
    public Vector2 Direction { get; set; }

    // Use this for initialization
    void Start()
    {
        this.Body = this.GetComponent<Rigidbody2D>();

        float x = Random.Range(0, 10);
        float y = Random.Range(0, 10);
        Vector2 direction = new Vector2(x, y).normalized;
        this.Direction = direction;

        float force = Random.Range(1, 4);
        Body.AddForce(direction * (force * Time.deltaTime));

        float angle = Random.Range(180, 360);
        StartCoroutine(RotateObject(angle, Vector3.forward, 3f));

    }

    // Update is called once per frame
    void Update()
    {
        CheckIfOutOfScreen();
    }

    private void CheckIfOutOfScreen()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y < 0 || screenPosition.x > Screen.width || screenPosition.x < 0)
            Destroy(this.gameObject);
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