//using UnityEngine;
//using System.Collections.Generic;
//using System.Collections;

//public class CatManager : MonoBehaviour
//{
//    [Header("Cat Configuration")]
//    [SerializeField] private GameObject catPrefab;
//    [SerializeField] private Transform spawnPoint;
//    [SerializeField] private Transform testPoint;
//    [SerializeField] private Transform[] sleepPoints;
//    [SerializeField] private string[] illnesses;
//    [SerializeField] private string[] names;
//    [SerializeField] private SpriteGroup[] spriteGroups;

//    [Header("Spawn Settings")]
//    [SerializeField] private float spawnInterval = 5f;
//    private float spawnTimer = 0;

//    private bool[] occupiedSleepPoints;
//    private List<Cat> cats = new List<Cat>();
//    [SerializeField] private Queue<Cat> waitingCats = new Queue<Cat>();

//    private void Start()
//    {
//        occupiedSleepPoints = new bool[sleepPoints.Length];

//    }

//    private void Update()
//    {
//        if (spawnTimer < spawnInterval)
//        {
//            spawnTimer += Time.deltaTime;

//            if (spawnTimer >= spawnInterval)
//            {
//                StartCoroutine(SpawnCatsRoutine());
//            }
//        }

//    }
//    private IEnumerator SpawnCatsRoutine()
//    {
//        while (true)
//        {
//            SpawnCat();
//            yield return new WaitForSeconds(spawnInterval);
//        }
//    }

//    //private void SpawnCat()
//    //{
//    //    GameObject newCatObj = Instantiate(catPrefab, spawnPoint.position, Quaternion.identity, transform);
//    //    Cat newCat = newCatObj.GetComponent<Cat>();

//    //    string randomIllness = illnesses[Random.Range(0, illnesses.Length)];
//    //    string randomName = names[Random.Range(0, names.Length)];
//    //    SpriteGroup randomSpriteGroup = spriteGroups[Random.Range(0, spriteGroups.Length)];
//    //    Sprite externalImage = randomSpriteGroup.sprites[0];
//    //    Sprite illnessImage = AssignSymptoms(randomSpriteGroup.sprites, randomIllness);
//    //    Vector3 targetpoint = new Vector3(Random.Range(-6.5f, 6.5f), 0.55f, Random.Range(-2.5f, 2.5f));

//    //    newCat.Initialize(randomIllness, randomName, testPoint, externalImage, illnessImage, targetpoint);
//    //    AssignSleepPoint(newCat);
//    //    AddCatToWaitingList(newCat);
//    //    cats.Add(newCat);

//    //}

//    //public void AddCatToWaitingList(Cat cat)
//    //{
//    //    waitingCats.Enqueue(cat);
//    //    Debug.Log("waitingCats: " + waitingCats.Count);
//    //    if (waitingCats.Count == 1)
//    //    {
//    //        Debug.Log("waitingCats: " + waitingCats.Count);
//    //        cat.MoveTo(testPoint.position, Cat.CatState.MovingToTest);
//    //    }
//    //    else
//    //    {
//    //        cat.MoveTo(randPoint, Cat.CatState.MovingToTest);
//    //    }
//    //}
//    // Modified spawning and queue management
//    private void SpawnCat()
//    {
//        GameObject newCatObj = Instantiate(catPrefab, spawnPoint.position, Quaternion.identity, transform);
//        Cat newCat = newCatObj.GetComponent<Cat>();

//        string randomIllness = illnesses[Random.Range(0, illnesses.Length)];
//        string randomName = names[Random.Range(0, names.Length)];
//        SpriteGroup randomSpriteGroup = spriteGroups[Random.Range(0, spriteGroups.Length)];
//        Sprite externalImage = randomSpriteGroup.sprites[0];
//        Sprite illnessImage = AssignSymptoms(randomSpriteGroup.sprites, randomIllness);
//        //Create specific wait point for this cat

//        Vector3 targetpoint = new Vector3(Random.Range(-6.5f, -2.0f), 0.55f, Random.Range(-2.5f, 2.5f));

//        newCat.Initialize(randomIllness, randomName, testPoint, externalImage, illnessImage, targetpoint);
//        AssignSleepPoint(newCat);
//        //AddCatToWaitingList(newCat);
//        cats.Add(newCat);
//    }

//    //public void AddCatToWaitingList(Cat cat)
//    //{
//    //    waitingCats.Enqueue(cat);

//    //    if (waitingCats.Count == 1)
//    //    {
//    //        // First cat goes directly to test point
//    //        cat.MoveTo(testPoint.position, Cat.CatState.MovingToTest);
//    //    }
//    //    else
//    //    {
//    //        // Subsequent cats go to their waiting positions
//    //        cat.MoveTo(cat.waitPoint, Cat.CatState.MovingToWait);
//    //    }
//    //}
//    public void AddCatToWaitingList(Cat cat)
//    {
//        waitingCats.Enqueue(cat);

//        if (waitingCats.Count == 1)
//        {
//            // Use testPoint's position directly
//            cat.MoveTo(testPoint.position, Cat.CatState.MovingToTest);
//        }
//        else
//        {
//            // Use the pre-defined wait point vector
//            cat.MoveTo(cat.waitPoint, Cat.CatState.MovingToWait);
//        }
//    }

//    // Call this when the current cat is diagnosed to move next cat
//    public void ProcessNextCat()
//    {
//        if (waitingCats.Count > 0)
//        {
//            Cat nextCat = waitingCats.Dequeue();
//            nextCat.MoveTo(testPoint.position, Cat.CatState.MovingToTest);

//            //// Update positions of remaining waiting cats
//            //foreach (Cat waitingCat in waitingCats)
//            //{
//            //    if (waitingCat.CurrentState == Cat.CatState.Waiting)
//            //    {
//            //        // Move waiting cats forward in the queue if needed
//            //    }
//            //}
//        }
//    }

//    public Cat GetNextCatForDiagnosis()
//    {
//        if (waitingCats.Count > 0)
//        {
//            return waitingCats.Dequeue();
//        }
//        return null;
//    }

//    private Sprite AssignSymptoms(Sprite[] spriteGroup, string illness)
//    {
//        for (int i = 1; i < spriteGroup.Length; i++)
//        {
//            if (spriteGroup[i].name == illness)
//            {
//                return spriteGroup[i];
//            }
//        }
//        return null;
//    }

//    private bool AssignSleepPoint(Cat cat)
//    {
//        for (int i = 0; i < sleepPoints.Length; i++)
//        {
//            if (!occupiedSleepPoints[i])
//            {
//                occupiedSleepPoints[i] = true;
//                cat.SetSleepPoint(sleepPoints[i]);

//                if (i == sleepPoints.Length - 1)
//                {
//                    cat.finalCat = true;
//                }

//                return true;
//            }
//        }
//        return false;
//    }
//}

//[System.Serializable]
//public class SpriteGroup
//{
//    public string groupName;
//    public Sprite[] sprites;
//}

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CatManager : MonoBehaviour
{
    [Header("Cat Configuration")]
    [SerializeField] private GameObject catPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform testPoint;
    [SerializeField] private Transform[] sleepPoints;
    [SerializeField] private string[] illnesses;
    [SerializeField] private string[] names;
    [SerializeField] private SpriteGroup[] spriteGroups;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 5f;

    private bool[] occupiedSleepPoints;
    private List<Cat> cats = new List<Cat>();
    private Queue<Cat> waitingCats = new Queue<Cat>();
    private Cat currentCatAtTest;

    private void Start()
    {
        occupiedSleepPoints = new bool[sleepPoints.Length];
        StartCoroutine(SpawnCatsRoutine());
    }

    private IEnumerator SpawnCatsRoutine()
    {
        while (true)
        {
            SpawnCat();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnCat()
    {
        GameObject newCatObj = Instantiate(catPrefab, spawnPoint.position, Quaternion.identity, transform);
        Cat newCat = newCatObj.GetComponent<Cat>();

        string randomIllness = illnesses[Random.Range(0, illnesses.Length)];
        string randomName = names[Random.Range(0, names.Length)];
        SpriteGroup randomSpriteGroup = spriteGroups[Random.Range(0, spriteGroups.Length)];
        Sprite externalImage = randomSpriteGroup.sprites[0];
        Sprite illnessImage = AssignSymptoms(randomSpriteGroup.sprites, randomIllness);
        Vector3 waitPoint = new Vector3(Random.Range(-6.5f, -2.0f), 0.55f, Random.Range(-2.5f, 2.5f));

        newCat.Initialize(this, randomIllness, randomName, testPoint, externalImage, illnessImage, waitPoint);
        AssignSleepPoint(newCat);
        cats.Add(newCat);
    }

    public void AddCatToQueue(Cat cat)
    {
        waitingCats.Enqueue(cat);
        TryProcessNextCat();
    }

    public void CatLeftTestPoint()
    {
        currentCatAtTest = null;
        TryProcessNextCat();
    }

    private void TryProcessNextCat()
    {
        if (currentCatAtTest == null && waitingCats.Count > 0)
        {
            currentCatAtTest = waitingCats.Dequeue();
            currentCatAtTest.MoveToTestPoint();
        }
    }

    private Sprite AssignSymptoms(Sprite[] spriteGroup, string illness)
    {
        for (int i = 1; i < spriteGroup.Length; i++)
        {
            if (spriteGroup[i].name == illness)
                return spriteGroup[i];
        }
        return null;
    }

    private bool AssignSleepPoint(Cat cat)
    {
        for (int i = 0; i < sleepPoints.Length; i++)
        {
            if (!occupiedSleepPoints[i])
            {
                occupiedSleepPoints[i] = true;
                cat.SetSleepPoint(sleepPoints[i]);

                if (i == sleepPoints.Length - 1)
                    cat.finalCat = true;

                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class SpriteGroup
{
    public string groupName;
    public Sprite[] sprites;
}