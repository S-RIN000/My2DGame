using UnityEngine;

namespace MyBird
{
    /// <summary>
    /// 기둥 프리팹 오브젝트를 스폰하는 클래스
    /// 1초에 하나씩 기둥을 스폰
    /// </summary>
    public class SpawnManager : MonoBehaviour
    {
        #region Variables
        //기둥 프리팹 오브젝트
        public GameObject pipePrefab;

        //스폰 타이머
        [SerializeField]
        private float spawnTimer = 1f;
        private float countdown = 0f;

        //스폰 높이 랜덤 범위 설정 값
        private float minSpawnY = -1.5f;
        private float maxSpawnY = 3.5f;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            spawnTimer = 1f;
        }

        private void Update()
        {
            //대기중에 스폰 안하기
            if (GameManager.IsStart == false || GameManager.IsDeath == true)
            {
                return;
            }

            //1초에 기둥 하나씩 스폰
            countdown += Time.deltaTime;
            if(countdown > spawnTimer)
            {
                //타이머 기능
                SpawnPipe();

                //타이머 초기화
                countdown = 0f;
                spawnTimer = 1f;
            }
        }
        #endregion

        #region Custom Method
        //기둥 스폰
        private void SpawnPipe()
        {
            float spawnY = this.transform.position.y + Random.Range(minSpawnY, maxSpawnY);
            Vector3 spawnPosition = new Vector3 (this.transform.position.x, spawnY, this.transform.position.z);
            Instantiate(pipePrefab, spawnPosition, Quaternion.identity);
        }
        #endregion
    }
}
