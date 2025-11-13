using System.Collections.Generic;
using UnityEngine;


namespace My2DGame
{
    /// <summary>
    /// 맵에 떨어진 아이템을 픽업하는 기능
    /// 픽업 시 아이템 효과 구현, 아이템 회전
    /// </summary>
    public class PickUpItem : MonoBehaviour
    {
        #region Variables
        //아이템 효과 정보- hp회복
        [SerializeField]
        private float healthRestore = 10f;

        //아이템 회전 연출
        [SerializeField]
        private Vector3 rotationSpeed = new Vector3(0f, 100f, 0f);

        //감지된 충돌체
        public List<Collider2D> detectiveColliders = new List<Collider2D>();
        #endregion

        #region Property

        #endregion

        #region Unity Event Method
        private void Update()
        {
            //회전
            this.transform.eulerAngles += rotationSpeed * Time.deltaTime;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //아이템 픽업
            bool isPickUp = PickUp(collision);
            if (isPickUp)
            {
                //아이템 삭제
                Destroy(gameObject);
            }

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            detectiveColliders.Remove(collision);   
        }
        #endregion

        #region Custom Method
        //픽업 시 아이템 효과 구현, 성공시 true, 실패시 false - hp회복
        protected virtual bool PickUp(Collider2D collision)
        {
            bool isUse = false;

            Damageable damageable = collision.GetComponent<Damageable>();
            
            if(damageable != null)
            {
                isUse = damageable.Heal(healthRestore);
            }

            return isUse;
        }
      
        //아이템을 회전 시킨다
        
        #endregion
    }
}
