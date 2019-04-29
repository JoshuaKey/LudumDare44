using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public Enemy EnemyPrefab;
    public float EnemySpawnRate = 10.0f;
    public float EnemyMinSpawnRate = 1.0f;
    public float EnemySpawnIncreaseChance = .5f;

    public CircleCollider2D InnerBounds;
    public CircleCollider2D OuterBounds;

    private int enemySpawnAmo = 1;

    // Start is called before the first frame update
    void Start() {
        Spawn(enemySpawnAmo);
    }

    public void Spawn(int amo) {
        float dist = (Random.value * (OuterBounds.radius - InnerBounds.radius)) + InnerBounds.radius;
        Vector3 dir = Quaternion.Euler(0, 0, Random.value * 360) * Vector3.right;
        Vector3 pos = InnerBounds.transform.position + dir * dist;

        print("Enemy Spawned: " + pos);

        for(int i = 0; i < amo; i++) {
            Instantiate(EnemyPrefab, pos, Quaternion.identity, this.transform);
        }

        EnemySpawnRate -= Time.deltaTime;
        EnemySpawnRate = Mathf.Max(EnemyMinSpawnRate, EnemySpawnRate - Time.deltaTime);

        StartCoroutine(SpawnWait());
    }

    private IEnumerator SpawnWait() {
        yield return new WaitForSeconds(EnemySpawnRate);

        while (Player.GetHealth().IsDead()) {
            yield return null;
        }

        if(Random.value < EnemySpawnIncreaseChance) {
            enemySpawnAmo += 1;
        }    

        Spawn(enemySpawnAmo);
    }
}
