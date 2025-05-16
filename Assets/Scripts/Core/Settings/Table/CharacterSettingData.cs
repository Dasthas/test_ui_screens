using System;
using UnityEngine;

namespace Core.Settings.Table
{
    [Serializable]
    public struct CharacterSettingData
    {
        public string CharacterName;
        public Sprite CharacterIcon;
        public Color BGColor;
    }
}