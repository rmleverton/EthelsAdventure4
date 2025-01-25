using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private string illness;
    [SerializeField] private string diagnosis;
    [SerializeField] private string catName; // The name of the cat

    public void SetIllness(string _illness)
    {
        illness = _illness;
    }

    public void SetName(string _name)
    {
        catName = _name;
    }

    public void SetDiagnosis(string _diagnosis)
    {
        gameObject.tag = "dCat";
        diagnosis = _diagnosis;
    }
}
