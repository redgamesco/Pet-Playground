using UnityEngine;
[RequireComponent(typeof(RectTransform))]

[ExecuteAlways]
public class ScaleToSpace : MonoBehaviour
{
    //Like aspect size setter, but just scales up statically to fit screen constraints
    //Normally, we'd set up our UI to use Anchors/Scaling appropriately, but sometimes it's nice to just scale for games like this.

    public float scaleOffsetPercent = 1.00f; //also our case is a little big, so we're just gonna offset the scaling a hair
    private Vector2 cachedSize = Vector2.zero;
    private void Update()
    {
        var screenDimensions = new Vector2(Screen.width, Screen.height);
        if (cachedSize != screenDimensions)
        {
            cachedSize = screenDimensions;
            UpdateScaling();
        }
    }
    private void UpdateScaling()
    {
        var rectTransform = GetComponent<RectTransform>();
        Canvas canvasParent = GetComponentInParent<Canvas>();

        float xScaleMax = cachedSize.x / (rectTransform.sizeDelta.x);
        float yScaleMax = cachedSize.y / (rectTransform.sizeDelta.y);

        float uniformScaleTarget = Mathf.Min(xScaleMax, yScaleMax);
        uniformScaleTarget *= scaleOffsetPercent;
        rectTransform.localScale = new Vector2(uniformScaleTarget, uniformScaleTarget);
    }

}
