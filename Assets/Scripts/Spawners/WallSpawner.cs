using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject @object;

    [SerializeField] private Transform[] spawners;

    private void Awake()
    {
        spawners = new Transform[2];

        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = transform.GetChild(i + 1).gameObject.transform;
        }
    }

    //Spawner Düzenlenecek
    private void Start()
    {
        for (float i = transform.position.z + 10; i < transform.position.z + 20; i += Random.Range(5, 10))
        {
            int spawnerCount = Random.Range(0, 2);

            Instantiate(@object, spawners[spawnerCount].localPosition + new Vector3(0, 0, i), Quaternion.identity);
        }
    }
}
