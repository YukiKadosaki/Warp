using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugSave : MonoBehaviour
{
    private World m_World;
    private TowerManager m_TowerManager;
    private Text m_Text;

    // Start is called before the first frame update
    void Start()
    {
        m_World = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
        var obj = GameObject.FindGameObjectWithTag("TowerManager");
        if (null != obj)
        {
            m_TowerManager = obj.GetComponent<TowerManager>();
        }
        m_Text = this.GetComponent<Text>();
    }

    private void Update()
    {
        if (null != m_World.LoadGame())
        {
            m_Text.text = "" + m_World.LoadGame().floorNumber;
        }
        if(null != m_TowerManager)
        {
            m_Text.text += " " + m_TowerManager.SaveData.floorNumber;
        }
    }
}
