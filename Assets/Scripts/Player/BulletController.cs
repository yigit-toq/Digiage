using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject bullet, barrel;

    [SerializeField] private AudioSource gunAudioSource;

    [SerializeField] private AudioClip fireAudio;

    public Animator GunAnimator;

    private float time;

    private void Awake()
    {
        Singleton.FireRate = 50;
        Singleton.FireRange = 100;
    }

    private void Update()
    {
        if (Singleton.Move)
        {
            if (time > Singleton.FireRate / 100)
            {
                GameObject obj = Instantiate(bullet, barrel.transform.position, Quaternion.identity);

                obj.AddComponent<Bullet>();

                gunAudioSource.PlayOneShot(fireAudio);

                GunAnimator.Play("Recoil");

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
            Destroy(gameObject, Singleton.FireRange / 100);
        }

        private void Update()
        {
            transform.Translate(Singleton.BulletSpeed * Time.deltaTime * Vector3.forward);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Hexagon"))
            {
                string str = collision.transform.Find("Text").GetComponent<TextMeshPro>().text;

                TweenController.BounceEffect(collision.transform, new Vector3(0.5f, 0.5f, 0.5f), 1.2f, 0.1f);

                hexagonCount = int.Parse(str);
                hexagonCount--;
                str = hexagonCount.ToString();
                collision.transform.Find("Text").GetComponent<TextMeshPro>().text = str;
                if (hexagonCount == 0)
                {
                    DG.Tweening.DOTween.Kill(collision.gameObject.transform);

                    Destroy(collision.gameObject);
                }
            }
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("YearZone"))
            {
                Destroy(gameObject);
            }
            if(other.gameObject.CompareTag("Hourglass"))
            {
                Singleton.GunYear += 10;

                FindObjectOfType<Manager>().YearText.text = Singleton.GunYear.ToString();

                TweenController.BounceEffect(other.transform, Vector3.one * 1.75f, 1.2f, 0.1f);

                Destroy(gameObject);
            }
        }
    }
}
