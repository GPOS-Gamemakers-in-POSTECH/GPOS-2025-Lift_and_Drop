using SingletonAudioManager;
using UnityEngine;

public class ClawPartTrigger : MonoBehaviour
{
    public ClawMachineController controller; // �θ� ��Ʈ�ѷ� ����

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item")) // �±װ� "Item"�̸�
        {
            Debug.Log("����");
            getStealthItem();
        }
    }

    void getStealthItem()
    {
        controller.SetTransparency(0.5f);
        AudioManager.Instance.playOnlyStealth();
    }
}
