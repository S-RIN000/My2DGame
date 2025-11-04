using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyBird
{
    public class Title : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private string loadToScene = "PlayScene";
        #endregion

        #region Unity Event Method
        #endregion

        #region Custom Method
        public void PlayButton()
        {
            SceneManager.LoadScene(loadToScene);
        }
        #endregion
    }
}
