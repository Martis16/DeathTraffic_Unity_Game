using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public static bool isGameOver;

    private void Awake()
    {
        isGameOver = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
        }
    }

}
