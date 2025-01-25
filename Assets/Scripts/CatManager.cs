//using UnityEngine;

//public class CatManager : MonoBehaviour
//{
//    [Header("Cat Configuration")]
//    [SerializeField] private GameObject catPrefab; // Prefab for the Cat
//    [SerializeField] private Transform spawnPoint; // Where cats will spawn
//    [SerializeField] private string[] illnesses; // List of illnesses
//    [SerializeField] private string[] names; // List of possible names
//    //[SerializeField] private Sprite[][] sprites; // List of possible cat sprites
//    [SerializeField] private SpriteGroup[] spriteGroups; // List of sprite groups

//    [Header("Spawn Settings")]
//    [SerializeField] private float spawnInterval = 200.0f; // Time between spawns
//    private float spawnTimer;

//    private void Update()
//    {
//        // Handle spawning cats at intervals
//        spawnTimer += Time.deltaTime;
//        if (spawnTimer >= spawnInterval)
//        {
//            SpawnCat();
//            spawnTimer = 0f;
//        }
//    }

//    private void SpawnCat()
//    {
//        if (catPrefab == null || illnesses.Length == 0 || names.Length == 0 || sprites.Length == 0)
//        {
//            Debug.LogWarning("CatManager is not properly configured.");
//            return;
//        }

//        // Instantiate the Cat prefab
//        GameObject newCat = Instantiate(catPrefab, spawnPoint.position, Quaternion.identity);
//        Cat catComponent = newCat.GetComponent<Cat>();

//        if (catComponent == null)
//        {
//            Debug.LogWarning("Cat prefab does not have a Cat component.");
//            return;
//        }

//        // Randomize illness, name, and sprite
//        string randomIllness = illnesses[Random.Range(0, illnesses.Length)];
//        string randomName = names[Random.Range(0, names.Length)];
//        Sprite[] CatSpriteGroup = sprites[Random.Range(0, sprites.Length)];

//        // Assign properties to the Cat
//        catComponent.SetIllness(randomIllness);
//        catComponent.SetName(randomName);
//        //newCat.GetComponent<SpriteRenderer>().sprite = randomSprite;

//        // Assign symptoms
//        AssignSymptoms(newCat, CatSpriteGroup, randomIllness);

//        Debug.Log($"Spawned cat '{randomName}' with illness '{randomIllness}'.");
//    }

//    private void AssignSymptoms(GameObject newCat, Sprite[] spriteGroup, string illness)
//    {

//        if (illness == "Mange")
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[0];
//        }
//        else if (illness == "Catatonia")
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[1];
//        }
//        else if (illness == "Crestfeline")
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[2];
//        }
//        else if (illness == "Dysentery")
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[3];
//        }
//        else if (illness == "Mad Cat Disease")
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[4];
//        }
//        else if (illness == "Feline Flu")
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[5];
//        }
//        else if (illness == "Catnip Withdrawal")
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[6];
//        }
//        else if (illness == "Radiation Sickness")
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[7];
//        }
//        else if (illness == "Wasteland Parasite")
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[8];
//        }

//    }
//}
using UnityEngine;

public class CatManager : MonoBehaviour
{
    [Header("Cat Configuration")]
    [SerializeField] private GameObject catPrefab; // Prefab for the Cat
    [SerializeField] private Transform spawnPoint; // Where cats will spawn
    [SerializeField] private string[] illnesses; // List of illnesses
    [SerializeField] private string[] names; // List of possible names
    [SerializeField] private SpriteGroup[] spriteGroups; // List of sprite groups

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 200.0f; // Time between spawns
    private float spawnTimer;

    private void Start()
    {
        SpawnCat();
    }
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
        if (catPrefab == null || illnesses.Length == 0 || names.Length == 0 || spriteGroups.Length == 0)
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

        // Randomize illness, name, and sprite group
        string randomIllness = illnesses[Random.Range(0, illnesses.Length)];
        string randomName = names[Random.Range(0, names.Length)];
        SpriteGroup randomSpriteGroup = spriteGroups[Random.Range(0, spriteGroups.Length)];

        // Assign properties to the Cat
        catComponent.SetIllness(randomIllness);
        catComponent.SetName(randomName);

        // Assign symptoms
        AssignSymptoms(newCat, randomSpriteGroup.sprites, randomIllness);

        Debug.Log($"Spawned cat '{randomName}' with illness '{randomIllness}'.");
    }

    private void AssignSymptoms(GameObject newCat, Sprite[] spriteGroup, string illness)
    {
        if (spriteGroup == null || spriteGroup.Length == 0)
        {
            Debug.LogWarning("Sprite group is empty or not assigned.");
            return;
        }


        // Continue for other illnesses as needed...
        if (illness == "Mange" && spriteGroup.Length > 0)
        {
            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[0];
        }
        else if (illness == "Catatonia" && spriteGroup.Length > 1)
        {
            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[1];
        }
        else if (illness == "Crestfeline" && spriteGroup.Length > 2)
        {
            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[2];
        }
        else if (illness == "Dysentery" && spriteGroup.Length > 3)
        {
            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[3];
        }
        else if (illness == "Mad Cat Disease" && spriteGroup.Length > 4)
        {
            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[4];
        }
        else if (illness == "Feline Flu" && spriteGroup.Length > 5)
        {
            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[5];
        }
        else if (illness == "Catnip Withdrawal" && spriteGroup.Length > 6)
        {
            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[6];
        }
        else if (illness == "Radiation Sickness" && spriteGroup.Length > 7)
        {
            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[7];
        }
        else if (illness == "Wasteland Parasite" && spriteGroup.Length > 8)
        {
            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[8];
        }
    }
}



[System.Serializable]
public class SpriteGroup
{
    public string groupName; // Optional: Name to identify the group
    public Sprite[] sprites; // Array of sprites in this group
}
