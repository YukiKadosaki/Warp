//Player_Sample�Ɗ֘A

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpReflesher : MonoBehaviour
{
    //�v���C���[�ɐG�ꂽ�Ƃ��A�v���C���[�̃W�����v���񕜂�����
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
