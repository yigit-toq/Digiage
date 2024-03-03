using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;

    [SerializeField] private Transform[] spawners;

    private void Awake()
    {
        spawners = new Transform[transform.childCount];

        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = transform.GetChild(i).gameObject.transform;
        }
    }

    //Spawner Düzenlenecek
    private void Start()
    {
        for (float i = transform.position.z + 5; i < transform.position.z + 20; i += Random.Range(3, 6))
        {
            int spawnerCount = Random.Range(0, spawners.Length);
            int possibility = Random.Range(0, 2);

            if (possibility == 0)
                Instantiate(objects[0], spawners[spawnerCount].localPosition + new Vector3(0, 0, i), Quaternion.identity);
            else
                Instantiate(objects[1], spawners[spawnerCount].localPosition + new Vector3(0, 0, i), Quaternion.identity);
        }
    }
}
