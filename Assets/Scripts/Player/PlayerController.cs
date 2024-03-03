using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Manager manager;

    private Rigidbody playerRigidbody;

    [SerializeField] private float startPosition;

    [SerializeField] private float horizontalSpeed;

    private void Awake()
    {
        manager = FindObjectOfType<Manager>();

        playerRigidbody = GetComponent<Rigidbody>();

        horizontalSpeed = 5;

        Singleton.GunYear = 1800;
    }

    //Sað ve sola hareket edilmesini saðlayan kod düzenlenecek.
    private void Update()
    {
        if (Singleton.Move)
            Movement();
        else
            playerRigidbody.velocity = Vector3.zero;
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
        if (collision.gameObject.CompareTag("Hexagon"))
        {
            DOTween.Kill(collision.gameObject.transform);

            manager.Finish.SetActive(true);

            Singleton.Move = false;

            Debug.LogWarning("Game Over");
        }
        if (collision.gameObject.CompareTag("Money"))
        {
            Debug.Log("Money");

            Singleton.Money += 20;

            //Ekrana yazýlmasý saðlanacak

            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("GetMagazine"))
        {
            //Debug.LogWarning("Þarjör Alýndý");

            TweenController.GetMagazine(other.transform.parent);
        }

        if (other.gameObject.CompareTag("YearZone"))
        {
            if (other.gameObject.name == "1")
            {
            }
            if (other.gameObject.name == "2")
            {
            }
            if (other.gameObject.name == "3")
            {
            }
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            transform.DOMoveZ(transform.position.z - 2f, 0.5f);

            Singleton.GunYear--;

            manager.YearText.text = Singleton.GunYear.ToString();

            Debug.LogWarning("Trap");
        }
    }
}
