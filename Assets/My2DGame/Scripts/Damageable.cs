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
        private Renderer renderer;

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

        //힐 할 때 호출되는 이벤트 함수
        public UnityAction<float> healAction;

        //무적 모드 효과
        public Material invincibleMaterial;     //무적모드 메테리얼
        private Material originMaterial;        //오리지날 메테리얼
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
            //참조
            animator = this.GetComponent<Animator>();
            renderer = this.GetComponent<Renderer>();
        }

        private void Start()
        {
            //초기화
            CurrentHealth = MaxHealth;
            originMaterial = renderer.material;
        }
        private void Update()
        {
            //무적 타이머 - 무적 모드일 때만 돈다
            if (isInvincible)
            {
                //죽음 체크
                if(IsDeath)
                    return;

                countdown += Time.deltaTime;
                if (countdown >= invincibleTimer)
                {
                    //타이머 구현 - 무적 모드 해제
                    isInvincible = false;
                    if(invincibleMaterial != null)
                    {
                        renderer.material = originMaterial;
                    }
                    //타이머 초기화
                    countdown = 0f;
                }
            }
        }
        #endregion

        #region Custom Method
        //데미지 주기
        public void TakeDamage(float damage, Vector2 knockback)
        {
            //죽음, 뮤적 체크
            if (isDeath || isInvincible) return;

            CurrentHealth -= damage;
            Debug.Log($"CurrentHealth : {CurrentHealth}");

            isInvincible = true;
            //무적 모드 효과
            if(invincibleMaterial != null)
            {
                renderer.material = invincibleMaterial;
            }

            //애니메이션
            animator.SetTrigger(AnimationString.HitTrigger);

            //데미지 효과 (knockback)
            //hitAction 이벤트에 등록된 함수 호출 
            hitAction?.Invoke(damage, knockback);
            //데미지 텍스트 연출 효과
            CharacterEvent.characterDamaged.Invoke(this.transform, damage);
        }
        //hp 회복, 힐 성공시 true, 실패시 false
        public bool Heal(float healAmount)
        {
            //죽음체크
            if (IsDeath)
                return false;

            //체력 만땅 체크
            if(CurrentHealth >= maxHealth)
                return false;

            //리얼 힐 값 구하기
            float maxHeal = MaxHealth - CurrentHealth;  //최대로 힐 할 수 있는 값
            float realHeal = (maxHeal < healAmount) ? maxHeal : healAmount;     //실제로 힐 하는 값 

            CurrentHealth += realHeal;
            Debug.Log($"CurrentHealth : {CurrentHealth}");

            //healAction null 체크 : healAction에 등록된 함수가 있는지 여부 체크
            /*
            if (healAction != null)
            {
                healAction.Invoke(realHeal);
            }
            */
            healAction?.Invoke(realHeal);

            //데미지 텍스트 연출 효과
            CharacterEvent.characterHeal?.Invoke(this.transform, realHeal);


            return true;
        }
        #endregion
    }
}
