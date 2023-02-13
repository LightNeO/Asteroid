using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 2.0f;
    public static int spawnAmount = 1;
    [SerializeField] private Asteroid _asteroidPrefab;
    private float spawnDistance = 80.0f;
    private float trajectoryVariance = 15.0f;
    void Start()
    {

        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);

    }

    private void Spawn()
    {

        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 _spawnPosition = transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion _swapnRotation = Quaternion.AngleAxis(variance, Vector3.forward);
            Asteroid asteroid = Instantiate(_asteroidPrefab, _spawnPosition, _swapnRotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrijectory(_swapnRotation * -spawnDirection);
        }
    }
}
