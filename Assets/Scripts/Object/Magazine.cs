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

                TweenController.BounceEffect(transform, new Vector3(0.2f, 0.2f, 0.2f), 1.2f, 0.1f);
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

        if(other.gameObject.CompareTag("Destroyed"))
        {
            Manager.UpgradeValue += magazineCount;

            Debug.LogWarning(Manager.UpgradeValue);

            if (Manager.UpgradeValue >= 12 && Manager.UpgradeValue <= 18)
            {
                SpriteRenderer renderer = other.transform.Find("Zone").transform.Find("1").GetComponent<SpriteRenderer>();

                renderer.color = Color.green;
            }
            if (Manager.UpgradeValue >= 18 && Manager.UpgradeValue <= 24)
            {
                SpriteRenderer renderer = other.transform.Find("Zone").transform.Find("2").GetComponent<SpriteRenderer>();

                renderer.color = Color.green;
            }
            if (Manager.UpgradeValue >= 24)
            {
                SpriteRenderer renderer = other.transform.Find("Zone").transform.Find("3").GetComponent<SpriteRenderer>();

                renderer.color = Color.green;
            }

            DG.Tweening.DOTween.Kill(gameObject.transform);
            Destroy(transform.parent.gameObject);
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
