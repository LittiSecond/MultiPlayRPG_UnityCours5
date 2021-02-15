using System;

namespace MultiPlayRPG
{
    public interface IHealth
    {
        int MaxHealth { get; }
        int Health { get; }

        //void SubscribeHealthChanged(Action<int> fun);

    }
}
