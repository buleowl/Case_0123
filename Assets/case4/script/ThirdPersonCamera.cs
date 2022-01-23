//�ĤT�H�٧��H��v��
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; //���H�ؼ�
    Transform cam;//�o�Ӭ۾�
    Vector3 pivotOffset = new Vector3(0f, 1.7f, 0f);
    Vector3 camOffset = new Vector3( 0f, 0f, -2.5f);//�����y��
    Vector3 velocity = Vector3.zero; //�ثe�t�סA��l���k�s
    float rotateSpeed = 1.5f; //��t
    float horizantolX = 0; //�ƹ����Ф���
    float verticalY = 0; //�ƹ����Ы���
    float angleH = 0 ; //��������d��
    float angleV = 0;//��������d��
    float horizantolSpeed = 5f;//��������t��
    float verticalSpeed = 5f;//��������t��
    float maxAngleV = 50f;
    float minAngleV = -70f;
    float mydistanAway = 4.5f;
    //float camFOV; //FOV,�����d��
    Quaternion camYRotation; //�|����Quaternion�ܼƳ]�w�A�Ψӳ]�w�۾��������
    Quaternion aimRotation;
    RaycastHit raycastHit;//�I���P�w�ܼ�
    Vector3 fwd = Vector3.zero;
    private void Awake()
    {
        cam = this.transform;//�����۾���m
        //camFOV = this.GetComponent<Camera>().fieldOfView;
        cam.position = target.position + Quaternion.identity * pivotOffset + Quaternion.identity * camOffset; //�۾���m�O�ؼЦ�m+�����y��*�L����í�w����
        cam.rotation = Quaternion.identity;//�@�}�l�קK��v���]��A�b��l�L���઺��m
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         CameraMovement();
         raycastCollison();
    }

    void CameraMovement()
    {         
        //��v���|�ݦV�ؼ�
        cam.LookAt(target);
        //�����ƹ��������P�����ƭ�
        horizantolX = Input.GetAxis("Mouse X");
        verticalY = Input.GetAxis("Mouse Y");
        //��mathf.clamp��k�]�w����T�׽d��A��+=�覡������Ƨe�{
        angleH += Mathf.Clamp(horizantolX, -1, 1) * horizantolSpeed;
        angleV += Mathf.Clamp(verticalY, -1, 1) * verticalSpeed;
        if (angleV > maxAngleV)
        {
            angleV = maxAngleV;
        }
        else if (angleV < minAngleV)
        {
            angleV = minAngleV;
        }
        camYRotation = Quaternion.Euler(0, angleH, 0);//��|���ưѼƳ]�w�A�שԨ���k
        aimRotation = Quaternion.Euler(-angleV, angleH, 0);
        cam.rotation =  aimRotation ; //��v���i����]�w
        //����v����s��m�A������H�ؼЩP�����
        cam.position = target.position + aimRotation* camOffset + camYRotation * pivotOffset;
    }
    //�I���P�w�A��raycast��k
    void raycastCollison()
    {
        fwd = cam.transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(cam.position, fwd, out raycastHit, 0.1f))
        {
            float dis = raycastHit.distance;
            Vector3 correction = Vector3.Normalize(cam.TransformDirection(Vector3.back)) * dis*Time.deltaTime;
            cam.position += correction;
        }

        //if (Physics.Linecast(target.position + Vector3.up, cam.position, out raycastHit))
        //{
        //    string name = raycastHit.collider.gameObject.tag;
        //    if (name != "MainCamera")
        //    {
        //        float currentDistance = Vector3.Distance(raycastHit.point, target.position);
        //        if (currentDistance < mydistanAway)
        //        {
        //            cam.position = raycastHit.point;
        //        }
        //    }
        
        //}

    
    }
}
