using System.Collections;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(LoadingSequence());
    }

    private void Start()
    {
        
    }

    private IEnumerator LoadingSequence()
    {
        yield return null;  
    }
}
