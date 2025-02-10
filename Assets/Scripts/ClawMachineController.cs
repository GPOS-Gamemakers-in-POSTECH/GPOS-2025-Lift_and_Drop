using SingletonAudioManager;
using SingletonGameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMachineController : MonoBehaviour
{
    [SerializeField] private GameObject[] clawParts; // Claw_Head, Claw_Crane 등 모든 부품을 여기에 할당
    [SerializeField] private SpriteRenderer _headSpriteRenderer;

    [SerializeField] private float blinkAlpha = 0.2f;
    [SerializeField] private float defaultStealthAlpha = 0.5f;
    [SerializeField] private float normalAlpha = 1f;

    [SerializeField] private int stealthLayer = 14;
    [SerializeField] private int invincibleLayer = 20;
    private readonly Dictionary<GameObject, int> originalLayers = new();
    private Coroutine currentCoroutine;
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
            originalLayers[part] = part.layer;
            if (part.name == "Claw_Head")
            {
                _headSpriteRenderer = part.GetComponent<SpriteRenderer>();
            }
        }
    }
    public IEnumerator StealthRoutine()
    {
        foreach (GameObject part in clawParts)
        {
            if (part != null)
            {
                part.layer = stealthLayer; // 투명화 레이어로 변경
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
            if (elapsed >= 2.1f)
            {
                SetTransparency(blinkAlpha);
                yield return new WaitForSeconds(0.075f);

                SetTransparency(defaultStealthAlpha);
                yield return new WaitForSeconds(0.075f);

                elapsed += 0.15f; // 깜빡이는 시간 추가SetTransparency
            }
            else
            {
                SetTransparency(blinkAlpha);
                yield return new WaitForSeconds(0.15f);

                SetTransparency(defaultStealthAlpha);
                yield return new WaitForSeconds(0.15f);

                elapsed += 0.3f; // 깜빡이는 시간 추가SetTransparency
            }
        }

        SetHeadSprite(HeadSprite.Normal);
        SetTransparency(normalAlpha);
        AudioManager.Instance.playOnlyNormal();

        foreach (GameObject part in clawParts)
        {
            if (part != null && originalLayers.ContainsKey(part))
            {
                part.layer = originalLayers[part]; // 원래 레이어로 되돌리기
            }
        }
        currentCoroutine = null;
    }
    public IEnumerator GetStealthItem()   //stealth 아이템 획득   1. 브금 변경, 2. 캐릭터 layer변경으로 충돌 판정 변화, 3. 캐릭터 표정 변화, 4. fade_Effect
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            SetNormalState();
            currentCoroutine = null;
        }

        // 새로운 코루틴 시작
        currentCoroutine = StartCoroutine(StealthRoutine());
        yield break;
    }
    public IEnumerator InvincibleRoutine()
    {
        foreach (GameObject part in clawParts)
        {
            if (part != null)
            {
                part.layer = invincibleLayer; // 투명화 레이어로 변경
                if (part.name == "Claw_Head")
                {
                    part.layer = 21;
                }
            }
        }


        SetHeadSprite(HeadSprite.Happy);
        AudioManager.Instance.playOnlyInvincible();

        yield return new WaitForSeconds(7f);

        float elapsed = 0f;
        while (elapsed < 3f)
        {
            if (elapsed >= 2.1f)
            {
                SetTransparency(blinkAlpha);
                yield return new WaitForSeconds(0.075f);

                SetTransparency(normalAlpha);
                yield return new WaitForSeconds(0.075f);

                elapsed += 0.15f; // 깜빡이는 시간 추가SetTransparency
            }
            else
            {
                SetTransparency(blinkAlpha);
                yield return new WaitForSeconds(0.15f);

                SetTransparency(normalAlpha);
                yield return new WaitForSeconds(0.15f);

                elapsed += 0.3f; // 깜빡이는 시간 추가SetTransparency
            }
        }

        SetHeadSprite(HeadSprite.Normal);
        SetTransparency(normalAlpha);
        AudioManager.Instance.playOnlyNormal();

        foreach (GameObject part in clawParts)
        {
            if (part != null && originalLayers.ContainsKey(part))
            {
                part.layer = originalLayers[part]; // 원래 레이어로 되돌리기
            }
        }
        currentCoroutine = null;
    }
    public IEnumerator GetInvincibleItem()   //invincible 아이템 획득   1. 브금 변경, 2. 캐릭터 layer변경으로 충돌 판정 변화, 3. 캐릭터 표정 변화, 4. fade_Effect
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            SetNormalState();
            currentCoroutine = null;
        }

        // 새로운 코루틴 시작
        currentCoroutine = StartCoroutine(InvincibleRoutine());
        yield break;
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

    private void SetHeadSprite(HeadSprite headSprite) // 1= normal, 2 = smile, 3= happy 
    {
        _headSpriteRenderer.sprite = headSprite switch
        {
            HeadSprite.Normal => Resources.Load<Sprite>("Sprites/Claw/Group 3"),
            HeadSprite.Smile => Resources.Load<Sprite>("Sprites/Claw/Group 13"),
            HeadSprite.Happy => Resources.Load<Sprite>("Sprites/Claw/Group 12"),
            HeadSprite.Trapped => Resources.Load<Sprite>("Sprites/Claw/Group 8"),
            _ => throw new System.NotImplementedException()
        };
    }
    private void SetNormalState()
    {
        SetHeadSprite(HeadSprite.Normal);
        SetTransparency(1.0f);
    }

    public void PlusMaxDepth()
    {
        foreach (GameObject part in clawParts)
        {
            if (part.CompareTag("Claw_Crane"))
            {
                Claw_Crane cc = part.GetComponent<Claw_Crane>();
                if (cc.originalmaxLength == cc.maxLength)
                {
                    cc.maxLength += 10f;
                }
            }
        }
    }

    public IEnumerator GetTrapped()
    {
        SetHeadSprite(HeadSprite.Trapped);
        GameManager.Instance.StopGame();
        AudioManager.Instance.StopBgm();
        yield return new WaitForSecondsRealtime(3);
        GameManager.Instance.RestartGame();
        GameManager.Instance.RestartScene(GameManager.currentSceneName);
    }
}
