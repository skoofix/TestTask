using Code.Services.Input;

namespace Code.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;

        public Game() => 
            InputService = new InputService();
    }
}