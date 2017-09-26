/*
*  Copyright (c) 2017 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*  
*/

using System.Windows.Threading;
using NUnit.Framework;

namespace com.vanderlande.wpf
{
    /// <summary>
    ///  Implementation of the code to test the PopupViewModelTest class
    /// </summary>
    [TestFixture]  
    public class PopupViewModelTest 
    {
        private const int stepSize = 100;

        #region Setup and TearDown
        /// <summary>
        ///  TestFixtureSetup can be used to place code that must be executed once
        ///  before the start of the tests in this test application
        /// </summary>
        [TestFixtureSetUp] 
        public void TestFixtureSetup()
        { 
        }

        /// <summary>
        /// Setup can be used for code that has to be executed before every test
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            DispatcherTimer.Reset();
        }
        
        /// <summary>
        /// TearDown is for code that has to be executed after every test
        /// </summary>
        [TearDown]
        public void TearDown()
        {
        }
        
        /// <summary>
        /// TestFixtureTearDown is for code that has to be executed after all tests
        /// have been executed
        /// </summary>
        [TestFixtureTearDown] 
        public void TestFixtureTearDown()
        { 
        }
        #endregion

        #region Test Methods
        [Test]  
        public void DefaultPopup()
        {
            PopupDummy sut = new PopupDummy();

            sut.OnCreated();
            Assert.IsTrue(sut.IsOpen);
            Assert.AreEqual(1500, sut.Duration);
            Assert.AreEqual(0, sut.Progress);
            DispatcherTimer.Kick();

            Assert.AreEqual(stepSize, sut.Progress);
            for (int i = 1; i < 14; ++i)
            {
                DispatcherTimer.Kick();
            }
            Assert.IsTrue(sut.IsOpen);
            Assert.AreEqual(14 * stepSize, sut.Progress);

            DispatcherTimer.Kick();
            Assert.IsFalse(sut.IsOpen);
            Assert.AreEqual(15 * stepSize, sut.Progress);
        }


        [Test]
        public void ForEverOpenPopup()
        {
            PopupDummy sut = new PopupDummy();

            sut.Duration = 0;
            sut.OnCreated();
            Assert.IsTrue(sut.IsOpen);
            Assert.AreEqual(0, sut.Duration);
            Assert.AreEqual(0, sut.Progress);
            DispatcherTimer.Kick();

            Assert.AreEqual(0, sut.Progress);
            for (int i = 1; i < 1000; ++i)
            {
                DispatcherTimer.Kick();
            }
            Assert.IsTrue(sut.IsOpen);
            Assert.AreEqual(0, sut.Progress);
        }

        
        [Test]
        public void AdjustDurationToSmallerValuePopup()
        {
            PopupDummy sut = new PopupDummy();

            sut.Duration = 5000;
            sut.OnCreated();
            Assert.IsTrue(sut.IsOpen);
            Assert.AreEqual(5000, sut.Duration);
            Assert.AreEqual(0, sut.Progress);
            DispatcherTimer.Kick();

            Assert.AreEqual(100, sut.Progress);
            for (int i = 1; i < 30; ++i)
            {
                DispatcherTimer.Kick();
            }
            Assert.IsTrue(sut.IsOpen);
            Assert.AreEqual(3000, sut.Progress);

            sut.Duration = 2000;
            Assert.IsFalse(sut.IsOpen);
        }


        [Test]
        public void AdjustDurationToLargerValuePopup()
        {
            PopupDummy sut = new PopupDummy();

            sut.Duration = 5000;
            sut.OnCreated();
            Assert.IsTrue(sut.IsOpen);
            Assert.AreEqual(5000, sut.Duration);
            Assert.AreEqual(0, sut.Progress);
            DispatcherTimer.Kick();

            Assert.AreEqual(100, sut.Progress);
            for (int i = 1; i < 30; ++i)
            {
                DispatcherTimer.Kick();
            }
            Assert.IsTrue(sut.IsOpen);
            Assert.AreEqual(3000, sut.Progress);

            sut.Duration = 6000;
            Assert.AreEqual(6000, sut.Duration);
            Assert.IsTrue(sut.IsOpen);
            for (int i = 1; i < 30; ++i)
            {
                DispatcherTimer.Kick();
            }
            Assert.AreEqual(5900, sut.Progress);

            DispatcherTimer.Kick();
            Assert.IsFalse(sut.IsOpen);
            Assert.AreEqual(6000, sut.Progress);
        }
        
        #endregion
		
    }  
}
