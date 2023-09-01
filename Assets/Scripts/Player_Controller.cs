using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    //移動
    [SerializeField,Header("移動のスピード")]
    private float moveSpeed;
    [HideInInspector]
    public Vector3 move;
    private float keep_MoveSpeed;

    //回転
    private Vector3 moveforward;
    private Vector3 cameraforward;
    [SerializeField, Header("回転のスピード")]
    private float rotationSpeed;

    [HideInInspector]
    public bool isMove = true; //動けるかどうか

    private bool isSwim = false;


    [HideInInspector]
    public bool foots = false;
    [HideInInspector]
    public bool oceanFoots = false;
    [HideInInspector]
    public bool sandFoots = false;


    [SerializeField]
    CinemachineVirtualCamera camera;

    CinemachinePOV cineCamera;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private OperationStatusWindow _menu;
    private AnimEvent _event;
    private StatusWindowStatus statusWindowStatus;
    private Area_Controller area_Controller;
    [SerializeField,Header("FadePanel")]
    private Fade fade;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _event = GetComponentInChildren<AnimEvent>();
        _menu = Camera.main.GetComponent<OperationStatusWindow>();
        statusWindowStatus = Camera.main.GetComponent<StatusWindowStatus>();
        area_Controller = GameObject.Find("Area").GetComponent<Area_Controller>();

        cineCamera = camera.GetCinemachineComponent(CinemachineCore.Stage.Aim).GetComponent<CinemachinePOV>();

        keep_MoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Swim" + isSwim);

        //Application.targetFrameRate = 60;
        //_animator.SetFloat("Speed", new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z).magnitude, 0.1f, Time.deltaTime);

        //if (isMove)
        //{
        //    Rotation();
        //    Move();
        //}
    }

    private void FixedUpdate()
    {
        Debug.Log("Swim" + isSwim);

        Application.targetFrameRate = 60;
        _animator.SetFloat("Speed", new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z).magnitude, 0.1f, Time.deltaTime);

        if (isMove)
        {
            Rotation();
            Move();
        }

    }

    private void Move()
    {
        _rigidbody.velocity = (moveforward * moveSpeed) + new Vector3(0, _rigidbody.velocity.y, 0);
    }

    private void Rotation()
    {
        cameraforward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;


        moveforward = (cameraforward * move.y + Camera.main.transform.right * move.x);

        if (moveforward != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(moveforward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void AreaChange(int a)
    {
        var Pos = gameObject.transform.position.x;

        area_Controller.AreaNumber += a;

        Pos = 2500 * area_Controller.AreaNumber;
        if (Pos > 2500 * area_Controller.AreaNumberMax)
        {
            Pos = 0;
            area_Controller.AreaNumber = 0;
        }
        else if (Pos < 0)
        {
            Pos = 2500 * area_Controller.AreaNumberMax;
            area_Controller.AreaNumber = area_Controller.AreaNumberMax;
        }

        area_Controller.Areatext.enabled = true;
        area_Controller.Areatext.text = area_Controller.area[area_Controller.AreaNumber].name;
        //area_Controller.area[area_Controller.AreaNumber].BGM.Play();
        area_Controller.Sounds();

        gameObject.transform.position = new Vector3(Pos, 0, 0);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        //p_Camera.yaw = 0.0f;
        //p_Camera.pitch = 0.0f;
        cineCamera.m_VerticalAxis.Value = 0.0f;
        cineCamera.m_HorizontalAxis.Value = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("OceanGround"))
        {
            isSwim = true;
            _animator.SetBool("Swim", true);
            cineCamera.m_VerticalAxis.m_MaxValue = 50;
            cineCamera.m_VerticalAxis.m_MinValue = -5;

        }
        if(other.gameObject.CompareTag("Ground"))
        {
            isSwim = false;
            foots = true;
            _animator.SetBool("Swim", false);
            cineCamera.m_VerticalAxis.m_MaxValue = 50;
            cineCamera.m_VerticalAxis.m_MinValue = -40;
        }
        if(other.gameObject.CompareTag("Ocean"))
        {
            oceanFoots = true;
        }
        if(other.gameObject.CompareTag("Sand"))
        {
            sandFoots = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            foots = false;
        }
        if (other.gameObject.CompareTag("OceanGround"))
        {
            isSwim = false;
            _animator.SetBool("Swim", false);
            cineCamera.m_VerticalAxis.m_MaxValue = 50;
            cineCamera.m_VerticalAxis.m_MinValue = -40;
        }

        if (other.gameObject.CompareTag("Ocean"))
        {
            oceanFoots = false;
        }
        if (other.gameObject.CompareTag("Sand"))
        {
            sandFoots = false;
        }
    }


    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// アイテム取得
    /// </summary>
    /// <param name="context"></param>
    public void OnGet(InputAction.CallbackContext context)
    {
        if (!_menu.isMenu)
        {
            if (isMove)
            {
                if (context.performed)
                {
                    isMove = false;
                    _rigidbody.velocity = Vector2.zero;
                    moveSpeed = keep_MoveSpeed;
                    _animator.speed = 1.0f;

                    if (isSwim)
                    {
                        _animator.SetTrigger("WaterGet");
                    }
                    else
                    {
                        _animator.SetTrigger("Get");
                    }
                }
                
            }
        }
    }

    /// <summary>
    /// ダッシュ
    /// </summary>
    /// <param name="context"></param>
    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            moveSpeed += 10.0f;
            _animator.speed = 1.3f;
        }
        if(context.canceled)
        {
            moveSpeed = keep_MoveSpeed;
            _animator.speed = 1.0f;
        }
    }

    /// <summary>
    /// ダンス
    /// </summary>
    /// <param name="context"></param>
    public void Dance_1(InputAction.CallbackContext context)
    {
        if (!_menu.isMenu)
        {
            if (!isSwim)
            {
                if (isMove)
                {
                    if (context.performed)
                    {
                        isMove = false;
                        _rigidbody.velocity = Vector2.zero;
                        moveSpeed = keep_MoveSpeed;
                        _animator.speed = 1.0f;
                        _animator.SetTrigger("Dance_1");
                    }
                }
            }
        }
    }


    public void OnPetting(InputAction.CallbackContext context)
    {
        if (!_menu.isMenu)
        {
            if (isMove)
            {
                if (context.performed)
                {
                    isMove = false;
                    _rigidbody.velocity = Vector2.zero;
                    moveSpeed = keep_MoveSpeed;
                    _animator.speed = 1.0f;

                    if (isSwim)
                    {
                        _animator.SetTrigger("WaterPetting");
                    }
                    else
                    {
                        _animator.SetTrigger("Petting");
                    }
                }

            }
        }
    }

    /// <summary>
    /// カメラリセット
    /// </summary>
    /// <param name="context"></param>
    public void OnCameraReset(InputAction.CallbackContext context)
    {
        if (!_menu.isMenu)
        {
            if (context.performed)
            {
                //var h_yaw = (int)p_Camera.yaw / 360;
                ////Debug.Log(h_yaw);
                //p_Camera.yaw = gameObject.transform.localEulerAngles.y + (h_yaw * 360);

                //p_Camera.pitch = 0;

                cineCamera.m_VerticalAxis.Value = 0.0f;
                cineCamera.m_HorizontalAxis.Value = gameObject.transform.localEulerAngles.y;
            }
        }
    }

    /// <summary>
    /// エリアチェンジ
    /// </summary>
    /// <param name="context"></param>
    public void OnAreaChange_R(InputAction.CallbackContext context)
    {
        if (!_menu.isMenu)
        {
            if (isMove)
            {
                if (context.performed)
                {
                    //AreaChange(1);
                    _rigidbody.velocity = Vector2.zero;
                    fade.FadeOut(1);
                }
            }
        }
    }
    public void OnAreaChange_L(InputAction.CallbackContext context)
    {
        if (!_menu.isMenu)
        {
            if (isMove)
            {
                if (context.performed)
                {
                    //AreaChange(-1);
                    _rigidbody.velocity = Vector2.zero;
                    fade.FadeOut(-1);
                }
            }
        }
    }
}