using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject bullet, barrel;

    [SerializeField] private Animator animator;

    private float time;

    private void Start()
    {
        time = Singleton.FireRate;
    }

    private void Update()
    {
        if (Singleton.Move)
        {
            if (time > Singleton.FireRate)
            {
                GameObject obj = Instantiate(bullet, barrel.transform.position, Quaternion.identity);

                obj.AddComponent<Bullet>();

                animator.Play("Recoil");

                Handheld.Vibrate();

                time = 0;
            }
            time += Time.deltaTime;
        }
    }

    private class Bullet : MonoBehaviour
    {
        static int hexagonCount = 0;

        private void Start()
        {
            Destroy(gameObject, Singleton.FireRange);
        }

        private void Update()
        {
            transform.Translate((Singleton.BulletSpeed) * Time.deltaTime * Vector3.forward);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Hexagon"))
            {
                string str = collision.transform.Find("Text").GetComponent<TextMeshPro>().text;

                hexagonCount = int.Parse(str);
                hexagonCount--;
                str = hexagonCount.ToString();
                collision.transform.Find("Text").GetComponent<TextMeshPro>().text = str;
                if (hexagonCount == 0)
                    Destroy(collision.transform.parent.gameObject);
            }

            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("YearZone"))
            {
                Destroy(gameObject);
            }
        }
    }
}
