﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoGameKunskapsspel
{
    public class PanToTarget : CutScene
    {
        private readonly Component target;
        public readonly Room room;
        private readonly List<string> dialogue;
        private DialogueWindow dialogueWindow;
        private readonly bool lastBatttle;
        private readonly State activeState;


        private Vector2 dir;
        private Vector2 speed = new(5f, 5f);

        public PanToTarget(Player player, KunskapsSpel kunskapsSpel, Component target, Room room, List<string> dialogue) : base(player, kunskapsSpel)
        {
            this.target = target;
            this.room = room;
            this.dialogue = dialogue;
            activeState = State.WatchingCutScene;
            lastBatttle = true;
            problems = new Problems().GetTheThreeLastProblem();
        }
        public PanToTarget(Player player, KunskapsSpel kunskapsSpel, Component target, Room room, List<string> dialogue, bool runOnCreate) : base(player, kunskapsSpel)
        {
            this.target = target;
            this.room = room;
            this.dialogue = dialogue;
            lastBatttle = false;
            activeState = State.Walking;
            StartScene();
        }

        public override void Update(GameTime gameTime)
        {
            kunskapsSpel.camera.Follow(hiddenFollowPoint);
            player.Update(gameTime);
            room.npc?.Update(gameTime);
            if (room.enemies != null)
                foreach (Enemy enemy in room.enemies)
                    enemy.Update(gameTime);

            if (phaseCounter == 1)
            {
                PhaseOne();
                return;
            }

            if (phaseCounter == 2)
            {
                PhaseTwo();
                return;
            }

            if (phaseCounter == 3)
            {
                EndScene();
                return;
            }
        }

        private void PhaseOne()
        {
            if (IsGoingAway(hiddenFollowPoint, hiddenFollowPoint + dir * speed, target.hitBox.Center.ToVector2()))
                phaseCounter = 2;

            hiddenFollowPoint += dir * speed;
        }

        readonly Dictionary<string, (int, List<string> solutions)> problems;

        private void PhaseTwo()
        {
            dialogueWindow ??= new DialogueWindow(kunskapsSpel, player, kunskapsSpel.camera, dialogue, activeState);
            
            if (dialogueWindow.ended && problems != null && lastBatttle)
            {
                _ = new FinalBattle(problems, kunskapsSpel, kunskapsSpel.camera, room.enemies[1], player, player.activeState);
            }
            else if(dialogueWindow.ended)
                phaseCounter++;
        }

        

        private static bool IsGoingAway(Vector2 currentPoint, Vector2 nextPoint, Vector2 goToPoint)
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

        public override void StartScene()
        {
            phaseCounter = 1;
            kunskapsSpel.activeCutscene = this;
            player.activeState = State.WatchingCutScene;
            hiddenFollowPoint = player.hitBox.Location.ToVector2();
            target.hasInteracted = true;
            dialogueWindow = null;

            dir = target.hitBox.Center.ToVector2() - hiddenFollowPoint;
            dir.Normalize();
        }

        public override void EndScene()
        {
            player.activeState = State.Walking;
            target.hasInteracted = true;
        }
    }
}
