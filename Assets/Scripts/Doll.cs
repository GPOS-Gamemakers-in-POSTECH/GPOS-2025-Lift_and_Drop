using UnityEngine;
using SingletonGameManager;
using System.Collections;
public class Doll : MonoBehaviour
{
    /*
    private bool isTouchingLeft = false;
    private bool isTouchingRight = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 가져오기
    }

    private void Update()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LeftClaw"))
        {
            isTouchingLeft = true;
        }
        else if (collision.gameObject.CompareTag("RightClaw"))
        {
            isTouchingRight = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LeftClaw"))
        {
            isTouchingLeft = false;
        }
        else if (collision.gameObject.CompareTag("RightClaw"))
        {
            isTouchingRight = false;
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            StartCoroutine(WaitForThreeSeconds());
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
