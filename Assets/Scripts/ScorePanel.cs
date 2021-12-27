using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    public Text TimeText;
    public Text CoinText;
    public int time;
    public int coin;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        coin = 0;
        
        InvokeRepeating("TimeIncreament", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        TimeText.text= time.ToString();
        CoinText.text = coin.ToString();
    }
    void CoinIncreament()
    {
        coin++;
    }
    void TimeIncreament()
    {
        time++;
    }
}
