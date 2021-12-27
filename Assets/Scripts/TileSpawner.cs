using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum Direction
{
    Forward,
    Left,
    Right
}
public class TileSpawner : MonoBehaviour
{
    
    
    public GameObject[] TilePrefabs;
    public GameObject BlockPrefab;
    public GameObject CoinSpawnerGo;    
    private Transform playerTransform;
    public List<GameObject> tileList;
    public List<GameObject> blockList;   
    private Direction goingDirection = Direction.Forward;
    private float spawnZ=0;
    private float spawnX=0;
    private int totalTilesOnScreen = 8;

    private float tileSizeZ;
    private float tileSizeX;

    // Start is called before the first frame update
    void Start()
    {
            
        Collider tileCollider;
        playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        tileCollider = TilePrefabs[0].GetComponent<Collider>();
        tileSizeZ = tileCollider.bounds.size.z/2;
        Debug.Log(tileSizeZ);
        spawnZ = tileSizeZ;
        
        for (int i=0 ; i<totalTilesOnScreen ; i++)
        {
            SpawnTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( tileList.Last().transform.localPosition.z - playerTransform.localPosition.z < 50f)
        {
            SpawnTile();
            
        }
        if(playerTransform.localPosition.z - tileList[0].transform.localPosition.z > 50f)
        {
            DestoryFirstTile();
        } 
            
    }

    private void SpawnTile()
    {
        int index = 0;
        switch(goingDirection)
        {
            case Direction.Forward:
                int[] indexArr = new int[2]{2, 3};
                int rnd = Random.Range(0,100);
                if(rnd > 80)
                {
                    index = indexArr[Random.Range(0,2)];                    
                }
                else
                {
                    if(rnd<10)
                        index = 6;
                    else 
                        index = 0;
                }
                    
                break;

            case Direction.Left:
                rnd = Random.Range(0,100);
                if(rnd > 60)
                {
                    index = 5;
                }
                else
                {
                    if(rnd <5)
                        index = 7;
                    else  
                        index = 1;
                }
                    
                break;

            case Direction.Right:
                rnd = Random.Range(0,100);
                if(rnd > 60)
                {
                    index = 4;
                }
                else
                {
                    if(rnd <5)
                        index = 7;
                    else  
                        index = 1;
                }
                break;

            
        }
        Debug.Log("spawn tile");
        GameObject go;
        Collider tileCollider;
        go = Instantiate(TilePrefabs[index]) as GameObject;
        go.transform.parent = transform;
        tileCollider = tileList.Last().GetComponent<Collider>();
        switch(index)
        {
            case 0: //ws
                spawnZ += tileCollider.bounds.size.z/2;  
                go.transform.position = new Vector3(spawnX, 0, spawnZ); 
                tileCollider = go.GetComponent<Collider>();
                spawnZ += tileCollider.bounds.size.z/2;
                goingDirection = Direction.Forward;
                break;

            case 1: //ad
                if(goingDirection == Direction.Right)
                {
                    spawnX += tileCollider.bounds.size.x/2;  
                    go.transform.position = new Vector3(spawnX, 0, spawnZ); 
                    tileCollider = go.GetComponent<Collider>();
                    spawnX += tileCollider.bounds.size.x/2;
                }
                else
                {
                    spawnX -= tileCollider.bounds.size.x/2;  
                    go.transform.position = new Vector3(spawnX, 0, spawnZ); 
                    tileCollider = go.GetComponent<Collider>();
                    spawnX -= tileCollider.bounds.size.x/2;
                }
                
                break;

            case 2: //wd
                spawnZ += (tileCollider.bounds.size.z/2 + 1.5f);  
                spawnX += 0.415f;  
                go.transform.position = new Vector3(spawnX, 0, spawnZ); 
                tileCollider = go.GetComponent<Collider>();
                spawnX += tileCollider.bounds.size.x/2 - 1.665f;
                spawnZ += 0.415f;
                goingDirection = Direction.Right;
                break;

            case 3: //wa
                spawnZ += (tileCollider.bounds.size.z/2 + 1.5f);
                spawnX -= 0.415f;  
                go.transform.position = new Vector3(spawnX, 0, spawnZ); 
                tileCollider = go.GetComponent<Collider>();
                spawnX -= tileCollider.bounds.size.x/2 - 1.665f;
                spawnZ += 0.415f;
                goingDirection = Direction.Left;
                break;

            case 4: //dw
                spawnX += tileCollider.bounds.size.x/2+1.6f;  
                spawnZ += 0.415f; 
                go.transform.position = new Vector3(spawnX, 0, spawnZ); 
                tileCollider = go.GetComponent<Collider>();
                spawnZ += tileCollider.bounds.size.z/2 - 1.665f;
                spawnX += 0.415f;
                goingDirection = Direction.Forward;
                break;

            case 5: //aw
                spawnX -= tileCollider.bounds.size.x/2+1.6f; 
                spawnZ += 0.415f; 
                go.transform.position = new Vector3(spawnX, 0, spawnZ); 
                tileCollider = go.GetComponent<Collider>();
                spawnZ += tileCollider.bounds.size.z/2 - 1.665f;
                spawnX -= 0.415f;
                goingDirection = Direction.Forward;
                break;
            case 6: //sw
                
                go.transform.position = new Vector3(spawnX, 0, spawnZ);                 
                goingDirection = Direction.Forward;
                GameObject block = Instantiate(BlockPrefab) as GameObject;
                block.transform.parent = go.transform;
                Vector3 myPosition = go.transform.position;
                myPosition.y = 1f;
                block.transform.position = myPosition;
                blockList.Add(block);
                tileCollider = go.GetComponent<Collider>();
                spawnZ += tileCollider.bounds.size.z/2;
                break;

            case 7: //ad
                if(goingDirection == Direction.Right)
                {
                     
                    go.transform.position = new Vector3(spawnX, 0, spawnZ); 
                    
                    block = Instantiate(BlockPrefab) as GameObject;
                    block.transform.parent = go.transform;
                    myPosition = go.transform.position;
                    myPosition.y = 1f;
                    block.transform.position = myPosition;
                    block.transform.Rotate(0, 90f, 0);
                    blockList.Add(block);
                    tileCollider = go.GetComponent<Collider>();
                    spawnX += tileCollider.bounds.size.x/2;
                }
                else
                {
                    
                    go.transform.position = new Vector3(spawnX, 0, spawnZ); 
                    
                    block = Instantiate(BlockPrefab) as GameObject;
                    block.transform.parent = go.transform;
                    myPosition = go.transform.position;
                    myPosition.y = 1f;
                    block.transform.position = myPosition;
                    block.transform.Rotate(0, 90f, 0);
                    blockList.Add(block);
                    tileCollider = go.GetComponent<Collider>();
                    spawnX -= tileCollider.bounds.size.x/2;
                }                
                break;
            
        }
        tileList.Add(go);
        int rng = Random.Range(0,100);
        if(rng <= 5)
        {
            DestoryLastTile();
            return;
        }
        
        if(index >=6)
            return;
        CoinSpawnerGo.GetComponent<CoinSpawner>().SpawnCoin(go.transform.localPosition.x,2.0f, go.transform.localPosition.z);
    }
    
    void DestoryFirstTile()
    {
        Destroy(tileList[0]);
        tileList.RemoveAt(0);
        Debug.Log("destroy first tile");
    }
    void DestoryLastTile()
    {
        Destroy(tileList.Last());
        tileList.RemoveAt(tileList.Count()-1);
        Debug.Log("destroy last tile");
    }
}
