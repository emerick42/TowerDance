using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;

using Input;

namespace TowerDance
{
    public enum UNIT
    {
        WARRIOR = 0,
        ARCHER
    }

    class Player
    {
        ListKey currentUnit;
        ListKey currentWay;
        Dictionary<ListKey, UNIT> dUnit;
        Dictionary<ListKey, DIRECTION> dWay;
        int expPlayer;

        public Player()
        {
            dUnit = new Dictionary<ListKey, UNIT>();
            dWay = new Dictionary<ListKey, DIRECTION>();
            currentUnit = ListKey.NONE;
            currentWay = ListKey.NONE;
            expPlayer = 0;
        }

        public void setChoiceUnit(/* A choisir en fonction de l'option du joueur */)
        {
            dUnit[ListKey.NONE] = UNIT.WARRIOR;
            dUnit[ListKey.FIRSTUNIT] = UNIT.WARRIOR;
            dUnit[ListKey.SECONDUNIT] = UNIT.ARCHER;

            dWay[ListKey.NONE] = DIRECTION.RIGHT;
            dWay[ListKey.LEFTWAY] = DIRECTION.LEFT;
            dWay[ListKey.RIGHTWAY] = DIRECTION.RIGHT;
            dWay[ListKey.UPWAY] = DIRECTION.UP;
            dWay[ListKey.DOWNWAY] = DIRECTION.DOWN;
        }

        public void updateInput(ControlInput controlInput)
        {
            if (controlInput.isPressed(ListKey.FIRSTUNIT))
                currentUnit = ListKey.FIRSTUNIT;
            else if (controlInput.isPressed(ListKey.SECONDUNIT))
                currentUnit = ListKey.SECONDUNIT;
            else if (controlInput.isPressed(ListKey.THIRDUNIT))
                currentUnit = ListKey.THIRDUNIT;
            else if (controlInput.isPressed(ListKey.FOURTHUNIT))
                currentUnit = ListKey.FOURTHUNIT;

            if (controlInput.isPushed(ListKey.LEFTWAY))
                currentWay = ListKey.LEFTWAY;
            else if (controlInput.isPushed(ListKey.UPWAY))
                currentWay = ListKey.UPWAY;
            else if (controlInput.isPushed(ListKey.RIGHTWAY))
                currentWay = ListKey.RIGHTWAY;
            else if (controlInput.isPushed(ListKey.DOWNWAY))
                currentWay = ListKey.DOWNWAY;
            else
                currentWay = ListKey.NONE;
        }

        public void update(GameTime gametime)
        {
        }

        private Entity createWarrior(ContentManager content)
        {
            Warrior warrior = new Warrior(false);

            warrior.Direction = dWay[currentWay];
            warrior.load(content);
            return warrior;
        }

        private Entity createArcher(ContentManager content)
        {
            Archer archer = new Archer(false);

            archer.Direction = dWay[currentWay];
            archer.load(content);
            return archer;
        }

        public bool newUnit(GameRessource gameRessource)
        {
            if (currentUnit != ListKey.NONE && currentWay != ListKey.NONE)
            {
                if (dUnit[currentUnit] == UNIT.WARRIOR)
                    if (gameRessource.Gold < 30) return false;
                    else gameRessource.Gold -= 30;
                else if (dUnit[currentUnit] == UNIT.ARCHER)
                    if (gameRessource.Gold < 20) return false;
                    else gameRessource.Gold -= 20;
                return true;
            }
            return false;
        }

        public Entity createUnit(ContentManager content)
        {
            if (dUnit[currentUnit] == UNIT.WARRIOR)
                return createWarrior(content);
            else if (dUnit[currentUnit] == UNIT.ARCHER)
                return createArcher(content);

            return createWarrior(content);
        }

        public ListKey CurrentUnit
        {
            get { return currentUnit; }
            set { currentUnit = value; }
        }

        public ListKey CurrentWay
        {
            get { return currentWay; }
            set { currentWay = value; }
        }
    }
}
