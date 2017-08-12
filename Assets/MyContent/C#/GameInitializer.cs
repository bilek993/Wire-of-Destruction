using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class GameInitializer : MonoBehaviour
{
    public Player Player;
    public SmoothGunMovement SmoothGunMovement;
    public PostProcessingBehaviour MainPostProcessingBehaviour;

    private DepthOfFieldModel.Settings _settingsDOF;
    private PostProcessingProfile _postProcessingProfile;
    private bool _returnToNormalDOF;
    private float _currentFocusDistance;

    void Start()
    {
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

        while (true)
        {
            if (_currentFocusDistance >= 0.47f)
                break;

            yield return new WaitForSeconds(0.01f);
        }

        enabled = false;
    }
}
