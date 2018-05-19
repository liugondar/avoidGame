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
        private Song explosionEffect;
        private Song collisionEffect;
        public bool IsBackgroundMusicPlaying { get; set; }

        public SoundManager(ContentManager content)
        {
            backgroundMusic = content.Load<Song>(@"Music\background");
            explosionEffect= content.Load<Song>(@"Music\explosion");
            collisionEffect= content.Load<Song>(@"Music\collision");
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
    }
}
