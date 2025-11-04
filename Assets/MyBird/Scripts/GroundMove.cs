using UnityEngine;

namespace MyBird
{
    /// <summary>
    /// 그라운드 배경 이동 (롤링) 구현
    /// </summary>
    public class GroundMove : MonoBehaviour
    {
        #region Variables
        //이동 속도
        [SerializeField]
        private float moveSpeed = 5f;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            if(GameManager.IsDeath == true)
                return;

            //그라운드 롤링 이동
            RollingMove();
        }
        #endregion

        #region Custom Method

        void RollingMove()
        {

            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);
           
            if (transform.localPosition.x <= -8.4f)     //자식 오브젝트는 localPosition
            {
                this.transform.localPosition = new Vector3(transform.localPosition.x +8.4f, transform.localPosition.y, transform.localPosition.z);
            }

        }
        #endregion
    }
}
