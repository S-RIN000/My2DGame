using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// 발사체를 발사
    /// </summary>
    public class ProjectileLauncher : MonoBehaviour
    {
        #region Variables
        //발사체 프리팹
        public GameObject projectilePrefabs;

        //발사 지점
        public Transform firePoint;
        #endregion

        #region Custom Method
        //발사체 발사
        public void FireProjectile()
        {
            GameObject projectileGo = Instantiate(projectilePrefabs, firePoint.position, projectilePrefabs.transform.rotation);
            Vector3 originScale = projectileGo.transform.localScale;

            //공격자의 방향에 맞춰 방향을 정한다 
            projectileGo.transform.localScale = new Vector3( originScale.x * this.transform.localScale.x > 0f ? 1 : -1 , originScale.y, originScale.z);

            //킬 예약
            Destroy(projectileGo, 2f);
        }
        #endregion
    }
}
