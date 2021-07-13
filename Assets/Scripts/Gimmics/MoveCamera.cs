using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private const float moveTime = 1;
    private Transform m_transform;
    private Vector3 m_startPosition;
    private bool m_IsMoving = false;//�J�������ړ������ǂ���

    public Vector3 StartPosition
    {
        get => m_startPosition;
        set { m_startPosition = value; }
    }

    public bool IsMoving
    {
        get => m_IsMoving;
        set { m_IsMoving = value; }
    }

    private void Start()
    {
        m_transform = transform;//transform�̃L���b�V��
    }

    public IEnumerator MoveToDestination(Vector3 destination)
    {
        //�ړ����̓J�����̈ړ���������Ȃ�
        if (!IsMoving)
        {
            IsMoving = true;
            Vector3 distanceVector = destination - m_transform.localPosition;

            float time;
            for (time = 0; time < moveTime; time += Time.deltaTime)
            {
                m_transform.localPosition += distanceVector * Time.deltaTime / moveTime;
                yield return null;
            }
            m_transform.localPosition = destination;
            IsMoving = false;
        }
    }

    public void DebugLog()
    {
        Debug.Log("DEBUG");
    }
}
