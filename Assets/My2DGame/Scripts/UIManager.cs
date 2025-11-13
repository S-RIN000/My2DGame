using TMPro;
using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// 플레이씬 UI를 관리하는 클래스
    /// 데미지 텍스트 연출
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        #region Variables
        //참조
        private Canvas gameCanvas;

        public GameObject damageTextPrefab;     //데미지 텍스트 연출 프리팹
        public GameObject healTextPrefab;       //힐 텍스트 연출 프리팹
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            gameCanvas = FindFirstObjectByType<Canvas>();
        }

        private void OnEnable()
        {
            //이벤트 함수 등록
            CharacterEvent.characterDamaged += CharacterTakeDamge;
            CharacterEvent.characterHeal += CharacterHeal;
        }
        private void OnDisable()
        {
            //이벤트 함수 해제
            CharacterEvent.characterDamaged -= CharacterTakeDamge;
            CharacterEvent.characterHeal -= CharacterHeal;
        }
        #endregion

        #region Custom Method
        //캐릭터가 데미지 입을 때 호출되는 함수 - 데미지 텍스트 연출
        //매개변수로 캐릭터의 오브젝트, 데미지량 입력받아 처리   
        public void CharacterTakeDamge(Transform character, float damageRecived)
        {
            //캐릭터 머리 위 위치 가져오기
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.position);

            GameObject textGo = Instantiate(damageTextPrefab, new Vector3(spawnPosition.x , spawnPosition.y +30f, spawnPosition.z), Quaternion.identity, gameCanvas.transform);

            //데미지 값 세팅
            TextMeshProUGUI damageText = textGo.GetComponent<TextMeshProUGUI>();
            damageText.text = damageRecived.ToString();
        }

        //캐릭터가 힐 할 때 호출되는 함수 - 데미지 텍스트 연출
        //매개변수로 캐릭터의 오브젝트, 데미지량 입력받아 처리   
        public void CharacterHeal(Transform character, float healAmount)
        {
            //캐릭터 머리 위 위치 가져오기
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.position);

            GameObject textGo = Instantiate(healTextPrefab, new Vector3(spawnPosition.x, spawnPosition.y , spawnPosition.z), Quaternion.identity, gameCanvas.transform);

            //데미지 값 세팅
            TextMeshProUGUI healText = textGo.GetComponent<TextMeshProUGUI>();
            healText.text = healAmount.ToString();
        }
        #endregion
    }
}
