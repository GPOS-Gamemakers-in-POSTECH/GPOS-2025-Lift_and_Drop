using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw_Head : MonoBehaviour
{
    public Vector2 inputVec;
    Rigidbody2D _rigidbody;
    public float speed;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
    }
    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * Time.fixedDeltaTime*speed;
        _rigidbody.MovePosition(_rigidbody.position+nextVec);
    }
}
