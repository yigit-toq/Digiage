using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
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
        for (float i = transform.position.z + 5; i < transform.position.z + 25; i += 2)
        {
            int spawnerCount = Random.Range(0, spawners.Length);
            int objectCount = Random.Range(0, objects.Length);

            Instantiate(objects[objectCount], spawners[spawnerCount].localPosition + new Vector3(0, 0, i), Quaternion.identity);
        }
    }
}
