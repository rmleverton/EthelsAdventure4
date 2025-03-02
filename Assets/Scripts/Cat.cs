//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class Cat : MonoBehaviour
//{
//    [SerializeField] private string illness;
//    [SerializeField] private string diagnosis;
//    [SerializeField] private string catName;
//    //[SerializeField] private Transform targetPoint;
//    [SerializeField] private Transform testPoint;
//    [SerializeField] private Transform sleepPoint;
//    [SerializeField] private Transform wayPoint;
//    private float speed = 2f;

//    [SerializeField] private SpriteRenderer spriteRenderer;
//    private Sprite externalImage;
//    private Sprite illImage;

//    [SerializeField] private GameObject prescriptionUI;
//    [SerializeField] private SpriteRenderer prescriptionUIImage;
//    [SerializeField] private Sprite defaultSad;

//    public bool highlighted;
//    public bool finalCat;

//    public Vector3 waitPoint { get; private set; } // Changed from Transform to Vector3
//    [SerializeField] private Vector3 targetPosition; // Changed from Transform

//    public enum CatState
//    {
//        Spawning,
//        MovingToTest,
//        WaitingForDiagnosis,
//        MovingToWaypoint,
//        MovingToSleep,
//        Sleeping,
//        Hungry,
//        MovingToWait,  // New state
//        Waiting        // New state
//    }

//    public CatState CurrentState { get; private set; } = CatState.Spawning;

//    public void Initialize(string _illness, string _name, Transform _testPoint, Sprite _exImage, Sprite _illImage, Vector3 _waitPoint)
//    {
//        illness = _illness;
//        catName = _name;
//        testPoint = _testPoint;
//        spriteRenderer.sprite = _exImage;
//        illImage = _illImage;
//        waitPoint = _waitPoint;
//        prescriptionUI.SetActive(false);
//        gameObject.SetActive(true);
//    }

//    public void SetDiagnosis(string _diagnosis, Sprite _prescription)
//    {
//        diagnosis = _diagnosis;
//        prescriptionUIImage.sprite = _prescription;
//        prescriptionUI.SetActive(true);

//        if (wayPoint != null)
//        {
//            MoveTo(wayPoint.position, CatState.MovingToWaypoint);
//        }
//        else
//        {
//            MoveTo(sleepPoint.position, CatState.MovingToSleep);
//        }
//    }

//    public void MoveTo(Vector3 target, CatState newState)
//    {
//        Debug.Log("Moving to: " + target);
//        //targetPoint.position = target;
//        targetPosition = target;
//        CurrentState = newState;
//    }

//    public void SetSleepPoint(Transform point) => sleepPoint = point;
//    public void SetWayPoint(Transform point) => wayPoint = point;
//    public Sprite GetIllnessImage() => illImage;

//    public bool CanBeFed => CurrentState == CatState.Hungry;

//    public void Feed(ItemInstance medicine)
//    {
//        if (CurrentState == CatState.Hungry)
//        {
//            if (medicine.GetItemCure() == illness)
//            {
//                prescriptionUI.SetActive(false);
//                CurrentState = CatState.Sleeping;
//            }
//            else
//            {
//                prescriptionUIImage.sprite = defaultSad;
//                CurrentState = CatState.Sleeping;
//            }

//            if (finalCat)
//            {
//                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//            }
//        }
//    }

//    private void Update()
//    {
//        if (targetPosition != null)
//        {
//            MoveTowardsTarget();
//        }

//        if (highlighted)
//        {
//            spriteRenderer.color = Color.yellow;
//        }
//        else
//        {
//            spriteRenderer.color = Color.white;
//        }
//    }

//    private void MoveTowardsTarget()
//    {
//        float step = speed * Time.deltaTime;
//        //transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);
//        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

//        // Calculate the direction of movement
//        Vector3 moveDirection = (targetPosition - transform.position).normalized;

//        // Rotate only around Y-axis
//        if (moveDirection != Vector3.zero)
//        {
//            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
//            spriteRenderer.transform.rotation = Quaternion.Euler(75, angle - 90, 0);
//        }

//        if (moveDirection.x < 0)
//        {
//            spriteRenderer.flipY = true;
//        }
//        else
//        {
//            spriteRenderer.flipY = false;
//        }


//        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
//        {
//            OnReachedTarget();
//        }
//    }

//    //private void OnReachedTarget()
//    //{
//    //    switch (CurrentState)
//    //    {
//    //        case CatState.MovingToTest:
//    //            CurrentState = CatState.WaitingForDiagnosis;
//    //            break;

//    //        case CatState.MovingToWaypoint:
//    //            MoveTo(sleepPoint.position, CatState.MovingToSleep);
//    //            break;

//    //        case CatState.MovingToSleep:
//    //            CurrentState = CatState.Hungry;
//    //            break;
//    //    }
//    //}
//    private void OnReachedTarget()
//    {
//        switch (CurrentState)
//        {
//            case CatState.MovingToTest:
//                CurrentState = CatState.WaitingForDiagnosis;
//                break;

//            case CatState.MovingToWait:
//                CurrentState = CatState.Waiting;
//                //targetPosition = null;
//                break;

//            case CatState.MovingToWaypoint:
//                MoveTo(sleepPoint.position, CatState.MovingToSleep);
//                break;

//            case CatState.MovingToSleep:
//                CurrentState = CatState.Hungry;
//                break;
//        }
//    }
//}
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cat : MonoBehaviour
{
    [SerializeField] private string illness;
    [SerializeField] private string diagnosis;
    [SerializeField] private string catName;
    [SerializeField] private Transform testPoint;
    [SerializeField] private Transform sleepPoint;
    [SerializeField] private Transform wayPoint;
    private float speed = 2f;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private Sprite externalImage;
    private Sprite illImage;

    [SerializeField] private GameObject prescriptionUI;
    [SerializeField] private SpriteRenderer prescriptionUIImage;
    [SerializeField] private Sprite defaultSad;

    public bool highlighted;
    public bool finalCat;

    private Vector3 waitPoint;
    private Vector3 targetPosition;

    private CatManager catManager;

    public enum CatState
    {
        Spawning,
        MovingToWait,
        Waiting,
        MovingToTest,
        WaitingForDiagnosis,
        MovingToWaypoint,
        MovingToSleep,
        Hungry,
        Sleeping
    }

    public CatState CurrentState { get; private set; } = CatState.Spawning;

    public void Initialize(CatManager manager, string _illness, string _name, Transform _testPoint, Sprite _exImage, Sprite _illImage, Vector3 _waitPoint)
    {
        catManager = manager;
        illness = _illness;
        catName = _name;
        testPoint = _testPoint;
        externalImage = _exImage;
        illImage = _illImage;
        waitPoint = _waitPoint;
        spriteRenderer.sprite = externalImage;
        prescriptionUI.SetActive(false);
        gameObject.SetActive(true);

        // Start moving to wait point
        MoveTo(waitPoint, CatState.MovingToWait);
    }

    public void SetDiagnosis(string _diagnosis, Sprite _prescription)
    {
        diagnosis = _diagnosis;
        prescriptionUIImage.sprite = _prescription;
        prescriptionUI.SetActive(true);

        // Notify CatManager this cat is leaving the test point
        catManager.CatLeftTestPoint();

        if (wayPoint != null)
        {
            MoveTo(wayPoint.position, CatState.MovingToWaypoint);
        }
        else
        {
            MoveTo(sleepPoint.position, CatState.MovingToSleep);
        }
    }

    public void MoveTo(Vector3 target, CatState newState)
    {
        targetPosition = target;
        CurrentState = newState;
    }

    public void MoveToTestPoint()
    {
        MoveTo(testPoint.position, CatState.MovingToTest);
    }

    public void SetSleepPoint(Transform point) => sleepPoint = point;
    public void SetWayPoint(Transform point) => wayPoint = point;
    public Sprite GetIllnessImage() => illImage;

    public bool CanBeFed => CurrentState == CatState.Hungry;

    public void Feed(ItemInstance medicine)
    {
        if (CurrentState == CatState.Hungry)
        {
            if (medicine.GetItemCure() == illness)
            {
                prescriptionUI.SetActive(false);
                CurrentState = CatState.Sleeping;
            }
            else
            {
                prescriptionUIImage.sprite = defaultSad;
                CurrentState = CatState.Sleeping;
            }

            if (finalCat)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void Update()
    {
        if (CurrentState == CatState.MovingToWait ||
            CurrentState == CatState.MovingToTest ||
            CurrentState == CatState.MovingToWaypoint ||
            CurrentState == CatState.MovingToSleep)
        {
            MoveTowardsTarget();
        }

        spriteRenderer.color = highlighted ? Color.yellow : Color.white;
    }

    private void MoveTowardsTarget()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            spriteRenderer.transform.rotation = Quaternion.Euler(75, angle - 90, 0);
        }

        spriteRenderer.flipY = moveDirection.x < 0;

        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            OnReachedTarget();
        }
    }

    private void OnReachedTarget()
    {
        switch (CurrentState)
        {
            case CatState.MovingToWait:
                CurrentState = CatState.Waiting;
                catManager.AddCatToQueue(this);
                break;

            case CatState.MovingToTest:
                CurrentState = CatState.WaitingForDiagnosis;
                break;

            case CatState.MovingToWaypoint:
                MoveTo(sleepPoint.position, CatState.MovingToSleep);
                break;

            case CatState.MovingToSleep:
                CurrentState = CatState.Hungry;
                break;
        }
    }
}