using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> arrowList = new();

    [SerializeField] private GameObject arrow;

    [SerializeField] private float interval;

    [SerializeField] private int speed;

    private void Awake()
    {
        for (float z = 0; z < 100; z += interval)
        {
            GameObject clone = Instantiate(arrow, new Vector3(transform.position.x, 0f, z), Quaternion.identity);

            arrowList.Add(clone);

            clone.transform.parent = transform;
        }
    }

    private void Start()
    {
        StartCoroutine(ArrowLoop());
    }

    private IEnumerator ArrowLoop()
    {
        while (true)
        {
            foreach (GameObject obj in arrowList)
            {
                if (obj != null)
                {
                    obj.transform.Translate(speed * Singleton.Speed * Time.deltaTime * Vector3.forward);

                    if (obj.transform.position.z >= 100f)
                        obj.transform.localPosition = Vector3.zero;
                }
            }
            yield return null;
        }
    }
}
