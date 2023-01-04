using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace MonoGameKunskapsspel
{
    public class MusicManager
    {
        Song song;
        readonly List<SoundEffect> walkingOnGrass;
        readonly List<SoundEffect> walkingOnStone;
        readonly List<string> musicPathList = new()
        {
            "Music/OnceOponATime",    //0
            "Music/UnderTale",    //1
            "Music/UnderTale",    //2
            "Music/063 Investigating the Caves",    //3
            "Music/064 Lair of Harpies",            //4
            "Music/Barrier",    //5
            "Music/Barrier",    //6
            "Music/Bwanga",    //7
        };
        private readonly KunskapsSpel kunskapsSpel;
        private string activeMusicPath;
        public bool isFadingOut;
        private bool isFadingIn;
        private double FADE_SPEED = 0.005;

        public MusicManager(KunskapsSpel kunskapsSpel) 
        {
            this.kunskapsSpel = kunskapsSpel;
            MediaPlayer.IsRepeating = true;
            walkingOnGrass = new() 
            { 
                kunskapsSpel.Content.Load<SoundEffect>("Music/GrassStep1"),
                kunskapsSpel.Content.Load<SoundEffect>("Music/GrassStep2"), 
                kunskapsSpel.Content.Load<SoundEffect>("Music/GrassStep3"), 
                kunskapsSpel.Content.Load<SoundEffect>("Music/GrassStep4"), 
            };
            walkingOnStone = new() 
            { 
                kunskapsSpel.Content.Load<SoundEffect>("Music/StoneStep1"),
                kunskapsSpel.Content.Load<SoundEffect>("Music/StoneStep2"), 
                kunskapsSpel.Content.Load<SoundEffect>("Music/StoneStep3"), 
                kunskapsSpel.Content.Load<SoundEffect>("Music/StoneStep4"), 
            };
        }
        private const double interval = 0.6;
        private double gameTimeElapsed = 0;
        private readonly Random random = new();
        public bool canBeChanged = true;
        public int increment = 0;

        public void Update(int id, GameTime gameTime)
        {
            if (activeMusicPath != musicPathList[id + increment] && kunskapsSpel.player.activeState != State.Ended)
                ChangeMusicTo(id + increment);

            if (isFadingIn)
                FadeIn();
            if (isFadingOut)
                FadeOut();

            if (gameTimeElapsed + interval > gameTime.TotalGameTime.TotalSeconds)
                return;
            gameTimeElapsed = gameTime.TotalGameTime.TotalSeconds;
            int i = random.Next(0, 4);
            SoundEffect.MasterVolume = 0.05f;
            if (id > 0 && id < 3 && kunskapsSpel.player.velocity != Point.Zero)
                walkingOnGrass[i].Play();
            if (id > 2 && id < 7 && kunskapsSpel.player.velocity != Point.Zero)
                walkingOnStone[i].Play();
        }

        public void Stop()
        {
            MediaPlayer.Stop();
            activeMusicPath = "";
        }

        private void ChangeMusicTo(int id)
        {
            isFadingOut = true;
            isFadingIn = false;
            activeMusicPath = musicPathList[id];
            song = kunskapsSpel.Content.Load<Song>(activeMusicPath);
        }

        private void FadeOut()
        {
            float volume = MediaPlayer.Volume;
            volume -= (float)FADE_SPEED;
            if (volume < 0)
            {
                volume = 0;
                isFadingOut = false;
                isFadingIn = true;
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
            }
            MediaPlayer.Volume = volume;
        }

        private void FadeIn()
        {
            float volume = MediaPlayer.Volume;
            volume += (float)FADE_SPEED;
            if (volume > 0.2f)
            {
                volume = 0.2f;
                isFadingIn = false;
            }
            MediaPlayer.Volume = volume;
        }


        public void PlayBossFightMusic()
        {
            MediaPlayer.Volume = 0.4f;
            increment = 1;
        }

        public void ChangeSlowlyToEnding()
        {
            increment = -6;
            FADE_SPEED = 0.002f;
        }
    }
}
