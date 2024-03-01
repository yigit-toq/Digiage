using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeArea : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private TextMeshPro[] upgradeTMP;

    [SerializeField] private string[] upgradeText;

    [SerializeField] private int value;

    [SerializeField] private Color32[] colors;

    private void Awake()
    {
        upgradeTMP[0].text = upgradeText[Random.Range(0, upgradeText.Length)];

        value = Random.Range(-6, 6) * 5;

        char sign;
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
}
