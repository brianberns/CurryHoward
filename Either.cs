using System;

namespace CurryHoward
{
    /// <summary>
    /// A container that holds either a "left" value or a "right" value, but not both.
    /// https://mikhail.io/2016/01/validation-with-either-data-type-in-csharp/
    /// </summary>
    class Either<TLeft, TRight>
    {
        /// <summary>
        /// Construct from a "left" value.
        /// </summary>
        public Either(TLeft left)
        {
            _left = left;
            _isLeft = true;
        }

        /// <summary>
        /// Implicit from a "left" value.
        /// </summary>
        public static implicit operator Either<TLeft, TRight>(TLeft left)
            => new Either<TLeft, TRight>(left);

        /// <summary>
        /// Construct from a "right" value.
        /// </summary>
        public Either(TRight right)
        {
            _right = right;
            _isLeft = false;
        }

        /// <summary>
        /// Implicit from a "right" value.
        /// </summary>
        public static implicit operator Either<TLeft, TRight>(TRight right)
            => new Either<TLeft, TRight>(right);

        /// <summary>
        /// Pattern matching in C#.
        /// </summary>
        public T Match<T>(
            Func<TLeft, T> leftFunc,
            Func<TRight, T> rightFunc)
            => _isLeft ? leftFunc(_left) : rightFunc(_right);

        private TLeft _left;
        private TRight _right;
        private bool _isLeft;
    }
}
