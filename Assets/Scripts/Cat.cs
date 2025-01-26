using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private string illness;
    [SerializeField] private string diagnosis;
    [SerializeField] private string catName;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private Transform testPoint;
    [SerializeField] private Transform sleepPoint;
    [SerializeField] private Transform wayPoint;
    private float speed = 2f;


    //private void Awake()
    //{
    //    Debug.Log($"Cat Awake: {gameObject.activeSelf}");
    //}

    public enum CatState
    {
        Spawning,
        MovingToTest,
        WaitingForDiagnosis,
        MovingToWaypoint,
        MovingToSleep,
        Sleeping,
        Hungry
    }

    public bool CanBeFed => CurrentState == CatState.Hungry;

    public void Feed()
    {
        if (CurrentState == CatState.Hungry)
        {
            // Move directly to sleep point
            if (sleepPoint != null)
            {
                MoveTo(sleepPoint, CatState.MovingToSleep);
                Debug.Log($"Cat {catName} has been fed and is now moving to sleep.");
            }
        }
    }

    public CatState CurrentState { get; private set; } = CatState.Spawning;

    //public void Initialize(string _illness, string _name, Transform point)
    //{
    //    illness = _illness;
    //    catName = _name;
    //    CurrentState = CatState.Spawning;
    //    testPoint = point;
    //}
    public void Initialize(string _illness, string _name, Transform point)
    {
        illness = _illness;
        catName = _name;
        CurrentState = CatState.Spawning;
        testPoint = point;

        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (targetPoint != null)
        {
            MoveTowardsTarget();
        }
        if(CurrentState == CatState.Hungry)
        {
            //stuff
        }
    }

    private void MoveTowardsTarget()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.5f)
        {
            OnReachedTarget();
        }
    }

    private void OnReachedTarget()
    {
        if (CurrentState == CatState.MovingToWaypoint && wayPoint != null)
        {
            // After reaching waypoint, set target to sleep point
            wayPoint = null;
            targetPoint = sleepPoint;
            CurrentState = CatState.MovingToSleep;
        }
        else
        {
            targetPoint = null;

            switch (CurrentState)
            {
                case CatState.MovingToTest:
                    transform.position = testPoint.position;
                    CurrentState = CatState.WaitingForDiagnosis;
                    break;

                case CatState.MovingToSleep:
                    transform.position = sleepPoint.position;
                    CurrentState = CatState.Hungry;
                    break;
            }
        }
    }

    public void MoveTo(Transform target, CatState newState)
    {
        targetPoint = target;
        CurrentState = newState;
    }

    public void SetDiagnosis(string _diagnosis)
    {
        diagnosis = _diagnosis;

        // Check if a waypoint exists
        if (wayPoint != null)
        {
            MoveTo(wayPoint, CatState.MovingToWaypoint);
        }
        else
        {
            MoveTo(sleepPoint, CatState.MovingToSleep);
        }
    }

    public void SetSleepPoint(Transform point)
    {
        sleepPoint = point;
    }

    public void SetWayPoint(Transform point)
    {
        //wayPoint = point;
        //Debug.Log("Waypoint set to: " + point.name);
        if (point == null)
        {
            Debug.Log("SetWayPoint received a null Transform.");
            //return;
        }

        wayPoint = point;
        Debug.Log($"Waypoint set to: {point.name}");

    }
    //public void SetTestPoint(Transform point)
    //{
    //    testPoint = point;
    //}
}
