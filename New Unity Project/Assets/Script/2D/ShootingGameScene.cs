using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGameScene : MonoBehaviour
{
    private Spawner2D EnemySpawner = null;
    // Start is called before the first frame update
    void Start()
    {
        EnemySpawner = new Spawner2D("Assets/Prefabs/Enemy.prefab");
        EnemySpawner.Spawn(new Vector2(475, -170), new Vector2(475, 170), 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
