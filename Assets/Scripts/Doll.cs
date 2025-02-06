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
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ��������
    }

    void Update()
    {
        if (isTouchingLeft && isTouchingRight)
        {
            rb.gravityScale = 0; // �߷� ���� (���߿� ��)
        }
        else
        {
            rb.gravityScale = 1; // ���� �߷�
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            StartCoroutine(WaitForThreeSeconds());
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
    IEnumerator WaitForThreeSeconds()
    {
        // 10�� ���
        yield return new WaitForSeconds(3);
        // 10�ʰ� ������ ����Ǵ� �ڵ�
        Debug.Log("�����");
        GameManager.Instance.Clear();
    }
}
