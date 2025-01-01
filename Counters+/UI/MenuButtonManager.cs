﻿using System;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using CountersPlus.UI.FlowCoordinators;
using Zenject;

namespace CountersPlus.UI
{
    // Code taken from DiUi. Don't like field injection? Make a PR.
    internal class MenuButtonManager : IInitializable, IDisposable
    {
        private MenuButton menuButton;
        [Inject] private MainFlowCoordinator mainFlowCoordinator;
        [Inject] private CountersPlusSettingsFlowCoordinator flowCoordinator;

        public void Initialize()
        {
            menuButton = new MenuButton("Counters+", "Configure Counters+ settings.", SummonFlowCoordinator);
            MenuButtons.Instance.RegisterButton(menuButton);
        }

        private void SummonFlowCoordinator()
        {
            flowCoordinator.DoSceneTransition(() =>
            {
                mainFlowCoordinator.PresentFlowCoordinator(flowCoordinator);
            });
        }

        public void Dispose()
        {
            MenuButtons.Instance.UnregisterButton(menuButton);
        }
    }
}
