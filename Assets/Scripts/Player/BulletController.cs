using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private List<GameObject> bulletList = new();

    [SerializeField] private GameObject bullet, barrel;

    private float index;

    private void Update()
    {
        if (index > Singleton.BulletCooldown)
        {
            GameObject obj = Instantiate(bullet, barrel.transform.position, Quaternion.identity);

            obj.transform.Find("Object").gameObject.AddComponent<Bullet>();

            bulletList.Add(obj);

            index = 0;
        }
        index += Time.deltaTime;

        Fire();
    }

    private void Fire()
    {
        if (bulletList == null)
            return;

        List<GameObject> bulletsToProcess = new(bulletList);

        foreach (GameObject bullet in bulletsToProcess)
        {
            if (bullet)
                bullet.transform.Translate(Singleton.BulletSpeed * Singleton.Speed * Time.deltaTime * Vector3.forward);
        }
    }

    private class Bullet : MonoBehaviour
    {
        private void Start()
        {
            Destroy(transform.parent.gameObject, Singleton.BulletRange);
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(transform.parent.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Magazine"))
            {
                // Þarjör dolumu saðlanacak ve banda fýrlamasý saðlanacak.
            }

            if (other.gameObject.CompareTag("Wall"))
            {
                // Duvarýn çeþidine göre iþlemlerin gerçekleþmesi saðlanacak.
            }

            Destroy(transform.parent.gameObject);
        }
    }
}
