using UnityEngine;
using SingletonGameManager;
using System.Collections;
using SingletonAudioManager;
public class Doll : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool isTouchingLeft = false;
    private bool isTouchingRight = false;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); // Rigidbody2D ��������
    }

    void Update()
    {
        if (isTouchingLeft && isTouchingRight)
        {
            _rigidbody.gravityScale = 0; // �߷� ���� (���߿� ��)
        }
        else
        {
            _rigidbody.gravityScale = 1; // ���� �߷�
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
            AudioManager.Instance.StartClearSound();
            StartCoroutine(DollReachedGoal());
        }
        else if (collision.gameObject.CompareTag("Quit"))
        {
            StartCoroutine(EndGame());
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
    IEnumerator DollReachedGoal()
    {
        yield return new WaitForSeconds(3);
        GameManager.Instance.OnDollReachedGoal();
        Destroy(this.gameObject);
    }
    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.QuitGame();
    }
}
