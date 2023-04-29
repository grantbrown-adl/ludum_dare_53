using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private float moveSpeedX, moveSpeedY;
    public static DamagePopup Create(Vector2 position, int damageAmount, bool isCritical, int directionX, string colour)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.Instance.pfDamagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup($"{damageAmount}", isCritical, directionX, colour);

        return damagePopup;
    }

    [SerializeField] TextMeshPro textMesh;
    [SerializeField] float fadeTimer;
    [SerializeField] Color textColor;
    [SerializeField] float fadeSpeed;
    [SerializeField] float increaseScaleAmount;
    [SerializeField] float decreaseScaleAmount;
    [SerializeField] float decreaseMoveVectorSpeed;
    [SerializeField] float vectorSpeedFactor;
    private static int sortingOrder;
    Vector3 textMoveVector;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Update()
    {        
        transform.position += textMoveVector * Time.deltaTime;
        textMoveVector -= decreaseMoveVectorSpeed * Time.deltaTime * textMoveVector;

        if(fadeTimer > fadeSpeed * 0.5f)
        {
            increaseScaleAmount = 1.0f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        } else
        {
            decreaseScaleAmount = 1.0f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        fadeTimer -= Time.deltaTime;
        if(fadeTimer <= 0)
        {
            textColor.a -= fadeSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a <= 0) Destroy(this.gameObject);
        }
       
    }

    public void Setup(string text, bool isCritical, int directionX, string colour)
    {
        if(isCritical)
        {
            textMesh.fontSize *= 1.5f;
            textColor = ToColor("red");

        } else
        {
            textColor = ToColor(colour);
            //textColor = textMesh.color;
        }
        textMesh.color = textColor;
        textMesh.SetText(text);

        if(sortingOrder >= 2147483647) sortingOrder = 1;
        else sortingOrder++;

        Debug.Log($"Sorting Order: {sortingOrder}");
        textMesh.sortingOrder = sortingOrder;
        

        textMoveVector = new Vector3(directionX, moveSpeedY) * vectorSpeedFactor;
    }

    public static Color ToColor(string colour)
    {
        return (Color)typeof(Color).GetProperty(colour.ToLowerInvariant()).GetValue(null, null);
    }
}