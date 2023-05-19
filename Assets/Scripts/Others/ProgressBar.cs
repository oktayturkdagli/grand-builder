using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] public Image progressBar;
    
    public void UpdateProgressBar(float maxProgressBarValue, float currentProgressBarValue)
    {
        if (currentProgressBarValue > maxProgressBarValue)
            currentProgressBarValue = maxProgressBarValue;
        
        if (currentProgressBarValue < 0)
            currentProgressBarValue = 0;

        progressBar.fillAmount = currentProgressBarValue / maxProgressBarValue;
    }
}
