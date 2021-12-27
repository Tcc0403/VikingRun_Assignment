using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    public GameObject GameOverUI;

    public GameObject ScorePanelUI;
    
    public Text TimeText;
    public Text CoinText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.y < -1)
            Die();
    }

    void Die()
    {
        Time.timeScale = 0f;
        ScorePanelUI.SetActive(false);
        GameOverUI.SetActive(true);
        TimeText.text = gameObject.GetComponent<ScorePanel>().time.ToString();
        CoinText.text = gameObject.GetComponent<ScorePanel>().coin.ToString();
    }
    
}
