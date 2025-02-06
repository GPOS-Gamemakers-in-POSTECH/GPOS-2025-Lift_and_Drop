using UnityEngine;

public class ClawMachineController : MonoBehaviour
{
    public GameObject[] clawParts; // Claw_Head, Claw_Crane �� ��� ��ǰ�� ���⿡ �Ҵ�

    void Start()
    {
        if (clawParts.Length == 0)
        {
            Debug.LogWarning("Claw parts are not assigned!");
        }
        foreach (GameObject part in clawParts)
        {
            ClawPartTrigger trigger = part.AddComponent<ClawPartTrigger>(); // �������� Trigger ��ũ��Ʈ �߰�
            trigger.controller = this; // ClawMachineController ����
        }
    }

    // ���� ���� �޼���
    public void SetTransparency(float alpha)
    {
        foreach (GameObject part in clawParts)
        {
            SpriteRenderer sr = part.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color color = sr.color;
                color.a = Mathf.Clamp(alpha, 0f, 1f); // 0 = ���� ����, 1 = ������
                sr.color = color;
            }
        }
    }
}
