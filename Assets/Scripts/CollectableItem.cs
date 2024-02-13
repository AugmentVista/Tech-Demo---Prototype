using TMPro;
using UnityEngine;
public class CollectibleItem : MonoBehaviour
{
    [SerializeField]
    private UIManager UImanager;
    [SerializeField]
    private FloatingPlatformsAbility floatingPlatformsAbility;

    public TextMeshProUGUI RockCountText;

    public float rotationSpeed = 30f; 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("UImanager: " + UImanager);
        Debug.Log("floatingPlatformsAbility: " + floatingPlatformsAbility);
        if (other.CompareTag("Player"))
        {
            CollectItem();
        }
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
    private void CollectItem()
    {
        floatingPlatformsAbility.RockCount++;
        gameObject.SetActive(false);
        UImanager.UpdateRockCount(floatingPlatformsAbility.RockCount);
    }
}