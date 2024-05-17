using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TileWorld.Events;
public class Movement : MonoBehaviour
{

    [SerializeField] float playerSpeed = 30.0f;
    [SerializeField] private bool buildCom = false;
    [SerializeField] List<AudioClip> runAudio;
    private bool groundedPlayer;
    private Vector3 playerVelocity;
    private float gravityValue = -9.8f;
    private Animator Theanimator;
    private CharacterController controller;
    private InputControls m_PlayerInput;

    private Vector2 init_touch, keep_touch, end_touch, Register_touch, move_rate, move_pos;
    private bool isPause;


    public void Awake()
    {
        m_PlayerInput = new InputControls();
    }
    void Start()
    {
        Theanimator = GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
        isPause = false;
    }



    void Update()
    {
        if (!isPause)
        {
            var pos = m_PlayerInput.Touch.TouchPosition.ReadValue<Vector2>();
            var c_touchPhase = m_PlayerInput.Touch.TouchPhase.ReadValue<UnityEngine.InputSystem.TouchPhase>();

            if (c_touchPhase != UnityEngine.InputSystem.TouchPhase.None)
            {
                if (c_touchPhase == UnityEngine.InputSystem.TouchPhase.Began)         //初次按下
                {
                    init_touch = pos;
                }
                else if (c_touchPhase == UnityEngine.InputSystem.TouchPhase.Moved)    //手指持續放在螢幕上
                {
                    keep_touch = pos;       //移動過程中的位置

                    Register_touch = keep_touch - init_touch;       //扣除初始觸碰點獲得向量
                    move_rate.x = Register_touch.x;               //vector2轉成vector3
                    move_rate.y = Register_touch.y;

                    move_pos = Vector3.ClampMagnitude(move_rate * 0.003f, 0.5f);     //Vector3.ClampMagnitude(x,y)假如x內值>y則只得到y
                                                                                     //假如x=(0,10,2),y=5得到的為(0,5,2)
                }
                else if (c_touchPhase == UnityEngine.InputSystem.TouchPhase.Ended)    //手指離開螢幕
                {
                    keep_touch = end_touch;                       //歸零
                    init_touch = end_touch;
                    move_pos = Vector2.zero;
                    move_rate.x = 0;
                    move_rate.y = 0;
                }


            }
            Move(move_pos);
        }
    }
    void Move(Vector2 direction)
    {

        if (buildCom)
        {

            if (controller.isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            var moveSpeed = playerSpeed * Time.deltaTime;
            var move = new Vector3(direction.x, 0, direction.y);

            controller.Move(move * moveSpeed);
            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
                Theanimator.SetBool("walk", true);
            }
            else
            {
                Theanimator.SetBool("walk", false);
            }
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
    void PlayRunAudio()
    {
        int randomIndex = Random.Range(0, runAudio.Count);
        GetComponent<AudioSource>().PlayOneShot(runAudio[randomIndex]);
    }

    void OnEnable()
    {
        TileWorldEvents.OnMergeComplete += MergeComplete;
        PlayboardEvent.GamePause += GamePause;
        PlayboardEvent.GameContinue += GameContinue;
        m_PlayerInput.Enable();
    }



    void OnDisenable()
    {
        TileWorldEvents.OnMergeComplete -= MergeComplete;
        PlayboardEvent.GamePause -= GamePause;
        PlayboardEvent.GameContinue -= GameContinue;
        m_PlayerInput.Disable();
    }
    private void MergeComplete()
    {
        buildCom = true;
    }
    private void GameContinue()
    {
        isPause = false;
    }

    private void GamePause()
    {
        isPause = true;
        Theanimator.SetBool("walk", false);
        Theanimator.Play("Base Layer.Idle");
    }
}
