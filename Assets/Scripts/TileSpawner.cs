using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject[] TilePrefabs;

    private Transform playerTransform;
    List<GameObject> tileList;    
    private float spawnZ=0;
    private float spawnX=0;
    private int totalTilesOnScreen = 8;

    private float tileSizeZ;
    private float tileSizeX;

    // Start is called before the first frame update
    void Start()
    {
        tileList  = new List<GameObject>();
        Collider tileCollider;
        playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        tileCollider = TilePrefabs[0].GetComponent<Collider>();
        tileSizeZ = tileCollider.bounds.size.z;
        spawnZ = tileSizeZ;
        for (int i=0 ; i<totalTilesOnScreen ; i++)
        {
            SpawnTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z - tileSizeZ > spawnZ -  tileSizeZ*totalTilesOnScreen)
        {
            SpawnTile();
            Destroy(tileList[0]);
            tileList.RemoveAt(0);
        }
    }

    private void SpawnTile(int index=0)
    {
        GameObject go;
        Collider tileCollider;
        go = Instantiate(TilePrefabs[index]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(spawnX, 0, spawnZ);
        tileCollider = go.GetComponent<Collider>();
        spawnZ += tileCollider.bounds.size.z;
        tileList.Add(go);
    }
    
}
