using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] private Chest _chest;
    [SerializeField] private Transform _chestSpawnPoints;
    [SerializeField] private float _chestRespawnTime;

    private Transform[] _spawnPoints;
    private Coroutine _spawner;
    private bool _isSpawning;
    private Queue<Chest> _chests = new Queue<Chest>();

    private void Start()
    {
        _spawnPoints = new Transform[_chestSpawnPoints.childCount];

        for (int i = 0; i < _chestSpawnPoints.childCount; i++)
        {
            _spawnPoints[i] = _chestSpawnPoints.GetChild(i);
        }

        StartSpawnChests();
    }

    private void StartSpawnChests()
    {
        if (_spawnPoints.Length > 0)
        {
            _spawner = StartCoroutine(SpawnChests());
        }
        else
        {
            _isSpawning = false;
            StopCoroutine(_spawner);
        }
    }

    private IEnumerator SpawnChests()
    {
        WaitForSeconds respawn = new WaitForSeconds(_chestRespawnTime);
        int spawnPointsMaxIndex = _spawnPoints.Length;
        int spawnPointsMinIndex = 0;
        _isSpawning = true;

        while (_isSpawning)
        {
            int index = Random.Range(spawnPointsMinIndex, spawnPointsMaxIndex);
            GameObject chest = Instantiate(_chest.gameObject, _spawnPoints[index].transform);
            _chests.Enqueue(chest.GetComponent<Chest>());
            yield return respawn;
        }
    }

    public Chest TransferChest()
    {
        return _chests.Count > 0 ? _chests.Dequeue() : null;
    }
}
