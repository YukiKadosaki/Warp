//ÉfÅ[É^ÇÃèâä˙âª

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveTrigger : MonoBehaviour
{
    private enum GameStartType
    {
        newGame,
        loadGame
    }

    [SerializeField]
    private GameStartType gameStartType;

    private World m_World;

    private void Start()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        m_World = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //èâä˙âª
        if(collision.CompareTag("Player") || collision.CompareTag("Copy"))
        {

            if(gameStartType == GameStartType.newGame) {
                m_World.NewGame();
            }
            else
            {
                m_World.LoadGame();
            }
            
        }
    }
}
