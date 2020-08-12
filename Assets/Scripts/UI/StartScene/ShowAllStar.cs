using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ShowAllStar : MonoBehaviour
{
    public GameObject Star;
    public float Step = 10f;
    public float LocalStep = 1f;
    
    private GameObject[] stars;

    private AudioViewBase audioV;
    // Start is called before the first frame update
    void Start()
    {
        stars = new GameObject[Star.transform.childCount];
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i] = Star.transform.GetChild(i).gameObject;
        }
        audioV = GetComponent<AudioViewBase>();
    }

    void Update()
    {
        if (audioV == null)
            return;
        for(int i = 1;i <= stars.Length - 1;i ++)
        {
            stars[i].transform.localScale = new Vector3(LocalStep + audioV.samples[i] * Step, LocalStep + audioV.samples[i] * Step, stars[i].transform.localScale.z);
        }
    }

    //private IEnumerator ShowStars()
    //{
    //    foreach (var i in stars)
    //    {
    //        i.SetActive(true);
    //        yield return new WaitForSeconds(0.25f);
    //    }
    //    yield break;
    //}
}
