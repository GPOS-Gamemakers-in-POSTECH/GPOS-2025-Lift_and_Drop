using UnityEngine;

public class ClawMachineController : MonoBehaviour
{
    public GameObject[] clawParts; // Claw_Head, Claw_Crane 등 모든 부품을 여기에 할당

    void Start()
    {
        if (clawParts.Length == 0)
        {
            Debug.LogWarning("Claw parts are not assigned!");
        }
        foreach (GameObject part in clawParts)
        {
            ClawPartTrigger trigger = part.AddComponent<ClawPartTrigger>(); // 동적으로 Trigger 스크립트 추가
            trigger.controller = this; // ClawMachineController 연결
        }
    }

    // 투명도 조절 메서드
    public void SetTransparency(float alpha)
    {
        foreach (GameObject part in clawParts)
        {
            SpriteRenderer sr = part.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color color = sr.color;
                color.a = Mathf.Clamp(alpha, 0f, 1f); // 0 = 완전 투명, 1 = 불투명
                sr.color = color;
            }
        }
    }
}
