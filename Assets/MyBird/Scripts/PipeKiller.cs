using UnityEngine;

namespace MyBird
{
    /// <summary>
    /// 지나가는(충돌하는) 기둥 오브젝트 킬
    /// </summary>
    public class PipeKiller : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }
    }
}
