//第三人稱尾隨攝影機
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; //跟隨目標
    Transform cam;//這個相機
    Vector3 pivotOffset = new Vector3(0f, 1.7f, 0f);
    Vector3 camOffset = new Vector3( 0f, 0f, -2.5f);//偏移座標
    Vector3 velocity = Vector3.zero; //目前速度，初始先歸零
    float rotateSpeed = 1.5f; //轉速
    float horizantolX = 0; //滑鼠指標水平
    float verticalY = 0; //滑鼠指標垂直
    float angleH = 0 ; //水平旋轉範圍
    float angleV = 0;//垂直旋轉範圍
    float horizantolSpeed = 5f;//水平旋轉速度
    float verticalSpeed = 5f;//垂直旋轉速度
    float maxAngleV = 50f;
    float minAngleV = -70f;
    float mydistanAway = 4.5f;
    //float camFOV; //FOV,視野範圍
    Quaternion camYRotation; //四元數Quaternion變數設定，用來設定相機旋轉相關
    Quaternion aimRotation;
    RaycastHit raycastHit;//碰撞判定變數
    Vector3 fwd = Vector3.zero;
    private void Awake()
    {
        cam = this.transform;//捕捉相機位置
        //camFOV = this.GetComponent<Camera>().fieldOfView;
        cam.position = target.position + Quaternion.identity * pivotOffset + Quaternion.identity * camOffset; //相機位置是目標位置+偏移座標*無旋轉穩定偏移
        cam.rotation = Quaternion.identity;//一開始避免攝影機跑位，在初始無旋轉的位置
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
        //攝影機會看向目標
        cam.LookAt(target);
        //捕捉滑鼠的水平與垂直數值
        horizantolX = Input.GetAxis("Mouse X");
        verticalY = Input.GetAxis("Mouse Y");
        //用mathf.clamp方法設定旋轉幅度範圍，用+=方式比較平滑呈現
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
        camYRotation = Quaternion.Euler(0, angleH, 0);//把四元數參數設定，尤拉角方法
        aimRotation = Quaternion.Euler(-angleV, angleH, 0);
        cam.rotation =  aimRotation ; //攝影機可旋轉設定
        //讓攝影機更新位置，持續跟隨目標周圍打轉著
        cam.position = target.position + aimRotation* camOffset + camYRotation * pivotOffset;
    }
    //碰撞判定，用raycast方法
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
