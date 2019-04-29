using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Background : MonoBehaviour {

    [Header("Sprites")]
    public SpriteRenderer LeftCenterSprite;
    public SpriteRenderer CenterCenterSprite; // Row x Col
    public SpriteRenderer RightCenterSprite;

    public SpriteRenderer LeftUpSprite;
    public SpriteRenderer CenterUpSprite;
    public SpriteRenderer RightUpSprite;

    public SpriteRenderer LeftDownSprite;
    public SpriteRenderer CenterDownSprite;
    public SpriteRenderer RightDownSprite;

    [Header("Tile Size")]
    public float TileSize = 7.5f;

    private Vector2Int playerTile;
    private float halftile;

    // Start is called before the first frame update
    void Start() {
        halftile = TileSize / 2.0f;
        playerTile = GetTile(Player.GetPosition());
        //print("Player Tile " + playerTile);

        UpdateBackgroundPos();

        // Tile Size = 5
        //Assert.AreEqual(GetTile(new Vector3(0, 0, 0)), new Vector2Int(0, 0));
        //Assert.AreEqual(GetTile(new Vector3(2.4f, 0, 0)), new Vector2Int(0, 0));
        //Assert.AreEqual(GetTile(new Vector3(-2.4f, 0, 0)), new Vector2Int(0, 0));
        //Assert.AreEqual(GetTile(new Vector3(0, 2.4f, 0)), new Vector2Int(0, 0));
        //Assert.AreEqual(GetTile(new Vector3(0, -2.4f, 0)), new Vector2Int(0, 0));
        //Assert.AreEqual(GetTile(new Vector3(2.4f, 2.4f, 0)), new Vector2Int(0, 0));

        //Assert.AreEqual(GetTile(new Vector3(2.6f, 0, 0)), new Vector2Int(1, 0));
        //Assert.AreEqual(GetTile(new Vector3(2.6f, 2.6f, 0)), new Vector2Int(1, 1));
        //Assert.AreEqual(GetTile(new Vector3(-2.6f, 0, 0)), new Vector2Int(-1, 0));
        //Assert.AreEqual(GetTile(new Vector3(-2.6f, -2.6f, 0)), new Vector2Int(-1, -1));
    }

    // Update is called once per frame
    void Update() {
        Vector2Int tile = GetTile(Player.GetPosition());
        if(playerTile != tile) {
            playerTile = tile;
            //print(Player.GetPosition());
            //print("Player Tile " + playerTile);
            //print("Player Tile " + playerTile.x + " " + playerTile.y);
            UpdateBackgroundPos();
        }
    }

    public Vector2Int GetTile(Vector3 pos) {
        // Tile 5 - 5
        // 0, 0
        // 2.5, 2.5
        // 0, 0

        // 7.5, 0
        // 10, 2.5
        // 2, 0
        //print("Before Pos " + pos);
        pos.x += halftile;
        pos.y += halftile;
        //print("After Pos " + pos);

        Vector2Int tile = new Vector2Int(Mathf.FloorToInt(pos.x / TileSize), Mathf.FloorToInt(pos.y / TileSize));
        return tile;
    }

    public Vector3 GetPos(Vector2Int tile) {
        // Tile Size 5 - 5
        // 0, 0 -> 0, 0
        // -1, 0 -> -5, 0
        // 1, -1 -> 5, -5
        return new Vector3(tile.x * TileSize, tile.y * TileSize, 0.0f);
    }

    public void UpdateBackgroundPos() {
        Vector3 origin = GetPos(playerTile);
        //print("Tile Origin " + origin);

        LeftCenterSprite.transform.position = origin + new Vector3(-TileSize, 0.0f, 0.0f);
        CenterCenterSprite.transform.position = origin + Vector3.zero;
        RightCenterSprite.transform.position = origin + new Vector3(TileSize, 0.0f, 0.0f);

        LeftUpSprite.transform.position = origin + new Vector3(-TileSize, TileSize, 0.0f);
        CenterUpSprite.transform.position = origin + new Vector3(0.0f, TileSize, 0.0f);
        RightUpSprite.transform.position = origin + new Vector3(TileSize, TileSize, 0.0f);

        LeftDownSprite.transform.position = origin + new Vector3(-TileSize, -TileSize, 0.0f);
        CenterDownSprite.transform.position = origin + new Vector3(0.0f, -TileSize, 0.0f);
        RightDownSprite.transform.position = origin + new Vector3(TileSize, -TileSize, 0.0f);
    }
}
