using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Instructions : MonoBehaviour
{
    Dictionary<int, GameObject> pages = new Dictionary<int, GameObject>(); 
    GameObject page1;
    GameObject page2;
    int currentPageIndex;

    void Awake()
    {
        page1 = transform.Find("Page1").gameObject;
        page2 = transform.Find("Page2").gameObject;
        pages.Add(1, page1);
        pages.Add(2, page2);
        currentPageIndex = 0;
    }

    public void ForwardButtonPressed(){
        int nextPageIndex = currentPageIndex + 1;
        GameObject nextPage = pages.ElementAt(nextPageIndex).Value;
        nextPage.SetActive(true);
        pages.ElementAt(currentPageIndex).Value.SetActive(false);
        currentPageIndex = nextPageIndex;
    }

    public void BackButtonPressed(){
        int previousPageIndex = currentPageIndex - 1;
        GameObject nextPage = pages.ElementAt(previousPageIndex).Value;
        nextPage.SetActive(true);
        pages.ElementAt(currentPageIndex).Value.SetActive(false);
        currentPageIndex = previousPageIndex;
    }
}
