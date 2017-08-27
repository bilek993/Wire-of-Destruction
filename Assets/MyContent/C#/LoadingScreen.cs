using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public Image LoadingProgressImage;
    public Text LoadingProgressText;

    private AsyncOperation _sceneLoading;
    private bool _showedInformationAboutAnyKey;

	IEnumerator Start ()
	{
	    LoadingProgressImage.canvasRenderer.SetAlpha(0.01f);
	    LoadingProgressText.canvasRenderer.SetAlpha(0.01f);
        LoadingProgressImage.CrossFadeAlpha(1f, 1f, false);
	    LoadingProgressText.CrossFadeAlpha(1f, 1f, false);
	    yield return new WaitForSeconds(1f);
        _sceneLoading = SceneManager.LoadSceneAsync("gameplay", LoadSceneMode.Single);
	    _sceneLoading.allowSceneActivation = false;
	}
	
	void FixedUpdate ()
	{
	    if (_sceneLoading != null)
	    {
	        float currentProgress = _sceneLoading.progress / 0.9f;
	        if (currentProgress < 1.0f)
	        {
	            LoadingProgressImage.fillAmount = currentProgress;
	            SetTextPercentage(currentProgress);
	        }
	        else
	        {
	            if (!_showedInformationAboutAnyKey)
	            {
	                _showedInformationAboutAnyKey = true;
	                LoadingProgressImage.fillAmount = 1f;
	                SetTextPercentage(1f);
	                SendMessage("StartGameplay");
	            }
	        }
	    }
	}

    private void SetTextPercentage(float progress)
    {
        int progressForText =  Mathf.RoundToInt(progress * 100);
        LoadingProgressText.text = progressForText + "%";
    }

    private IEnumerator StartGameplay()
    {
        LoadingProgressImage.CrossFadeAlpha(0f, 1f, false);
        LoadingProgressText.CrossFadeAlpha(0f, 1f, false);
        yield return new WaitForSeconds(1f);
        _sceneLoading.allowSceneActivation = true;
    }
}
