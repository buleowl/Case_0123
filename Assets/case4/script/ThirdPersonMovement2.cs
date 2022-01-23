using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement2 : MonoBehaviour
{
    //public Transform player;
    public CharacterController controller;
    public Transform groundCheck;
    //public Rigidbody rb;
    public Vector3 moveDirection = Vector3.zero;
    private float jumpHeight = 3f;
    //private float defaultSpeed = 5f;
    private float speed = 3.5f;
    public float turnsmoothtime = 0.3f;
    float turnSmoothvelarsity;
    public Transform cam;
    //bool isJump = false;
    float _stateTime;
    float Factoer = 1;
    float gravity = -9.81f;
    float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;
    //public float checkradius = 0.2f;

    private void Start()
    {
        //player = this.transform;
        //rb = this.GetComponent<Rigidbody>();
    }
    void Update()
    {
       checkGround();

        movement();

    }

    void movement()
    {        
        
        //抓取鍵盤的按鍵設定
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //左右x軸，上下z軸，y軸設定0
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized; //單位向量:-1~1

        //跳躍
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //如果判定有移動
        if (direction.magnitude >= 0.1f)
        {
            //角色的轉向移動角度的目標設定，變動數值是x跟z軸之間轉，另加攝影機變數判定現在畫面的xz軸方向
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg+ cam.eulerAngles.y;
            //相似於math.lerp的方法，角色旋轉於目標角度，轉向的速度跟smoothtime有關，以目前的y軸為現在位置(旋轉用y軸)
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothvelarsity, turnsmoothtime);
            //實際旋轉設定，用尤拉角
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //位置向量設定
            Vector3 movdir = Quaternion.Euler(0f, targetangle, 0f)*Vector3.forward;
            //角色實際移動的參數添加
            velocity.y += gravity*Time.deltaTime;
            controller.Move(movdir.normalized * speed * Time.deltaTime);
            
            controller.Move(velocity * Time.deltaTime);
            // player.Translate( Vector3.forward * speed * Time.deltaTime );
        }



    }
    void checkGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        
        }

    }


}

