using Asteroids.Common.Observer;

namespace Asteroids.Controller
{
    public interface IUpdateWeaponResult : IResult
    {
        int Charges { get; }
        float Cooldown { get; }
        float CooldownRemaining { get; }
    }
}