using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    [Header("SpawnTileMap Option")]
    public Tilemap tileMap;
    public float respawnCooldown = 2f;
    public int monsterCount = 10;

    [Header("Player Spawn")]
    public GameObject playerPrefab;

    [Header("Monster Spawn")]
    public GameObject MonsterPrefab;

    private List<Vector3> availablePlaces;
    private List<Vector3> Placed = new List<Vector3>();
    private void Start() {
        FindLocationsOfTiles();
        Vector2 randomPosition = new Vector2(availablePlaces[0].x+0.5f, availablePlaces[0].y + 0.5f);
        SpawnObject(playerPrefab, randomPosition);

        for(int i = 1; i <= monsterCount;)
        {
            int position = Random.Range(0, availablePlaces.Count);
            if(Placed.Contains(availablePlaces[position]) == false)
            {
                Vector2 randomMonsterPosition = new Vector2(availablePlaces[position].x+0.5f, availablePlaces[position].y + 0.5f);
                GameObject monster = SpawnObject(MonsterPrefab, randomMonsterPosition);
                Placed.Add(availablePlaces[position]);
                i++;

                Debug.Log("Spawn monster");
            }
            else
                continue;
            
            if(availablePlaces.Count < monsterCount){
                return;
            }
        }
    }

    private void FixedUpdate() {
        Debug.Log("monster current: " + GameObject.FindGameObjectsWithTag("Monster").Length);
        if(GameObject.FindGameObjectsWithTag("Monster").Length < monsterCount)
        {
            //respawn;
        }
    }

    public GameObject SpawnObject(GameObject prefab, Vector2 position){
        GameObject unit = PhotonNetwork.Instantiate(prefab.name, position, Quaternion.identity);
        return unit;
    }

    private void FindLocationsOfTiles()
    {
        availablePlaces = new List<Vector3>(); // create a new list of vectors by doing...
 
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
    }
}
