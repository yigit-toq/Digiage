using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    [SerializeField] private float startPosition;

    [SerializeField] private float horizontalSpeed;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        horizontalSpeed = 5;
    }

    //Sað ve sola hareket edilmesini saðlayan kod düzenlenecek.
    private void Update()
    {
        if (Singleton.Move)
            Movement();
    }

    private void Movement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPosition = touch.position.x;
                    break;
                case TouchPhase.Moved:
                    float movement = (touch.position.x - startPosition) / Screen.width;

                    playerRigidbody.velocity = new Vector3(movement * horizontalSpeed, 0f, 0f);
                    break;
                case TouchPhase.Ended:
                    playerRigidbody.velocity = Vector3.zero;
                    break;
            }
        }

        transform.Translate(Singleton.Speed * Time.deltaTime * Vector3.forward);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.3f, 1.3f), transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Singleton.Move = false;
    }
}
