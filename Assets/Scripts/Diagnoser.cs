using UnityEngine;
using UnityEngine.UI;

public class Diagnoser : MonoBehaviour
{
    [SerializeField] private GameObject uiMenu; // Reference to the UI menu GameObject
    private GameObject _cat;
    [SerializeField] private Image catImage;
    [SerializeField] private Sprite[] meds;

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
            Sprite illImage = _cat.GetComponent<Cat>().GetIllnessImage();
            catImage.sprite = illImage;
                
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
        Sprite diagnosisSprite = GetDiagnosisSprite(diagnosis);
        _cat.GetComponent<Cat>().SetDiagnosis(diagnosis, diagnosisSprite);
        CloseMenu();
        Debug.Log(diagnosis);

    }

    private Sprite GetDiagnosisSprite(string diagnosis)
    {
        if (diagnosis == "Mange" && meds.Length > 0)
            return meds[0];
        else if (diagnosis == "Catatonia" && meds.Length > 1)
            return meds[1];
        else if (diagnosis == "Crestfeline" && meds.Length > 2)
            return meds[2];
        else if (diagnosis == "Dysentery" && meds.Length > 3)
            return meds[3];
        else if (diagnosis == "Mad Cat Disease" && meds.Length > 4)
            return meds[4];
        else if (diagnosis == "Feline Flu" && meds.Length > 5)
            return meds[5];
        else if (diagnosis == "Catnip Withdrawal" && meds.Length > 6)
            return meds[6];
        else if (diagnosis == "Radiation Sickness" && meds.Length > 7)
            return meds[7];
        else if(diagnosis == "Wasteland Parasites" && meds.Length > 8)
            return meds[8];
        else
        {
            return null;
        }
    }
}
