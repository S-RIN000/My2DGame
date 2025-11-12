using UnityEngine;
using TMPro;

namespace My2DGame
{
    /// <summary>
    /// 데미지 효과 : 캐릭터 머리 위에 텍스트 띄우기
    /// 위로 이동하기, 이동하면서 페이드 아웃, 페이드 아웃이 끝나면 킬 (1s)
    /// </summary>
    public class DamageText : MonoBehaviour
    {
        #region Variables
        //참조
        private RectTransform rectTransform;
        private TextMeshProUGUI damageText;

        [SerializeField]
        private float moveSpeed = 10f;

        //페이드 효과
        private Color startColor;

        [SerializeField]
        private float fadeTimer = 1f;
        private float countdown = 0f;

        //페이드 지연
        [SerializeField]
        private float delayTime = 1f;
        private float delayCountown = 0f;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            rectTransform = this.GetComponent<RectTransform>();
            damageText = this.GetComponent<TextMeshProUGUI>();
        }
        private void Start()
        {
            //초기화
            countdown = 0f;
            startColor = damageText.color;
        }
        private void Update()
        {
            //위로 이동
            rectTransform.position += Vector3.up * Time.deltaTime * moveSpeed;
            
            //페이드 딜레이
            if(delayCountown < delayTime)
            {
                delayCountown += Time.deltaTime;
                return;
            }

            //페이드 효과
            countdown += Time.deltaTime;

            float alpahValue = startColor.a * (1- (countdown / fadeTimer));
            damageText.color = new Color (startColor.r, startColor.g, startColor.b, alpahValue);

            //페이드 효과 완료 후 킬
            if (countdown > fadeTimer)
            {
                Destroy(this.gameObject);
            }
        }
        #endregion 
    }
}
