//using UnityEngine;

//public class Cat : MonoBehaviour
//{
//    [SerializeField] private string illness;
//    [SerializeField] private string diagnosis;
//    [SerializeField] private string catName; // The name of the cat

//    public void SetIllness(string _illness)
//    {
//        illness = _illness;
//    }

//    public void SetName(string _name)
//    {
//        catName = _name;
//    }

//    public void SetDiagnosis(string _diagnosis)
//    {
//        gameObject.tag = "dCat";
//        diagnosis = _diagnosis;
//    }
//}


using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private string illness;
    [SerializeField] private string diagnosis;
    [SerializeField] private string catName;
    private Transform targetPoint;
    private Transform sleepPoint;
    private float speed = 2f;

    public enum CatState
    {
        Spawning,
        MovingToTest,
        WaitingForDiagnosis,
        MovingToSleep,
        Sleeping,
        Hungry
    }

    public CatState CurrentState { get; private set; } = CatState.Spawning;

    public void Initialize(string _illness, string _name)
    {
        illness = _illness;
        catName = _name;
        CurrentState = CatState.Spawning;
    }

    private void Update()
    {
        if (targetPoint != null)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
        {
            OnReachedTarget();
        }
    }

    private void OnReachedTarget()
    {
        targetPoint = null;

        switch (CurrentState)
        {
            case CatState.MovingToTest:
                CurrentState = CatState.WaitingForDiagnosis;
                break;

            case CatState.MovingToSleep:
                CurrentState = CatState.Sleeping;
                break;
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
        MoveTo(sleepPoint, CatState.MovingToSleep);
        

    }

    public void SetSleepPoint(Transform point)
    {
        sleepPoint = point;
    }
}
