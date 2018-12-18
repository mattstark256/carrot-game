using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    [SerializeField]
    private float reproduceInterval = 10;
    [SerializeField]
    private float growDuration = 5;

    public bool isGrown = false;

    private float nextReproduceTime = 0;


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
