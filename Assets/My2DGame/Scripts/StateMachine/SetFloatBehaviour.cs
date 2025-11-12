using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// 애니메이터 Float 타입 파라미터 값을 설정하는 클래스
    /// 애니메이터의 Sub StateMachine 또는 State(상태)에서 들어갈 때와 나갈 때의 값 설정
    /// </summary>
    public class SetFloatBehaviour : StateMachineBehaviour
    {
        #region Variables
        public string floatName;    //float타입 파라미터 이름

        public bool updateOnStateEnter, updateOnStateExit;      //State(상태)에서 설정 여부 체크 
        public bool updateOnStateMachineEnter, updateOnStateMachineExit;    //Sub StateMachine에서 설정 여부 체크

        public float enterValue;    //들어갈 때 설정값
        public float exitValue;     //나올 때 설정값
        #endregion

        #region State

        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(updateOnStateEnter)
            {
                animator.SetFloat(floatName, enterValue);
            }
        }

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(updateOnStateExit)
            {
                animator.SetFloat(floatName, exitValue);
            }
        }

        #endregion

        #region Sub StateMachine
        // OnStateMachineEnter is called when entering a state machine via its Entry Node
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if(updateOnStateMachineEnter)
            {
                animator.SetFloat(floatName, enterValue);
            }
        }

        // OnStateMachineExit is called when exiting a state machine via its Exit Node
        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if(updateOnStateMachineExit)
            {
                animator.SetFloat(floatName, exitValue);
            }
        }
        #endregion
    }
}
