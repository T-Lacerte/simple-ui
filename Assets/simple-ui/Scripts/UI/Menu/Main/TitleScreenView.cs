using SimpleUI.EventChannels;
using UnityEngine;
using UnityEngine.Serialization;

namespace simple_ui.Scripts.UI.Menu
{
    public class TitleScreenView : MenuView
    {
        [SerializeField] private MainMenuOpenMenuEventChannel openMenuEventChannel;

        public override string GetScreenName()
        {
            return MainMenuNames.TITLE_SCREEN;
        }

        public override void OnStartCanceled()
        {
            if (StartPressed)
            {
                openMenuEventChannel.Publish(MainMenuNames.MAIN_SCREEN);
            }
            base.OnStartCanceled();
        }
    }
}