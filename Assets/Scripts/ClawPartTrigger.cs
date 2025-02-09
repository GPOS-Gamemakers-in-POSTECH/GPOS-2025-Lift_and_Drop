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
        else if (other.CompareTag("OnSwitch")) // 태그가 "Item"이면
        {
            GameManager.Instance.OnSwitchReached();
            Destroy(other.gameObject);
        }
    }
}
