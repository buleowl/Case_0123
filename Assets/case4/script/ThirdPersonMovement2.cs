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
        
        //�����L������]�w
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //���kx�b�A�W�Uz�b�Ay�b�]�w0
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized; //���V�q:-1~1

        //���D
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //�p�G�P�w������
        if (direction.magnitude >= 0.1f)
        {
            //���⪺��V���ʨ��ת��ؼг]�w�A�ܰʼƭȬOx��z�b������A�t�[��v���ܼƧP�w�{�b�e����xz�b��V
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg+ cam.eulerAngles.y;
            //�ۦ���math.lerp����k�A��������ؼШ��סA��V���t�׸�smoothtime�����A�H�ثe��y�b���{�b��m(�����y�b)
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothvelarsity, turnsmoothtime);
            //��ڱ���]�w�A�ΤשԨ�
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //��m�V�q�]�w
            Vector3 movdir = Quaternion.Euler(0f, targetangle, 0f)*Vector3.forward;
            //�����ڲ��ʪ��ѼƲK�[
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

