using UnityEngine;

namespace My2DGame
{
    public class DessolveBehavior : StateMachineBehaviour
    {
        #region Variables
        private Renderer renderer;

        public Material dessolveMaterial;

        //딜레이 타이머
        public float delayTimer = 1f;
        private float delayCountdown = 0f;

        //디졸브 타이머
        private float countdown = 0f;

        private string SplitValue = "_SplitValue";
        #endregion

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //참조
            renderer = animator.GetComponent<Renderer>();

            //초기화
            delayCountdown = 0f;
            countdown = 1f;

            //디졸브
            renderer.material = dessolveMaterial;
            renderer.material.SetFloat(SplitValue, 1.0f);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //딜레이 체크
            if (delayCountdown < delayTimer)
            {
                delayCountdown += Time.deltaTime;
                return;
            }

            //디졸브 효과 시작 : 1 -> 0
            if(countdown >= 0f)
            {
                countdown -= Time.deltaTime;
                if(countdown < 0f) countdown = 0f;
                
                renderer.material.SetFloat(SplitValue, countdown);
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
