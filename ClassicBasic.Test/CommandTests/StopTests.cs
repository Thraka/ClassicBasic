﻿// <copyright file="StopTests.cs" company="Peter Ibbotson">
// (C) Copyright 2017 Peter Ibbotson
// </copyright>

namespace ClassicBasic.Test.CommandTests
{
    using System.Collections.Generic;
    using ClassicBasic.Interpreter;
    using ClassicBasic.Interpreter.Commands;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Test the stop exception.
    /// </summary>
    [TestClass]
    public class StopTests
    {
        /// <summary>
        /// Check stop throws the correct exception and stores the
        /// correct position (after the end) in the continue token.
        /// </summary>
        [TestMethod]
        public void StopTest()
        {
            var mockRunEnvironment = new Mock<IRunEnvironment>();
            var programLine = new ProgramLine(30, new List<IToken> { new Token("Name"), new Token("1") });
            programLine.NextToken();
            mockRunEnvironment.Setup(mre => mre.CurrentLine).Returns(programLine);
            var sut = new Stop(mockRunEnvironment.Object);
            bool exceptionThrown = false;
            try
            {
                sut.Execute();
            }
            catch (ClassicBasic.Interpreter.Exceptions.BreakException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
            mockRunEnvironment.VerifySet(mre => mre.ContinueToken = 1);
        }
    }
}
