﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonoGameKunskapsspel
{
    public class MathiasIntroduction : CutScene
    {
        public Room room;
        private readonly List<string> dialogue;
        private double timeSpan = 0.0;
        private DialogueWindow dialogueWindow;

        public MathiasIntroduction(Player player, KunskapsSpel kunskapsSpel, Room room, List<string> dialogue) : base(player, kunskapsSpel, room, dialogue)
        {
            this.room = room;
            this.dialogue = dialogue;
        }

        public override void Update(GameTime gameTime)
        {
            if (phaseCounter == 1)
                PhaseOne(gameTime);

            if (phaseCounter == 2)
                PhaseTwo();

            if (phaseCounter == 3)
                EndScene();
        }

        private void PhaseTwo()
        {
            if (dialogueWindow == null)
                dialogueWindow = new DialogueWindow(kunskapsSpel, player, kunskapsSpel.camera, dialogue);
            else
                phaseCounter++;
        }

        private void PhaseOne(GameTime gameTime)
        {
            if (IsGoingAway(hiddenFollowPoint, hiddenFollowPoint + dir * speed, room.mathias.hitBox.Center.ToVector2()))
                phaseCounter = 2;

            timeSpan = gameTime.TotalGameTime.TotalSeconds;
            hiddenFollowPoint += dir * speed;
        }

        private bool IsGoingAway(Vector2 currentPoint, Vector2 nextPoint, Vector2 goToPoint)
        {
            currentPoint.Normalize();
            nextPoint.Normalize();
            goToPoint.Normalize();

            float currentX = currentPoint.X - goToPoint.X;
            float currentY = currentPoint.Y - goToPoint.Y;

            float nextX = nextPoint.X - goToPoint.X;
            float nextY = nextPoint.Y - goToPoint.Y;

            double currentHypotenuse = Math.Sqrt(currentX * currentX + currentY * currentY);
            double nextHypotenuse = Math.Sqrt(nextX * nextX + nextY * nextY);

            if (nextHypotenuse < currentHypotenuse)
                return false;

            return true;
        }

        private Vector2 dir;
        private Vector2 speed = new Vector2(5f, 5f);

        public override void StartScene()
        {
            player.activeState = State.WatchingCutScene;
            hiddenFollowPoint = player.hitBox.Location.ToVector2();
            dir = room.mathias.hitBox.Center.ToVector2() - hiddenFollowPoint;
            dir.Normalize();
        }

        public override void EndScene()
        {
            player.activeState = State.Walking;
            room.mathias.hasInteracted = true;
        }
    }
}
