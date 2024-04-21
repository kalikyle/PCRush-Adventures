using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Assets.PixelHeroes.Scripts.CharacterScrips;
using Assets.PixelHeroes.Scripts.CollectionScripts;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Firestore;
using static TreeEditor.TextureAtlas;
using System.Collections;
using System.Reflection;
using System.Drawing;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace Assets.PixelHeroes.Scripts.EditorScripts
{
    public class CharacterEditor : MonoBehaviour
    {
        public SpriteCollection SpriteCollection;
        public List<LayerEditor> Layers;
        public CharacterBuilder CharacterBuilder;
        public Sprite EmptyIcon;

        public TMP_InputField playerName;

        public static event Action<string> SliceTextureRequest = path => {};
        public static event Action<string> CreateSpriteLibraryRequest = path => { };

        public int HeadIndex = 0;
        public int BodyIndex = 0;
        public int HairIndex = 0;
        public int ArmorIndex = 0;
        public int HelmetIndex = 0;
        public int WeaponIndex = 0;
        public int ShieldIndex = 0;
        public int CapeIndex = 0;
        public int BackIndex = 0;

        public string HeadColor = "";
        public string BodyColor = "";
        public string HairColor = "";
        public string HSBArmor = "";

        public void Start()
        {


            if (GameManager.instance.UserID != "")
            {
                playerName.text = GameManager.instance.PlayerName;

                StartCoroutine(LoadIndexesWithDelay());
                CharacterBuilder.LoadSavedData();
            }

                foreach (var layer in Layers)
            {
                if (layer.Controls)
                {
                    layer.Content = SpriteCollection.Layers.Single(i => i.Name == layer.Name);
                    layer.Controls.Dropdown.options = new List<Dropdown.OptionData>();

                    if (layer.CanBeEmpty) layer.Controls.Dropdown.options.Add(new Dropdown.OptionData("Empty", EmptyIcon));

                    layer.Controls.Dropdown.options.AddRange(layer.Content.Textures.Select(i => new Dropdown.OptionData(Regex.Replace(i.name, "([a-z])([A-Z])", "$1 $2"), Sprite.Create(layer.Content.GetIcon(i), new Rect(0, 0, 16, 16), Vector2.one / 2, 100))));
                    layer.Controls.Dropdown.value = -1;
                    layer.Controls.Dropdown.value = layer.Index + (layer.CanBeEmpty ? 1 : 0);
                    layer.Controls.Dropdown.onValueChanged.AddListener(value => SetIndex(layer, value));
                    layer.Controls.Prev.onClick.AddListener(() => Switch(layer, -1));
                    layer.Controls.Next.onClick.AddListener(() => Switch(layer, +1));
                    layer.Controls.Hide.onClick.AddListener(() => Hide(layer));
                    layer.Controls.Paint.onClick.AddListener(() => Paint(layer));
                    layer.Controls.Hue.onValueChanged.AddListener(value => Rebuild(layer));
                    layer.Controls.Saturation.onValueChanged.AddListener(value => Rebuild(layer));
                    layer.Controls.Brightness.onValueChanged.AddListener(value => Rebuild(layer));
                    layer.Controls.OnSelectFixedColor = color => { layer.Color = color; if (layer.Name == "Body") Layers.Single(i => i.Name == "Head").Color = color; Rebuild(layer); };
                }
            }

            //Rebuild(null);
          



        }

        private void ChangesIndexes()
        {

           
            foreach (var layer in Layers)
            {
                if (layer.Controls)
                {
                    if(layer.Name == "Head")
                    {
                        layer.Controls.Dropdown.value = HeadIndex + (layer.CanBeEmpty ? 1 : 0);
                        
                        
                        //layer.Controls.Dropdown.onValueChanged.AddListener(HeadIndex => SetIndex(layer, HeadIndex));
                        //Rebuild(layer);
                    }
                    if (layer.Name == "Body")
                    {
                        layer.Controls.Dropdown.value = BodyIndex + (layer.CanBeEmpty ? 1 : 0);
                        layer.Color = ParseColorFromString(BodyColor);
                            Layers.Single(i => i.Name == "Head").Color = ParseColorFromString(BodyColor);
                        Rebuild(layer);
                        //layer.Controls.Dropdown.onValueChanged.AddListener(BodyIndex => SetIndex(layer, BodyIndex));
                        //Rebuild(layer);
                    }
                    if (layer.Name == "Armor")
                    {
                        string[] HSB = HSBArmor.Split(":");
                        layer.Controls.Dropdown.value = ArmorIndex + (layer.CanBeEmpty ? 1 : 0);
         
                        layer.Controls.Hue.value = int.Parse(HSB[0]);
                        layer.Controls.Saturation.value = int.Parse(HSB[1]);
                        layer.Controls.Brightness.value = int.Parse(HSB[2]);
                        Rebuild(layer);
                    }
                    if (layer.Name == "Hair")
                    {
                        layer.Controls.Dropdown.value = HairIndex + (layer.CanBeEmpty ? 1 : 0);
                        layer.Color = ParseColorFromString(HairColor);
                        Rebuild(layer);


                        // layer.Controls.Dropdown.onValueChanged.AddListener(value => SetIndex(layer, HairIndex));
                        //Rebuild(layer);
                    }
                }
            }
        }
        public UnityEngine.Color ParseColorFromString(string colorString)
        {
            UnityEngine.Color color;

            // Try parsing the color string
            if (ColorUtility.TryParseHtmlString(colorString, out color))
            {
                // Color parsing successful, return the color
                return color;
            }
            else
            {
                // Failed to parse color string, return a default color
                UnityEngine.Debug.LogError("Failed to parse color from string: " + colorString);
                return UnityEngine.Color.white; // You can choose a default color here
            }
        }

        private IEnumerator LoadIndexesWithDelay()
        {
            yield return new WaitForSeconds(.5f);

            try
            {
                string[] Headparts = CharacterBuilder.Head.Split('#');
                string HeadTextureName = Headparts[0];
                string HeadColorsParts = Headparts[1];

                string[] HColorParts = HeadColorsParts.Split('/');
                HeadColor = "#" + HColorParts[0];

                string[] Bodyparts = CharacterBuilder.Body.Split('#');
                string BodyTextureName = Bodyparts[0];
                string BodyColorsParts = Bodyparts[1];

                string[] BColorParts = BodyColorsParts.Split('/');
                BodyColor = "#" + BColorParts[0];


                string[] Hairparts = CharacterBuilder.Hair.Split('#');
                string HairTextureName = Hairparts[0];
                string HairColorsParts = Hairparts[1];

                string[] HairColorParts = HairColorsParts.Split('/');
                HairColor = "#" + HairColorParts[0];

                string[] Armorparts = CharacterBuilder.Armor.Split('#');
                string ArmorTextureName = Armorparts[0];
                string ArmorColorsParts = Armorparts[1];

                string[] ArmorColorParts = ArmorColorsParts.Split('/');
                HSBArmor = ArmorColorParts[1];

                string[] Helmetparts = CharacterBuilder.Helmet.Split('#');
                string HelmetTextureName = Helmetparts[0];

                string[] Weaponparts = CharacterBuilder.Weapon.Split('#');
                string WeaponTextureName = Weaponparts[0];

                string[] Shieldparts = CharacterBuilder.Shield.Split('#');
                string ShieldTextureName = Shieldparts[0];

                string[] Capeparts = CharacterBuilder.Cape.Split('#');
                string CapeTextureName = Capeparts[0];

                string[] Backparts = CharacterBuilder.Back.Split('#');
                string BackTextureName = Backparts[0];

                // Call the FindLayerIndex method after the delay
                HeadIndex = FindLayerIndex("Head", HeadTextureName);
                BodyIndex = FindLayerIndex("Body", BodyTextureName);
                HairIndex = FindLayerIndex("Hair", HairTextureName);
                ArmorIndex = FindLayerIndex("Armor", ArmorTextureName);
                HelmetIndex = FindLayerIndex("Helmet", HelmetTextureName);
                WeaponIndex = FindLayerIndex("Weapon", WeaponTextureName);
                ShieldIndex = FindLayerIndex("Shield", ShieldTextureName);
                CapeIndex = FindLayerIndex("Cape", CapeTextureName);
                BackIndex = FindLayerIndex("Back", BackTextureName);



                ChangesIndexes();
            }catch(Exception){}

        }


        public int FindLayerIndex(string layerName, string textureName)
        {
            int layerIndex = -1;
            for (int i = 0; i < SpriteCollection.Layers.Count; i++)
            {
                if (SpriteCollection.Layers[i].Name == layerName)
                {
                    layerIndex = i;
                    break;
                }
            }

            // If the layer with the specified name is found
            if (layerIndex != -1)
            {
                // Iterate through the textures of the specified layer
                var textures = SpriteCollection.Layers[layerIndex].Textures;
                for (int j = 0; j < textures.Count; j++)
                {
                    // Check if the name of the current texture matches the input textureName
                    if (textures[j].name == textureName)
                    {
                        // Return the index of the texture within the layer
                        return j;
                    }
                }
            }

            // Return -1 if the layerName or textureName is not found in the SpriteCollection
            return -1;
        }

        // Other methods and code...
    


    public void Rebuild()
        {
            Rebuild(null);
           

        }

        public void Hide(LayerEditor layer)
        {
            layer.Hidden = !layer.Hidden;
            Rebuild(layer);
        }

        public void Paint(LayerEditor layer)
        {
            #if UNITY_EDITOR

            ColorPicker.Open(layer.Color);
            ColorPicker.OnColorPicked = color =>
            {
                layer.Color = color;
                Rebuild(layer);
            };

            #endif
        }

        private void Switch(LayerEditor layer, int direction)
        {
            layer.Switch(direction);
            Rebuild(layer);
        }

        private void SetIndex(LayerEditor layer, int index)
        {
            if (layer.CanBeEmpty) index--;

            layer.SetIndex(index);

            if (layer.Name == "Body")
            {
                Layers.Single(i => i.Name == "Head").SetIndex(index);
            }

            Rebuild(layer);
        }

        private void Rebuild(LayerEditor layer)
        {
            var layers = Layers.ToDictionary(i => i.Name, i => i.SpriteData);

            CharacterBuilder.Head = layers["Head"];
            CharacterBuilder.Body = layers["Body"];
            CharacterBuilder.Hair = layers["Hair"];
            CharacterBuilder.Armor = layers["Armor"];
            CharacterBuilder.Helmet = layers["Helmet"];
            CharacterBuilder.Weapon = layers["Weapon"];
            CharacterBuilder.Shield = layers["Shield"];
            CharacterBuilder.Cape = layers["Cape"];
            CharacterBuilder.Back = layers["Back"];
            CharacterBuilder.Rebuild(layer?.Name);

            
        }

        


        // Rebuild the character with loaded data

        public void UnloadThisScene()
        {
            
            SceneManager.UnloadSceneAsync(1);
            GameManager.instance.LoadCharacter();
            GameManager.instance.SaveCharInfo(GameManager.instance.UserID, playerName.text);
            GameManager.instance.UIExplore.SetActive(true);
            //SceneManager.LoadSceneAsync(0);
        }

       


#if UNITY_EDITOR

        public void Save()
        {
            var path = EditorUtility.SaveFilePanel("Save as PNG", Application.dataPath, "SpriteSheet.png", "png");

            if (path == "") return;

            File.WriteAllBytes(path, CharacterBuilder.Texture.EncodeToPNG());
            
            if (path.StartsWith(Application.dataPath))
            {
                path = "Assets" + path.Substring(Application.dataPath.Length);
                AssetDatabase.Refresh();
                SliceTextureRequest(path);

                if (EditorUtility.DisplayDialog("Success", $"Texture saved and sliced:\n{path}\n\nDo you want to create Sprite Library Asset for it?", "Yes", "No"))
                {
                    CreateSpriteLibraryRequest(path);
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Success", $"Texture saved:\n{path}\n\nTip: textures are automatically sliced when saving to Assets.", "OK");
            }
        }

        #endif
    }
}