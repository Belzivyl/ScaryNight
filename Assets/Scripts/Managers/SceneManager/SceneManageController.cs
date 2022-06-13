using UnityEngine;
using UnityEngine.SceneManagement;

internal static class SceneManageController
{
	public static SceneManagerSO sceneManagerSO;

	public static void SetSceneMagerController()
	{
		sceneManagerSO = Resources.Load<SceneManagerSO>("Scene/SceneData");
	}
	public static void SetSceneDifficulty(int difficulty)
	{
		sceneManagerSO.CurrentSceneDifficultyLevel = difficulty;
	}
	public static void ChangeKillCount(int CurrentKillCount)
	{
		sceneManagerSO.SceneCurrentKillCount += CurrentKillCount;
	}
	public static void IncreaseLevelNumber()
	{
		sceneManagerSO.CurrentSceneNumber += 1;
	}

	public static void IncreaseLevelDifficulty()
	{
		sceneManagerSO.CurrentSceneDifficultyLevel += 1;
		sceneManagerSO.SceneMaxKillCount += 2;
	}
	public static void LoadDeathScene()
	{
		LoadScene(2);
	}

	private static void LoadScene(int sceneNumber)
	{
		SceneManager.LoadScene(sceneNumber);
	}

	public static void RestoreToDefaults()
	{
		sceneManagerSO.SceneCurrentKillCount = 0;
		sceneManagerSO.CurrentSceneDifficultyLevel = sceneManagerSO.BaseDifficultyLevel;
		sceneManagerSO.CurrentSceneNumber = sceneManagerSO.BaseSceneNumber;
		sceneManagerSO.SceneMaxKillCount = sceneManagerSO.BaseMaxKillCount;
	}
}

