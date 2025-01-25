//using UnityEngine;

//public class CatMovement : MonoBehaviour
//{
//    [SerializeField] private Transform testPoint;
//    [SerializeField] private Transform sleepPoint;

//    private Cat catComponent;
//    private SpriteRenderer spriteRenderer;
//    private Transform currentTarget;
//    private int assignedSleepIndex = -1;

//    public enum CatState
//    {
//        Spawning,
//        MovingToTest,
//        WaitingForDiagnosis,
//        MovingToSleep,
//        Sleeping,
//        Hungry
//    }
//    public CatState CurrentState { get; private set; } = CatState.Spawning;

//    private void Start()
//    {
//        catComponent = GetComponent<Cat>();
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        MoveToTestPoint();
//    }

//    private void Update()
//    {
//        if (currentTarget != null)
//        {
//            MoveTowardsTarget();
//        }
//    }

//    private void MoveTowardsTarget()
//    {
//        float step = 2f * Time.deltaTime; // Adjust speed as needed
//        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, step);

//        if (Vector3.Distance(transform.position, currentTarget.position) < 0.01f)
//        {
//            OnReachedTarget();
//        }
//    }

//    private void MoveToTestPoint()
//    {
//        currentTarget = testPoint;
//        CurrentState = CatState.MovingToTest;
//    }

//    private void OnReachedTarget()
//    {
//        switch (CurrentState)
//        {
//            case CatState.MovingToTest:
//                CurrentState = CatState.WaitingForDiagnosis;
//                currentTarget = null;
//                break;

//            case CatState.MovingToSleep:
//                CurrentState = CatState.Sleeping;
//                currentTarget = null;
//                break;
//        }
//    }

//    public void AssignToSleepPoint()
//    {
//        // Find the lowest available sleep point
//        for (int i = 0; i < sleepPoints.Length; i++)
//        {
//            if (!IsSleepPointOccupied(i))
//            {
//                assignedSleepIndex = i;
//                currentTarget = sleepPoints[i];
//                CurrentState = CatState.MovingToSleep;
//                break;
//            }
//        }
//    }

//    private bool IsSleepPointOccupied(int index)
//    {
//        // Check if any other cat is already at this sleep point
//        Collider[] colliders = Physics.OverlapSphere(sleepPoints[index].position, 0.5f);
//        foreach (Collider collider in colliders)
//        {
//            if (collider.gameObject != gameObject && collider.GetComponent<CatMovement>() != null)
//            {
//                return true;
//            }
//        }
//        return false;
//    }

//    public void Feed()
//    {
//        if (CurrentState == CatState.Hungry)
//        {
//            CurrentState = CatState.Sleeping;
//        }
//    }
//}