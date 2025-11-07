using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace My2DGame
{
    /// <summary>
    /// 플레이어를 제어하는 클래스
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        //참조
        private Rigidbody2D rd2D;
        private Animator animator;

        //이동
        [SerializeField]
        private float walkSpeed = 3f;       //걷는 속도
        [SerializeField]
        private float runSpeed = 6f;        //달리는 속도

        //이동 입력 값
        private Vector2 inputMove = Vector2.zero;

        //반전
        private bool isFacingRight = true;

        //걷기
        private bool isMove = false;

        //달리기
        private bool isRun = false;

        //점프
        
        #endregion

        #region Property
        public bool IsFacingRight
        { 
            get { return isFacingRight; } 
            private set 
            { 
                //반전 구현
                if(isFacingRight != value)
                {
                    this.transform.localScale *= new Vector2(-1, 1);
                }

                isFacingRight = value; 
            }
        }
        public bool IsMove
        { 
            get { return isMove; }
            private set 
            { 
                isMove = value;
                animator.SetBool(AnimationString.IsMove, value);
            }
        }

        public bool IsRun
        {
            get { return isRun; }
            private set
            {
                isRun = value;
                animator.SetBool(AnimationString.IsRun, value);
            }
        }

        //현재 이동 속도 - 읽기 전용
        public float CurrentMoveSpeed
        {
            get
            {
                if(IsMove)  //이동 가능
                {
                    if(IsRun)
                    {
                        return runSpeed;
                    }
                    else
                    {
                        return walkSpeed;
                    }
                }
                else    //이동 불가능
                {
                    return 0f;
                }
            }
        }
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            rd2D = this.GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }
        private void FixedUpdate()
        {
            //이동
            rd2D.linearVelocity = new Vector2(inputMove.x * CurrentMoveSpeed, rd2D.linearVelocity.y);  //위아래로는 이동x
        }

        #endregion

        #region Custom Method
        //방향 전환
        void SetFacingDirection(Vector2 moveInput)
        {
            if(moveInput.x > 0f && isFacingRight == false)    //오른쪽으로 이동
            {
                IsFacingRight = true;
            }
            else if(moveInput.x < 0f) //왼쪽으로 이동
            {
                IsFacingRight = false;
            }
        }

        //걷기 입력 처리
        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
            //Debug.Log(inputMove);
            IsMove = inputMove != Vector2.zero;
            //방향 전환
            SetFacingDirection(inputMove);
        }

        //달리기 입력 처리
        public void OnRun(InputAction.CallbackContext context)
        {
            if(context.started)     //버튼을 눌렀을때 (누르기 시작할 때)
            {
                IsRun = true;
                //Debug.Log("달리기");
            }
            else if(context.canceled)   //버튼을 뗄 때
            {
                IsRun = false;
                //Debug.Log("달리기 끝");
            }
         
        }
        public void OnJump(InputAction.CallbackContext context)
        {
         

        }
        #endregion
    }
}
