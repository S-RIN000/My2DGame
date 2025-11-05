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

        //이동
        [SerializeField]
        private float walkSpeed = 5f;       //걷는 속도

        //이동 입력 값
        private Vector2 inputMove = Vector2.zero;

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            rd2D = this.GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            //이동
            rd2D.linearVelocity = new Vector2(inputMove.x * walkSpeed, rd2D.linearVelocity.y);  //위아래로는 이동x
        }

        #endregion

        #region Custom Method
        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
            //Debug.Log(inputMove);
        }
        #endregion
    }
}
