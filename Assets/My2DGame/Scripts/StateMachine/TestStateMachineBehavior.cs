using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// Animator의 State 또는 SubState에 부착되는 스크립트
    /// StateMachineBehaviour를 상속받는 클래스
    /// </summary>
    public class TestStateMachineBehavior : StateMachineBehaviour
    {
        //애니메이션 상태로 들어갈 때 호출되는 함수 - 1회 호출
        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        //애니메이션 상태에서 호출되는 함수 - 매 프레임마다 호출
        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        //애니메이션 상태를 나갈 때 호출되는 함수 - 1회 호출
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
            // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}

        //애니메이터의 Sub State Machine 상태에 들어갈 때 호출 - 1회 호출
        // OnStateMachineEnter is called when entering a state machine via its Entry Node
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            
        }

        //애니메이터의 Sub State Machine 상태에서 나갈 때 호출 - 1회 호출
        // OnStateMachineExit is called when exiting a state machine via its Exit Node
        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            
        }
    }
}