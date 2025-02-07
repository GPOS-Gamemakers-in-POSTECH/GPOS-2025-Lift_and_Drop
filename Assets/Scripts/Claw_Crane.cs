using UnityEngine;
using UnityEngine.UIElements;

public class Claw_Crane : MonoBehaviour
{
    public Vector2 inputVec;
    Rigidbody2D rb;
    public float speed;
    public float maxLength = 10f;
    float firstPostionY;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        firstPostionY = rb.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.y = Input.GetAxisRaw("Vertical");
        if (firstPostionY - rb.position.y >= maxLength && inputVec.y < 0)
        {
            inputVec.y = 0;  // ÇÏÇâ ÀÌµ¿À» ¸ØÃã
        }
    }
    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }
}
