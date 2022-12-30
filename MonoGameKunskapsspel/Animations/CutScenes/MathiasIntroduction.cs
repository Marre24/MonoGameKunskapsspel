using Microsoft.Xna.Framework;
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
        private readonly Component target;
        public readonly Room room;
        private readonly List<string> dialogue;
        private DialogueWindow dialogueWindow;

        private Vector2 dir;
        private Vector2 speed = new(5f, 5f);

        public MathiasIntroduction(Player player, KunskapsSpel kunskapsSpel, Component target, Room room, List<string> dialogue) : base(player, kunskapsSpel)
        {
            this.target = target;
            this.room = room;
            this.dialogue = dialogue;
            StartScene();
        }

        public override void Update(GameTime gameTime)
        {
            kunskapsSpel.camera.Follow(hiddenFollowPoint);

            if (phaseCounter == 1)
                PhaseOne();

            if (phaseCounter == 2)
                PhaseTwo();

            if (phaseCounter == 3)
                EndScene();
        }

        private void PhaseOne()
        {
            if (IsGoingAway(hiddenFollowPoint, hiddenFollowPoint + dir * speed, target.hitBox.Center.ToVector2()))
                phaseCounter = 2;

            hiddenFollowPoint += dir * speed;
        }

        private void PhaseTwo()
        {
            if (dialogueWindow == null)
                dialogueWindow = new DialogueWindow(kunskapsSpel, player, kunskapsSpel.camera, dialogue);
            else
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
            player.activeState = State.WatchingCutScene;
            hiddenFollowPoint = player.hitBox.Location.ToVector2();

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
