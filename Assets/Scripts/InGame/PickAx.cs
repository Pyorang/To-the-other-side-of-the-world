using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PickAx : SingletonBehaviour<PickAx>
{
    [SerializeField] Animator animator;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        float length = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);

        this.gameObject.SetActive(false);
    }

    public Animator GetAnimator()
    {
        return animator;
    }
}
