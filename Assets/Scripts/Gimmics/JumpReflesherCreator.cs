using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpReflesherCreator : MonoBehaviour
{
    private GameObject m_JumpReflesher;
    private bool m_Frozen;//�W�����v���t�����b�V���[�𐶐�����R���[�`�����쓮�����Ȃ����߂Ɏg��

    public bool Frozen
    {
        get => m_Frozen;
        set { m_Frozen = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_JumpReflesher = (GameObject)Resources.Load("Prefab/JumpReflesher");
        Instantiate(m_JumpReflesher, this.transform.localPosition, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Sample p;
        if(collision.TryGetComponent<Player_Sample>(out p) && !Frozen)
        {
            StartCoroutine(CreateJumpReflesher());
            Frozen = true;
        }
    }

    //�W�����v���t�����b�V���[�𐶐����A�N�[���^�C��������
    private IEnumerator CreateJumpReflesher()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(m_JumpReflesher, this.transform.localPosition, Quaternion.identity);
        Frozen = false;
    }
}
