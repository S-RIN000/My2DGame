using UnityEngine;

namespace MyBird
{
    /// <summary>
    /// 플레이어 캐릭터(Bird)를 관리하는 클래스
    /// 점프, 이동, 충돌 ...
    /// </summary>
    public class Player : MonoBehaviour
    {
        #region Variables
        //참조
        private Rigidbody2D rd2D;

        //대기
        [SerializeField]
        private float readyForce = 5f;

        //점프
        private bool keyJump = false;
        [SerializeField]
        private float jumpForce = 5f;

        //회전
        private Vector3 birdRotation = Vector3.zero;    //회전을 저장하는 값
        [SerializeField]
        private float upRotate = 5f;
        [SerializeField]
        private float downRotate = -5f;                 //위, 아래로 회전하는 스피드 값 (이렇게 값을 두개 줘도 되고 하나로 써도 됨)

        //이동
        [SerializeField]
        private float moveSpeed = 5f;                   //이동 속도

        //버드 대기 UI
        public GameObject readyUI;
        //게임 오버 UI
        public GameObject gameOverUI;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //참조
            rd2D = this.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            //인풋 입력 처리
            InputBird();

            //시작 여부 체크
            if (GameManager.IsStart == false)
            {
                return;
            }

            //버드 회전
            RotateBird();

            //버드 이동
            MoveBird();
        }
        private void FixedUpdate()  //업데이트에서 해도 되긴 하는데 제대로 물리엔진을 적용하려면 여기서 하는 것이 좋음
        {
            //시작 여부 체크
            if (GameManager.IsStart == false)
            {
                ReadyBird();
                return;
            }

            //점프하기
            if (keyJump) //키가 눌렸는가?
            {
                //Debug.Log("점프");
                JumpBird();
                keyJump = false;
            }
        }

        //충돌체크 - 매개변수로 부딫힌 충돌체를 입력 받는다
        private void OnCollisionEnter2D(Collision2D collision)  //트리거가 아닌 충돌체
        {
            //충돌한 충돌체 체크
            if(collision.gameObject.tag == "Pipe")
            {
                //Debug.Log("아야");
                GameManager.IsDeath = true;
                GameOver();
            }
            else if (collision.gameObject.tag == "Ground")
            {
                //Debug.Log("아이고");
                GameManager.IsDeath = true;
                GameOver();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)     //트리거 충돌체 (통과)
        {
            //충돌한 충돌체 체크
            if(collision.gameObject.tag == "Point")
            {
                GameManager.Score++;
                //Debug.Log($"점수: {GameManager.Score}");
            }

        }
        #endregion

        #region Custom Method
        //게임 오버 처리
        void GameOver()
        {
            GameManager.IsDeath = true;
            gameOverUI.SetActive(true);
        }    

        //입력 처리
        void InputBird()
        {
            /*
            //입력 처리
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
            {
                keyJump = true;
            }
            */

            if (GameManager.IsDeath)
                return;
            
            //스페이스 키 OR 마우스 좌클릭으로 입력받기 (or을 누적)
            keyJump |= Input.GetKeyDown(KeyCode.Space); 
            keyJump |= Input.GetMouseButtonDown(0);

            //플레이어 이동 시작
            if(GameManager.IsStart == false && keyJump == true)
            {
                GameManager.IsStart = true;

                //UI
                readyUI.SetActive(false);
            }
        }

        //버드 대기
        void ReadyBird()
        {
            if (rd2D.linearVelocityY < 0f)
            {
                rd2D.linearVelocity = Vector2.up * readyForce;
            }
        }

        //버드 점프하기
        void JumpBird()
        {
            //힘을 이용하여 오브젝트를 위로 이동
            //rd2D.AddForce(Vector2.up * jumpForce);
            //rd2D.linearVelocity 를 이용하여 오브젝트를 위로 이동
            rd2D.linearVelocity = Vector2.up * jumpForce;
        }

        //버드 회전하기
        void RotateBird()
        {
            //점프해서 올라갈때 최대 30도까지 표현
            //        내려갈때 최소 -30도까지 표현

            float degree = 0f;  //멈춰 있을 때
            if(rd2D.linearVelocity.y > 0f)  //올라갈 때
            {
                degree = upRotate;
            }
            else if (rd2D.linearVelocity.y < 0f)    //내려갈 때
            {
                degree = downRotate;
            }

            birdRotation = new Vector3(0f, 0f, Mathf.Clamp(birdRotation.z + degree, -90f, 30f));
            transform.eulerAngles = birdRotation;
        }    

        //버드 이동
        void MoveBird()
        {
            if (GameManager.IsDeath)
                return;
            transform.Translate(Vector2.right * Time.deltaTime * moveSpeed, Space.World);
        }
        #endregion
    }
}
