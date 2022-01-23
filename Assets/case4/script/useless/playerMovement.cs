using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f; //重力
    CharacterController controller;
    private Vector3 velocity = Vector3.zero; //向量
    public Transform groundCheck;
    public float checkRadius = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        controller = transform.GetComponent<CharacterController>();

    }
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
            Movement();
    }
    void Movement()
    {
        var playerHorizaton = Input.GetAxis("Horizontal");
        var playerVertical = Input.GetAxis("Vertical");
        var playerMove = transform.forward * playerVertical+transform.right * playerHorizaton;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(playerMove * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
}
