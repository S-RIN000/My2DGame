using UnityEngine;
using System.Collections;

namespace My2DGame
{
    /// <summary>
    /// 플레이어 동작에 따라 잔상 이펙트 효과 구현
    /// </summary>
    public class TrailEffect : MonoBehaviour
    {
        #region Variables
        //탐조
        private SpriteRenderer spriteRenderer;

        //잔상 메터리얼
        public Material ghostMaterial;

        //잔상 효과
        private bool isTrailActive = false;     //효과 활성, 비활성
        [SerializeField] private float trailActiveTime = 2f;     //효과 지속 시간
        [SerializeField] private float trailRefreshRate = 0.1f;  //잔상들의 발생 간격 시간
        [SerializeField] private float trailDestoryDelay = 1f;   //1초후에 킬 - 페이드아웃 효과

        //잔상 페이드 아웃
        private string shaderValueRef = "_Alpha";
        [SerializeField] private float shaderValurRate = 0.1f;           //알파값 감소 비율 (1 -> 0)
        [SerializeField] private float shaderValueRefreshRate = 0.1f;    //알파값이 감소 되는 시간 간격
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        #endregion

        #region Custom Method
        //잔상 효과 플레이
        public void StartTrailEffect()
        {
            //현재 효과 진행중이면
            if (isTrailActive)
                return;
            StartCoroutine(ActiveTrail(trailActiveTime));
        }

        //매개변수 효과 지속시간 
        IEnumerator ActiveTrail(float activeTime)
        {
            isTrailActive = true;
            while(activeTime > 0f)
            {
                activeTime -= trailRefreshRate;

                //잔상 게임 오브젝트 만들기 - 현재 플레이어의 위치에 플레이의 스프라이트로 만든다 
                GameObject ghostObject = new GameObject();      //=하이라키 창에 빈 오브젝트 만들기 
                //트랜스폼 세팅 - 플레이어의 트랜스폼과 동일
                ghostObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
                ghostObject.transform.localScale = transform.localScale;
                //스프라이트 렌더러 세팅 
                SpriteRenderer renderer = ghostObject.AddComponent < SpriteRenderer>();
                renderer.sprite = spriteRenderer.sprite;
                renderer.sortingLayerName = spriteRenderer.sortingLayerName;
                renderer.sortingOrder = spriteRenderer.sortingOrder - 1;
                renderer.material = ghostMaterial;

                //페이드 아웃 효과
                StartCoroutine(AnimateMaterialFloat(renderer.material, shaderValueRef, 0f, shaderValurRate, shaderValueRefreshRate));

                //고스트 오브젝트 킬
                Destroy(ghostObject, trailDestoryDelay);

                //딜레이 - trailRefreshRate (0.1f)
                yield return new WaitForSeconds(trailRefreshRate);
            }

            //효과 해제
            isTrailActive = false;
        }

        //메터리얼 속성(알파) 값 감소
        IEnumerator AnimateMaterialFloat(Material material, string valueRef, float goal, float rate, float refreshRate)
        {
            float value = material.GetFloat(valueRef);  

            while(value > goal)
            {
                value -= rate;
                material.SetFloat(valueRef, value);

                yield return new WaitForSeconds(refreshRate);
            }
        }
        #endregion
    }
}
