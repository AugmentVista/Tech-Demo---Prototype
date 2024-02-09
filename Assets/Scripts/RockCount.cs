using UnityEngine;
using TMPro;
public class RockCount : MonoBehaviour
{
    private TextMeshPro rockCounter;

    void Start()
    {
        rockCounter = GetComponentInChildren<TextMeshPro>();
    }
    public void UpdateRockCount(int count)
    {
        if (rockCounter != null)
        {
            rockCounter.text = count.ToString();
        }
    }
}

