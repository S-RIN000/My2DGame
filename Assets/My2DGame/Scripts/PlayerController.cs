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
        private TouchingDirection touchingDirection;
        private Damageable damageable;
        private TrailEffect trailEffect;

        //private ProjectileLauncher projectileLauncher;

        //이동
        [SerializeField] private float walkSpeed = 3f;       //걷는 속도
        [SerializeField] private float runSpeed = 6f;        //달리는 속도
        [SerializeField] private float airSpeed = 2f;        //공중에서의 속도
        [SerializeField] private float jumpForce = 5f;       //점프

        //이동 입력 값
        private Vector2 inputMove = Vector2.zero;

        //반전
        private bool isFacingRight = true;

        //걷기
        private bool isMove = false;

        //달리기
        private bool isRun = false;
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
                if(CannotMove)  //애니메이터 파라미터 값 읽어오기
                {
                    return 0f;
                }

                if(IsMove && touchingDirection.IsWall == false)  //이동 가능
                {
                    if(touchingDirection.IsGround)  //땅에 있을 때
                    {
                        if (IsRun)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        return airSpeed;
                    }
                }
                else    //이동 불가능
                {
                    return 0f;
                }
            }
        }

        //애니메이터의 파라미터 값 (CannotMove) 읽어오기 
        public bool CannotMove
        { 
            get
            {
                return animator.GetBool(AnimationString.CannotMove);
            }
        }
        //애니메이터의 파라미터 값 (LockVelocity) 읽어오기 
        public bool LockVelocity
        {
            get
            {
                return animator.GetBool(AnimationString.LockVelocity);
            }
        }
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            rd2D = this.GetComponent<Rigidbody2D>();
            animator = this.GetComponent<Animator>();
            touchingDirection = this.GetComponent <TouchingDirection>();
            damageable = this.GetComponent <Damageable>();
            trailEffect = this.GetComponent <TrailEffect>();

            //projectileLauncher = this.GetComponent<ProjectileLauncher>();

            //이벤트 함수 등록
            damageable.hitAction += OnHit;
        }
        private void FixedUpdate()
        {
            //좌우 이동
            if (LockVelocity == false)
            {
                rd2D.linearVelocity = new Vector2(inputMove.x * CurrentMoveSpeed, rd2D.linearVelocity.y);  //위아래로는 이동x
            }
            //점프 애니메이션
            animator.SetFloat(AnimationString.YVelocity, rd2D.linearVelocityY);
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

            if(damageable.IsDeath == false)
            {
                //Debug.Log(inputMove);
                IsMove = inputMove != Vector2.zero;
                //방향 전환
                SetFacingDirection(inputMove);
            }
            else
            {
                IsMove = false;
            }
        }

        //달리기 입력 처리
        public void OnRun(InputAction.CallbackContext context)
        {
            if(context.started)     //버튼을 눌렀을때 (누르기 시작할 때)
            {
                IsRun = true;
                //Debug.Log("달리기");
                //잔상 효과 시작
                if (trailEffect != null)
                {
                    trailEffect.StartTrailEffect();
                }
            }
            else if(context.canceled)   //버튼을 뗄 때
            {
                IsRun = false;
                //Debug.Log("달리기 끝");
            }
         
        }

        //점프 입력처리
        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.started && touchingDirection.IsGround)
            {
                //Debug.Log("점프");
                animator.SetTrigger(AnimationString.JumpTrigger);
                rd2D.linearVelocity = new Vector2(rd2D.linearVelocity.x, jumpForce);

                //잔상 효과 시작
                if (trailEffect != null)
                {
                    trailEffect.StartTrailEffect();
                }
            }

        }

        //기본 공격 입력처리
        public void OnAttack1(InputAction.CallbackContext context)
        {
            if(context.started && touchingDirection.IsGround)
            {
                //Debug.Log("얍얍");
                animator.SetTrigger(AnimationString.AttackTrigger);
            }
            
        }

        //활 쏘기
        public void OnBow(InputAction.CallbackContext context)
        {
            if(context.started && touchingDirection.IsGround)
            {
                //Debug.Log("붕");
                animator.SetTrigger(AnimationString.BowAttackTrigger);

                //발사체 발사
                //projectileLauncher.FireProjectile(); ==> 애니메이션 이벤트로 딜레이 해결
            }
        }

        //데미지 이벤트에 등록되는 함수
        public void OnHit(float damage, Vector2 knockback)
        {
            rd2D.linearVelocity = new Vector2(knockback.x, rd2D.linearVelocityY + knockback.y);
        }

       
        #endregion
    }
}
