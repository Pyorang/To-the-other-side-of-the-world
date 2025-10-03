using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IPointerDownHandler
{
    private bool hasDestroyed = false;
    public int blockDurability = 1;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(PickAx.Instance.gameObject.activeSelf)
            PickAx.Instance.gameObject.SetActive(false);

        if (!hasDestroyed)
        {
            blockDurability--;

            if (blockDurability <= 0)
                hasDestroyed = true;

            SetPickAxActive();
            StartCoroutine(WaitForAnimation());
        }
    }

    public void SetPickAxActive()
    {
        Vector3 targetPosition = transform.position;
        PickAx.Instance.transform.position = targetPosition;
        PickAx.Instance.gameObject.SetActive(true);
        //AudioManager.Instance.PlaySound("PickAxSound");
    }

    protected virtual void ProcessBlockDestruction()
    {
        if (hasDestroyed)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator WaitForAnimation()
    {
        while(PickAx.Instance.gameObject.activeSelf)
        {
            yield return null;
        }

        ProcessBlockDestruction();
    }
}
