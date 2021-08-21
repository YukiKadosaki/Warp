//Player_Sampleと関連

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpReflesher : MonoBehaviour
{
    //プレイヤーに触れたとき、プレイヤーのジャンプを回復させる
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Sample player;
        if(collision.TryGetComponent<Player_Sample>(out player))
        {
            Debug.Log("!!!TOUCH!!!");
            player.CanSecondJump = true;
            //player.IsSecondJumping = false;
            Destroy(this.gameObject);
        }
    }
}
