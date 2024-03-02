using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject bullet, barrel;

    private float index;

    [SerializeField] private Animator gunAnimator;

    private void Start()
    {
        index = Singleton.FireRate;
    }

    private void Update()
    {
        if (Singleton.Move)
        {
            if (index > Singleton.FireRate)
            {
                GameObject obj = Instantiate(bullet, barrel.transform.position, Quaternion.identity);

                gunAnimator.SetTrigger("Recoil");

                Handheld.Vibrate();

                obj.AddComponent<Bullet>();

                index = 0;
            }
            index += Time.deltaTime;
        }
    }

    private class Bullet : MonoBehaviour
    {
        static int magazineCount = 0;
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
            if (other.gameObject.CompareTag("Magazine"))
            {
                if (magazineCount != 6)
                {
                    magazineCount++;

                    other.transform.parent.Find("Text").GetComponent<TextMeshPro>().text = magazineCount.ToString();
                }
                Destroy(gameObject);
            }
            if(other.gameObject.CompareTag("YearZone"))
            {
                switch (other.gameObject.name)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                }
                Destroy(gameObject);
            }
        }

        private IEnumerator BounceEffect(Transform transform)
        {
            float scaleFactor = 1.2f;
            float animationTime = 0.25f;

            Vector3 targetScale = transform.localScale * scaleFactor;
            for (float t = 0.0f; t < animationTime; t += Time.deltaTime)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, t / animationTime);
            }
            yield return null;

            targetScale = transform.localScale / scaleFactor;
            for (float t = 0.0f; t < animationTime; t += Time.deltaTime)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, t / animationTime);
            }
        }
    }
}
