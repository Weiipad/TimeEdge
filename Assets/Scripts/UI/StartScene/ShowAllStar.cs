using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAllStar : MonoBehaviour
{
    public GameObject Star;

    private GameObject[] stars;
    // Start is called before the first frame update
    void Start()
    {
        stars = new GameObject[Star.transform.childCount];
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i] = Star.transform.GetChild(i).gameObject;
        }
        StartCoroutine(ShowStars());
    }

    private IEnumerator ShowStars()
    {
        foreach (var i in stars)
        {
            i.SetActive(true);
            yield return new WaitForSeconds(0.25f);
        }
        yield break;
    }
}
