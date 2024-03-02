using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Manager : MonoBehaviour
{
    public GameObject Slide;

    public void OnMouseDown()
    {
        if (!Singleton.Move)
        {
            Slide.SetActive(false);

            Singleton.Move = true;
        }
    }
}
