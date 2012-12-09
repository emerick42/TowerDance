using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TowerDefense
{
    class CollisionAction
    {
        public CollisionAction()
        {  }

        public void start(List<Entity> listEntity)
        {
            foreach (EntityUnit entity in listEntity)
            {
                foreach (EntityUnit checkEntity in listEntity)
                {
                    if (entity != checkEntity && entity.Ennemy != checkEntity.Ennemy
                        && (entity.LifePoint > 0 || checkEntity.LifePoint > 0))
                    {
                        if (entity.Sphere.Intersect(checkEntity.Sphere))
                        {
                            entity.setAction(checkEntity, true);
                            break;
                        }
                        else
                            entity.setAction(checkEntity, false);
                    }
                    else
                        entity.setAction(checkEntity, false);
                }
            }
        }

    }
}
