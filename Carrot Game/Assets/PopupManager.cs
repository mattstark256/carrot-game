using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;

    private void Awake()
    {
        instance = this;
    }


    [SerializeField]
    private RectTransform popupParent;
    [SerializeField]
    private GameObject[] popupPrefabs;

    private List<int> queuedPopups = new List<int>();

    private GameObject currentPopup;

    private void Start()
    {
        queuedPopups.Add(0);
        queuedPopups.Add(1);
    }

    private void Update()
    {
        if (currentPopup != null &&
            Input.GetButtonDown("Close"))
        {
            Destroy(currentPopup);
            Time.timeScale = 1;
        }

        if (currentPopup == null &&
            queuedPopups.Count > 0)
        {
            currentPopup = Instantiate(popupPrefabs[queuedPopups[0]], popupParent);
            queuedPopups.RemoveAt(0);
            Time.timeScale = 0;
        }
    }


    public void ShowPopupTutorial2() { StartCoroutine(ShowPopupAfterDelay(2, 0.5f)); }
    public void ShowPopupTutorial3() { StartCoroutine(ShowPopupAfterDelay(3, 0.5f)); }
    public void ShowPopupEnemy(int popupNumber) { StartCoroutine(ShowPopupAfterDelay(popupNumber, 0.3f)); }

    public void ShowPopupGameOverBad() { StartCoroutine(ShowPopupsAfterDelay(new int[] { 7, 8 }, 1f)); }
    public void ShowPopupGameOverGood() { StartCoroutine(ShowPopupsAfterDelay(new int[] { 9, 8 }, 1f)); }


    private IEnumerator ShowPopupAfterDelay(int popup, float delay)
    {
        yield return new WaitForSeconds(delay);
        queuedPopups.Add(popup);
    }

    private IEnumerator ShowPopupsAfterDelay(int[] popups, float delay)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < popups.Length; i++)
        {
            queuedPopups.Add(popups[i]);
        }
    }
}
