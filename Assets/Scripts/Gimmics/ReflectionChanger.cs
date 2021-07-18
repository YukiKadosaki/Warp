using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionChanger : MonoBehaviour
{
    const float coolTime = 0.3f;

    private ChangeableReflection[] m_Reflections;//�����ɑ��݂���ω����郊�t���N�^�[
    private double time = 0;

    private void Start()
    {
        //�����ɑ��݂���ω����郊�t���N�^�[�̃I�u�W�F�N�g���擾
        GameObject[] obj = GameObject.FindGameObjectsWithTag("ChangeReflector");

        //�����ɑ��݂���ω����郊�t���N�^�[�̃X�N���v�g���擾
        m_Reflections = new ChangeableReflection[obj.Length];
        int i = 0;
        foreach(GameObject o in obj)
        {
            m_Reflections[i] = o.GetComponent<ChangeableReflection>();
            i++;

        }

    }

    //�N�[���^�C���v��
    private void Update()
    {
        //���Ԃ��v��
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[���G��āA�N�[���^�C�����łȂ��Ƃ��A���t���N�^�[��ύX
        if (collision.CompareTag("Player") && time > coolTime)
        {
            for(int i = 0;i < m_Reflections.Length; i++) {
                m_Reflections[i].ChangeMyDirection();
                time = 0;
            }
        }
    }

}
