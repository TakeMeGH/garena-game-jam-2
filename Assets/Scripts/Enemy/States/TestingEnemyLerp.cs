using System;
using UnityEngine;

namespace TKM
{
    public class TestingEnemyLerp : MonoBehaviour
    {
        [SerializeField] Transform targetPostion;
        [Range(0.0f, 1.0f)]
        [SerializeField] float _lerpValue;
        [SerializeField] float _multiplier;
        Vector3 defaultPostion;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            defaultPostion = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            float e = Mathf.Exp(1f);

            float newX = Mathf.Lerp(defaultPostion.x, targetPostion.position.x, _lerpValue);
            float newY = Mathf.Lerp(defaultPostion.y, targetPostion.position.y, Mathf.Min(1, (float)Math.Log(1 + _lerpValue * (e - 1)) * _multiplier));

            transform.position = new Vector2(newX, newY);
        }
    }
}
