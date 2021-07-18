using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransParencyBlock : MonoBehaviour
{
    private enum State
    {
        transparent,
        nonTransparent
    }

    private SpriteRenderer m_SpriteRenderer;
    [SerializeField]
    private State m_State;
    
    private Sprite m_Sprite;
    private Collider2D m_Collider;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
        m_Sprite= Resources.Load<Sprite>("Sprites/Transparent");
        m_SpriteRenderer.sprite = m_Sprite; //エディタ画面では違うスプライトにするため
        m_Collider = this.GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //透明か透明じゃないかを変更する
            if (m_State == State.nonTransparent)
            {
                m_State = State.transparent;
            }
            else
            {
                m_State = State.nonTransparent;
            }
        }

        //実体化しているとき
        if (m_State == State.nonTransparent)
        {
            m_SpriteRenderer.color = new Color(1, 1, 1, 1);
            m_Collider.isTrigger = false;
        }
        else//透明化しているとき
        {
            m_SpriteRenderer.color = new Color(1, 1, 1, 0.3f);
            m_Collider.isTrigger = true;
        }
    }

    //潰されたときの死亡判定
    private void OnTriggerStay2D(Collider2D collision)
    {
        Player_Sample p;
        if(Input.GetKeyDown(KeyCode.Z) && collision.TryGetComponent(out p))
        {
            Debug.Log("KILL");
            StartCoroutine(p.KillPlayer());
        }
    }



}
