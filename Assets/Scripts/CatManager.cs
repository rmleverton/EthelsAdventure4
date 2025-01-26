////using UnityEngine;

////public class CatManager : MonoBehaviour
////{
////    [Header("Cat Configuration")]
////    [SerializeField] private GameObject catPrefab; // Prefab for the Cat
////    [SerializeField] private Transform spawnPoint; // Where cats will spawn
////    [SerializeField] private string[] illnesses; // List of illnesses
////    [SerializeField] private string[] names; // List of possible names
////    //[SerializeField] private Sprite[][] sprites; // List of possible cat sprites
////    [SerializeField] private SpriteGroup[] spriteGroups; // List of sprite groups

////    [Header("Spawn Settings")]
////    [SerializeField] private float spawnInterval = 200.0f; // Time between spawns
////    private float spawnTimer;

////    private void Update()
////    {
////        // Handle spawning cats at intervals
////        spawnTimer += Time.deltaTime;
////        if (spawnTimer >= spawnInterval)
////        {
////            SpawnCat();
////            spawnTimer = 0f;
////        }
////    }

////    private void SpawnCat()
////    {
////        if (catPrefab == null || illnesses.Length == 0 || names.Length == 0 || sprites.Length == 0)
////        {
////            Debug.LogWarning("CatManager is not properly configured.");
////            return;
////        }

////        // Instantiate the Cat prefab
////        GameObject newCat = Instantiate(catPrefab, spawnPoint.position, Quaternion.identity);
////        Cat catComponent = newCat.GetComponent<Cat>();

////        if (catComponent == null)
////        {
////            Debug.LogWarning("Cat prefab does not have a Cat component.");
////            return;
////        }

////        // Randomize illness, name, and sprite
////        string randomIllness = illnesses[Random.Range(0, illnesses.Length)];
////        string randomName = names[Random.Range(0, names.Length)];
////        Sprite[] CatSpriteGroup = sprites[Random.Range(0, sprites.Length)];

////        // Assign properties to the Cat
////        catComponent.SetIllness(randomIllness);
////        catComponent.SetName(randomName);
////        //newCat.GetComponent<SpriteRenderer>().sprite = randomSprite;

////        // Assign symptoms
////        AssignSymptoms(newCat, CatSpriteGroup, randomIllness);

////        Debug.Log($"Spawned cat '{randomName}' with illness '{randomIllness}'.");
////    }

////    private void AssignSymptoms(GameObject newCat, Sprite[] spriteGroup, string illness)
////    {

////        if (illness == "Mange")
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[0];
////        }
////        else if (illness == "Catatonia")
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[1];
////        }
////        else if (illness == "Crestfeline")
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[2];
////        }
////        else if (illness == "Dysentery")
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[3];
////        }
////        else if (illness == "Mad Cat Disease")
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[4];
////        }
////        else if (illness == "Feline Flu")
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[5];
////        }
////        else if (illness == "Catnip Withdrawal")
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[6];
////        }
////        else if (illness == "Radiation Sickness")
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[7];
////        }
////        else if (illness == "Wasteland Parasite")
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[8];
////        }

////    }
////}
////using UnityEngine;

////public class CatManager : MonoBehaviour
////{
////    [Header("Cat Configuration")]
////    [SerializeField] private GameObject catPrefab; // Prefab for the Cat
////    [SerializeField] private Transform spawnPoint; // Where cats will spawn
////    [SerializeField] private Transform testPoint; // Where cats will spawn
////    [SerializeField] private Transform[] sleepPoint; // Where cats will spawn
////    [SerializeField] private string[] illnesses; // List of illnesses
////    [SerializeField] private string[] names; // List of possible names
////    [SerializeField] private SpriteGroup[] spriteGroups; // List of sprite groups

////    [Header("Spawn Settings")]
////    [SerializeField] private float spawnInterval = 200.0f; // Time between spawns
////    private float spawnTimer;

////    private void Start()
////    {
////        SpawnCat();
////    }
////    private void Update()
////    {
////        // Handle spawning cats at intervals
////        spawnTimer += Time.deltaTime;
////        if (spawnTimer >= spawnInterval)
////        {
////            SpawnCat();
////            spawnTimer = 0f;
////        }
////    }

////    private void SpawnCat()
////    {
////        if (catPrefab == null || illnesses.Length == 0 || names.Length == 0 || spriteGroups.Length == 0)
////        {
////            Debug.LogWarning("CatManager is not properly configured.");
////            return;
////        }

////        // Instantiate the Cat prefab
////        GameObject newCat = Instantiate(catPrefab, spawnPoint.position, Quaternion.identity);
////        Cat catComponent = newCat.GetComponent<Cat>();

////        if (catComponent == null)
////        {
////            Debug.LogWarning("Cat prefab does not have a Cat component.");
////            return;
////        }

////        // Randomize illness, name, and sprite group
////        string randomIllness = illnesses[Random.Range(0, illnesses.Length)];
////        string randomName = names[Random.Range(0, names.Length)];
////        SpriteGroup randomSpriteGroup = spriteGroups[Random.Range(0, spriteGroups.Length)];

////        // Assign properties to the Cat
////        catComponent.SetIllness(randomIllness);
////        catComponent.SetName(randomName);

////        // Assign symptoms
////        AssignSymptoms(newCat, randomSpriteGroup.sprites, randomIllness);

////        Debug.Log($"Spawned cat '{randomName}' with illness '{randomIllness}'.");
////    }

////    private void AssignSymptoms(GameObject newCat, Sprite[] spriteGroup, string illness)
////    {
////        if (spriteGroup == null || spriteGroup.Length == 0)
////        {
////            Debug.LogWarning("Sprite group is empty or not assigned.");
////            return;
////        }


////        // Continue for other illnesses as needed...
////        if (illness == "Mange" && spriteGroup.Length > 0)
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[0];
////        }
////        else if (illness == "Catatonia" && spriteGroup.Length > 1)
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[1];
////        }
////        else if (illness == "Crestfeline" && spriteGroup.Length > 2)
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[2];
////        }
////        else if (illness == "Dysentery" && spriteGroup.Length > 3)
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[3];
////        }
////        else if (illness == "Mad Cat Disease" && spriteGroup.Length > 4)
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[4];
////        }
////        else if (illness == "Feline Flu" && spriteGroup.Length > 5)
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[5];
////        }
////        else if (illness == "Catnip Withdrawal" && spriteGroup.Length > 6)
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[6];
////        }
////        else if (illness == "Radiation Sickness" && spriteGroup.Length > 7)
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[7];
////        }
////        else if (illness == "Wasteland Parasite" && spriteGroup.Length > 8)
////        {
////            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[8];
////        }
////    }
////}





//using UnityEngine;

//public class CatManager : MonoBehaviour
//{
//    [Header("Cat Configuration")]
//    [SerializeField] private GameObject catPrefab;
//    [SerializeField] private Transform spawnPoint;
//    [SerializeField] private Transform testPoint;
//    //[SerializeField] private Transform[] sleepPoints;
//    [SerializeField] private string[] illnesses;
//    [SerializeField] private string[] names;
//    [SerializeField] private SpriteGroup[] spriteGroups;

//    [SerializeField] private CatMovement[] catMovements;
//    [SerializeField] private Cat[] cats;

//    [Header("Spawn Settings")]
//    [SerializeField] private float spawnInterval = 200.0f;
//    private float spawnTimer;

//    [SerializeField] private Transform[] sleepPoints;
//    private bool[] occupiedSleepPoints;


//    private void Start()
//    {
//        occupiedSleepPoints = new bool[sleepPoints.Length];
//        SpawnCat();
//    }

//    private void Update()
//    {
//        spawnTimer += Time.deltaTime;
//        if (spawnTimer >= spawnInterval)
//        {
//            SpawnCat();
//            spawnTimer = 0f;
//        }
//    }

//    private void SpawnCat()
//    {
//        if (catPrefab == null || illnesses.Length == 0 || names.Length == 0 || spriteGroups.Length == 0)
//        {
//            Debug.LogWarning("CatManager is not properly configured.");
//            return;
//        }

//        GameObject newCat = Instantiate(catPrefab, spawnPoint.position, Quaternion.identity);
//        Cat catComponent = newCat.GetComponent<Cat>();
//        catMovements.Add(newCat.AddComponent<CatMovement>());

//        if (catComponent == null)
//        {
//            Debug.LogWarning("Cat prefab does not have a Cat component.");
//            return;
//        }

//        // Randomize cat properties
//        string randomIllness = illnesses[Random.Range(0, illnesses.Length)];
//        string randomName = names[Random.Range(0, names.Length)];
//        SpriteGroup randomSpriteGroup = spriteGroups[Random.Range(0, spriteGroups.Length)];

//        // Assign properties
//        catComponent.SetIllness(randomIllness);
//        catComponent.SetName(randomName);

//        // Assign symptoms
//        AssignSymptoms(newCat, randomSpriteGroup.sprites, randomIllness);

//        Debug.Log($"Spawned cat '{randomName}' with illness '{randomIllness}'.");

//        AssignSleepPoint(newCat);
//    }

//    private void AssignSymptoms(GameObject newCat, Sprite[] spriteGroup, string illness)
//    {
//        if (spriteGroup == null || spriteGroup.Length == 0)
//        {
//            Debug.LogWarning("Sprite group is empty or not assigned.");
//            return;
//        }


//        // Continue for other illnesses as needed...
//        if (illness == "Mange" && spriteGroup.Length > 0)
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[0];
//        }
//        else if (illness == "Catatonia" && spriteGroup.Length > 1)
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[1];
//        }
//        else if (illness == "Crestfeline" && spriteGroup.Length > 2)
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[2];
//        }
//        else if (illness == "Dysentery" && spriteGroup.Length > 3)
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[3];
//        }
//        else if (illness == "Mad Cat Disease" && spriteGroup.Length > 4)
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[4];
//        }
//        else if (illness == "Feline Flu" && spriteGroup.Length > 5)
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[5];
//        }
//        else if (illness == "Catnip Withdrawal" && spriteGroup.Length > 6)
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[6];
//        }
//        else if (illness == "Radiation Sickness" && spriteGroup.Length > 7)
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[7];
//        }
//        else if (illness == "Wasteland Parasite" && spriteGroup.Length > 8)
//        {
//            newCat.GetComponent<SpriteRenderer>().sprite = spriteGroup[8];
//        }
//    }

//    private void AssignSleepPoint(GameObject cat)
//    {
//        // Find lowest available sleep point
//        for (int i = 0; i < sleepPoints.Length; i++)
//        {
//            if (!occupiedSleepPoints[i])
//            {
//                occupiedSleepPoints[i] = true;
//                catMovement.sleepPoint = sleepPoints[i].position;

//                //// Store sleep point index on the cat for later reference
//                //CatSleepTracker tracker = cat.AddComponent<CatSleepTracker>();
//                //tracker.SleepPointIndex = i;
//                break;
//            }
//        }
//    }

//    // Method to free up a sleep point when a cat is removed
//    public void FreeSleepPoint(int sleepPointIndex)
//    {
//        occupiedSleepPoints[sleepPointIndex] = false;
//    }

//}

//[System.Serializable]
//public class SpriteGroup
//{
//    public string groupName; // Optional: Name to identify the group
//    public Sprite[] sprites; // Array of sprites in this group
//}

////// Companion script to track sleep point
////public class CatSleepTracker : MonoBehaviour
////{
////    public int SleepPointIndex { get; set; }

////    private void OnDestroy()
////    {
////        // When cat is removed, free up the sleep point
////        gameObject.GetComponent<CatManager>().FreeSleepPoint(SleepPointIndex);
////    }
////}
///


using UnityEngine;

public class CatManager : MonoBehaviour
{
    [Header("Cat Configuration")]
    [SerializeField] private GameObject catPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform testPoint;
    [SerializeField] private Transform wayPointN;
    [SerializeField] private Transform wayPointS;
    [SerializeField] private Transform[] sleepPoints;
    [SerializeField] private string[] illnesses;
    [SerializeField] private string[] names;
    [SerializeField] private SpriteGroup[] spriteGroups;

    private bool[] occupiedSleepPoints;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 5f;
    private float spawnTimer;

    private void Start()
    {
        occupiedSleepPoints = new bool[sleepPoints.Length];
        SpawnCat();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnCat();
            spawnTimer = 0f;
        }
    }

    private void SpawnCat()
    {
        //if (catPrefab == null || illnesses.Length == 0 || names.Length == 0 || spriteGroups.Length == 0)
        //{
        //    Debug.LogWarning("CatManager is not properly configured.");
        //    return;
        //}

        //GameObject newCatObj = Instantiate(catPrefab, spawnPoint.position, Quaternion.identity);
        //Cat newCat = newCatObj.GetComponent<Cat>();
        if (catPrefab == null || illnesses.Length == 0 || names.Length == 0 || spriteGroups.Length == 0)
        {
            Debug.LogWarning("CatManager is not properly configured.");
            return;
        }

        GameObject newCatObj = Instantiate(catPrefab, spawnPoint.position, Quaternion.identity, transform);
        Cat newCat = newCatObj.GetComponent<Cat>();

        string randomIllness = illnesses[Random.Range(0, illnesses.Length)];
        string randomName = names[Random.Range(0, names.Length)];
        SpriteGroup randomSpriteGroup = spriteGroups[Random.Range(0, spriteGroups.Length)];

        newCat.Initialize(randomIllness, randomName, testPoint);
        AssignSymptoms(newCatObj, randomSpriteGroup.sprites, randomIllness);
        AssignSleepPoint(newCat);

        newCat.MoveTo(testPoint, Cat.CatState.MovingToTest);

        Debug.Log($"Spawned cat '{randomName}' with illness '{randomIllness}'.");
    }

    private void AssignSymptoms(GameObject catObj, Sprite[] spriteGroup, string illness)
    {
        if (spriteGroup == null || spriteGroup.Length == 0)
        {
            Debug.LogWarning("Sprite group is empty or not assigned.");
            return;
        }

        SpriteRenderer renderer = catObj.GetComponent<SpriteRenderer>();

        if (illness == "Mange" && spriteGroup.Length > 0)
            renderer.sprite = spriteGroup[0];
        else if (illness == "Catatonia" && spriteGroup.Length > 1)
            renderer.sprite = spriteGroup[1];
        else if (illness == "Crestfeline" && spriteGroup.Length > 2)
            renderer.sprite = spriteGroup[2];
        else if (illness == "Dysentery" && spriteGroup.Length > 3)
            renderer.sprite = spriteGroup[3];
        else if (illness == "Mad Cat Disease" && spriteGroup.Length > 4)
            renderer.sprite = spriteGroup[4];
        else if (illness == "Feline Flu" && spriteGroup.Length > 5)
            renderer.sprite = spriteGroup[5];
        else if (illness == "Catnip Withdrawal" && spriteGroup.Length > 6)
            renderer.sprite = spriteGroup[6];
        else if (illness == "Radiation Sickness" && spriteGroup.Length > 7)
            renderer.sprite = spriteGroup[7];
        else if (illness == "Wasteland Parasites" && spriteGroup.Length > 8)
            renderer.sprite = spriteGroup[8];
        // Add additional illnesses and sprites as needed
    }

    public bool AssignSleepPoint(Cat cat)
    {
        for (int i = 0; i < sleepPoints.Length; i++)
        {
            //if (!occupiedSleepPoints[i])
            //{
            //    occupiedSleepPoints[i] = true;
            //    cat.SetSleepPoint(sleepPoints[i]);
            //    //cat.MoveTo(sleepPoints[i], Cat.CatState.MovingToSleep);
            //    return true;
            //}

            if (!occupiedSleepPoints[i])
            {
                occupiedSleepPoints[i] = true;
                Transform selectedSleepPoint = sleepPoints[i];

                // Calculate distances
                float distanceToSleepPoint = Vector3.Distance(testPoint.position, selectedSleepPoint.position);
                float distanceToWaypointN = Vector3.Distance(testPoint.position, wayPointN.position);
                float distanceToWaypointS = Vector3.Distance(testPoint.position, wayPointS.position);

                // Determine closest waypoint
                Transform closestWaypoint = null;
                if (distanceToWaypointN < distanceToSleepPoint || distanceToWaypointS < distanceToSleepPoint)
                {
                    closestWaypoint = (distanceToWaypointN <= distanceToWaypointS) ? wayPointN : wayPointS;
                    cat.SetWayPoint(closestWaypoint); // Assign closest waypoint if found

                }


                // Set waypoint and sleep point for the cat
                cat.SetSleepPoint(selectedSleepPoint);

                return true;
            }
        
    }

        Debug.LogWarning("No available sleep points for the cat.");
        return false;
    }

    public void FreeSleepPoint(Transform sleepPoint)
    {
        for (int i = 0; i < sleepPoints.Length; i++)
        {
            if (sleepPoints[i] == sleepPoint)
            {
                occupiedSleepPoints[i] = false;
                break;
            }
        }
    }
}

[System.Serializable]
public class SpriteGroup
{
    public string groupName;
    public Sprite[] sprites;
}
