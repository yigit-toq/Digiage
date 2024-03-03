using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Magazine : MonoBehaviour
{
    public int MagazineCount;

    private bool getMagazine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet") && !getMagazine)
        {
            if (MagazineCount <= 6)
            {
                MagazineCount++;

                transform.parent.Find("Text").GetComponent<TextMeshPro>().text = MagazineCount.ToString();

                TweenController.BounceEffect(transform, new Vector3(0.2f, 0.2f, 0.2f), 1.2f, 0.1f);
            }
            if (MagazineCount == 6)
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
            transform.parent.position += new Vector3(0, 0, Singleton.Speed * 5 * Time.deltaTime);
            yield return null;
        }
    }
}
