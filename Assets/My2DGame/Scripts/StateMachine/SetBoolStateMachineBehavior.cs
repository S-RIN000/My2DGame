using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// 애니메이터 bool타입 파라미터 값을 설정하는 클래스 
    /// 애니메이터의 SubStateMachine에 들어갈 때와 나갈 때의 값 설정
    /// </summary>
    public class SetBoolStateMachineBehavior : StateMachineBehaviour
    {
        #region Variables
        public string boolName;     //bool타입 파라미터 이름

        public bool enterValue;     //애니메이션 상태(state)에서 들어갈 때의 값
        public bool exitValue;      //애니메이션 상태(state)에서 나갈 때의 값
        #endregion

        // OnStateMachineEnter is called when entering a state machine via its Entry Node
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            animator.SetBool(boolName, enterValue);
        }

        // OnStateMachineExit is called when exiting a state machine via its Exit Node
        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            animator.SetBool(boolName, exitValue);
        }
    }
}