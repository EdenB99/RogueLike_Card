using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CostIcon : MonoBehaviour
{
    private TextMeshProUGUI CostText;

    private void Awake()
    {
        CostText = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        GameManager.Instance.Player.OnEnergyChange += CostChange;
    }
    private void CostChange(int  cost)
    {
        CostText.text = cost.ToString();
    }
}
