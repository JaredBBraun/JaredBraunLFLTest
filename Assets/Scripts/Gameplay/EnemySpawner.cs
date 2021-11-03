using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] private Transform _spawnPoint = null;

  [SerializeField] private Enemy _enemyPrefab = null;
  private Enemy _lastSpawned = null;
  public event Action OnEnemyRespawn;

  // Start is called before the first frame update
  void Start()
  {
    // SpawnEnemy();
  }

  public void SetEnemyPrefab(Enemy enemyPrefab)
  {
    if (enemyPrefab != null)
    {
      _enemyPrefab = enemyPrefab;
    }
    SpawnEnemy();
  }

  public void AddOnRepsawnListener(Action onEnemyRespawn)
  {
    OnEnemyRespawn += onEnemyRespawn;
  }

  void SpawnEnemy()
  {
    _lastSpawned = Instantiate(_enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
    _lastSpawned.OnEnemyDeath += SpawnEnemyDelayed;
  }

  void SpawnEnemyDelayed()
  {
    OnEnemyRespawn?.Invoke();
    Invoke("SpawnEnemy", 1f);
    //TODO don't do this can invoke in bad states can cause nulls/issues better to have this managed internally instead of using unity built in delays
  }
}
