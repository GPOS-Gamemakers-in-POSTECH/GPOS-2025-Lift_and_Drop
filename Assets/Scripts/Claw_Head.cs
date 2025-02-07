using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw_Head : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = speed * Time.fixedDeltaTime * inputVec.normalized;
        _rigidbody.MovePosition(_rigidbody.position + nextVec);
    }
}