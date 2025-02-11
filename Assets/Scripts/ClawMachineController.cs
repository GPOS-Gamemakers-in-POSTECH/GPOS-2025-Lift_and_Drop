using SingletonAudioManager;
using SingletonGameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMachineController : MonoBehaviour
{
    [SerializeField] private GameObject[] clawParts; // Claw_Head, Claw_Crane �� ��� ��ǰ�� ���⿡ �Ҵ�
    [SerializeField] private SpriteRenderer _headSpriteRenderer;

    [SerializeField] private float blinkAlpha = 0.2f;
    [SerializeField] private float defaultStealthAlpha = 0.5f;
    [SerializeField] private float normalAlpha = 1f;

    [SerializeField] private int stealthLayer = 14;
    [SerializeField] private int invincibleLayer = 20;
    private readonly Dictionary<GameObject, int> originalLayers = new();
    private Coroutine currentCoroutine;

    private Rigidbody2D _clawHeartRb;
    bool hasMoved;
    void Start()
    {
        if (clawParts.Length == 0)
        {
            Debug.LogWarning("Claw parts are not assigned!");
        }
        foreach (GameObject part in clawParts)
        {
            ClawPartTrigger trigger = part.AddComponent<ClawPartTrigger>(); 
            trigger.controller = this; // ClawMachineController
            originalLayers[part] = part.layer;
            if (part.name == "Claw_Head")
            {
                _headSpriteRenderer = part.GetComponent<SpriteRenderer>();
            }
        }

        GameObject clawHeart = GameObject.Find("Claw_Heart");
        if (clawHeart != null)
        {
            _clawHeartRb = clawHeart.GetComponent<Rigidbody2D>();
        }
        else
        {
            Debug.LogError("Claw_Heart Not in this Scene");
        }
    }
    private void Update()
    {
        if (_clawHeartRb == null)
        {
            return; //
        }

        AudioManager.isMovingKeyPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        hasMoved = _clawHeartRb.linearVelocity.magnitude > 0.1f;
        if (AudioManager.isMovingKeyPressed && hasMoved)
        {
            AudioManager.sfxSources[1].volume = 0.5f;
            if (!AudioManager.isMoving)
            {
                AudioManager.Instance.StartMotorSound();
            }
            if (AudioManager.exitCoroutine != null)
            {
                StopCoroutine(AudioManager.exitCoroutine);
                AudioManager.exitCoroutine = null;
            }
        }
        else if (AudioManager.isMoving)
        {
            if (AudioManager.exitCoroutine == null)
            {
                AudioManager.exitCoroutine = StartCoroutine(AudioManager.Instance.WaitAndPlayExitSound());
            }
        }
    }
    public IEnumerator StealthRoutine()
    {
        foreach (GameObject part in clawParts)
        {
            if (part != null)
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
            if (elapsed >= 2.1f)
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
        currentCoroutine = null;
    }
    public IEnumerator GetStealthItem()   
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            SetNormalState();
            currentCoroutine = null;
        }

        // ���ο� �ڷ�ƾ ����
        currentCoroutine = StartCoroutine(StealthRoutine());
        yield break;
    }
    public IEnumerator InvincibleRoutine()
    {
        foreach (GameObject part in clawParts)
        {
            if (part != null)
            {
                part.layer = invincibleLayer; // ����ȭ ���̾�� ����
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

                elapsed += 0.15f; // �����̴� �ð� �߰�SetTransparency
            }
            else
            {
                SetTransparency(blinkAlpha);
                yield return new WaitForSeconds(0.15f);

                SetTransparency(normalAlpha);
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
        currentCoroutine = null;
    }
    public IEnumerator GetInvincibleItem()   
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            SetNormalState();
            currentCoroutine = null;
        }

        // ���ο� �ڷ�ƾ ����
        currentCoroutine = StartCoroutine(InvincibleRoutine());
        yield break;
    }

    // ������ ���� �޼���
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
