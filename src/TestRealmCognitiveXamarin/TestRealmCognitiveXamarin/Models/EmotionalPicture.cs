using Realms;

namespace TestRealmCognitiveXamarin.Models
{
    public class EmotionalPicture : RealmObject
    {
        public string Data { get; set; }
        public string Emotion { get; set; }
    }
}
