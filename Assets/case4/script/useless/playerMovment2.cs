using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovment2 : MonoBehaviour
{
    public CharacterController controller;
    float speed = 12f;
    float gravity = -9.81f;
    Vector3 velocity;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
    }

    void playerMovment()
    {
        float x = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move =transform.right * x + transform.forward * v;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
