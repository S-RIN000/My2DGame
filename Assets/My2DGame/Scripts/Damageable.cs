using UnityEngine;
using UnityEngine.Events;

namespace My2DGame
{
    /// <summary>
    /// Health를 관리하는 클래스
    /// </summary>
    public class Damageable : MonoBehaviour
    {
        #region Variables
        //참조
        private Animator animator;

        [SerializeField]
        private float currentHealth;

        [SerializeField]
        private float maxHealth = 100f;

        //죽음 체크
        private bool isDeath = false;

        //무적 모드
        private bool isInvincible = false;
        //무적 모드 타이머
        [SerializeField]
        private float invincibleTimer = 3f;
        private float countdown = 0f;

        //데미지 입을 때 호출되는 이벤트 함수
        public UnityAction<float, Vector2> hitAction;
        #endregion

        #region Property
        public float CurrentHealth
        {
            get { return currentHealth; }
            private set
            {
                currentHealth = value;

                if(currentHealth <= 0)
                {
                    IsDeath = true;
                }
            }
        }

        public float MaxHealth
        {
            get { return maxHealth; }
            private set
            {
                maxHealth = value;
            }
        }

        public bool IsDeath
        {
            get { return isDeath; }
            private set
            {
                isDeath = value;
                animator.SetBool(AnimationString.IsDeath, value);
            }
        }
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            animator = this.GetComponent<Animator>();
        }

        private void Start()
        {
            //초기화
            CurrentHealth = MaxHealth;
        }
        private void Update()
        {
            //무적 타이머 - 무적 모드일 때만 돈다
            if (isInvincible)
            {
                countdown += Time.deltaTime;
                if (countdown >= invincibleTimer)
                {
                    //타이머 구현
                    isInvincible = false;
                    //타이머 초기화
                    countdown = 0f;
                }
            }
        }
        #endregion

        #region Custom Method
        public void TakeDamage(float damage, Vector2 knockback)
        {
            //죽음, 뮤적 체크
            if (isDeath || isInvincible) return;

            CurrentHealth -= damage;
            Debug.Log($"CurrentHealth : {CurrentHealth}");

            isInvincible = true;

            //애니메이션
            animator.SetTrigger(AnimationString.HitTrigger);

            //데미지 효과 (knockback)
            //hitAction 이벤트에 등록된 함수 호출 
            hitAction?.Invoke(damage, knockback);
            //데미지 텍스트 연출 효과
            CharacterEvent.characterDamaged.Invoke(this.transform, damage);
        }
        #endregion
    }
}
