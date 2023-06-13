﻿using Exiled.API.Features;
namespace TardisPlugin
{
    public sealed class Plugin : Plugin<Config>
    {
        public override string Author => "morgana";

        public override string Name => "Tardis";

        public override string Prefix => Name;

        public static Plugin Instance;

        private EventHandlers _handlers;

        public override void OnEnabled()
        {
            Instance = this;

            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            Instance = null;

            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            _handlers = new EventHandlers();

            Exiled.Events.Handlers.Map.Generated += _handlers.onMapGenerated;
            Exiled.Events.Handlers.Player.Spawned += _handlers.OnPlayerSpawned;

        }

        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Map.Generated -= _handlers.onMapGenerated;
            Exiled.Events.Handlers.Player.Spawned -= _handlers.OnPlayerSpawned;
            _handlers = null;
        }
    }
}