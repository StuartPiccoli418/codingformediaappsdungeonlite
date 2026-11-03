using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DirectorState { BuildUp, Peak, Fade }

public class AIDirector : MonoBehaviour
{
    public static AIDirector Instance { get; private set; }

    [Header("Player Tracking")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float playerMaxHealth = 100f;
    private float currentVehicleStress = 0f;

    [Header("Director Tuning")]
    [SerializeField] private float stressDecayRate = 5f;
    [SerializeField] private float peakThreshold = 85f;
    [SerializeField] private float fadeThreshold = 30f;
    [SerializeField] private float peakDuration = 15f;

    [Header("Current Status")]
    public DirectorState currentState = DirectorState.BuildUp;
    public float currentStress = 0f;
    
    private float peakTimer = 0f;
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        CalculateStress();
        EvaluateState();
    }

    private void CalculateStress()
    {
        // 1. Stress from missing health
        float healthPercentage = MockPlayerHealth(); // Replace with your actual player health script
        float healthStress = (1f - healthPercentage) * 50f; 

        // 2. Stress from proximity to active enemies
        float proximityStress = 0f;
        activeEnemies.RemoveAll(item => item == null); // Clean dead references
        
        foreach (var enemy in activeEnemies)
        {
            float distance = Vector3.Distance(playerTransform.position, enemy.transform.position);
            if (distance < 15f) // Enemy is close
            {
                proximityStress += (15f - distance) * 2f; 
            }
        }

        // Combine and clamp
        float targetStress = healthStress + proximityStress;
        
        if (targetStress < currentStress)
        {
            currentStress -= stressDecayRate * Time.deltaTime;
        }
        else
        {
            currentStress = Mathf.Lerp(currentStress, targetStress, Time.deltaTime * 2f);
        }

        currentStress = Mathf.Clamp(currentStress, 0f, 100f);
    }

    private void EvaluateState()
    {
        switch (currentState)
        {
            case DirectorState.BuildUp:
                if (currentStress >= peakThreshold)
                {
                    currentState = DirectorState.Peak;
                    peakTimer = peakDuration;
                }
                break;

            case DirectorState.Peak:
                peakTimer -= Time.deltaTime;
                if (peakTimer <= 0f)
                {
                    currentState = DirectorState.Fade;
                }
                break;

            case DirectorState.Fade:
                if (currentStress <= fadeThreshold)
                {
                    currentState = DirectorState.BuildUp;
                }
                break;
        }
    }

    // Call this when your procedural generator or spawners build an enemy
    public void RegisterEnemy(GameObject enemy)
    {
        activeEnemies.Add(enemy);
    }

    // Interface methods for Spawners to query what they are allowed to do
    public bool CanSpawnHorde() => currentState == DirectorState.BuildUp && currentStress < 50f;
    public bool CanSpawnRoamingEnemy() => currentState != DirectorState.Fade;
    public bool ShouldDropSupplies() => currentState == DirectorState.Fade || currentStress > 70f;

    // Placeholders - link to your actual player script
    private float MockPlayerHealth()
    {
        // Replace this return with: ActualPlayerScript.Instance.Health / MaxHealth
        return 0.8f; 
    }
}

