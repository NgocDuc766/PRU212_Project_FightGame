using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject healingItemPrefab;
    public GameObject manaItemPrefab;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnItem), 0f, 20f); // Gọi hàm SpawnItem mỗi 10 giây
    }

    private void SpawnItem()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            0f
        );

        // Random chọn vật phẩm hồi máu hoặc mana
        GameObject item = Random.value > 0.5f ? Instantiate(healingItemPrefab, spawnPosition, Quaternion.identity) : Instantiate(manaItemPrefab, spawnPosition, Quaternion.identity);

        if (item.GetComponent<HealingItem>() != null)
            item.GetComponent<HealingItem>().Respawn(spawnPosition);
        else if (item.GetComponent<ManaItem>() != null)
            item.GetComponent<ManaItem>().Respawn(spawnPosition);
    }
}
