using UnityEngine;

public class Attack : MonoBehaviour

{
    public int damage = 1;
    public int ExtraAttackPower = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ExtraAttackPower += ExtraAttackPower + 1;
                Debug.Log("Attack Power is: " + GameManager.Instance.ExtraAttackPower + damage);
            }

            Destroy(gameObject);
        }
    }
}
