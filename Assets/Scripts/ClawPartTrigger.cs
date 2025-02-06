using SingletonAudioManager;
using UnityEngine;

public class ClawPartTrigger : MonoBehaviour
{
    public ClawMachineController controller; // 부모 컨트롤러 연결

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item")) // 태그가 "Item"이면
        {
            Debug.Log("닿음");
            getStealthItem();
        }
    }

    void getStealthItem()
    {
        controller.SetTransparency(0.5f);
        AudioManager.Instance.playOnlyStealth();
    }
}
