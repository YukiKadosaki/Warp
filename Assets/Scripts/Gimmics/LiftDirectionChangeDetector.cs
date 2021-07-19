using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDirectionChangeDetector : MonoBehaviour
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
        if (collision.CompareTag("Wall"))
        {
            m_Lift.ChangeDirection();
        }
    }
}
