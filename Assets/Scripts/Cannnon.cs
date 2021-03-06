﻿using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannnon : MonoBehaviour {

    private Vector2 _target;
    private readonly float _angle;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.Rotate(Vector3.forward, Angle, Space.Self);
        //CheckIfCannonAngledTowardTarget();
    }

    private void CheckIfCannonAngledTowardTarget()
    {
        float a = Storage.Instance.ActualCannonAngle;
        float a2 = Storage.Instance.Angle;
        if (a2 - 5 <= a && a <= a2 + 5)
            return;

        if (a2 < 0)
            RotateTowardsTarget(true);
        else
            RotateTowardsTarget(false);
    }

    public void RotateTowardsTarget(bool increment)
    {

        if (increment)
        {
            float angle = Mathf.LerpAngle(0, Storage.Instance.Angle, 2f * Time.deltaTime);
            transform.Rotate(Vector3.forward, angle);
            //transform.RotateAround(new Vector3(0, -4, 1), Vector3.forward, -2);
            //Storage.Instance.ActualCannonAngle += 2;
        }
        else
        {
            float angle = Mathf.LerpAngle(0, (Storage.Instance.Angle * -1), 2f * Time.deltaTime);
            transform.Rotate(Vector3.forward, angle);
            //transform.RotateAround(new Vector3(0, -4, 1), Vector3.forward, -2);
            //Storage.Instance.ActualCannonAngle += 2;
        }
    }

    public void RotateTowardsTarget()
    {
        StartCoroutine(RotateObject(Storage.Instance.Angle, Vector3.forward, 0.10f));

        this.RotateObject(Storage.Instance.Angle, Vector3.forward, 0.10f);
    }

    public IEnumerator RotateObject(float angle, Vector3 axis, float inTime)
    {
        // calculate rotation speed
        float rotationSpeed = angle / inTime;

        //while (true)
        //{
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
        //}
    }

}
