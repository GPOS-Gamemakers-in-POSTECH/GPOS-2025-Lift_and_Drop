using UnityEngine;
using UnityEngine.UIElements;

public class Claw_Crane : MonoBehaviour
{
    public Vector2 inputVec;
    Rigidbody2D rb;
    public float speed;
    public float minY = 0f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.y = Input.GetAxisRaw("Vertical");
        if (rb.position.y <= minY && inputVec.y < 0)
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
