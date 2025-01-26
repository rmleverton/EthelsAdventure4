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
        Sprite externalImage = randomSpriteGroup.sprites[0];
        Sprite illnessImage = AssignSymptoms(randomSpriteGroup.sprites, randomIllness);

        newCat.Initialize(randomIllness, randomName, testPoint, externalImage, illnessImage);
        //AssignSymptoms(newCatObj, randomSpriteGroup.sprites, randomIllness);
        AssignSleepPoint(newCat);

        newCat.MoveTo(testPoint, Cat.CatState.MovingToTest);

        Debug.Log($"Spawned cat '{randomName}' with illness '{randomIllness}'.");
    }

    private Sprite AssignSymptoms( Sprite[] spriteGroup, string illness)
    {
        if (spriteGroup == null || spriteGroup.Length == 0)
        {
            Debug.LogWarning("Sprite group is empty or not assigned.");
            return null;
        }

        if (illness == "Mange" && spriteGroup.Length > 1)
            return spriteGroup[1];
        else if (illness == "Catatonia" && spriteGroup.Length > 2)
            return spriteGroup[2];
        else if (illness == "Crestfeline" && spriteGroup.Length > 3)
            return spriteGroup[3];
        else if (illness == "Dysentery" && spriteGroup.Length > 4)
            return spriteGroup[4];
        else if (illness == "Mad Cat Disease" && spriteGroup.Length > 5)
            return spriteGroup[5];
        else if (illness == "Feline Flu" && spriteGroup.Length > 6)
            return spriteGroup[6];
        else if (illness == "Catnip Withdrawal" && spriteGroup.Length > 7)
            return spriteGroup[7];
        else if (illness == "Radiation Sickness" && spriteGroup.Length > 8)
            return spriteGroup[8];
        else if (illness == "Wasteland Parasites" && spriteGroup.Length > 9)
            return spriteGroup[9];
        else
        {
            return null;
        }
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
