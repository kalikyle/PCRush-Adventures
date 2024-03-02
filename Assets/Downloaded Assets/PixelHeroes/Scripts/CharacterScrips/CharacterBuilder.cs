using System.Collections.Generic;
using System.Linq;
using Assets.PixelHeroes.Scripts.CollectionScripts;
using Assets.PixelHeroes.Scripts.Utils;
using UnityEngine;


namespace Assets.PixelHeroes.Scripts.CharacterScrips
{
    public class CharacterBuilder : MonoBehaviour
    {
  
        public SpriteCollection SpriteCollection;
        public string Head = "Human";
        public string Body = "Human";
        public string Hair;
        public string Armor;
        public string Helmet;
        public string Weapon = "Katana";
        public string Shield;
        public string Cape;
        public string Back;
        public UnityEngine.U2D.Animation.SpriteLibrary SpriteLibrary;

        public Texture2D Texture { get; private set; }
        private Dictionary<string, Sprite> _sprites;

        public void Rebuild(string changed = null)
        {
            var width = SpriteCollection.Layers[0].Textures[0].width;
            var height = SpriteCollection.Layers[0].Textures[0].height;
            var dict = SpriteCollection.Layers.ToDictionary(i => i.Name, i => i);
            var layers = new Dictionary<string, Color32[]>();
            
            if (Back != "") layers.Add("Back", dict["Back"].GetPixels(Back, null, changed));
            if (Shield != "") layers.Add("Shield", dict["Shield"].GetPixels(Shield, null, changed));
            if (Body != "") layers.Add("Body", dict["Body"].GetPixels(Body, null, changed));
            if (Armor != "") layers.Add("Armor", dict["Armor"].GetPixels(Armor, null, changed));
            if (Head != "") layers.Add("Head", dict["Head"].GetPixels(Head, null, changed));
            if (Hair != "") layers.Add("Hair", dict["Hair"].GetPixels(Hair, Helmet == "" ? null : layers["Head"], changed));
            if (Cape != "") layers.Add("Cape", dict["Cape"].GetPixels(Cape, null, changed));
            if (Helmet != "") layers.Add("Helmet", dict["Helmet"].GetPixels(Helmet, null, changed));
            if (Weapon != "") layers.Add("Weapon", dict["Weapon"].GetPixels(Weapon, null, changed));

            if (Texture == null) Texture = new Texture2D(width, height) { filterMode = FilterMode.Point };

            if (Shield != "")
            {
                var s = layers["Shield"];
                var index = layers.Count - (Weapon == "" ? 1 : 2);
                var layer = layers.ElementAt(index);
                
                layers[layer.Key] = layer.Value.ToArray();

                var b = layers[layer.Key];

                for (var i = 64 * 256; i < 2 * 64 * 256; i++)
                {
                    if (s[i].a > 0) b[i] = s[i];
                }
            }
            
            Texture = TextureHelper.MergeLayers(Texture, layers.Values.ToArray());
            Texture.SetPixels(0, 912 - 16, 16, 16, new Color[16 * 16]);

            if (_sprites == null)
            {
                var clipNames = new List<string> { "Idle", "Ready", "Run", "Crawl", "Climb", "Jump", "Push", "Jab", "Slash", "Shot", "Fire1H", "Fire2H", "Block", "Death" };

                clipNames.Reverse();

                _sprites = new Dictionary<string, Sprite>();

                for (var i = 0; i < clipNames.Count; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        var key = clipNames[i] + "_" + j;

                        _sprites.Add(key, Sprite.Create(Texture, new Rect(j * 64, i * 64, 64, 64), new Vector2(0.5f, 0.125f), 100, 0, SpriteMeshType.FullRect));
                    }
                }
            }

            var spriteLibraryAsset = ScriptableObject.CreateInstance<UnityEngine.U2D.Animation.SpriteLibraryAsset>();

            foreach (var sprite in _sprites)
            {
                var split = sprite.Key.Split('_');

                spriteLibraryAsset.AddCategoryLabel(sprite.Value, split[0], split[1]);
            }

            SpriteLibrary.spriteLibraryAsset = spriteLibraryAsset;
        }
    }
}