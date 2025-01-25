using UnityEngine;

public class CatManager : MonoBehaviour
{
    [Header("Cat Configuration")]
    [SerializeField] private GameObject catPrefab; // Prefab for the Cat
    [SerializeField] private Transform spawnPoint; // Where cats will spawn
    [SerializeField] private string[] illnesses; // List of illnesses
    [SerializeField] private string[] names; // List of possible names
    [SerializeField] private Sprite[] sprites; // List of possible cat sprites
    [SerializeField] private SpriteRenderer[] symptomLayers; // Array of symptom sprite renderers

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 200.0f; // Time between spawns
    private float spawnTimer;

    private void Update()
    {
        // Handle spawning cats at intervals
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnCat();
            spawnTimer = 0f;
        }
    }

    private void SpawnCat()
    {
        if (catPrefab == null || illnesses.Length == 0 || names.Length == 0 || sprites.Length == 0)
        {
            Debug.LogWarning("CatManager is not properly configured.");
            return;
        }

        // Instantiate the Cat prefab
        GameObject newCat = Instantiate(catPrefab, spawnPoint.position, Quaternion.identity);
        Cat catComponent = newCat.GetComponent<Cat>();

        if (catComponent == null)
        {
            Debug.LogWarning("Cat prefab does not have a Cat component.");
            return;
        }

        // Randomize illness, name, and sprite
        string randomIllness = illnesses[Random.Range(0, illnesses.Length)];
        string randomName = names[Random.Range(0, names.Length)];
        Sprite randomSprite = sprites[Random.Range(0, sprites.Length)];

        // Assign properties to the Cat
        catComponent.SetIllness(randomIllness);
        catComponent.SetName(randomName);
        newCat.GetComponent<SpriteRenderer>().sprite = randomSprite;

        // Assign symptoms
        AssignSymptoms(newCat.GetComponent<SpriteRenderer>(), randomIllness);

        Debug.Log($"Spawned cat '{randomName}' with illness '{randomIllness}'.");
    }

    private void AssignSymptoms(SpriteRenderer spriteRenderer, string illness)
    {
        // Example: Configure the sprite layers based on the illness
        // Map illnesses to specific symptom decals (as sprite layers)
        foreach (var layer in symptomLayers)
        {
            layer.enabled = false; // Disable all symptom layers initially
        }

        if (illness == "Fever")
        {
            symptomLayers[0].enabled = true; // Enable a specific layer for Fever
        }
        else if (illness == "Cough")
        {
            symptomLayers[1].enabled = true; // Enable a specific layer for Cough
        }
        // Add more conditions for other illnesses and layers as needed
    }
}
