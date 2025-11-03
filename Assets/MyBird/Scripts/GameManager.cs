using UnityEngine;

namespace MyBird
{
    /// <summary>
    /// 게임 전체(흐름)을 관리하는 클래스
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Variables
        private static bool isStart;
        #endregion

        #region Property
        public static bool IsStart
        { 
            get { return isStart; }
            set { isStart = value; }
        }
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            isStart = false;
        }
        #endregion

        #region Custom Method
        #endregion
    }
}
