using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace UtilityPlus.SpellTracker
{
    class SummonerSpellTracker
    {
        private Obj_AI_Hero hero;
        private bool isSummoner1;
        private string spellName;

        private Render.Sprite sImage;

        public Vector2 heroHPBarPosition;

        private bool isGreyScale = false;

        public SummonerSpellTracker(Obj_AI_Hero hero, string spellName, bool isSummoner1 = true)
        {
            this.hero = hero;
            this.isSummoner1 = isSummoner1;
            this.spellName = spellName;

            InitSummonerImage(GetSummonerBitmap(spellName));
        }

        public static Bitmap GetSummonerBitmap(string spellName)
        {
            return ImageLoader.Load(spellName);
        }

        private void InitSummonerImage(Bitmap bmp)
        {
            Game.OnUpdate += Drawing_OnDraw;
            
            sImage = new Render.Sprite(bmp, new Vector2(0, 0));

            //sImage.Scale = new Vector2(0.5f, 0.5f);
            sImage.VisibleCondition = sender => !(!hero.IsHPBarRendered
                    || (hero.IsAlly && !Tracker.menu.Item("TrackAllyCooldown").GetValue<bool>())
                    || (hero.IsEnemy && !Tracker.menu.Item("TrackEnemyCooldown").GetValue<bool>()));

            sImage.PositionUpdate = delegate
            {
                var summoner2OffSet = isSummoner1 ? 0 : sImage.Height;

                var startX = heroHPBarPosition.X - 9;
                var startY = heroHPBarPosition.Y + summoner2OffSet +
                    (hero.IsAlly ? Tracker.allyOffsetY : Tracker.enemyOffsetY);

                return new Vector2(startX, startY);
            };
            sImage.Add(0); 
           
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            heroHPBarPosition = new Vector2(hero.HPBarPosition.X, hero.HPBarPosition.Y);
        }

        public void SetGreyScale()
        {
            if (!isGreyScale)
            {
                isGreyScale = true;
                sImage.SetSaturation(0.0f);
            }
        }

        public void SetNormalScale()
        {
            if (isGreyScale)
            {
                isGreyScale = false;
                sImage.Reset();
            }
        }
    }
}
