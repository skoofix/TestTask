using Code.Services.Input;

namespace Code.Infrastructure.GameStateMachine.States
{
    public class BootstrapState : IState
    {
        private const string Bootstrap = "Bootstrap";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Bootstrap, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            _stateMachine.SwitchState<LoadLevelState, string>("TestTask");

        private void RegisterServices()
        {
            Game.InputService = SetupInputService();
        }

        public void Exit()
        {
            
        }

        private static IInputService SetupInputService()
        {
            return new InputService();
        }
    }
}