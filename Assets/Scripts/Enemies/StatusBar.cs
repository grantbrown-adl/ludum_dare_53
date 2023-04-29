using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Transform statusBar;

    public void SetStatusBarState(float current, float max)
    {
        float state = current;
        state /= max;
        if(state <= 0) state = 0.0f;
        //statusBar.transform.localScale = new Vector2(state, 1.0f);
        //statusBar.transform.localScale = new Vector2(Mathf.Lerp(state, current / max, Time.deltaTime * 3), 1.0f);
        statusBar.transform.localScale = Vector2.Lerp(statusBar.localScale, new Vector2(state, 1.0f), Time.deltaTime * 20.0f);

        /**
         * _healthbar.fillAmount = Mathf.Lerp(_healthBar.FillAmount, CurrentHealth / MaxHealth, Time.deltaTime * 10f);
         
         */
    }
}
