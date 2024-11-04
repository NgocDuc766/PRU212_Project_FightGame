using UnityEngine;

public class ManaItem : MonoBehaviour
{
    public int ManaAmount = 10;
    private float spawnInterval = 20f;
    private float lifetime = 5f;

    private void OnEnable()
    {
        Invoke(nameof(Despawn), lifetime);  // Sau lifetime giây, nếu không nhặt thì biến mất
    }

    private void Despawn()
    {
        gameObject.SetActive(false);
        // Tắt vật phẩm để chờ lần xuất hiện tiếp
    }

    public void Respawn(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        // Kích hoạt vật phẩm
    }
}
