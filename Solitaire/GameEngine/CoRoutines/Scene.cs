using System;
using System.Collections;
using System.Threading.Tasks;

namespace GameEngine
{
    public abstract partial class Scene
    {
        /// <summary>
        /// Begins running an asynchronous task in the background.
        /// </summary>
        /// <param name="routine">The routine to run.</param>
        public static async void StartCoroutine(IEnumerator routine)
        {
            while (routine.MoveNext())
            {
                if (routine.Current is WaitForSeconds waiter)
                    await Task.Delay(waiter._WaitTime);
            }
        }
    }
}