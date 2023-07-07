using UnityEngine;

[ExecuteInEditMode]
public class GradientSkyboxShaderController : MonoBehaviour
{
    [SerializeField] private Gradient dayGradient;
    [SerializeField] private Gradient nightGradient;
    [Range(0f, 24f)]
    [SerializeField] private float timeOfDay;
    [Range(0, 360f)]
    [SerializeField] private float rotation;

    private float zeroOneTimeOfDay = 0f;
    private SpriteRenderer spriteRenderer;
    private Material material;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.sharedMaterial;
        zeroOneTimeOfDay = timeOfDay / 24f;
        material.SetColor("_TopColor", dayGradient.Evaluate(zeroOneTimeOfDay));
        material.SetColor("_BottomColor", nightGradient.Evaluate(zeroOneTimeOfDay));
        material.SetFloat("_TimeOfDay", zeroOneTimeOfDay);
        material.SetFloat("_Rotation", rotation);
    }

    private void LateUpdate()
    {
        zeroOneTimeOfDay = timeOfDay / 24f;
        material.SetColor("_TopColor", dayGradient.Evaluate(zeroOneTimeOfDay));
        material.SetColor("_BottomColor", nightGradient.Evaluate(zeroOneTimeOfDay));
        material.SetFloat("_TimeOfDay", zeroOneTimeOfDay);
        material.SetFloat("_Rotation", rotation);
        spriteRenderer.material = material;
    }
}
