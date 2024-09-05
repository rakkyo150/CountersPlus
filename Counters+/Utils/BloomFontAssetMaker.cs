using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace CountersPlus.Utils.Bloom_Font_Asset_Makers
{
    internal static class BloomFontAssetMaker
    {   
        private static TMP_FontAsset _bloomFontAsset = null;
        
        public static TMP_FontAsset BloomFontAsset()
        {
            if (_bloomFontAsset != null) return _bloomFontAsset;

            var original = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().FirstOrDefault(x => x.name == "Teko-Medium SDF");
            _bloomFontAsset = CopyFontAsset(original, "Teko-Medium SDF (Bloom)");
            _bloomFontAsset.material.shader = Resources.FindObjectsOfTypeAll<Shader>().First(x => x.name.Contains("TextMeshPro/Distance Field"));
            return _bloomFontAsset;
        }

        private static TMP_FontAsset CopyFontAsset(TMP_FontAsset original, string newName = "")
        {
            if (string.IsNullOrEmpty(newName))
            {
                newName = original.name;
            }

            TMP_FontAsset newFontAsset = GameObject.Instantiate(original);

            Texture2D texture = original.atlasTexture;

            Texture2D newTexture = new Texture2D(texture.width, texture.height, texture.format, texture.mipmapCount, true) { name = $"{newName} Atlas" };
            Graphics.CopyTexture(texture, newTexture);

            Material material = new Material(original.material) { name = $"{newName} Atlas Material" };
            material.SetTexture("_MainTex", newTexture);

            IPA.Utilities.FieldAccessor<TMP_FontAsset, Texture2D>.Set(ref newFontAsset, "m_AtlasTexture", newTexture);
            // newFontAsset.GetType().GetField("m_AtlasTexture", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(newFontAsset, newTexture);
            newFontAsset.name = newName;
            newFontAsset.atlasTextures = new[] { newTexture };
            newFontAsset.material = material;

            return newFontAsset;
        }
    }
}
