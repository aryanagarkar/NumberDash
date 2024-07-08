using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace UIElements
{
    /// <summary>
    /// Manages the instructions pages, allowing users to navigate forward and backward through the pages.
    /// </summary>

    public class Instructions : MonoBehaviour
    {
        // Store pages associated with their page number for easy navigation
        Dictionary<int, GameObject> pages = new Dictionary<int, GameObject>();

        // Reference to each page
        GameObject page1;
        GameObject page2;

        // Index of the current page
        int currentPageIndex;

        /// <summary>
        /// Initializes the Instructions display, sets up the dictionary of pages and default values.
        /// </summary>

        void Awake()
        {
            // Find the pages under this GameObject
            page1 = transform.Find("Page1").gameObject;
            page2 = transform.Find("Page2").gameObject;

            // Add the pages to the dictionary with their corresponding page number
            pages.Add(1, page1);
            pages.Add(2, page2);

            // Set the starting current page index at initialization
            currentPageIndex = 0;
        }

        /// <summary>
        /// Navigates to the next page when the forward button is pressed.
        /// </summary>
        public void ForwardButtonPressed()
        {
            int nextPageIndex = currentPageIndex + 1;

            GameObject nextPage = pages.ElementAt(nextPageIndex).Value;

            nextPage.SetActive(true);
            pages.ElementAt(currentPageIndex).Value.SetActive(false);

            currentPageIndex = nextPageIndex;
        }

        /// <summary>
        /// Navigates to the previous page when the back button is pressed.
        /// </summary>
        public void BackButtonPressed()
        {
            int previousPageIndex = currentPageIndex - 1;

            GameObject nextPage = pages.ElementAt(previousPageIndex).Value;

            nextPage.SetActive(true);
            pages.ElementAt(currentPageIndex).Value.SetActive(false);

            currentPageIndex = previousPageIndex;
        }
    }
}