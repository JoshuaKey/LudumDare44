using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnBox : MonoBehaviour {

    public string EnemyTag = "Enemy";
    public string PlayerTag = "Player";
    public string BulletTag = "Bullet";

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag(BulletTag)) {
            print("Bullet Despawned: " + collision.name);
            Destroy(collision.gameObject); // Destory Bullet - Out of Range
        } else if (collision.CompareTag(EnemyTag)) {
            print("Enemy Despawned: " + collision.name);
            Destroy(collision.gameObject); // Destory Enemy - Despawn
        } else if (collision.CompareTag(PlayerTag)) {
            print("Player is outside Bounding Box");
            Debug.Break();
        } else {
            print(collision.name + " is outside Bounding Box");
        }
    }
}
