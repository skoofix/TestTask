﻿namespace Code.Infrastructure
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }

    public interface IExitableState
    {
        void Exit();
    }
    
    public interface IUpdatableState : IState
    {
        void Update();
    }
}