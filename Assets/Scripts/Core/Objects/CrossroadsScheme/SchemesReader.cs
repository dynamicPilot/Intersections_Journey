using IJ.Core.Menus.Main.Levels;
using System.Collections.Generic;
using UnityEngine;

namespace IJ.Core.Objects.Schemes
{
    public class SchemesReader
    {
        private CrossroadsSchemes _schemesCollection;
        private IDictionary<LevelsPanelUI.CROSS, Sprite> _schemas = 
            new Dictionary<LevelsPanelUI.CROSS, Sprite>();

        public SchemesReader(CrossroadsSchemes schemesCollection)
        {
            _schemesCollection = schemesCollection;
            CreateDictionary();

        }
        void CreateDictionary()
        {
            _schemas[LevelsPanelUI.CROSS.tCross] = _schemesCollection._tCross;
            _schemas[LevelsPanelUI.CROSS.cross] = _schemesCollection._cross;
            _schemas[LevelsPanelUI.CROSS.doubleCross] = _schemesCollection._doubleCross;
            _schemas[LevelsPanelUI.CROSS.cross3222] = _schemesCollection._cross3222;
        }

        public Sprite GetSprite(LevelsPanelUI.CROSS type)
        {
            if (!_schemas.ContainsKey(type)) return _schemas[LevelsPanelUI.CROSS.cross];
            return _schemas[type];
        }

    }
}
