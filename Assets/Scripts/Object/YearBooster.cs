using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YearBooster : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] spriteRenderers;

    [SerializeField] private int upgradeValue;

    private void Awake()
    {
        spriteRenderers = new SpriteRenderer[3];
    }

    private void Start()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i] = transform.Find("Zone").transform.Find((i + 1).ToString()).GetComponent<SpriteRenderer>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Magazine"))
        {
            upgradeValue += other.GetComponent<Magazine>().MagazineCount;

            if (upgradeValue >= 12 && upgradeValue <= 18)
                spriteRenderers[0].color = Color.green;
            if (upgradeValue >= 18 && upgradeValue <= 24)
                spriteRenderers[1].color = Color.green;
            if (upgradeValue >= 24)
                spriteRenderers[2].color = Color.green;

            DG.Tweening.DOTween.Kill(other.gameObject.transform);
            Destroy(other.transform.parent.gameObject);
        }
    }
}
