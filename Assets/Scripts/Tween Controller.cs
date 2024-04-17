using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenController : MonoBehaviour
{
    public static void BounceEffect(Transform transform, Vector3 currentScale, float targetScale, float delay)
    {
        transform.DOScale(currentScale * targetScale, delay);
        transform.DOScale(currentScale, delay).SetDelay(delay);
    }

    public static void SpinEffect(GameObject[] objects, int level)
    {
        if (objects != null)
        {
            objects[level].transform.DORotate(new Vector3(0f, -180f, 0f), 0.5f);
            objects[level].SetActive(false);
            objects[level + 1].SetActive(true);
            objects[level + 1].transform.DORotate(new Vector3(0f, 0f, 0f), 0.5f);
        }
    }

    public static void GetMagazine(Transform transform)
    {
        float target = -1.9f;
        float duration = Mathf.Abs(target - transform.position.x) / 2;

        transform.DOMoveX(target, duration);
        transform.DOMoveY(-0.2f, duration);
    }
}
