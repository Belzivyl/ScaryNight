using UnityEngine;


internal sealed class PlayerInitialization
{
    //заменить на статический метод, чтобы не загаживать экземплярами класса кучу
    public PlayerInitialization(ControllersManager controllersManager) 
    {
        var player = Object.FindObjectOfType<PlayerView>();
        var playerUI = Object.FindObjectOfType<PlayerUIView>();
        var joystick = Object.FindObjectOfType<Joystick>();
        var playerHitBox = Object.FindObjectOfType<PlayerViewMovable>();
        var inputController = new InputController(joystick, player);
        var playerModel = new PlayerModel(Resources.Load<PlayerSO>("Player/Player data").PlayerStruct);
        var playerHealthUIModel = new PlayerHealthUIModel(Resources.Load<PlayerHealthUISO>("Player/PlayerHealthUI data").PlayerHealthUIStructer);
        var PlayerHealthController = new PlayerHealthController(player, playerModel, playerUI);
        var PlayerMoveController = new PlayerMoveController(inputController.GetInput(), playerHitBox, playerModel);
        var PlayerUIController = new PlayerUIController(playerHealthUIModel, player, playerUI, playerModel);

        controllersManager.Add(inputController);
		controllersManager.Add(PlayerHealthController);
        controllersManager.Add(PlayerMoveController);
        controllersManager.Add(PlayerUIController);
        
        PlayerHealthController.SubscribeOnEvents();
        PlayerMoveController.SubscribeOnEvents();
        PlayerMoveController.SetTurnRange();
        PlayerUIController.SubscribeOnEvents();
        PlayerUIController.MakePlayerImageAlphaChanelToZero();
        PlayerUIController.MakeKillCountTextToDefalt();
    }
}
