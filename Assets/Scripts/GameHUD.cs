using UnityEngine;
using TMPro; // Required for TextMeshPro

public class GameHUD : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI Attack_Power;
    public int damage = 1;

    void Update()
    {
        // Continuously update the text to match the GameManager's gold value
        if (GameManager.Instance != null && goldText != null)
        {
            goldText.text = "Gold: " + GameManager.Instance.playerGold;
        }
        if (GameManager.Instance != null && Attack_Power != null)
        {
            Attack_Power.text = "Attack_Power: " + GameManager.Instance.ExtraAttackPower + damage;
        }

    }
}