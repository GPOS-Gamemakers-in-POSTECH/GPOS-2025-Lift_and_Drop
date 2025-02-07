using JetBrains.Annotations;
using SingletonAudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    public IEnumerator GetStealthItem()   //stealth ������ ȹ��   1. ��� ����, 2. ĳ���� layer�������� �浹 ���� ��ȭ, 3. ĳ���� ǥ�� ��ȭ, 4. fade_Effect
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
                originalLayers[part] = part.layer; // ���� ���̾� ����
                part.layer = stealthLayer; // ����ȭ ���̾�� ����
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
                yield return new WaitForSeconds(0.075f); // 0.2�� ���� ���� ���� ����

                SetTransparency(defaultStealthAlpha);
                yield return new WaitForSeconds(0.075f); // 0.2�� ���� ���� ���� ����

                elapsed += 0.15f; // �����̴� �ð� �߰�SetTransparency
            }
            else
            {
                SetTransparency(blinkAlpha);
                yield return new WaitForSeconds(0.15f); // 0.2�� ���� ���� ���� ����

                SetTransparency(defaultStealthAlpha);
                yield return new WaitForSeconds(0.15f); // 0.2�� ���� ���� ���� ����

                elapsed += 0.3f; // �����̴� �ð� �߰�SetTransparency
            }
        }

        SetHeadSprite(1);
        SetTransparency(normalAlpha);
        AudioManager.Instance.playOnlyNormal();

        foreach (GameObject part in clawParts)
        {
            if (part != null && originalLayers.ContainsKey(part))
            {
                part.layer = originalLayers[part]; // ���� ���̾�� �ǵ�����
            }
        }
    }

    // ���� ���� �޼���
    private void SetTransparency(float alpha)
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
