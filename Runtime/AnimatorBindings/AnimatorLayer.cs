using System;
using UnityEngine;

namespace ScriptGenerator.Runtime.AnimatorBindings
{
    public class AnimatorLayer : IEquatable<AnimatorLayer>
    {
        private readonly Animator _animator;
        private readonly int _layerIndex;

        public AnimatorLayer(Animator animator, int layerIndex)
        {
            _animator = animator;
            _layerIndex = layerIndex;
        }

        public float Weight
        {
            get => _animator.GetLayerWeight(_layerIndex);
            set => _animator.SetLayerWeight(_layerIndex, value);
        }

        public bool Equals(AnimatorLayer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(_animator, other._animator) && _layerIndex == other._layerIndex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AnimatorLayer) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_animator, _layerIndex);
        }

        public static bool operator ==(AnimatorLayer left, AnimatorLayer right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AnimatorLayer left, AnimatorLayer right)
        {
            return !Equals(left, right);
        }
    }
}