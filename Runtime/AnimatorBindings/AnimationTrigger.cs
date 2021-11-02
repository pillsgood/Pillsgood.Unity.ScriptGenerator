using System;
using UnityEngine;

namespace ScriptGenerator.Runtime.AnimatorBindings
{
    public class AnimationTrigger : IEquatable<AnimationTrigger>
    {
        private readonly Animator _animator;
        private readonly int _binding;

        public AnimationTrigger(Animator animator, int binding)
        {
            _animator = animator;
            _binding = binding;
        }

        public bool Equals(AnimationTrigger other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(_animator, other._animator) && _binding == other._binding;
        }

        public void Trigger()
        {
            _animator.SetTrigger(_binding);
        }

        public void Reset()
        {
            _animator.ResetTrigger(_binding);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AnimationTrigger) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_animator, _binding);
        }

        public static bool operator ==(AnimationTrigger left, AnimationTrigger right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AnimationTrigger left, AnimationTrigger right)
        {
            return !Equals(left, right);
        }
    }
}