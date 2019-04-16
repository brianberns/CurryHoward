using System;

namespace CurryHoward
{
    static class Proofs
    {
        /// <summary>
        /// (A ∧ (A → B)) → B
        /// </summary>
        static B ModusPonens<A, B>(
            Func<A, B> A_implies_B,
            A proof_of_A)
            => A_implies_B(proof_of_A);

        /// <summary>
        /// (A ∧ B) → (B ∧ A)
        /// </summary>
        static (B, A) ConjunctionIsCommutative<A, B>(
            (A proof_of_A, B proof_of_B) A_and_B)
            => (A_and_B.proof_of_B, A_and_B.proof_of_A);

        /// <summary>
        /// (A → C) ∧ (B → C) ∧ (A ∨ B) → C
        /// </summary>
        static C DisjunctionElimination<A, B, C>(
            Func<A, C> A_implies_C,
            Func<B, C> B_implies_C,
            Either<A, B> A_or_B)
            => A_or_B.Match(
                a => A_implies_C(a),
                b => B_implies_C(b));

        /// <summary>
        /// (A → B) ∧ ¬B → ¬A
        /// </summary>
        static Not<A> ModusTollens<A, B>(
            Func<A, B> A_implies_B,
            Not<B> not_B)
        {
            Absurd f(A proof_of_A)
            {
                var proof_of_B = A_implies_B(proof_of_A);
                return not_B.Apply(proof_of_B);
            }
            return new Not<A>(f);
        }

        /// <summary>
        /// (¬A ∧ ¬B) → ¬(A ∨ B)
        /// </summary>
        static Not<Either<A, B>> DeMorgan1<A, B>(
            Not<A> not_A,
            Not<B> not_B)
        {
            Absurd f(Either<A, B> A_or_B)
            {
                return A_or_B.Match(
                    a => not_A.Apply(a),
                    b => not_B.Apply(b));
            }
            return new Not<Either<A, B>>(f);
        }

        /// <summary>
        /// ¬A ∨ ¬B → ¬(A ∧ B)
        /// </summary>
        static Not<(A, B)> DeMorgan2<A, B>(
            Either<Not<A>, Not<B>> not_A_or_not_B)
        {
            Absurd f((A proof_of_A, B proof_of_B) A_and_B)
            {
                return not_A_or_not_B.Match(
                    not_A => not_A.Apply(A_and_B.proof_of_A),
                    not_B => not_B.Apply(A_and_B.proof_of_B));
            }
            return new Not<(A, B)>(f);
        }

        /// <summary>
        /// A ∨ ~A
        /// </summary>
        static Either<A, Not<A>> ExcludedMiddle<A>()
        {
            throw new Exception("???");
        }

        /// <summary>
        /// A → ¬¬A
        /// </summary>
        static Not<Not<A>> DoubleNegation<A>(A proof_of_A)
        {
            Absurd f(Not<A> not_A)
            {
                return not_A.Apply(proof_of_A);
            }
            return new Not<Not<A>>(f);
        }
    }
}
