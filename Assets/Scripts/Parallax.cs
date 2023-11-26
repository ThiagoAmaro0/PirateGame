using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private float _xSpeed;
    [SerializeField] private float _ySpeed;
    [SerializeField] private float _xMultiplier;
    [SerializeField] private float _yMultiplier;

    // Update is called once per frame
    void Update()
    {
        _material.mainTextureOffset = new Vector2(Mathf.Sin(Time.time * _xMultiplier) *
                                        _xSpeed, Mathf.Sin(Time.time * _xMultiplier) * _ySpeed);
    }
}
