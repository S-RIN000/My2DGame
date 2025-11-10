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
        

        //이동
        //이동 속도
        [SerializeField] private float runSpeed = 4f;
        //이동 방향
        private Vector2 directionVector = Vector2.right;

        //이동 가능한 방향 정의
        public enum WalkableDirection
        { 
            Left, Right
        }
        //현재 이동 방향
        private WalkableDirection walkDirection = WalkableDirection.Right;

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

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            rd2D = this.GetComponent<Rigidbody2D>();
            touchingDirection = this.GetComponent<TouchingDirection>();
            animator = this.GetComponent<Animator>();
            
        }

        private void FixedUpdate()
        {
            //벽 체크
            if(touchingDirection.IsWall && touchingDirection.IsGround)
            {
                Flip();
            }

            //이동하기
            rd2D.linearVelocity = new Vector2(directionVector.x * runSpeed, rd2D.linearVelocityY);
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
        #endregion
    }
}
