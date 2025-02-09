using SingletonAudioManager;
using SingletonGameManager;
using System.Collections;

using UnityEngine;

public class ClawPartTrigger : MonoBehaviour
{
    public ClawMachineController controller; // 부모 컨트롤러 연결

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ItemStealth")) // 태그가 "Item"이면
        {
            StartCoroutine(controller.GetStealthItem());
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ItemDepth")) // 태그가 "Item"이면
        {
            controller.PlusMaxDepth();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ItemInvincible")) // 태그가 "Item"이면
        {
            StartCoroutine(controller.GetInvincibleItem());
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (this.gameObject.layer == 20 || this.gameObject.layer == 21)  //무적인 경우
            {
                ;
            }
            else  //아닌 경우
            {
                StartCoroutine(controller.GetTrapped());
                Destroy(collision.gameObject);
            }
        }
    }
}
