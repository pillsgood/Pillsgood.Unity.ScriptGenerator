#if U2D_ANIMATION
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace ScriptGenerator.Runtime.SpriteLibraryBindings
{
    public abstract class SpriteLibCategory : IEnumerable<Sprite>
    {
        private readonly string[] _labels;
        public readonly string categoryName;
        public readonly SpriteLibraryAsset libraryAsset;

        public SpriteLibCategory(string categoryName, SpriteLibraryAsset libraryAsset)
        {
            this.categoryName = categoryName;
            this.libraryAsset = libraryAsset;
            _labels = this.libraryAsset.GetCategoryLabelNames(this.categoryName).ToArray();
        }

        public int Count => _labels.Length;

        public Sprite this[int index] => libraryAsset.GetSprite(categoryName, _labels[index]);

        public IEnumerator<Sprite> GetEnumerator()
        {
            for (var i = 0; i < Count; i++) yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#endif