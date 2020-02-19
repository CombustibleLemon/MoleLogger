using Keystroke.API;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keystroke.API.CallbackObjects;

namespace keylogger
{
    class Program
    {
        static void Main(string[] args)
        {
            KeyHandler keyHandler = new KeyHandler();

            string programTitle = "MoleLogger v1.1 by Dan Leonard"; 
#if DEBUG
            programTitle += " DEBUG";
#endif
            Console.Title = programTitle;

            using (var api = new KeystrokeAPI())
            {
                api.CreateKeyboardHook((character) => {
                    keyHandler.parseCharacter(character);
                });
                Application.Run();
            }
        }
    }

    class KeyHandler
    {
        public List<KeyCode> currentLevel = new List<KeyCode>();

        public void parseCharacter(KeyPressed character)
        {
            string removedBrackets = character.CurrentWindow.Substring(1, character.CurrentWindow.Length - 2);
            
            if (removedBrackets == "Mole Game")
            {
                moleCharacter(character.KeyCode);
            }
        }

        private void moleCharacter(KeyCode keyCode)
        {
            if (keyCode == KeyCode.R)
            {
                currentLevel.Clear();
            }
            else if (keyCode == KeyCode.Down || keyCode == KeyCode.Up || keyCode == KeyCode.Left || keyCode == KeyCode.Right)
            {
                currentLevel.Add(keyCode);
            }
            else if (keyCode == KeyCode.Z)
            {
                currentLevel.RemoveAt(currentLevel.Count - 1);
            }
            else if (keyCode == KeyCode.Space)
            {
                foreach (KeyCode stroke in currentLevel)
                {
                    Console.Write("{0},",stroke.ToString());
                }
                Console.WriteLine();
            }
        }
    }
}
