using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    TextMeshPro PatternText;
    SpriteRenderer PatternSpriteRenderer;

    private void Awake()
    {
        PatternText = GetComponentInChildren<TextMeshPro>();
        PatternSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public void SetPattern(EnemyPattern pattern, int EnemyDamge)
    {
        int currentDamage = EnemyDamge + pattern.DamageAmount;
        PatternSpriteRenderer.sprite = pattern.patternImage;
        PatternText.text = currentDamage.ToString();
    }
        
    
}
