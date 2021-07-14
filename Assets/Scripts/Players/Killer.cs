using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    private Player_Sample m_Player;
    private bool m_Frozen;

    public bool Frozen
    {
        get => m_Frozen;
        set { m_Frozen = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーが触れてきたらプレイヤーをkillする
        if(collision.TryGetComponent<Player_Sample>(out m_Player) && !Frozen)
        {
            StartCoroutine(m_Player.KillPlayer());
            StartCoroutine(Frost());
        }
    }

    private IEnumerator Frost()
    {
        Frozen = true;
        yield return new WaitForSeconds(0.2f);
        Frozen = false;
    }
}
