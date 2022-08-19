using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Button : MonoBehaviour
{
    public void ButtonClicked(){
        SoundManager.PlayClipByName(AudioClipName.ButtonClick);
    }

    /*GameObject tooltip;
    
    void Start()
    {
      tooltip = Resources.Load<GameObject>("Prefabs/ToolTip");  
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData){
        Vector2 position;
        RectTransform buttonRectTransform = GetComponent<RectTransform>();
        int offset = 30;
        Instantiate(tooltip, transform);
        position = new Vector2(transform.position.x, transform.position.y + buttonRectTransform.rect.height / 2 + offset);
        GameObject instantiatedTooltip = GameObject.FindWithTag("ToolTip");
        instantiatedTooltip.transform.position = position;
        instantiatedTooltip.GetComponent<TextMeshProUGUI>().fontSize = buttonRectTransform.rect.height / 2;
        instantiatedTooltip.GetComponent<TextMeshProUGUI>().text = getText();
        instantiatedTooltip.GetComponent<TextMeshProUGUI>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData){
        Destroy(GameObject.FindWithTag("ToolTip"));
    }

    private string getText(){
        return name.Replace("Icon", "");
    }
    */
}
