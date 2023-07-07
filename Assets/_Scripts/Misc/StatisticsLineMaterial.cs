using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StatisticsLineMaterial : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float duration = 5f;
    [SerializeField] private float minValue = 325;
    [SerializeField] private Ease ease = Ease.OutCubic;
    [SerializeField] private AnimationCurve curve;

    private float point0 = -325;
    private float point1 = -325;
    private float point2 = -325;
    private float point3 = -325;
    private float point4 = -325;
    private float point5 = -325;
    private float point6 = -325;
    private float point7 = -325;
    private float point8 = -325;
    private float point9 = -325;

    private void OnEnable()
    {
        Invoke("StartAnimationCurve", 1f);
    }

    private void StartAnimationCurve()
    {
        DOTween.To(() => point0, x => point0 = x, -minValue + (curve.Evaluate(0f) * 2 * minValue), duration).SetEase(ease).OnUpdate(() => {
            material.SetVector("_Point0", new Vector2(-675, point0));
        });
        DOTween.To(() => point1, x => point1 = x, -minValue + (curve.Evaluate(0.11f) * 2 * minValue), duration).SetEase(ease).OnUpdate(() => {
            material.SetVector("_Point1", new Vector2(-525, point1));
        });
        DOTween.To(() => point2, x => point2 = x, -minValue + (curve.Evaluate(0.22f) * 2 * minValue), duration).SetEase(ease).OnUpdate(() => {
            material.SetVector("_Point2", new Vector2(-375, point2));
        });
        DOTween.To(() => point3, x => point3 = x, -minValue + (curve.Evaluate(0.33f) * 2 * minValue), duration).SetEase(ease).OnUpdate(() => {
            material.SetVector("_Point3", new Vector2(-225, point3));
        });
        DOTween.To(() => point4, x => point4 = x, -minValue + (curve.Evaluate(0.44f) * 2 * minValue), duration).SetEase(ease).OnUpdate(() => {
            material.SetVector("_Point4", new Vector2(-75, point4));
        });
        DOTween.To(() => point5, x => point5 = x, -minValue + (curve.Evaluate(0.55f) * 2 * minValue), duration).SetEase(ease).OnUpdate(() => {
            material.SetVector("_Point5", new Vector2(75, point5));
        });
        DOTween.To(() => point6, x => point6 = x, -minValue + (curve.Evaluate(0.66f) * 2 * minValue), duration).SetEase(ease).OnUpdate(() => {
            material.SetVector("_Point6", new Vector2(225, point6));
        });
        DOTween.To(() => point7, x => point7 = x, -minValue + (curve.Evaluate(0.77f) * 2 * minValue), duration).SetEase(ease).OnUpdate(() => {
            material.SetVector("_Point7", new Vector2(375, point7));
        });
        DOTween.To(() => point8, x => point8 = x, -minValue + (curve.Evaluate(0.88f) * 2 * minValue), duration).SetEase(ease).OnUpdate(() => {
            material.SetVector("_Point8", new Vector2(525, point8));
        });
        DOTween.To(() => point9, x => point9 = x, -minValue + (curve.Evaluate(1f) * 2 * minValue), duration).SetEase(ease).OnUpdate(() => {
            material.SetVector("_Point9", new Vector2(675, point9));
        });
    }

    private void OnDisable()
    {
        point0 = -minValue;
        point1 = -minValue;
        point2 = -minValue;
        point3 = -minValue;
        point4 = -minValue;
        point5 = -minValue;
        point6 = -minValue;
        point7 = -minValue;
        point8 = -minValue;
        point9 = -minValue;
        material.SetVector("_Point0", new Vector2(-675, point0));
        material.SetVector("_Point1", new Vector2(-525, point1));
        material.SetVector("_Point2", new Vector2(-375, point2));
        material.SetVector("_Point3", new Vector2(-225, point3));
        material.SetVector("_Point4", new Vector2(-75, point4));
        material.SetVector("_Point5", new Vector2(75, point5));
        material.SetVector("_Point6", new Vector2(225, point6));
        material.SetVector("_Point7", new Vector2(375, point7));
        material.SetVector("_Point8", new Vector2(525, point8));
        material.SetVector("_Point9", new Vector2(675, point9));
    }
}
