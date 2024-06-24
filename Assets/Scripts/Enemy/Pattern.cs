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

        SetAlpha(0.0f);
        StartCoroutine(FadeInEffect());

    }
    /// <summary>
    /// 텍스트와 이미지를 천천히 사라지게 하는 코루틴
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeOutEffect()
    {
        float duration = 0.5f; // 페이드 아웃 시간
        float elapsedTime = 0f;

        Color initialTextColor = PatternText.color;
        Color initialSpriteColor = PatternSpriteRenderer.color;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            PatternText.color = new Color(initialTextColor.r, initialTextColor.g, initialTextColor.b, alpha);
            PatternSpriteRenderer.color = new Color(initialSpriteColor.r, initialSpriteColor.g, initialSpriteColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 완전히 투명하게 설정
        PatternText.color = new Color(initialTextColor.r, initialTextColor.g, initialTextColor.b, 0f);
        PatternSpriteRenderer.color = new Color(initialSpriteColor.r, initialSpriteColor.g, initialSpriteColor.b, 0f);
    }
    /// <summary>
    /// 텍스트와 이미지를 천천히 등장시키는 코루틴
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeInEffect()
    {
        float duration = 1.0f; // 페이드 인 시간
        float elapsedTime = 0f;

        Color initialTextColor = PatternText.color;
        Color initialSpriteColor = PatternSpriteRenderer.color;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            PatternText.color = new Color(initialTextColor.r, initialTextColor.g, initialTextColor.b, alpha);
            PatternSpriteRenderer.color = new Color(initialSpriteColor.r, initialSpriteColor.g, initialSpriteColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 완전히 불투명하게 설정
        PatternText.color = new Color(initialTextColor.r, initialTextColor.g, initialTextColor.b, 1f);
        PatternSpriteRenderer.color = new Color(initialSpriteColor.r, initialSpriteColor.g, initialSpriteColor.b, 1f);
    }
    private void SetAlpha(float alpha)
    {
        Color textColor = PatternText.color;
        PatternText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);

        Color spriteColor = PatternSpriteRenderer.color;
        PatternSpriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
    }

}
