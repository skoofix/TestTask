using Code.Services.Input;

namespace Code.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public GameStateMachine.GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine.GameStateMachine(new SceneLoader(coroutineRunner), curtain);
        }
    }
}