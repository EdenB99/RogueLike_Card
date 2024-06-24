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
    /// �ؽ�Ʈ�� �̹����� õõ�� ������� �ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeOutEffect()
    {
        float duration = 0.5f; // ���̵� �ƿ� �ð�
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

        // ������ �����ϰ� ����
        PatternText.color = new Color(initialTextColor.r, initialTextColor.g, initialTextColor.b, 0f);
        PatternSpriteRenderer.color = new Color(initialSpriteColor.r, initialSpriteColor.g, initialSpriteColor.b, 0f);
    }
    /// <summary>
    /// �ؽ�Ʈ�� �̹����� õõ�� �����Ű�� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeInEffect()
    {
        float duration = 1.0f; // ���̵� �� �ð�
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

        // ������ �������ϰ� ����
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
