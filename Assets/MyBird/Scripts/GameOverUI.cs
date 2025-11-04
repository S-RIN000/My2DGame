using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace MyBird
{
    /// <summary>
    /// 게임오버 UI를 관리하는 클래스
    /// </summary>
    public class GameOverUI : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private string loadToScene = "TitleScene";

        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI bestScoreText;

        public GameObject newText;
        #endregion

        #region Unity Event Method
        //게임오버 UI 값 설정
        private void OnEnable()
        {
            scoreText.text = GameManager.Score.ToString();

            //베스트 스코어 가져오기
            int bestScore = PlayerPrefs.GetInt("BestScore", 0);     //처음 플레이 시, 0
            //베스트 스코어와 현재 스코어 비교
            if (GameManager.Score > bestScore)
            {
                bestScore = GameManager.Score;
                //베스트 스코어 저장
                PlayerPrefs.SetInt("BestScore", bestScore);

                //UI
                newText.SetActive(true);
            }
            bestScoreText.text = bestScore.ToString();
        }
        private void Update()
        {
            
        }
        #endregion

        #region Custom Method
        public void ReTry()
        {
            string nowScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(nowScene);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(loadToScene);
        }

        #endregion
    }
}
