using UnityEngine;
using System.Collections.Generic;


namespace My2DGame
{
    /// <summary>
    /// 트리거 충돌체에 들어오는 모든 충돌체 감지해서 수집
    /// </summary>
    public class DetectionZone : MonoBehaviour
    {
        #region Variables
        //감지된 충돌체 리스트
        public List<Collider2D> detectiveColliders = new List<Collider2D>();
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //충돌체가 들어오면 리스트에 추가
            detectiveColliders.Add(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //충돌체가 나가면 리스트에서 제거
            detectiveColliders.Remove(collision);
        }
        #endregion
    }
}
