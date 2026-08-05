using UnityEngine;
using System.Collections.Generic;


namespace CodeFrontGames.Snake2DLite
{
    public class AudioManager : MonoBehaviour
    {


        public static AudioManager Instance;


        [System.Serializable]
        public class Sound
        {
            public string name;
            public AudioClip clip;
            [Range(0f, 1f)] public float volume = 1f;
            [Range(0.1f, 3f)] public float pitch = 1f;
            public bool loop = false;
            [HideInInspector] public AudioSource source;
        }


        public enum Sounds
        {
            Background,
            ButtonPress,
            FoodEaten,
            GameLost
        }


        [SerializeField] public List<Sound> sounds;
        [SerializeField] public string backgroundMusicName;


        void Awake()
        {
            // Singleton pattern
            if (Instance == null)
            {
                Instance = this;
            }

            // Create an AudioSource for each sound
            foreach (var s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        void Start()
        {
            if (!string.IsNullOrEmpty(backgroundMusicName))
                Play(backgroundMusicName);
        }

        public void Play(string name)
        {
            Sound s = sounds.Find(sound => sound.name == name);
            if (s == null)
            {
                return;
            }

            s.source.Play();
        }

        public void Stop(string name)
        {
            Sound s = sounds.Find(sound => sound.name == name);
            if (s != null)
                s.source.Stop();
        }

    }
}