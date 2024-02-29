using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Singleton.Speed * Time.deltaTime * Vector3.forward);
    }
}
