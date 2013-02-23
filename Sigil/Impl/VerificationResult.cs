﻿using System;
using System.Collections.Generic;

namespace Sigil.Impl
{
    internal class VerificationResult
    {
        public bool Success { get; private set; }
        public Stack<IEnumerable<TypeOnStack>> Stack { get; private set; }
        public int StackSize { get { return Stack.Count; } }

        // Set when the stack is underflowed
        public bool IsStackUnderflow { get; private set; }
        public int ExpectedStackSize { get; private set; }

        // Set when stacks don't match during an incoming
        public bool IsStackMismatch { get; private set; }
        public Stack<IEnumerable<TypeOnStack>> ExpectedStack { get; private set; }
        public Stack<IEnumerable<TypeOnStack>> IncomingStack { get; private set; }

        // Set when types are dodge
        public bool IsTypeMismatch { get; private set; }
        public int TransitionIndex { get; private set; }
        public int StackIndex { get; private set; }
        public IEnumerable<TypeOnStack> ExpectedAtStackIndex { get; private set; }

        // Set when the stack was expected to be a certain size, but it wasn't
        public bool IsStackSizeFailure { get; private set; }

        public static VerificationResult Successful(Stack<IEnumerable<TypeOnStack>> stack)
        {
            return new VerificationResult { Success = true, Stack = stack };
        }

        public static VerificationResult FailureUnderflow(int expectedSize, Stack<IEnumerable<TypeOnStack>> stack)
        {
            return
                new VerificationResult
                {
                    Success = false,

                    IsStackUnderflow = true,
                    ExpectedStackSize = expectedSize,
                    Stack = stack
                };
        }

        public static VerificationResult FailureStackMismatch(Stack<IEnumerable<TypeOnStack>> expected, Stack<IEnumerable<TypeOnStack>> incoming)
        {
            return
                new VerificationResult
                {
                    Success = false,

                    IsStackMismatch = true,
                    ExpectedStack = expected,
                    IncomingStack = incoming
                };
        }

        public static VerificationResult FailureTypeMismatch(int transitionIndex, int stackIndex, IEnumerable<TypeOnStack> expectedTypes, Stack<IEnumerable<TypeOnStack>> stack)
        {
            return
                new VerificationResult
                {
                    Success = false,

                    IsTypeMismatch = true,
                    TransitionIndex = transitionIndex,
                    StackIndex = stackIndex,
                    ExpectedAtStackIndex = expectedTypes,
                    Stack = stack
                };
        }

        public static VerificationResult FailureStackSize(int expectedSize)
        {
            return
                new VerificationResult
                {
                    Success = false,

                    IsStackSizeFailure = true,
                    ExpectedStackSize = expectedSize
                };
        }
    }
}
