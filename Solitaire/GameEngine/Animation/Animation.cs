using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    /// <summary>
    /// Animates a property over time.
    /// </summary>
    /// <typeparam name="TProperty">The property type.</typeparam>
    public abstract class Animation<TProperty> : GameObject where TProperty : struct
    {
        /// <summary>
        /// Creates a new <see cref="Animation"/>.
        /// </summary>
        /// <param name="start">The starting value.</param>
        /// <param name="end">The final value</param>
        /// <param name="setMethod">The method called to set the property value.</param>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="onComplete">Callback when animation completes.</param>
        public Animation(TProperty start, TProperty end, Action<TProperty> setMethod, float duration, Action? onComplete = null)
        {
            StartValue = start;
            EndValue = end;
            Duration = duration;
            SetMethod = setMethod;
            Callback = onComplete;
        }

        /// <summary>
        /// Gets the duration in seconds of the animation.
        /// </summary>
        protected float Duration { get; }

        /// <summary>
        /// Gets the value at the start of the animation.
        /// </summary>
        protected TProperty StartValue { get; }

        /// <summary>
        /// Gets the value at the end of the animation.
        /// </summary>
        protected TProperty EndValue { get; }

        /// <summary>
        /// Gets the setter method used to update the property.
        /// </summary>
        protected Action<TProperty> SetMethod { get; }

        /// <summary>
        /// Gets the callback method to run when the animation completes.
        /// </summary>
        protected Action? Callback { get; }

        /// <summary>
        /// Gets the played duration of the animation.
        /// </summary>
        public float CurrentAnimationTime => _CurrentAnimationTime;
        protected float _CurrentAnimationTime = 0f;

        /// <summary>
        /// Gets the progress of the animation from 0 (not started) to 1 (finished).
        /// </summary>
        public float Progress => _CurrentAnimationTime / Duration;

        protected internal override void Draw(VideoService videoService) { }

        protected internal sealed override void Update()
        {
            _CurrentAnimationTime = Math.Min(Duration, _CurrentAnimationTime + Scene.DeltaTime);
            UpdateValue();
            if (_CurrentAnimationTime == Duration)
            {
                Callback?.Invoke();
                Dispose();
            }
        }

        /// <summary>
        /// Called one per frame to increment the value.
        /// </summary>
        protected abstract void UpdateValue();
    }
}
