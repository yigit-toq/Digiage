using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> arrowList = new();

    [SerializeField] private GameObject arrow;

    private float interval;

    private void Awake()
    {
        interval = 1f;

        for (float z = transform.position.z; z < 100; z += interval)
        {
            GameObject clone = Instantiate(arrow, new Vector3(transform.position.x, 0f, z), Quaternion.identity);

            arrowList.Add(clone);

            clone.transform.parent = transform;
        }
    }

    private void Update()
    {
        foreach (GameObject obj in arrowList)
        {
            obj.transform.Translate(2 * Singleton.Speed * Time.deltaTime * Vector3.forward);
            
            if (obj.transform.position.z > 100f)
            {
                obj.transform.localPosition = Vector3.zero;
            }
        }
    }
}
