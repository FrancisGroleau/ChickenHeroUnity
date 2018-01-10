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
    }
    // Update is called once per frame
    void Update()
    {
        CheckForOutOfScreen();
        Spin();
    }
    private void CheckForOutOfScreen()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y < 0 || screenPosition.x > Screen.width || screenPosition.x < 0)
            Destroy(this.gameObject);
    }

    private void Spin()
    {
         //Quaternion rotation = Quaternion.AngleAxis(10f, Vector3.forward);
         //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 6f * Time.deltaTime);

        transform.RotateAround(transform.position, Vector3.forward, 10);

    }
}
