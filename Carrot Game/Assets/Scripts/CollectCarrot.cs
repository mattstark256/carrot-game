using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCarrot : MonoBehaviour
{
    [SerializeField]
    private FieldManager fieldManager;
    [SerializeField]
    private float pickDistance = 1;
    [SerializeField]
    private GameObject mouthCarrot;
    [SerializeField]
    private CarrotPile carrotPile;
    [SerializeField]
    private ScoreCounter scoreCounter;
    [SerializeField]
    private MirrorPlayerActions mirror;

    private bool carryingCarrot = false;
    private bool inCarrotPile = false;

    private bool tutorial2WasShown = false;
    private bool tutorial3WasShown = false;

    private void Awake()
    {
        mouthCarrot.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (carryingCarrot)
        {
            if (inCarrotPile && Input.GetButtonDown("Action"))
            {
                carryingCarrot = false;
                mouthCarrot.SetActive(false);
                scoreCounter.AddToScore(1);
                carrotPile.AddCarrot();
                mirror.MirrorActions();

                if (!tutorial3WasShown) { PopupManager.instance.ShowPopupTutorial3(); tutorial3WasShown = true; }
            }
        }
        else
        {
            Carrot nearestCarrot = fieldManager.GetNearestFullGrownCarrot(transform.position);
            if (nearestCarrot != null &&
                Vector3.Distance(transform.position, nearestCarrot.transform.position) < pickDistance)
            {
                if (Input.GetButtonDown("Action"))
                {
                    Destroy(nearestCarrot.gameObject);
                    carryingCarrot = true;
                    mouthCarrot.SetActive(true);

                    if (!tutorial2WasShown) { PopupManager.instance.ShowPopupTutorial2(); tutorial2WasShown = true; }
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == carrotPile.gameObject)
        {
            inCarrotPile = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == carrotPile.gameObject)
        {
            inCarrotPile = false;
        }
    }

    public bool IsBusy()
    {
        return carryingCarrot;
    }
}
