using UnityEngine;

public class Diagnoser : MonoBehaviour
{
    [SerializeField] private GameObject uiMenu; // Reference to the UI menu GameObject
    private GameObject _cat;

    private void Start()
    {
        // Ensure the UI menu is initially disabled
        if (uiMenu != null)
        {
            uiMenu.SetActive(false);
        }
        else
        {
            Debug.LogWarning("UI menu is not assigned in the Diagnoser script.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Collider {other}");
        // Check if the object entering the collider has the "Cat" tag
        if (other.CompareTag("Cat"))
        {
            _cat = other.gameObject;
                
            Debug.Log("Open UI");
            OpenMenu();
        }
    }

    private void OpenMenu()
    {
        if (uiMenu != null)
        {
            uiMenu.SetActive(true); // Enable the UI menu
            Debug.Log("UI menu opened.");
        }
    }

    private void CloseMenu()
    {
        if (uiMenu != null)
        {
            uiMenu.SetActive(false); // Disable the UI menu
            Debug.Log("UI menu closed.");
        }
    }

    public void MakeDiagnosis(string diagnosis){
        _cat.GetComponent<Cat>().SetDiagnosis(diagnosis);
        CloseMenu();
        Debug.Log(diagnosis);

    }
}
