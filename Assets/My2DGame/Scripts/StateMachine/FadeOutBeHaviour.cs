using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// 애니메이션 상태 동안 페이드 아웃 처리
    /// </summary>
    public class FadeOutBeHaviour : StateMachineBehaviour
    {
        #region Vriables
        //참조
        private SpriteRenderer spriteRenderer;
        private GameObject removeObject;

        //딜레이 타이머
        public float delayTimer = 1f;
        public float delayCountdown = 0f;

        //페이드 타이머
        public float fadeTimer = 1f;
        public float countdown = 0f;
        public Color startColor;

        #endregion

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //참조
            spriteRenderer = animator.GetComponent<SpriteRenderer>();
            removeObject = animator.gameObject;

            //초기화
            countdown = 0f;
            startColor = spriteRenderer.color;  //현재 컬러 값
            delayCountdown = 0f;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //딜레이 체크
            if(delayCountdown < delayTimer)
            {
                delayCountdown += Time.deltaTime;
                return;
            }

            //페이드 아웃 타이머 1 -> 0
            countdown += Time.deltaTime;

            //countdown / fadeTimer => 0 -> 1
            float alphaValue = startColor.a * (1 - (countdown / fadeTimer));
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alphaValue);

            //타이머가 종료 - 킬
            if (countdown > fadeTimer)
            {
                Destroy(removeObject);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
