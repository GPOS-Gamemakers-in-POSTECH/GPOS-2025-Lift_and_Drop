using UnityEngine;

public class Right_Pivot : MonoBehaviour
{
    public float rotationSpeed = 1f; // 집게 올라가는 속도
    private bool isMovingUp = false; // 집게가 올라가는 상태인지 확인하는 변수

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // 스페이스바를 누르면 집게가 올라감
            isMovingUp = true;
        }
        else
        {
            // 스페이스바를 떼면 집게가 내려감
            isMovingUp = false;
        }

        // 집게가 올라가는 상태라면
        if (isMovingUp)
        {
            transform.Rotate(0, 0, +rotationSpeed * Time.deltaTime);
        }
        else
        {
            // 내려가는 상태라면
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }
}
