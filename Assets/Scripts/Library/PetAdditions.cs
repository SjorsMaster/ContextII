using System;

namespace Petwork.Additions
{
    /// <summary>
    /// This class if for custom functions I could use
    /// </summary>
    public class Tools
    {
        public static void ChangeScene(UnityEngine.SceneManagement.Scene scene)
        {
            ChangeScene(scene.name);
        }
        public static void ChangeScene(String name)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        }
    }

    /// <summary>
    /// This class is for custom reference variables I want to have
    /// </summary>
    public class petDefinitions
    {
        public enum petState
        {
            Neutral,
            Happy,
            Sad,
            Angry,
            Hungry
        }

        //These are the faces, again, for readabillity
        public enum petFaces
        {
            mouthNeutral,
            eyesNeutral,
            mouthSad,
            eyesSad,
            mouthHappy,
            eyesHappy
        }


        [Serializable]
        public struct Expressions
        {
            public UnityEngine.Sprite mouth_neutral;
            public UnityEngine.Sprite eyes_neutral;
            public UnityEngine.Sprite mouth_sad;
            public UnityEngine.Sprite eyes_sad;
            public UnityEngine.Sprite mouth_happy;
            public UnityEngine.Sprite eyes_happy;
            public UnityEngine.Sprite mouth_hungry;
            public UnityEngine.Sprite eyes_blink;
        }

        [Serializable]
        public struct CycleAdditions
        {
            public float Happiness;
            public float Hunger;
            public float Health;
            public float Energy;
            public float Money;
        }
    }

    /// <summary>
    /// This class is for the pet variables
    /// </summary>
    public class PetParameters
    {
        public float Happiness;
        public float Hunger;
        public float Health;
        public float Energy;
        public string Name;
        public DateTime lastActive;
        public float Money;
    }
}
