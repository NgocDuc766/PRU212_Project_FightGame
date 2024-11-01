using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    private Animator animator;
    // variable for comboAttack
    public int comboStep = 0;
    private float lastAttackTime;
    public float comboResetTime = 1f;  // Thời gian để reset combo nếu không nhấn kịp
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Reset combo nếu thời gian giữa các đòn quá lâu
        if (Time.time - lastAttackTime > comboResetTime)
        {
            ResetCombo();
        }

        // Kiểm tra khi người chơi nhấn nút đánh (ở đây là chuột trái)
        if (Input.GetKeyUp(KeyCode.J))
        {
            PerformCombo();
        }
    }

    void PerformCombo()
    {
        comboStep++;  // Tăng bước combo
        if (comboStep > 3) comboStep = 1;  // Giới hạn số bước combo

        animator.SetInteger("ComboIndex", comboStep);  // Gửi comboStep tới Animator
        animator.SetTrigger("Attack");  // Kích hoạt đòn đánh
        lastAttackTime = Time.time;  // Cập nhật thời gian của đòn đánh gần nhất
    }

    void ResetCombo()
    {
        comboStep = 0;
        animator.SetInteger("ComboIndex", 0);  // Reset trạng thái combo
    }
}
