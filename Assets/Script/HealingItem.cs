using UnityEngine;

public class HealingItem : MonoBehaviour
{
    public int healAmount = 10;       
    private float spawnInterval = 5f; 
    private float lifetime = 5f;       

    private void OnEnable()
    {
        Invoke(nameof(Despawn), lifetime);  // Sau lifetime giây, nếu không nhặt thì biến mất
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
    //        if (playerHealth != null)
    //        {
    //            playerHealth.Heal(healAmount); // Hồi máu cho player
    //        }
    //        Despawn();
    //    }
    //}

    private void Despawn()
    {
        gameObject.SetActive(false);
        Debug.Log("Despawned");
        // Tắt vật phẩm để chờ lần xuất hiện tiếp
    }

    public void Respawn(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        Debug.Log("Respawned");
        // Kích hoạt vật phẩm
    }
}
