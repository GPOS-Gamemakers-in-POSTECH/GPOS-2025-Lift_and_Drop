using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw_Head : MonoBehaviour
{
    public Vector2 inputVec;
    Rigidbody2D rb;
    public float speed;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxis("Horizontal");
    }
    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * Time.fixedDeltaTime*speed;
        rb.MovePosition(rb.position+nextVec);
    }
}
