using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Magazine : MonoBehaviour
{
    [SerializeField] private int magazineCount;

    private bool getMagazine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet") && !getMagazine)
        {
            if (magazineCount <= 6)
            {
                magazineCount++;

                transform.parent.Find("Text").GetComponent<TextMeshPro>().text = magazineCount.ToString();

                TweenController.BounceEffect(transform, Vector3.one * 0.2f);
            }
            if (magazineCount == 6)
            {
                getMagazine = true;

                Destroy(transform.parent.Find("Trigger").gameObject);

                TweenController.GetMagazine(transform.parent);
            }
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Conveyor"))
        {
            StartCoroutine(Movement());
        }
    }

    private IEnumerator Movement()
    {
        while (true)
        {
            transform.parent.position += new Vector3(0, 0, Singleton.Speed * Time.deltaTime);
            yield return null;
        }
    }
}
