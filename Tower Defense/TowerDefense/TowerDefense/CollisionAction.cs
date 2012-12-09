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
                    if (entity != checkEntity)
                    {
                        if (checkCollision(entity, checkEntity) == true)
                        {
                            entity.setAction(checkEntity, true);
                            break;
                        }
                        else
                            entity.setAction(checkEntity, false);
                    }
                }
            }
        }

        private bool checkCollision(EntityUnit unit1, EntityUnit unit2)
        {
        	Point d;

            d.X = (int)(unit1.Sphere.Center.X - unit2.Sphere.Center.X);
            d.Y = (int)(unit1.Sphere.Center.Y - unit2.Sphere.Center.Y); 
            int dist2 = d.X * d.X + d.Y * d.Y;

            float radiusSum = unit1.Sphere.Radius;


            if (dist2 <= radiusSum * radiusSum)
                return true;
            else
                return false; 
         } 

    }
}
