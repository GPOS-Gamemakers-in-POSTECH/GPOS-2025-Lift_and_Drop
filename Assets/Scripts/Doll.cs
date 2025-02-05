using UnityEngine;
using SingletonGameManager;
using System.Collections;
public class Doll : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isTouchingLeft = false;
    private bool isTouchingRight = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 가져오기
    }

    void Update()
    {
        if (isTouchingLeft && isTouchingRight)
        {
            rb.gravityScale = 0; // 중력 제거 (공중에 뜸)
        }
        else
        {
            rb.gravityScale = 1; // 정상 중력
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftClaw"))
        {
            isTouchingLeft = true;
            Debug.Log("왼");
        }
        else if (collision.CompareTag("RightClaw"))
        {
            isTouchingRight = true;
            Debug.Log("오른");
        }
        else if (collision.CompareTag("Goal"))
        {
            StartCoroutine(WaitForThreeSeconds());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftClaw"))
        {
            isTouchingLeft = false;
        }
        else if (collision.CompareTag("RightClaw"))
        {
            isTouchingRight = false;
        }
    }
    IEnumerator WaitForThreeSeconds()
    {
        // 10초 대기
        yield return new WaitForSeconds(3);
        // 10초가 지나면 실행되는 코드
        Debug.Log("들어갔어요");
        GameManager.Instance.Clear();
    }
}
