using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBounce : MonoBehaviour
{
    [SerializeField]
    float bounceDuration = 1;
    [SerializeField]
    float bounceDistance = 10;
    [SerializeField]
    float minBounceInterval = 1;
    [SerializeField]
    float maxBounceInterval = 2;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        StartCoroutine(BounceWait());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BounceWait()
    {
        float waitDuration = Mathf.Lerp(minBounceInterval, maxBounceInterval, Random.value);
        yield return new WaitForSeconds(waitDuration);
        StartCoroutine(Bounce());
    }

    IEnumerator Bounce()
    {
        float f = 0;
        while (f < 1)
        {
            f += Time.deltaTime / bounceDuration;
            f = Mathf.Clamp01(f);

            transform.localPosition = startPosition + Vector3.up * (1 - Mathf.Pow(f * 2 - 1, 2)) * bounceDistance;

            yield return null;
        }
        StartCoroutine(BounceWait());
    }
}
