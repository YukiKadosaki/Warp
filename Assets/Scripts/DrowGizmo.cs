#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
/// <summary>
/// �M�Y�������
/// </summary>
public class DrowGizmo : MonoBehaviour
{

    [SerializeField]
    enum MODE
    {
        CUBE,
        WIRECUBE,
        SPHERE,
        WIRESPHERE
    }

    [SerializeField, Header("�`�惂�[�h�I��")]
    MODE m_mode;

    [SerializeField, Range(0, 5), Header("�M�Y���̑傫��")]
    float m_gizmoSize = 0.3f;

    [SerializeField, Header("�M�Y���̐F")]
    Color m_gizmoColor = new Color(1f, 0, 0, 0.3f);

    void OnDrawGizmos()
    {
        Gizmos.color = m_gizmoColor;

        Vector3 thisObjPos = this.gameObject.transform.position;
        switch (m_mode)
        {
            case MODE.CUBE:
                Gizmos.DrawCube(thisObjPos, Vector3.one * m_gizmoSize);
                break;

            case MODE.SPHERE:
                Gizmos.DrawSphere(thisObjPos, m_gizmoSize);
                break;

            case MODE.WIRECUBE:
                Gizmos.DrawWireCube(thisObjPos, Vector3.one * m_gizmoSize);
                break;

            case MODE.WIRESPHERE:
                Gizmos.DrawWireSphere(thisObjPos, m_gizmoSize);
                break;
        }

    }

    [CustomEditor(typeof(DrowGizmo))]
    public class CustomWindow : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.HelpBox("�M�Y����Edit�T�|�[�g", MessageType.Info);
            }
            EditorGUILayout.EndVertical();

            base.OnInspectorGUI();
        }
    }
}
#endif