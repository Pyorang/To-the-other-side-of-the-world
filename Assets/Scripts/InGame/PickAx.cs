using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PickAx : SingletonBehaviour<PickAx>
{
    private bool _isCommonPickAxe = true;

    [SerializeField] private GameObject _commonPickAxe;
    [SerializeField] private GameObject _diamondPickAxe;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    private void Start()
    {
        _commonPickAxe.SetActive(_isCommonPickAxe);
        _diamondPickAxe.SetActive(!_isCommonPickAxe);
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        float length = GetAnimator().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);

        CameraShaker.s_instance.StartShake();
        this.gameObject.SetActive(false);
    }

    public Animator GetAnimator()
    {
        if (_isCommonPickAxe)
        {
            return _commonPickAxe.GetComponent<Animator>();
        }
        else
        {
            return _diamondPickAxe.GetComponent<Animator>();
        }
    }

    public void ChangePickAxe()
    {
        _isCommonPickAxe = !_isCommonPickAxe;
        _commonPickAxe.SetActive(_isCommonPickAxe);
        _diamondPickAxe.SetActive(!_isCommonPickAxe);
    }
}
