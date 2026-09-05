using System.Collections;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    [Header("Ammo")]
    public GameObject ammoPrefab;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Spawn Settings")]
    public float spawnInterval = 30f;

    private GameObject currentAmmo;

    void Start()
    {
        StartCoroutine(SpawnAmmoRoutine());
    }

    IEnumerator SpawnAmmoRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentAmmo == null)
            {
                SpawnAmmo();
            }
        }
    }

    void SpawnAmmo()
    {
        if (ammoPrefab == null)
        {
            Debug.LogWarning("Ammo Prefab is not assigned!");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No Ammo Spawn Points assigned!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);

        Transform spawnPoint = spawnPoints[randomIndex];

        currentAmmo = Instantiate(
            ammoPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        Debug.Log(
            "Ammo spawned at " +
            spawnPoint.name
        );
    }
}