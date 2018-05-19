using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMaster.Bus
{
    public class SoundManager
    {
        private Song backgroundMusic;
        private SoundEffect explosionEffect;
        private SoundEffect collisionEffect;
        public bool IsBackgroundMusicPlaying { get; set; }
        public bool IsSoundPlaying { get; set; }

        public SoundManager(ContentManager content)
        {
            backgroundMusic = content.Load<Song>(@"Sounds\background");
            explosionEffect = content.Load<SoundEffect>(@"Sounds\explosion");
            collisionEffect = content.Load<SoundEffect>(@"Sounds\collision");
            IsSoundPlaying = true;
        }

        public void PlayBackgroundSound()
        {
            if (MediaPlayer.GameHasControl)
            {
                MediaPlayer.Play(backgroundMusic);
                MediaPlayer.IsRepeating = true;
                IsBackgroundMusicPlaying = true;
            }
        }
        public void ChangeStateBackgroundSound()
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            IsBackgroundMusicPlaying = !MediaPlayer.IsMuted;
        }
        public void PlayCollisionSound()
        {
            if (IsSoundPlaying) collisionEffect.Play(volume: 0.25f, pitch: 0.0f, pan: 0.0f);
        }
        public void PlayExplosionSound()
        {
            if (IsSoundPlaying) explosionEffect.Play(volume: 0.25f, pitch: 0.0f, pan: 0.0f);
        }

        public void ChangeStateSound()
        {
            IsSoundPlaying = !IsSoundPlaying;
        }
    }
}
