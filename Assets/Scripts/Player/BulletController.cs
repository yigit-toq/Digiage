using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject bullet, barrel;

    private float index;

    private void Update()
    {
        if (Singleton.Move)
        {
            if (index > Singleton.FireRate)
            {
                GameObject obj = Instantiate(bullet, barrel.transform.position, Quaternion.identity);

                Handheld.Vibrate();

                obj.AddComponent<Bullet>();

                index = 0;
            }
            index += Time.deltaTime;
        }
    }

    private class Bullet : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, Singleton.FireRange);
        }

        private void Update()
        {
            transform.Translate(Singleton.BulletSpeed * Singleton.Speed * Time.deltaTime * Vector3.forward);
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
        }

        static int magazineCount = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Magazine"))
            {
                if (magazineCount != 6)
                {
                    magazineCount++;

                    other.gameObject.transform.Find("Text").GetComponent<TextMeshPro>().text = magazineCount.ToString();
                }
            }
            if (other.gameObject.CompareTag("Wall"))
            {
                // Duvarýn çeþidine göre iþlemlerin gerçekleþmesi saðlanacak.
            }

            Destroy(gameObject);
        }
    }
}
