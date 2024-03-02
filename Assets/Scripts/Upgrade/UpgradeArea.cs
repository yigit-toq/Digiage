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

        value = Random.Range(-6, 6) * 5;

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

            TweenController.BounceEffect(transform, new Vector3(1.4f, 1.5f, 1f));

            Destroy(other.gameObject);
        }

        //Deðerler güncellenebilir
        if(other.gameObject.CompareTag("Player"))
        {
            if (value >= 100)
            {
                if (upgradeTMP[0].text == upgradeText[0])
                {
                    Singleton.FireRate -= value / 100;
                }
                else
                {
                    Singleton.FireRange += value / 100;
                }
            }

            Debug.LogWarning("Rate" + Singleton.FireRate);
            Debug.LogWarning("Range" + Singleton.FireRange);

            Destroy(gameObject);
        }
    }
}
