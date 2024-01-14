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

            _bloomFontAsset = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().FirstOrDefault(x => x.name == "Teko-Bold SDF");
            return _bloomFontAsset;
        }
    }
}
