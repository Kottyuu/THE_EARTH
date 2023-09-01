using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Camera : MonoBehaviour
{
    // カメラ回転変数
    public float cameraSensitive = 200f;
    public Transform target;
    public Vector2 pitchMinMax;
    public float smoothing = 0.12f;
    public Vector3 rotationSmoothVelocity;
    public Vector3 currentRotation;
    public Vector2 look;
    public float yaw;
    public float pitch;

    public float keep_yaw;

    // カメラ衝突時変数
    public float camDinMin = 0.5f;
    public float camDinMax = 5f;
    private Vector3 cameraDirection;
    private float cameraDistace;
    private Vector2 cameraDistaceMinMax;
    public Transform cam;
    public float SmoothTime;
    public Vector3 velocity;

    // 障害物レイヤー
    [SerializeField]
    private LayerMask obstacleLayer;

    [SerializeField, Header("カメラ")]
    GameObject mainCamera;
    private OperationStatusWindow _menu;

    private void Start()
    {
        cameraDirection = cam.transform.localPosition.normalized;
        cameraDistace = cameraDistaceMinMax.y;
        cameraDistaceMinMax = new Vector2(camDinMin, camDinMax);
        _menu = Camera.main.GetComponent<OperationStatusWindow>();
    }

    private void Update()
    {
        //Debug.Log(gameObject.transform.parent.localEulerAngles.y - yawMinMax);

        //右スティック
        yaw += look.x * cameraSensitive * Time.deltaTime;
        pitch -= look.y * cameraSensitive * Time.deltaTime;

        //右スティックで回転
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, smoothing);
        transform.eulerAngles = currentRotation;
        transform.position = Vector3.MoveTowards(transform.position, target.position, 0.5f);

        //カメラとオブジェクトの衝突判定
        CheakCameraOcclusion(cam);

    }

    public void CheakCameraOcclusion(Transform cam)
    {
        Vector3 desiredCameraPossion = transform.TransformPoint(cameraDirection * cameraDistaceMinMax.y);
        RaycastHit hit;

        if (Physics.Linecast(transform.position, desiredCameraPossion, out hit, obstacleLayer))
        {
            cameraDistace = Mathf.Clamp(hit.distance, cameraDistaceMinMax.x, cameraDistaceMinMax.y);
        }
        else
        {
            cameraDistace = cameraDistaceMinMax.y;
        }

        //カメラを動かす
        cam.localPosition = Vector3.SmoothDamp(cam.localPosition, cameraDirection * cameraDistace, ref velocity, SmoothTime);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }
}
