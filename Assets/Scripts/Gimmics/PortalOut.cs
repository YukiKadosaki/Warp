using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalOut : MonoBehaviour
{
    [SerializeField]
    private int m_PortalNumber;

    public int PortalNumber
    {
        get => m_PortalNumber;
    }
}
