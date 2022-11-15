using System;

namespace GameEngine
{
    /// <summary>
    /// A sound.
    /// </summary>
    public class Sound
    {
        /// <summary>
        /// Creates a new <see cref="Sound"/>.
        /// </summary>
        /// <param name="filename">The sound's file location.</param>
        /// <param name="volume">The sound's volume from 0 (mute) to 1 (max).</param>
        /// <param name="isRepeating">Whether the sound should repeat.</param>
        public Sound(string filename, int volume = 1, bool isRepeating = false)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));

            if (volume < 0 || volume > 1)
                throw new ArgumentOutOfRangeException(nameof(volume));

            Filename = filename;
            Volume = volume;
            IsRepeatingSound = isRepeating;
        }

        /// <summary>
        /// Gets the sound's filename.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        /// Gets the sound's volume.
        /// </summary>
        public int Volume { get; }

        /// <summary>
        /// Gets whether the sound is repeating.
        /// </summary>
        public bool IsRepeatingSound { get; }
    }
}
