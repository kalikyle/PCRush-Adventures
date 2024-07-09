using System.Collections.Generic;
using System.Linq;
using Assets.PixelHeroes.Scripts.CollectionScripts;
using Assets.PixelHeroes.Scripts.EditorScripts;
using Assets.PixelHeroes.Scripts.Utils;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        public string Weapon;
        public string Shield;
        public string Cape;
        public string Back;


        public UnityEngine.U2D.Animation.SpriteLibrary SpriteLibrary;
       
        
        public int CharChanged = 0;

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
            CharChanged = 1;
           
            SaveChanges();

           
        }
        public void Rebuilds(string changed = null)
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
            CharChanged = 1;

           


        }


        public void CombineHeadAndHairSprites()
        {
            try
            {

                int specificX = 0; // The x-coordinate of the top-left corner of the area
                int specificY = 0;
                float specificWidth = 64; // The width of the area
                float specificHeight = 64;


                Rect zoomedRect = new Rect(specificX, specificY, specificWidth, specificHeight);
                Sprite _rebuildSprite = Sprite.Create(Texture, zoomedRect, new Vector2(0.5f, 0.5f));
                _rebuildSprite.name = "PlayerSprite";

                DialogueManager.GetInstance().PlayerSprite = _rebuildSprite;

                //bool spriteExists = false;

                //foreach (var sprite in DialogueManager.GetInstance().PlayerSprite)
                //{
                //    if (sprite.name == _rebuildSprite.name)
                //    {
                //        spriteExists = true;
                //        break;
                //    }
                //}

                // Insert the sprite at the top of the list if it doesn't already exist
                //if (!spriteExists)
                //{
                   
                //}
                //else
                //{
                //    Debug.Log("Sprite with the same name already exists in the list. Not inserting again.");
                //}

                //GameManager.instance.playersprite = _rebuildSprite;
                GameManager.instance.PlayerOWImage.sprite = _rebuildSprite;
                GameManager.instance.PlayerImage.sprite = _rebuildSprite;
                GameManager.instance.PlayerStatsImage.sprite = _rebuildSprite;

                

            }
            catch { }
            


            //// Retrieve the head and hair sprites from the SpriteCollection
            //Sprite headSprite = null;
            //Sprite hairSprite = null;
            //string[] Headparts = Head.Split('#');
            //string HeadTextureName = Headparts[0];
            //string[] Hairparts = Hair.Split('#');
            //string HairTextureName = Hairparts[0];
            //float specificWidth = 64; // The width of the area
            //float specificHeight = 64;
            //// Find the head and hair sprites in the SpriteCollection
            //foreach (var layer in SpriteCollection.Layers)
            //{
            //    if (layer.Name == "Head")
            //    {
            //        var texture = layer.Textures.FirstOrDefault(t => t.name == HeadTextureName);
            //        if (texture != null)
            //        {
            //            int specificX = 0; // The x-coordinate of the top-left corner of the area
            //            int specificY = 0; // The y-coordinate of the top-left corner of the area
            //             // The height of the area

            //            // Create a rectangle representing the zoomed area in the texture
            //            Rect zoomedRect = new Rect(specificX, specificY, specificWidth, specificHeight);
            //            headSprite = Sprite.Create(texture, zoomedRect, new Vector2(0.5f,0.5f));
            //        }
            //        else
            //        {
            //            Debug.LogError("Head texture not found in the layer.");
            //        }
            //    }
            //    else if (layer.Name == "Hair")
            //    {
            //        var texture = layer.Textures.FirstOrDefault(t => t.name == HairTextureName);
            //        if (texture != null)
            //        {
            //            int specificX = 0; // The x-coordinate of the top-left corner of the area
            //            int specificY = 0; // The y-coordinate of the top-left corner of the area
            //            // The height of the area

            //            // Create a rectangle representing the zoomed area in the texture
            //            Rect zoomedRect = new Rect(specificX, specificY, specificWidth, specificHeight);
            //            hairSprite = Sprite.Create(texture, zoomedRect, new Vector2(0.5f, 0.5f));
            //        }
            //        else
            //        {
            //            Debug.LogError("Hair texture not found in the layer.");
            //        }
            //    }

            //    //GameManager.instance.PlayerImagehead.sprite = headSprite;
            //   // GameManager.instance.PlayerImagehair.sprite = hairSprite;

            //}
            //GameManager.instance.PlayerImage.sprite = headSprite;

            //// Check if both head and hair sprites are found
            //if (headSprite != null && hairSprite != null)
            //{
            //    // Calculate the width and height of the combined texture
            //    // Calculate the combined width and height using the zoomed rectangles
            //    int combinedWidth = Mathf.Max(64, 64);
            //    int combinedHeight = Mathf.Max((int)specificHeight,64);

            //    // Create a new texture to hold the combined sprites
            //    Texture2D combinedTexture = new Texture2D(combinedWidth, combinedHeight, TextureFormat.RGBA32, false);

            //    // Copy the head sprite onto the combined texture
            //    Color[] headPixels = headSprite.texture.GetPixels();
            //    combinedTexture.SetPixels((int)headSprite.textureRect.x, (int)headSprite.textureRect.y, (int)headSprite.textureRect.width, (int)headSprite.textureRect.height, headPixels);

            //    // Calculate the offset to position the hair sprite relative to the head sprite
            //    int xOffset = (int)(hairSprite.textureRect.x - headSprite.textureRect.x);
            //    int yOffset = (int)(hairSprite.textureRect.y - headSprite.textureRect.y);

            //    // Copy the hair sprite onto the combined texture, overlaying it on top of the head sprite
            //    Color[] hairPixels = hairSprite.texture.GetPixels();
            //    for (int y = 0; y < hairSprite.textureRect.height; y++)
            //    {
            //        for (int x = 0; x < hairSprite.textureRect.width; x++)
            //        {
            //            int combinedX = x + xOffset;
            //            int combinedY = y + yOffset;

            //            // Check if the combined coordinates are within the bounds of the combined texture
            //            if (combinedX >= 0 && combinedX < combinedWidth && combinedY >= 0 && combinedY < combinedHeight)
            //            {
            //                Color hairPixel = hairPixels[x + y * (int)hairSprite.textureRect.width];
            //                // Only overlay non-transparent pixels
            //                if (hairPixel.a > 0)
            //                {
            //                    combinedTexture.SetPixel(combinedX, combinedY, hairPixel);
            //                }
            //            }
            //        }
            //    }

            //    // Apply changes to the combined texture
            //    combinedTexture.Apply();

            //    // Create a sprite from the combined texture
            //    Sprite combinedSprite = Sprite.Create(combinedTexture, new Rect(0, 0, combinedTexture.width, combinedTexture.height), Vector2.one * 0.5f);

            //    // Assign the combined sprite to the player image or wherever it's needed
            //    GameManager.instance.PlayerImage.sprite = combinedSprite;
            //}
            //else
            //{
            //    Debug.LogError("Head or hair sprite not found in the SpriteCollection.");
            //}
        }


        //public void SaveChangess()
        //{

        //    // Save each layer's data to PlayerPrefs
        //    PlayerPrefs.SetString("Head", Head);
        //    PlayerPrefs.SetString("Body", Body);
        //    PlayerPrefs.SetString("Hair", Hair);
        //    PlayerPrefs.SetString("Armor", Armor);
        //    PlayerPrefs.SetString("Helmet", Helmet);
        //    PlayerPrefs.SetString("Weapon", Weapon);
        //    PlayerPrefs.SetString("Shield", Shield);
        //    PlayerPrefs.SetString("Cape", Cape);
        //    PlayerPrefs.SetString("Back", Back);
        //    PlayerPrefs.SetInt("CharChanged", CharChanged);

        //    // Save PlayerPrefs
        //    PlayerPrefs.Save();


        //}
        //public void LoadSavedDatas()
        //{
        //    // Load each layer's data from PlayerPrefs
        //    Head = PlayerPrefs.GetString("Head");
        //    Body = PlayerPrefs.GetString("Body");
        //    Hair = PlayerPrefs.GetString("Hair");
        //    Armor = PlayerPrefs.GetString("Armor");
        //    Helmet = PlayerPrefs.GetString("Helmet");
        //    Weapon = PlayerPrefs.GetString("Weapon");
        //    Shield = PlayerPrefs.GetString("Shield");
        //    Cape = PlayerPrefs.GetString("Cape");
        //    Back = PlayerPrefs.GetString("Back");

        //    Rebuild();


        //}


        public string DocumentId => GameManager.instance.UserID; // Combine UserID with "PlayerCharacters" for the document ID
        public void SaveChanges()
        {
            // Create a dictionary to hold the character data
            Dictionary<string, object> characterData = new Dictionary<string, object>
    {
        { "Head", Head },
        { "Body", Body },
        { "Hair", Hair },
        { "Armor", Armor },
        { "Helmet", Helmet },
        { "Weapon", Weapon },
        { "Shield", Shield },
        { "Cape", Cape },
        { "Back", Back },
    };

            // Get a reference to the user's document in Firestore
            DocumentReference userDocRef = FirebaseFirestore.DefaultInstance.Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);
            CollectionReference characterDesignCollectionRef = userDocRef.Collection("PlayerCharacterDesign");
            // Update the user's document with the character data
            DocumentReference characterDataDocRef = characterDesignCollectionRef.Document("characterData");
            characterDataDocRef.SetAsync(characterData)
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCompleted)
                    {
                        Debug.Log("Character data saved to Firestore.");
                    }
                    else if (task.IsFaulted)
                    {
                        Debug.LogError("Failed to save character data to Firestore: " + task.Exception);
                    }
                });
        }

        public void LoadSavedData()
        {
            if (GameManager.instance.UserID != "")
            {
                FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

                // Get a reference to the user's document in Firestore
                DocumentReference userDocRef = db.Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);

                // Create a reference to the PlayerCharacterDesign subcollection
                CollectionReference characterDesignCollectionRef = userDocRef.Collection("PlayerCharacterDesign");

                // Get the document containing the character data from the PlayerCharacterDesign subcollection
                DocumentReference characterDataDocRef = characterDesignCollectionRef.Document("characterData");

                // Load the character data from the document
                characterDataDocRef.GetSnapshotAsync()
                    .ContinueWithOnMainThread(task =>
                    {
                        try
                        {
                            if (task.IsCompleted)
                            {
                                DocumentSnapshot snapshot = task.Result;
                                if (snapshot.Exists)
                                {
                                    // Extract character data from the document
                                    Dictionary<string, object> data = snapshot.ToDictionary();
                                    Head = data.ContainsKey("Head") ? data["Head"].ToString() : "";
                                    Body = data.ContainsKey("Body") ? data["Body"].ToString() : "";
                                    Hair = data.ContainsKey("Hair") ? data["Hair"].ToString() : "";
                                    Armor = data.ContainsKey("Armor") ? data["Armor"].ToString() : "";
                                    Helmet = data.ContainsKey("Helmet") ? data["Helmet"].ToString() : "";
                                    Weapon = data.ContainsKey("Weapon") ? data["Weapon"].ToString() : "";
                                    Shield = data.ContainsKey("Shield") ? data["Shield"].ToString() : "";
                                    Cape = data.ContainsKey("Cape") ? data["Cape"].ToString() : "";
                                    Back = data.ContainsKey("Back") ? data["Back"].ToString() : "";
                                    // Repeat for other fields...


                                    // Initialize the DefaultCharacter dictionary
                                    GameManager.instance.DefaultCharacter["Helmet"] = Helmet;
                                    GameManager.instance.DefaultCharacter["Shield"] = Shield;
                                    GameManager.instance.DefaultCharacter["Armor"] = Armor;
                                    GameManager.instance.DefaultCharacter["Weapon"] = Weapon;

                                    // Rebuild character using the loaded data
                                    Rebuild();
                                    CombineHeadAndHairSprites();

                                    Debug.Log("Character data loaded from Firestore.");
                                }
                                else
                                {
                                    Debug.LogWarning("No character data found in Firestore for user ID: " + GameManager.instance.UserID);
                                }
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Debug.LogError("Error loading character data from Firestore: " + ex.Message);
                        }
                    });
            }
        }

       

    }
}