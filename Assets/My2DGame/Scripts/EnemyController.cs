using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// Enemy를 관리하는 클래스
    /// </summary>
    [RequireComponent (typeof(Rigidbody2D), typeof(TouchingDirection))]
    public class EnemyController : MonoBehaviour
    {
        #region Variables
        //참조
        private Rigidbody2D rd2D;
        private TouchingDirection touchingDirection;
        private Animator animator;
        private Damageable damageable;

        //적감지
        public DetectionZone detectionZone;
        //그라운드 감지
        public DetectionZone detectiveGround;

        //이동
        //이동 속도
        [SerializeField] private float runSpeed = 4f;
        //이동 방향
        private Vector2 directionVector = Vector2.right;

        //이동 가능한 방향 정의
        public enum WalkableDirection
        { 
            Left,
            Right
        }
        //현재 이동 방향
        private WalkableDirection walkDirection = WalkableDirection.Right;

        //감속 Lerp 계수
        [SerializeField] private float stopRate = 0.2f;

        //적 감지 - 타겟이 있다
        private bool hasTarget = false;
        #endregion

        #region Property
        public WalkableDirection WalkDirection
        { 
            get { return walkDirection; } 
            private set 
            { 
                //방향 전환이 일어난 시점
                if(walkDirection != value)
                {
                    //이미지 플립
                    transform.localScale *= new Vector2(-1,1);
                    //valur 값에 따라 이동 방향 설정
                    if(value == WalkableDirection.Left)
                    {
                        directionVector = Vector2.left;
                    }
                    else if (value == WalkableDirection.Right)
                    {
                        directionVector = Vector2.right;
                    }
                }

                walkDirection = value; 
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

        //적 감지
        public bool HasTarget
        { 
            get { return hasTarget; }
            set
            {
                hasTarget = value;
                animator.SetBool(AnimationString.HasTarget, value);
            }
        }

        //공격 쿨 타임 - 애니메이션 파라미터 값 세팅
        public float CoolDownTime
        {
            get
            {
                return animator.GetFloat(AnimationString.CoolDownTime);
            }
            set
            {
                animator.SetFloat(AnimationString.CoolDownTime, value);
            }
        }
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            rd2D = this.GetComponent<Rigidbody2D>();
            touchingDirection = this.GetComponent<TouchingDirection>();
            animator = this.GetComponent<Animator>();
            damageable = this.GetComponent<Damageable>();

            //이벤트 함수 등록
            damageable.hitAction += OnHit;

            //DetectiveZone 이벤트 함수 등록
            detectiveGround.noRemainColliders += OnCliffDetection;
        }
        private void Update()
        {
            //적 감지
            HasTarget = detectionZone.detectiveColliders.Count > 0; //리스트의 카운트 갯수가 0보다 크면 타겟이 있는 것

            //공격 쿨 다운
            if (CoolDownTime > 0f)
            {
                CoolDownTime = CoolDownTime - Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            //벽 체크
            if(touchingDirection.IsWall && touchingDirection.IsGround)
            {
                Flip();
            }

            //이동하기
            if (LockVelocity == false)
            {
                if (CannotMove)
                {
                    rd2D.linearVelocity = new Vector2(Mathf.Lerp(rd2D.linearVelocityX, 0f, stopRate), rd2D.linearVelocityY);
                }
                else
                {
                    rd2D.linearVelocity = new Vector2(directionVector.x * runSpeed, rd2D.linearVelocityY);
                }
            }
            
        }
        #endregion

        #region Custom Method
        //방향 전환
        void Flip()
        {
            if(WalkDirection == WalkableDirection.Left)
            {
                WalkDirection = WalkableDirection.Right;
            }
            else if (WalkDirection == WalkableDirection.Right)
            {
                WalkDirection = WalkableDirection.Left;
            }
            else
            {
                Debug.Log("Error Flip Direction");
            }
        }
        //데미지 이벤트에 등록되는 함수
        public void OnHit(float damage, Vector2 knockback)
        {
            rd2D.linearVelocity = new Vector2(knockback.x, rd2D.linearVelocityY + knockback.y);
        }

        //디텍션 이벤트에 등록되는 함수
        public void OnCliffDetection()
        {
            if (touchingDirection.IsGround)
            {
                Flip();
            }
        }
        #endregion
    }
}
