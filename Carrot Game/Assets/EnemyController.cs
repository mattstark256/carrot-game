using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject mouthCarrot;
    [SerializeField]
    private FieldManager fieldManager;
    [SerializeField]
    private float grabDistance = 0.5f;

    private SpriteRenderer spriteRenderer;
    private RabbitCharacterController characterController;

    private Carrot targetCarrot;
    private bool goingToCarrot = false;
    private bool returningHome = false;
    private int collectedCarrots = 0;
    private int carrotQuota = 0;

    private bool textHasBeenShown = false;
    [SerializeField]
    private int popupNumber;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterController = GetComponent<RabbitCharacterController>();
        spriteRenderer.enabled = false;
        transform.position = door.transform.position;
        mouthCarrot.SetActive(false);
    }


    void Update()
    {
        if (!goingToCarrot &&
            !returningHome &&
            carrotQuota > collectedCarrots)
        {
            targetCarrot = fieldManager.GetNearestFullGrownCarrot(transform.position);
            if (targetCarrot != null)
            {
                goingToCarrot = true;
                spriteRenderer.enabled = true;

                if (!textHasBeenShown) { textHasBeenShown = true; PopupManager.instance.ShowPopupEnemy(popupNumber); }
            }
        }

        if (goingToCarrot)
        {
            if (targetCarrot == null)
            {
                targetCarrot = fieldManager.GetNearestFullGrownCarrot(transform.position);
            }

            if (targetCarrot != null)
            {
                characterController.MoveTowardsPosition(targetCarrot.transform.position);
                if ((targetCarrot.transform.position - transform.position).magnitude < grabDistance)
                {
                    Destroy(targetCarrot.gameObject);
                    mouthCarrot.SetActive(true);
                    goingToCarrot = false;
                    StartCoroutine(ReturnCarrot());
                }
            }
        }
    }


    private IEnumerator ReturnCarrot()
    {
        returningHome = true;
        while (transform.position != door.transform.position)
        {
            characterController.MoveTowardsPosition(door.transform.position);
            yield return null;
        }
        spriteRenderer.enabled = false;
        mouthCarrot.SetActive(false);
        yield return new WaitForSeconds(1);

        collectedCarrots++;
        returningHome = false;
    }


    public void IncrementCarrotQuota()
    {
        carrotQuota++;
    }
}
