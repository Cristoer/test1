using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    [SerializeField] ZombieController zombiePrefab;
    [SerializeField] Transform player;
    [SerializeField] GameController gameController;
    [SerializeField] Transform[] levelPositonConfig;
    List<SpawnPosition> _spawnPositions=new List<SpawnPosition>();
    void Start()
    {
        if (GameMgr.instance.selectLevel >= levelPositonConfig.Length) {
            Debug.LogWarning("selectLevel is more than levelPosition length");
            return;
        }
        Transform positionParent=Instantiate(levelPositonConfig[GameMgr.instance.selectLevel], transform);
        positionParent.GetComponentsInChildren(_spawnPositions);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.position;
        int count = _spawnPositions.Count;
        for (int i = 0; i < count; i++)
        {
            SpawnPosition spawnPosition = _spawnPositions[i];
            if (Vector3.Distance(playerPosition, spawnPosition.transform.position) > 50f) {
                if (i > 0)
                {
                    _spawnPositions.RemoveRange(0, i);
                }
                break;
            };
            CreateZombie(spawnPosition);
            if (i >= count - 1) {
                _spawnPositions.Clear();
            }
        }
    }
    void CreateZombie(SpawnPosition spawnPosition) {
       ZombieController zombie= Instantiate(zombiePrefab);
        zombie.Init(spawnPosition, player, gameController);
        zombie.RemoveEvent += RemoveZombie;
        gameController.AddZombie(zombie);
    }

    void RemoveZombie(ZombieController zombie) {
        Destroy(zombie.gameObject);
        gameController.RemoveZombie(zombie);
    }
}
