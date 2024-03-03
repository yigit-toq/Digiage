using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeArea : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private TextMeshPro[] upgradeTMP;

    [SerializeField] private string[] upgradeText;

    [SerializeField] private float value;

    private char sign;

    [SerializeField] private Color32[] colors;

    private void Awake()
    {
        upgradeTMP[0].text = upgradeText[Random.Range(0, upgradeText.Length)];

        value = Random.Range(-4, 4) * 5;

        if (value < 0)
        {
            spriteRenderer.color = colors[1];
            sign = '-';
        }
        else
        {
            spriteRenderer.color = colors[0];
            sign = '+';
        }
        upgradeTMP[1].text = sign + Mathf.Abs(value).ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (value >= 0)
            {
                spriteRenderer.color = colors[0];
                sign = '+';
            }

            value++;
            upgradeTMP[1].text = sign + Mathf.Abs(value).ToString();

            //Nesne yok olduðunda çalýþmasý engellenecek
            TweenController.BounceEffect(transform, new Vector3(1.4f, 1.5f, 1f), 1.2f, 0.1f);

            Destroy(other.gameObject);
        }

        //Deðerler güncellenebilir
        if(other.gameObject.CompareTag("Player"))
        {
            DG.Tweening.DOTween.Kill(gameObject.transform);

            if (upgradeTMP[0].text == upgradeText[0])
            {
                if (Singleton.FireRate > 50)
                    Singleton.FireRate -= value / 4;
            }
            else
            {
                if (Singleton.FireRange > 50)
                    Singleton.FireRange += value / 2;
            }
            Debug.LogWarning("Range: " + Singleton.FireRange + "Rate: " + Singleton.FireRate);
            Destroy(gameObject);
        }
    }
}
