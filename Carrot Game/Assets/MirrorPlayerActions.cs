using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlayerActions : MonoBehaviour
{
    [SerializeField]
    EnemyController enemy1;
    [SerializeField]
    EnemyController enemy2;
    [SerializeField]
    EnemyController enemy3;

    public void MirrorActions()
    {
        StartCoroutine(MirrorActionsCoroutine());
    }

    private IEnumerator MirrorActionsCoroutine()
    {
        yield return new WaitForSeconds(10);
        enemy1.IncrementCarrotQuota();
        yield return new WaitForSeconds(7);
        enemy2.IncrementCarrotQuota();
        yield return new WaitForSeconds(8);
        enemy3.IncrementCarrotQuota();
    }
}
