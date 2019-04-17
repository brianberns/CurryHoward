using System;

namespace CurryHoward
{
    static class Proofs
    {
        /// <summary>
        /// ((A → B) ∧ A) → B
        /// </summary>
        public static B ModusPonens<A, B>(
            Func<A, B> A_implies_B,
            A proof_of_A)
            => A_implies_B(proof_of_A);

        /// <summary>
        /// (A ∧ B) → (B ∧ A)
        /// </summary>
        public static (B, A) ConjunctionIsCommutative<A, B>(
            (A proof_of_A, B proof_of_B) A_and_B)
            => (A_and_B.proof_of_B, A_and_B.proof_of_A);

        /// <summary>
        /// (A ∧ B) ∧ (B ∧ C) → (A ∧ C)
        /// </summary>
        public static Func<A, C> Syllogism<A, B, C>(
            Func<A, B> A_implies_B,
            Func<B, C> B_implies_C)
        {
            C A_implies_C(A proof_of_A)
            {
                var proof_of_B = A_implies_B(proof_of_A);
                return B_implies_C(proof_of_B);
            }
            return new Func<A, C>(A_implies_C);
        }

        /// <summary>
        /// (A → C) ∧ (B → C) ∧ (A ∨ B) → C
        /// </summary>
        public static C DisjunctionElimination<A, B, C>(
            Func<A, C> A_implies_C,
            Func<B, C> B_implies_C,
            Either<A, B> A_or_B)
            => A_or_B.Match(
                a => A_implies_C(a),
                b => B_implies_C(b));

        /// <summary>
        /// (A → B) ∧ ¬B → ¬A
        /// </summary>
        public static Not<A> ModusTollens<A, B>(
            Func<A, B> A_implies_B,
            Not<B> not_B)
        {
            Absurd A_implies_absurd(A proof_of_A)
            {
                var proof_of_B = A_implies_B(proof_of_A);
                return not_B.Apply(proof_of_B);
            }
            return new Not<A>(A_implies_absurd);
        }

        /// <summary>
        /// (¬A ∧ ¬B) → ¬(A ∨ B)
        /// </summary>
        public static Not<Either<A, B>> DeMorgan1<A, B>(
            Not<A> not_A,
            Not<B> not_B)
        {
            Absurd A_or_B_implies_absurd(Either<A, B> A_or_B)
            {
                return A_or_B.Match(
                    a => not_A.Apply(a),
                    b => not_B.Apply(b));
            }
            return new Not<Either<A, B>>(A_or_B_implies_absurd);
        }

        /// <summary>
        /// ¬A ∨ ¬B → ¬(A ∧ B)
        /// </summary>
        public static Not<(A, B)> DeMorgan2<A, B>(
            Either<Not<A>, Not<B>> not_A_or_not_B)
        {
            Absurd A_and_B_implies_Absurd((A proof_of_A, B proof_of_B) A_and_B)
            {
                return not_A_or_not_B.Match(
                    not_A => not_A.Apply(A_and_B.proof_of_A),
                    not_B => not_B.Apply(A_and_B.proof_of_B));
            }
            return new Not<(A, B)>(A_and_B_implies_Absurd);
        }

        /// <summary>
        /// A ∨ ¬A
        /// </summary>
        public static Either<A, Not<A>> ExcludedMiddle<A>()
        {
            throw new Exception("???");
        }

        /// <summary>
        /// A → ¬¬A
        /// But can't show: ¬¬A → A
        /// </summary>
        public static Not<Not<A>> DoubleNegation<A>(A proof_of_A)
        {
            Absurd not_A_implies_absurd(Not<A> not_A)
                => not_A.Apply(proof_of_A);
            return new Not<Not<A>>(not_A_implies_absurd);
        }

        /// <summary>
        /// ¬¬(A ∨ ¬A)
        /// https://rawgit.com/iblech/talk-constructive-mathematics/master/negneg-translation.pdf, page 14
        /// </summary>
        public static Not<Not<Either<A, Not<A>>>> DoubleNegativeExcludedMiddle<A>()
        {
            Absurd not_ex_mid_implies_absurd(Not<Either<A, Not<A>>> not_ex_mid)
            {
                Absurd A_implies_absurd(A proof_of_A)
                {
                    Either<A, Not<A>> ex_mid_1 = proof_of_A;
                    return not_ex_mid.Apply(ex_mid_1);
                }
                var not_A = new Not<A>(A_implies_absurd);
                Either<A, Not<A>> ex_mid_2 = not_A;
                return not_ex_mid.Apply(ex_mid_2);
            }
            return new Not<Not<Either<A, Not<A>>>>(not_ex_mid_implies_absurd);
        }
    }
}
