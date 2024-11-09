using Asteroids.Common.Observer;

namespace Asteroids.Controller.CommonResults
{
    public interface IUpdateWeaponResult : IResult
    {
        int Charges { get; }
        float Cooldown { get; }
        float CooldownRemaining { get; }
    }

    internal sealed class UpdateWeaponResult : IUpdateWeaponResult
    {
        public int Charges { get; }
        public float Cooldown { get; }
        public float CooldownRemaining { get; }

        public UpdateWeaponResult(int charges, float cooldown, float cooldownRemaining)
        {
            Charges = charges;
            Cooldown = cooldown;
            CooldownRemaining = cooldownRemaining;
        }
    }
}