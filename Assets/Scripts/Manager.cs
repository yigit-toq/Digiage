using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Manager : MonoBehaviour
{
    public void OnMouseDown()
    {
        if (!Singleton.Move)
            Singleton.Move = true;
    }
}
