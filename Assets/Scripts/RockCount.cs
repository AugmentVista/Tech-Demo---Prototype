using UnityEngine;
using TMPro;

public class RockCount : MonoBehaviour
{
    private TextMeshPro rockCounter;

    void Start()
    {
        GameObject rockFrame = GameObject.Find("RockFrame"); 
        if (rockFrame != null)
        {
            rockCounter = rockFrame.GetComponentInChildren<TextMeshPro>(); 
        }
    }

    public void UpdateRockCount(int count)
    {
        if (rockCounter != null)
        {
            rockCounter.text = count.ToString(); 
        }
    }
}
