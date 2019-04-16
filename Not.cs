using System;

namespace CurryHoward
{
    /// <summary>
    /// ⊥
    /// </summary>
    enum Absurd
    {
    }

    /// <summary>
    /// A → ⊥
    /// </summary>
    class Not<A>
    {
        public Not(Func<A, Absurd> A_implies_absurd)
        {
            _func = A_implies_absurd;
        }

        public Absurd Apply(A a)
            => _func(a);

        private Func<A, Absurd> _func;
    }
}
