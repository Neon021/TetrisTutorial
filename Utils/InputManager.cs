using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TetrisTutorial.Enums;

namespace TetrisTutorial.Utils
{
    internal class InputManager
    {
        public Dictionary<Controls, Keys> ControlScheme { get; private set; }

        private KeyboardState _newState, _oldState;

        public InputManager()
        {
            _newState = Keyboard.GetState();
            _oldState = _newState;

            ControlScheme = new Dictionary<Controls, Keys>
            {
                { Controls.Left, Keys.Left },
                { Controls.Right, Keys.Right },
                { Controls.SoftDrop, Keys.Down },
                { Controls.HardDrop, Keys.Up },
                { Controls.RotateCW, Keys.LeftControl },
                { Controls.RotateCCW, Keys.LeftShift }
            };
        }

        public void Update()
        {
            _oldState = _newState;
            _newState = Keyboard.GetState();
        }

        public bool IsPressed(Controls key)
        {
            if (!ControlScheme.ContainsKey(key))
                return false;

            Keys k = ControlScheme[key];
            //If the state of this Keyboard key was(_oldState) up and now(_newState) down.
            return _oldState.IsKeyUp(k) && _newState.IsKeyDown(k);
        }
        public bool IsReleased(Controls key)
        {
            if (!ControlScheme.ContainsKey(key))
                return false;

            Keys k = ControlScheme[key];
            //If the state of this Keyboard key was(_oldState) down and now(_newState) up.
            return _oldState.IsKeyDown(k) && _newState.IsKeyUp(k);
        }

        public bool IsDown(Controls key)
        {
            if (!ControlScheme.ContainsKey(key))
                return false;

            Keys k = ControlScheme[key];
            return _newState.IsKeyDown(k);
        }
    }
}
