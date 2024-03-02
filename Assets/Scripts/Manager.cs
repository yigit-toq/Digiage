using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject slide;

    public void OnMouseDown()
    {
        if (!Singleton.Move)
        {
            slide.SetActive(false);

            Singleton.Move = true;
        }
    }
}
