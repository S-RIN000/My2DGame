using UnityEngine;

namespace MyBird
{
    /// <summary>
    /// 게임 전체(흐름)을 관리하는 클래스
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Variables
        //게임 시작 여부 체크
        private static bool isStart;

        //게임 오버 체크
        private static bool isDeath;

        //게임 스코어
        private static int score;
        #endregion

        #region Property
        public static bool IsStart
        { 
            get { return isStart; }
            set { isStart = value; }
        }

        public static bool IsDeath
        {
            get { return isDeath; }
            set { isDeath = value; }
        }

        public static int Score
        { 
            get { return score; }
            set { score = value; }
        }
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            IsStart = false;
            IsDeath = false;
            Score = 0;
        }
        #endregion

        #region Custom Method
        #endregion
    }
}
