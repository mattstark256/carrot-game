using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotPile : MonoBehaviour
{
    [SerializeField]
    private GameObject carrotPrefab;
    [SerializeField]
    private float carrotSpawnRadius = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCarrot()
    {
        GameObject newCarrot = Instantiate(carrotPrefab);
        newCarrot.transform.SetParent(transform, false);
        newCarrot.transform.localPosition = Random.insideUnitCircle * carrotSpawnRadius;
        newCarrot.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }
}
