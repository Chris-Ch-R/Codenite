using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [Header("SpawnTileMap Option")]
    public float respawnCooldown = 2f;
    public int monsterCount = 10;

    [Header("Player Spawn")]
    public Tilemap playerSpawnpoint;
    public GameObject playerPrefab;

    [Header("Monster Spawn")]
    public Tilemap monsterSpawnpoint;
    public GameObject MonsterPrefab;
    private void Start()
    {
        //spawn player by user id to index of spwanpoint
        List<Vector3> availablePlaces = FindLocationsOfTiles(playerSpawnpoint);
        int playerID = PhotonNetwork.LocalPlayer.ActorNumber;
        Vector2 randomPosition = new Vector2(availablePlaces[playerID - 1].x + 0.5f, availablePlaces[playerID - 1].y + 0.5f);
        SpawnObject(playerPrefab, randomPosition);

        if (PhotonNetwork.IsMasterClient)
        {
            List<Vector3> Placed = new List<Vector3>();
            availablePlaces = FindLocationsOfTiles(monsterSpawnpoint);
            for (int i = 1; i <= monsterCount;)
            {
                int position = Random.Range(0, availablePlaces.Count);
                if (Placed.Contains(availablePlaces[position]) == false)
                {
                    Vector2 randomMonsterPosition = new Vector2(availablePlaces[position].x + 0.5f, availablePlaces[position].y + 0.5f);
                    GameObject monster = SpawnObject(MonsterPrefab, randomMonsterPosition);
                    Placed.Add(availablePlaces[position]);
                    i++;

                    Debug.Log("Spawn monster");
                }
                else
                    continue;

                if (availablePlaces.Count < monsterCount)
                {
                    return;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Debug.Log("monster current: " + GameObject.FindGameObjectsWithTag("Monster").Length);
    }

    public GameObject SpawnObject(GameObject prefab, Vector2 position)
    {
        GameObject unit = PhotonNetwork.Instantiate(prefab.name, position, Quaternion.identity);

        return unit;
    }

    private List<Vector3> FindLocationsOfTiles(Tilemap tileMap)
    {
        List<Vector3> availablePlaces = new List<Vector3>(); // create a new list of vectors by doing...
        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++) // scan from left to right for tiles
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++) // scan from bottom to top for tiles
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)tileMap.transform.position.y); // if you find a tile, record its position on the tile map grid
                Vector3 place = tileMap.CellToWorld(localPlace); // convert this tile map grid coords to local space coords
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place);
                }
            }
        }
        return availablePlaces;
    }
}
