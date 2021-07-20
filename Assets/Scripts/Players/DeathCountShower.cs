using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCountShower : MonoBehaviour
{
    private TowerManager m_TowerManager;
    private Text m_Text;

    // Start is called before the first frame update
    void Start()
    {
        m_TowerManager = GameObject.FindGameObjectWithTag("TowerManager").GetComponent<TowerManager>();
        m_Text = this.GetComponent<Text>();
    }

    private void Update()
    {
        m_Text.text = "" + m_TowerManager.DeathCount;
    }

}
