using JetBrains.Annotations;
using SingletonAudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;  

public class ClawMachineController : MonoBehaviour
{
    [SerializeField] private GameObject[] clawParts; // Claw_Head, Claw_Crane �� ��� ��ǰ�� ���⿡ �Ҵ�
    [SerializeField] private SpriteRenderer _headSpriteRenderer;

    [SerializeField] private float blinkAlpha = 0.2f;
    [SerializeField] private float defaultStealthAlpha = 0.5f;
    [SerializeField] private float normalAlpha = 1f;

    [SerializeField] private int stealthLayer = 14;

    private readonly Dictionary<GameObject, int> originalLayers = new();

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
            originalLayers[part] = part.layer;
            if (part.name == "Claw_Head")
            {
                _headSpriteRenderer = part.GetComponent<SpriteRenderer>();
            }
        }
    }

    public IEnumerator GetStealthItem()   //stealth ������ ȹ��   1. ��� ����, 2. ĳ���� layer�������� �浹 ���� ��ȭ, 3. ĳ���� ǥ�� ��ȭ, 4. fade_Effect
    {
        foreach(GameObject part in clawParts)
        {
            if(part != null)
            {
                part.layer = stealthLayer; // ����ȭ ���̾�� ����
                if (part.name == "Claw_Head")
                {
                    part.layer = 15;
                }   
            }
        }


        SetHeadSprite(HeadSprite.Smile);
        SetTransparency(defaultStealthAlpha);
        AudioManager.Instance.playOnlyStealth();
        
        yield return new WaitForSeconds(7f);

        float elapsed = 0f;
        while (elapsed < 3f)
        {
            if(elapsed>=2.1f)
            {
                SetTransparency(blinkAlpha);
                yield return new WaitForSeconds(0.075f); 

                SetTransparency(defaultStealthAlpha);
                yield return new WaitForSeconds(0.075f); 

                elapsed += 0.15f; // �����̴� �ð� �߰�SetTransparency
            }
            else
            {
                SetTransparency(blinkAlpha);
                yield return new WaitForSeconds(0.15f); 

                SetTransparency(defaultStealthAlpha);
                yield return new WaitForSeconds(0.15f); 

                elapsed += 0.3f; // �����̴� �ð� �߰�SetTransparency
            }
        }

        SetHeadSprite(HeadSprite.Normal);
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

    private void SetHeadSprite(HeadSprite headSprite) // 1= normal, 2 = smile, 3= happy 
    {
        _headSpriteRenderer.sprite = headSprite switch
        {
            HeadSprite.Normal => Resources.Load<Sprite>("Sprites/Claw/Group 3"),
            HeadSprite.Smile => Resources.Load<Sprite>("Sprites/Claw/Group 13"),
            HeadSprite.Happy => Resources.Load<Sprite>("Sprites/Claw/Group 12"),
            _ => throw new System.NotImplementedException()
        };
    }


    public void PlusMaxDepth()
    {
        foreach (GameObject part in clawParts)
        {
            if(part.CompareTag("Claw_Crane"))
            {
                Claw_Crane cc = part.GetComponent<Claw_Crane>();
                cc.maxLength = cc.maxLength + 10f;
            }
        }
    }

}
