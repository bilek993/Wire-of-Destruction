using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class GameInitializer : MonoBehaviour
{
    public Player Player;
    public SmoothGunMovement SmoothGunMovement;
    public PostProcessingBehaviour MainPostProcessingBehaviour;
    public Image Crosshair;

    private DepthOfFieldModel.Settings _settingsDOF;
    private PostProcessingProfile _postProcessingProfile;
    private bool _returnToNormalDOF;
    private float _currentFocusDistance;

    void Start()
    {
        Crosshair.canvasRenderer.SetAlpha(0.01f);
        SetPostProcess();
		SendMessage("InitializeGame");
	}

    void Update()
    {
        if (_returnToNormalDOF)
        {
            if (_currentFocusDistance < 0.8f)
                _currentFocusDistance += Time.deltaTime;

            updateDOF(_currentFocusDistance);
        }
    }

    private void SetPostProcess()
    {
        _postProcessingProfile = Instantiate(MainPostProcessingBehaviour.profile);
        MainPostProcessingBehaviour.profile = _postProcessingProfile;
        _settingsDOF = _postProcessingProfile.depthOfField.settings;
        _currentFocusDistance = _settingsDOF.focusDistance;
    }

    private void updateDOF(float focusDistance)
    {
        _settingsDOF.focusDistance = focusDistance;
        _postProcessingProfile.depthOfField.settings = _settingsDOF;
    }

    IEnumerator InitializeGame()
    {
        yield return new WaitForSeconds(4f);
        _returnToNormalDOF = true;
        Player.enabled = true;
        SmoothGunMovement.enabled = true;
        Crosshair.CrossFadeAlpha(1f, 0.25f, false);

        while (true)
        {
            if (_currentFocusDistance >= 0.47f)
                break;

            yield return new WaitForSeconds(0.01f);
        }

        enabled = false;
    }
}
