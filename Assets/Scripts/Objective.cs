using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class objective : MonoBehaviour
{
    public TextMeshProUGUI objectiveText; 
    public float fadeDuration = 2f; 
    public float visibleDuration = 5f; 

    private void Start()
    {
       
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {

        Color startColor = new Color(objectiveText.color.r, objectiveText.color.g, objectiveText.color.b, 0f); 
        Color endColor = new Color(objectiveText.color.r, objectiveText.color.g, objectiveText.color.b, 1f); 

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            objectiveText.color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        objectiveText.color = endColor; 


        yield return new WaitForSeconds(visibleDuration);

   
        startColor = new Color(objectiveText.color.r, objectiveText.color.g, objectiveText.color.b, 1f); 
        endColor = new Color(objectiveText.color.r, objectiveText.color.g, objectiveText.color.b, 0f); 

        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            objectiveText.color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        objectiveText.color = endColor; 
    }
}
