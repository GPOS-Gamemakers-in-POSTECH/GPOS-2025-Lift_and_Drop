using SingletonAudioManager;
using SingletonGameManager;
using System.Collections;

using UnityEngine;

public class ClawPartTrigger : MonoBehaviour
{
    public ClawMachineController controller; // �θ� ��Ʈ�ѷ� ����

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ItemStealth")) // �±װ� "Item"�̸�
        {
            StartCoroutine(controller.GetStealthItem());
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ItemDepth")) // �±װ� "Item"�̸�
        {
            controller.PlusMaxDepth();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ItemInvincible")) // �±װ� "Item"�̸�
        {
            StartCoroutine(controller.GetInvincibleItem());
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (this.gameObject.layer == 20 || this.gameObject.layer == 21)  //������ ���
            {
                ;
            }
            else  //�ƴ� ���
            {
                StartCoroutine(controller.GetTrapped());
                Destroy(collision.gameObject);
            }
        }
    }
}
