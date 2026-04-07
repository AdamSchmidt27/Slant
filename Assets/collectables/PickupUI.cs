using TMPro;
using UnityEngine;

public class PickupUI : MonoBehaviour
{
    public static PickupUI Instance;

    public TMP_Text pickupText;
    public int totalPickups = 10;

    private int pickupCount = 0;

    private void Awake()
    {
        Instance = this;
        UpdateUI();
    }

    public void AddPickup(int amount)
    {
        pickupCount += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        pickupText.text = "Apples: " + pickupCount + " / " + totalPickups;
    }
}
