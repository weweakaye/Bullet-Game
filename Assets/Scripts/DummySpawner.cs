using System.Collections;
using UnityEngine;

public class DummySpawner : MonoBehaviour
{
    [Header("Dummy")]
    public GameObject dummyPrefab;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Spawn Settings")]
    public float spawnInterval = 5f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            SpawnDummy();
        }
    }

    void SpawnDummy()
    {
        if (dummyPrefab == null)
        {
            Debug.LogWarning("Dummy Prefab is not assigned!");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No Dummy Spawn Points assigned!");
            return;
        }

        int randomIndex =
            Random.Range(0, spawnPoints.Length);

        Transform spawnPoint =
            spawnPoints[randomIndex];

        Instantiate(
            dummyPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        Debug.Log(
            "Training Dummy spawned at " +
            spawnPoint.name
        );
    }
}