using UnityEngine;


/// <summary>
/// With this scriptable object the player can make custom foods
/// It has a lot of variables you can set as well!
/// Description isn't heavily used sadly.
/// </summary>
namespace Petwork.Food
{
    [CreateAssetMenu(fileName = "NewFood", menuName = "Food")]
    public class Food : ScriptableObject
    {
        [SerializeField] private string foodName = "New food name";
        [SerializeField] private Sprite foodSprite;
        [SerializeField] private float feedAmount = 0.0f;
        [SerializeField] private float foodHealthiness = 0.0f;
        [SerializeField] private TextAsset foodDescription;
        [SerializeField] private float feedHappiness = 0.0f;
        [SerializeField] private float foodPrice = 0.0f;
        [SerializeField] private float foodEnergy = 0.0f;

        public enum foodType
        {
            PleaseSelectType = -1,
            Fruit,
            Protein,
            Dairy,
            Vegitble,
            Grains
        };

        public foodType myType = foodType.PleaseSelectType;
        public string Name => foodName;
        public Sprite Sprite => foodSprite;
        public float Feeds => feedAmount;
        public float Health => foodHealthiness;
        public float Happiness => feedHappiness;
        public float Price => foodPrice;
        public float Energy => foodEnergy;
        public string Description => foodDescription.text
            .Replace("{name}", $"{foodName}")
            .Replace("{feeds}", $"{feedAmount}")
            .Replace("{health}", $"{foodHealthiness}")
            .Replace("{happiness}", $"{feedHappiness}")
            .Replace("{energy}", $"{foodEnergy}")
            .Replace("{type}", $"{myType}");

    }
}

