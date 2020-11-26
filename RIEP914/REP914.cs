using Exiled.API.Features;

namespace RIEP914
{
    public class REP914 : Plugin<Config>
    {
        internal static REP914 singleton;

        private EventHandlers ev;

        public override void OnEnabled()
        {
            base.OnEnabled();

            singleton = this;

            ev = new EventHandlers();
            Exiled.Events.Handlers.Scp914.UpgradingItems += ev.OnUpgradingItems;
            Exiled.Events.Handlers.Player.ChangingRole += ev.OnChangeRole;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();

            Exiled.Events.Handlers.Scp914.UpgradingItems -= ev.OnUpgradingItems;
            Exiled.Events.Handlers.Player.ChangingRole -= ev.OnChangeRole;

            ev = null;
        }

        public override string Name => "Rep914";
    }
}
