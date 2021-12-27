using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    int maxAmountCoin = 8;
    List<GameObject> coinList;
    // Start is called before the first frame update
    void Start()
    {
        coinList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    public void SpawnCoin(float x, float y, float z)
    {
        Debug.Log("Spawn a coin");
        GameObject go;
        go = Instantiate(coinPrefab) as GameObject;
        go.transform.SetParent(gameObject.transform);
        go.transform.localPosition = new Vector3(x,y,z);
        go.tag = "coin";
        coinList.Add(go);
        if(coinList.Count>maxAmountCoin)
        {
            Destroy(coinList[0]);
            coinList.RemoveAt(0);
        }
    }
}
