using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject cubePrefab;

    void Start()
    {
        int diceroll = Random.Range(1, 11);

        if (diceroll <= 6)
        {
            Instantiate(cubePrefab, transform.position, Quaternion.identity);
            Debug.Log("Trap has spawned!");
        }
        else
        {
            Debug.Log("Tresure has spawned!");
        }
    }

    void Update()
    {
        
    }
}
