using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeShower : MonoBehaviour
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
        double time = m_TowerManager.SaveData.playTime;
        int hour = (int)time / 3600;
        int minute = (int)time / 60;
        int sec = (int)time % 60;
        m_Text.text = hour + ":" + minute + ":" + sec;
    }

}
