using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private FloatingPlatformsAbility floatingPlatformsAbility;
    [SerializeField] private FirstPersonController playerController;
    public TextMeshProUGUI speedText;
    public float speed;

    private void FixedUpdate()
    {
        speed = playerController.rb.velocity.magnitude;

        speedText.text = "Speed: " + speed.ToString("F2"); // Display speed with 2 decimals
    }
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
