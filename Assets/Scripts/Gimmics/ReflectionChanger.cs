using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionChanger : MonoBehaviour
{
    const float coolTime = 0.3f;

    private ChangeableReflection[] m_Reflections;//部屋に存在する変化するリフレクター
    private double time = 0;

    private void Start()
    {
        //部屋に存在する変化するリフレクターのオブジェクトを取得
        GameObject[] obj = GameObject.FindGameObjectsWithTag("ChangeReflector");

        //部屋に存在する変化するリフレクターのスクリプトを取得
        m_Reflections = new ChangeableReflection[obj.Length];
        int i = 0;
        foreach(GameObject o in obj)
        {
            m_Reflections[i] = o.GetComponent<ChangeableReflection>();
            i++;

        }

    }

    //クールタイム計測
    private void Update()
    {
        //時間を計る
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーが触れて、クールタイム中でないとき、リフレクターを変更
        if (collision.CompareTag("Player") && time > coolTime)
        {
            for(int i = 0;i < m_Reflections.Length; i++) {
                m_Reflections[i].ChangeMyDirection();
                time = 0;
            }
        }
    }

}
