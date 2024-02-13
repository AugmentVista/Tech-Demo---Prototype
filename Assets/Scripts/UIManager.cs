using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private
    FloatingPlatformsAbility floatingPlatformsAbility;
    public void UpdateRockUI()
    {
        UpdateRockCount(floatingPlatformsAbility.RockCount);
        Debug.Log(floatingPlatformsAbility.RockCount);
    }
    public void UpdateRockCount(int count)
    {
        floatingPlatformsAbility.RockCountText.text = "Rock-Platforms\n" + count.ToString();
        Debug.Log(floatingPlatformsAbility.RockCountText.text);
    }
}
