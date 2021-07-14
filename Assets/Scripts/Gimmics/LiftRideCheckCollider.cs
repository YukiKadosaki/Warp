using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�v���C���[�����t�g�̎q���ɂ���
public class LiftRideCheckCollider : MonoBehaviour
{
    private Player_Sample m_Player;
    private Lift m_Lift;

    // Start is called before the first frame update
    void Start()
    {
        m_Lift = this.transform.parent.GetComponent<Lift>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Player�Ȃ�e�I�u�W�F�N�g�̎q���ɂ���
        if (collision.TryGetComponent<Player_Sample>(out m_Player)){
            float pflip = m_Player.transform.lossyScale.x;//�v���C���[�����]���Ă��邩�ǂ���    
            m_Player.gameObject.transform.parent = this.transform.parent;
            Vector3 scale = transform.parent.lossyScale;
            m_Player.transform.localScale = new Vector3(1f * pflip/ scale.x, 1f / scale.y, 1f / scale.z);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Player�Ȃ�e�I�u�W�F�N�g�̎q���ɂ���
        if (collision.TryGetComponent<Player_Sample>(out m_Player))
        {
            m_Player.gameObject.transform.parent = null;
            float pflip = m_Player.transform.lossyScale.x;//�v���C���[�����]���Ă��邩�ǂ���
            m_Player.gameObject.transform.localScale = new Vector3(pflip, 1, 1);
        }
    }
}
