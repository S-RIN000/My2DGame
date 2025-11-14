using Unity.VisualScripting;
using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// 원거리 공격 발사를 관리하는 클래스 
    /// </summary>
    public class Projectile : Attack
    {
        #region Variables
        //참조
        private Rigidbody2D rd2D;

        //이동 속도
        [SerializeField]
        private Vector2 moveSpeed = new Vector2(5f, 0f);
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            rd2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            rd2D.linearVelocity = new Vector2(moveSpeed.x * transform.localScale.x , moveSpeed.y);
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            //화살 킬
            Destroy(this.gameObject);
        }
        #endregion

        #region
        #endregion
    }
}
