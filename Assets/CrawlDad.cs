using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlDad : MonoBehaviour
{


    [SerializeField] private SkinnedMeshRenderer mesh;
    [SerializeField] private AudioSource audio;
    private bool fading = false;


    private void Update()
    {
        if (fading)
        {
        float volume = audio.volume;
        float newVolume = volume - Time.deltaTime * 0.3f;
        audio.volume = newVolume;
        }
    }

    public void Disappear()
    {
        if (!fading)
        {

        StartCoroutine(KillSelf());
        }
    }

    private IEnumerator KillSelf()
    {
        fading = true;
        mesh.enabled = false;
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);

    }
}
