using UnityEngine;

[CreateAssetMenu(fileName = "New Scene Management data", menuName = "New scene data", order = 1)]
public class SceneManagerSO : ScriptableObject
{ 
	public int SceneMaxKillCount;
	public int SceneCurrentKillCount;
	public int CurrentSceneNumber;
	public int CurrentSceneDifficultyLevel;

	public int BaseDifficultyLevel = 1;
	public int BaseMaxKillCount = 4;
	public int BaseSceneNumber = 1;
}	

