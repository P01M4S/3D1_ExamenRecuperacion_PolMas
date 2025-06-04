using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

public int Coins = 0;
public Text coinsText;
public List<GameObject> enemisOnScrean;
public Enemy enemyScript;


void Start()
{
    coinsText.text = "Coins: " + Coins.ToString();
}

}
    if(Input.GetKeyDown(KeyCode.N))
    {
        foreach(GameObject enemy in enemisOnScrean)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.Death();
        }
    }



public void AddCoins()
{
    Coins++;
    coinsText.text = "Coins: " + Coins.ToString();
}



