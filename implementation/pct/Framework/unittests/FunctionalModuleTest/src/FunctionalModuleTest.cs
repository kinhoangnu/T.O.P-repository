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

using System;
using System.IO;
using System.Windows.Threading;
using NUnit.Framework;

namespace com.vanderlande.wpf
{
    /// <summary>
    ///  Implementation of the code to test the FunctionalModuleTest class
    /// </summary>
    [TestFixture]  
    public class FunctionalModuleTest 
    {  
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
            DummyFunctionalModule.Reset();
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
        public void TestHappyFlowNoTimer()
        {
            DummyFunctionalModule sut = new DummyFunctionalModule(FunctionalModule.TimerRunMode.Never, new TimeSpan(0, 0, 0, 0));
            sut.DoOnIdle();
            Assert.AreEqual(1, DummyFunctionalModule.CalledOnIdle);
            Assert.IsTrue(sut.DoInitialize());
            Assert.AreEqual(1, DummyFunctionalModule.CalledInitialize);
            Assert.IsFalse(sut.DoCanDeActivate());
            Assert.AreEqual(0, DummyFunctionalModule.CalledCanDeActivate);
            Assert.IsFalse(sut.IsActive);
            Assert.IsTrue(sut.DoCanActivate());
            Assert.IsTrue(sut.DoActivate());
            Assert.IsTrue(sut.IsActive);
            Assert.AreEqual(1, DummyFunctionalModule.CalledCanActivate);
            Assert.AreEqual(1, DummyFunctionalModule.CalledActivate);
            Assert.IsFalse(sut.DoCanActivate());
            DispatcherTimer.Kick();
            Assert.AreEqual(0, DummyFunctionalModule.CalledOnTimer);
            Assert.IsTrue(sut.DoCanDeActivate());
            Assert.AreEqual(1, DummyFunctionalModule.CalledCanDeActivate);
            Assert.IsFalse(sut.DoCanDispose());
            Assert.AreEqual(0, DummyFunctionalModule.CalledCanDispose);
            Assert.IsTrue(sut.DoDeActivate());
            Assert.AreEqual(1, DummyFunctionalModule.CalledDeActivate);
            Assert.IsFalse(sut.IsActive);
            Assert.IsTrue(sut.DoCanDispose());
            Assert.AreEqual(1, DummyFunctionalModule.CalledCanDispose);
            Assert.IsTrue(sut.DoDispose());
            Assert.AreEqual(1, DummyFunctionalModule.CalledDispose);
            sut.DoOnIdle();
            Assert.AreEqual(2, DummyFunctionalModule.CalledOnIdle);
        }


        [Test]
        public void TestHappyFlowWithTimer()
        {
            DummyFunctionalModule sut = new DummyFunctionalModule(FunctionalModule.TimerRunMode.OnlyWhenActive, new TimeSpan(0,0,0,100));
            Assert.IsTrue(sut.DoInitialize());
            Assert.AreEqual(1, DummyFunctionalModule.CalledInitialize);
            Assert.IsFalse(sut.DoCanDeActivate());
            Assert.AreEqual(0, DummyFunctionalModule.CalledCanDeActivate);
            Assert.IsFalse(sut.IsActive);
            Assert.IsTrue(sut.DoCanActivate());
            DispatcherTimer.Kick();
            Assert.AreEqual(0, DummyFunctionalModule.CalledOnTimer);
            Assert.IsTrue(sut.DoActivate());
            Assert.IsTrue(sut.IsActive);
            Assert.AreEqual(1, DummyFunctionalModule.CalledCanActivate);
            Assert.AreEqual(1, DummyFunctionalModule.CalledActivate);
            Assert.IsFalse(sut.DoCanActivate());
            DispatcherTimer.Kick();
            Assert.AreEqual(1, DummyFunctionalModule.CalledOnTimer);
            Assert.IsTrue(sut.DoCanDeActivate());
            Assert.IsFalse(sut.DoCanDispose());
            Assert.AreEqual(0, DummyFunctionalModule.CalledCanDispose);
            Assert.IsTrue(sut.DoDeActivate());
            Assert.AreEqual(1, DummyFunctionalModule.CalledDeActivate);
            Assert.IsFalse(sut.IsActive);
            Assert.IsTrue(sut.DoCanDispose());
            Assert.AreEqual(1, DummyFunctionalModule.CalledCanDispose);
            Assert.IsTrue(sut.DoDispose());
            Assert.AreEqual(1, DummyFunctionalModule.CalledDispose);
        
        }

        [Test]
        public void TestExceptions()
        {
            DummyFunctionalModule.ThrowInitialize = true;
            DummyFunctionalModule.ThrowCanActivate = true;
            DummyFunctionalModule.ThrowActivate = true;
            DummyFunctionalModule.ThrowCanDeActivate = true;
            DummyFunctionalModule.ThrowDeActivate = true;
            DummyFunctionalModule.ThrowCanDispose = true;
            DummyFunctionalModule.ThrowDispose = true;
            DummyFunctionalModule.ThrowOnIdle = true;
            DummyFunctionalModule.ThrowOnTimer = true;

            DummyFunctionalModule sut = new DummyFunctionalModule(FunctionalModule.TimerRunMode.OnlyWhenActive, new TimeSpan(0, 0, 0, 100));
            sut.DoOnIdle();
            Assert.AreEqual(1, DummyFunctionalModule.CalledOnIdle);
            Assert.AreEqual("OnIdle", Logger.TheFunction);
            Assert.IsEmpty(Logger.TheFunction);

            Assert.IsFalse(sut.DoInitialize());
            Assert.AreEqual("Initialize", Logger.TheFunction);
            Assert.IsEmpty(Logger.TheFunction);
            DummyFunctionalModule.ThrowInitialize = false;
            Assert.IsTrue(sut.DoInitialize());
            Assert.IsEmpty(Logger.TheFunction);
            Assert.AreEqual(2, DummyFunctionalModule.CalledInitialize);

            Assert.IsFalse(sut.DoCanActivate());
            Assert.AreEqual("CanActivate", Logger.TheFunction);
            DummyFunctionalModule.ThrowCanActivate = false;
            Assert.IsTrue(sut.DoCanActivate());
            Assert.IsEmpty(Logger.TheFunction);
            Assert.AreEqual(2, DummyFunctionalModule.CalledCanActivate);

            Assert.IsFalse(sut.DoActivate());
            Assert.AreEqual("Activate", Logger.TheFunction);
            Assert.IsFalse(sut.IsActive);
            Assert.AreEqual(1, DummyFunctionalModule.CalledActivate);
            DummyFunctionalModule.ThrowActivate = false;
            Assert.IsTrue(sut.DoActivate());
            Assert.IsEmpty(Logger.TheFunction);
            Assert.AreEqual(2, DummyFunctionalModule.CalledActivate);

            DispatcherTimer.Kick();
            Assert.AreEqual(1, DummyFunctionalModule.CalledOnTimer);
            Assert.AreEqual("OnTimer", Logger.TheFunction);
            DummyFunctionalModule.ThrowOnTimer = false;
            DispatcherTimer.Kick();
            Assert.IsEmpty(Logger.TheFunction);
            Assert.AreEqual(2, DummyFunctionalModule.CalledOnTimer);

            Assert.IsFalse(sut.DoCanDeActivate());
            Assert.AreEqual("CanDeActivate", Logger.TheFunction);
            Assert.AreEqual(1, DummyFunctionalModule.CalledCanDeActivate);
            DummyFunctionalModule.ThrowCanDeActivate = false;
            Assert.IsTrue(sut.DoCanDeActivate());
            Assert.IsEmpty(Logger.TheFunction);
            Assert.AreEqual(2, DummyFunctionalModule.CalledCanDeActivate);

            Assert.IsFalse(sut.DoDeActivate());
            Assert.AreEqual("DeActivate", Logger.TheFunction);
            Assert.AreEqual(1, DummyFunctionalModule.CalledDeActivate);
            DummyFunctionalModule.ThrowDeActivate = false;
            Assert.IsTrue(sut.DoDeActivate());
            Assert.IsEmpty(Logger.TheFunction);
            Assert.AreEqual(2, DummyFunctionalModule.CalledDeActivate);

            Assert.IsFalse(sut.DoCanDispose());
            Assert.AreEqual("CanDispose", Logger.TheFunction);
            Assert.AreEqual(1, DummyFunctionalModule.CalledCanDispose);
            DummyFunctionalModule.ThrowCanDispose = false;
            Assert.IsTrue(sut.DoCanDispose());
            Assert.IsEmpty(Logger.TheFunction);
            Assert.AreEqual(2, DummyFunctionalModule.CalledCanDispose);

            Assert.IsFalse(sut.DoDispose());
            Assert.AreEqual("Dispose", Logger.TheFunction);
            Assert.AreEqual(1, DummyFunctionalModule.CalledDispose);
            DummyFunctionalModule.ThrowDispose = false;
            Assert.IsTrue(sut.DoDispose());
            Assert.IsEmpty(Logger.TheFunction);
            Assert.AreEqual(2, DummyFunctionalModule.CalledDispose);

        }
        #endregion

		
    }  
}
