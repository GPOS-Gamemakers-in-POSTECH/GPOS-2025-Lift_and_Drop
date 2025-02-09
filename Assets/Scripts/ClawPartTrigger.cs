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
        else if (other.CompareTag("OnSwitch")) // �±װ� "Item"�̸�
        {
            GameManager.Instance.OnSwitchReached();
            Destroy(other.gameObject);
        }
    }
}
