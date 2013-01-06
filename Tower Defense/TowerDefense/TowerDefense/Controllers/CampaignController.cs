using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TowerDance.Models.Dance;

namespace TowerDance.Controllers
{
    class CampaignController : AController
    {
        SongLibrary _songLibrary;
        int _defaultPlayerID;
        List<MusicSheet> _toPlay = new List<MusicSheet>();
        bool _canContinue = false;
        
        public CampaignController(int defaultPlayerID)
        {
            _defaultPlayerID = defaultPlayerID;
            _songLibrary = new SongLibrary();
            _songLibrary.initialize();
            if (!findNeededMusicSheets())
            {
                addChild(new CampaignErrorController());
                stop();
            }
            else
            {
                string t = "Hey Stranger !\n\n"
                    + "You are here to save our realm ? I knew it !\n"
                    + "It's not the first time, and every time \n"
                    + "someone come .. We're lucky today !\n\n"
                    + "Oh beware, the enemy army is coming\n"
                    + "(yeah, and it's not winter) ! You have to\n"
                    + "fight them by using our great invention :\n"
                    + "mana from music.\n\n"
                    + "The more you are symbiotic with the music\n"
                    + "you hear, the more you are powerful. And then\n"
                    + "you can invoke units to fight for you.\n"
                    + "One day, you will have spells, but because\n"
                    + "the Creator is late, you should wait for that.\n\nThe King";
                addChild(new TextController(t));
                addChild(new ControlSelectController(_toPlay[0], _defaultPlayerID));
                _toPlay.RemoveAt(0);
            }
        }

        public override void update(GameTime gameTime)
        {
            if (_canContinue == false)
            {
                string t = "Ok, we will try to fight alone .. Bye\n\nThe King (who will probably die soon)\n\n\n\n\n\n\nThanks, really ..";
                addChild(new TextController(t));
                stop();
                return;
            }
            else
            {
                _canContinue = false;
                if (_toPlay.Count > 0)
                {
                    string t = "Well, how was that ? Funny ? You tried\n"
                        + "the hard mode ? I didn't the first time,\n"
                        + "because I was a coward ! It's over of course ..\n"
                        + "But you know what ? The next part of their\n"
                        + "army is coming, and they are angry. Good luck !\n\nThe King";
                    addChild(new TextController(t));
                    addChild(new ControlSelectController(_toPlay[0], _defaultPlayerID));
                    _toPlay.RemoveAt(0);
                }
                else
                {
                    string t = "Congratulations, you saved our realm\n"
                        + "by being symbiotic with the music !\n"
                        + "I knew you were powerful, I knew I can trust you !\n\n"
                        + "Thanks a lot for everything. I hope we will see you again\n\nThe king";
                    addChild(new TextController(t));
                    stop();
                }
            }
        }

        public override void updateBackgrounded(GameTime gameTime)
        {

        }

        public override void signal(string signal)
        {
            if (signal == "won")
            {
                _canContinue = true;
            }
            if (signal == "lost")
            {
                string t = "Oh shit I thought you were the one ..\n"
                    + "Well no problem in fact, it was just a game !\n"
                    + "Someone else will come, and he will win. Maybe.\n\nThe king (dead of course, because of you)";
                addChild(new TextController(t));
                stop();
            }
        }

        private bool findNeededMusicSheets()
        {
            List<string> neededName = new List<string>() { "CLASSICAL INSANITY", "Dublin Delight" };
            List<int> neededSheetId = new List<int>() { 3, 2 };
            foreach (Song s in _songLibrary.songs)
            {
                int i = 0;
                while (i < neededName.Count)
                {
                    if (s.title.Equals(neededName[i]) && neededSheetId[i] < s.musicSheets.Count)
                    {
                        _toPlay.Add(s.musicSheets[neededSheetId[i]]);
                        neededName.RemoveAt(i);
                        neededSheetId.RemoveAt(i);
                        break;
                    }
                    i++;
                }
            }
            if (neededName.Count > 0)
                return false;
            return true;
        }
    }
}
