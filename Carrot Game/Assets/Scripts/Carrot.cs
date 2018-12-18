using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    [SerializeField]
    private float reproduceInterval = 10;
    [SerializeField]
    private float growDuration = 5;

    [SerializeField]
    private float tiltAmount = 10;
    [SerializeField]
    private float tiltDuration = 2;
    [SerializeField]
    private float waveSpacing = 5;
    private float waveOffset = 0;

    public bool isGrown = false;

    private float nextReproduceTime = 0;

    private void Awake()
    {
        waveOffset = (transform.position.x + transform.position.y) / waveSpacing + Random.Range(-0.1f, 0.1f);
    }


    private void Update()
    {
        transform.localRotation = Quaternion.Euler(0, 0, tiltAmount * Mathf.Sin(2 * Mathf.PI * (Time.time / tiltDuration + waveOffset)));
    }


    public void InitializeFullyGrownPlant()
    {
        isGrown = true;
        nextReproduceTime = Time.time + Random.Range(0, reproduceInterval);
    }


    public bool IsReadyToReproduce()
    {
        return (isGrown && Time.time > nextReproduceTime);
    }


    public void IncrementReproduceTimer()
    {
        nextReproduceTime += reproduceInterval;
    }


    public void Grow()    { StartCoroutine(GrowCoroutine()); }
    private IEnumerator GrowCoroutine()
    {
        float f = 0;
        while (f < 1)
        {
            f += Time.deltaTime / growDuration;
            f = Mathf.Clamp01(f);

            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, f);

            yield return null;
        }
        
        nextReproduceTime = Time.time + reproduceInterval/2;
        isGrown = true;
    }


}
