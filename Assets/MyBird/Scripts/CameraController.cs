using UnityEngine;

namespace MyBird
{
    /// <summary>
    /// 카메라 컨트롤러 : 플레이어 오른쪽 이동에 따라간다 
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        #region Variables
        //플레이어 오브젝트
        public Transform player;

        //카메라 위치 조정
        [SerializeField]
        private float offsetX = 1.5f;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            FollowPlayer();
        }
        #endregion

        #region Custom Method
        //플레이어 따라가기
        void FollowPlayer()
        {
            this.transform.position = new Vector3(player.position.x + offsetX, this.transform.position.y, this.transform.position.z);
        }
        #endregion
    }
}
