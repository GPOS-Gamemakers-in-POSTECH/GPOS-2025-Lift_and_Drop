using JetBrains.Annotations;
using SingletonAudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    public IEnumerator GetStealthItem()   //stealth 아이템 획득   1. 브금 변경, 2. 캐릭터 layer변경으로 충돌 판정 변화, 3. 캐릭터 표정 변화, 4. fade_Effect
    {
        float blinkAlpha = 0.2f;
        float defaultStealthAlpha = 0.5f;
        float normalAlpha = 1f;
        int stealthLayer = 14;
        Dictionary<GameObject, int> originalLayers = new Dictionary<GameObject, int>();

        foreach(GameObject part in clawParts)
        {
            if(part != null)
            {
                originalLayers[part] = part.layer; // 원래 레이어 저장
                part.layer = stealthLayer; // 투명화 레이어로 변경
                if (part.name == "Claw_Head")
                {
                    part.layer = 15;
                }   
            }
        }


        SetHeadSprite(2);
        SetTransparency(defaultStealthAlpha);
        AudioManager.Instance.playOnlyStealth();
        
        yield return new WaitForSeconds(7f);

        float elapsed = 0f;
        while (elapsed < 3f)
        {
            if(elapsed>=2.1f)
            {
                SetTransparency(blinkAlpha);
                yield return new WaitForSeconds(0.075f); // 0.2초 동안 낮은 투명도 유지

                SetTransparency(defaultStealthAlpha);
                yield return new WaitForSeconds(0.075f); // 0.2초 동안 원래 투명도 유지

                elapsed += 0.15f; // 깜빡이는 시간 추가SetTransparency
            }
            else
            {
                SetTransparency(blinkAlpha);
                yield return new WaitForSeconds(0.15f); // 0.2초 동안 낮은 투명도 유지

                SetTransparency(defaultStealthAlpha);
                yield return new WaitForSeconds(0.15f); // 0.2초 동안 원래 투명도 유지

                elapsed += 0.3f; // 깜빡이는 시간 추가SetTransparency
            }
        }

        SetHeadSprite(1);
        SetTransparency(normalAlpha);
        AudioManager.Instance.playOnlyNormal();

        foreach (GameObject part in clawParts)
        {
            if (part != null && originalLayers.ContainsKey(part))
            {
                part.layer = originalLayers[part]; // 원래 레이어로 되돌리기
            }
        }
    }

    // 투명도 조절 메서드
    private void SetTransparency(float alpha)
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

    private void SetHeadSprite(int a) // 1= normal, 2 = smile, 3= happy 
    {
        GameObject claw_Head = GameObject.Find("Claw_Head");
        SpriteRenderer sr = claw_Head.GetComponent<SpriteRenderer>();
        if (a == 1)
        {
            sr.sprite = Resources.Load<Sprite>("Sprites/Claw/Head_Normal");
        }
        else if (a == 2)
        {
            sr.sprite = Resources.Load<Sprite>("Sprites/Claw/Head_Smile");
        }
        else if (a == 3)
        {
            sr.sprite = Resources.Load<Sprite>("Sprites/Claw/Head_Happy");
        }

    }


    public void PlusMaxDepth()
    {
        foreach(GameObject part in clawParts)
        {
            if(part.CompareTag("Claw_Crane"))
            {
                Claw_Crane cc = part.GetComponent<Claw_Crane>();
                cc.maxLength = cc.maxLength + 10f;
            }
        }
    }

}
