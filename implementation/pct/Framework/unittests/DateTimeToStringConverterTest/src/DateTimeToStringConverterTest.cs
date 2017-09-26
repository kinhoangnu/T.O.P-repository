/*
*  Copyright (c) 2016 Vanderlande Industries
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
using NUnit.Framework;

namespace com.vanderlande.wpf
{
    /// <summary>
    ///  Implementation of the code to test the DateTimeToStringConverterTest class
    /// </summary>
    [TestFixture]  
    public class DateTimeToStringConverterTest 
    {  
        #region Setup and TearDown
        /// <summary>
        ///  TestFixtureSetup can be used to place code that must be executed once
        ///  before the start of the tests in this test application
        /// </summary>
        [TestFixtureSetUp] 
        public void TestFixtureSetup()
        { 
            // Add code here that needs to be executed once prior to executing
            // any of the tests in the fixture
        }

        /// <summary>
        /// Setup can be used for code that has to be executed before every test
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // Add code here that needs to be executed just before each test 
            // method is called
        }
        
        /// <summary>
        /// TearDown is for code that has to be executed after every test
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            // Add code here that needs to be executed after each test method 
            // is run
        }
        
        /// <summary>
        /// TestFixtureTearDown is for code that has to be executed after all tests
        /// have been executed
        /// </summary>
        [TestFixtureTearDown] 
        public void TestFixtureTearDown()
        { 
            // Add code here that needs to performed once after all tests in 
            // the fixture are executed
        }
        #endregion

        #region Test Methods
        [Test]  
        public void TestNormalDateTimeConverter()
        {
            DateTimeToStringConverter conv = new DateTimeToStringConverter();
            DateTimeToStringConverter.TimeZone = TimeZoneInfo.Local;
            DateTime dt = new DateTime(2016, 1, 2, 13, 14, 15, 16, DateTimeKind.Local);
            string result = conv.Convert(dt, typeof(string), null, null) as string;
            Assert.That(result, Is.EqualTo("02-01-2016 13:14:15"));
        }

        [Test]
        public void TestWithMilliseconds()
        {
            DateTimeToStringConverter conv = new DateTimeToStringConverter();
            DateTimeToStringConverter.TimeZone = TimeZoneInfo.Local;
            DateTimeToStringConverter.AddMilliseconds = true;
            DateTime dt = new DateTime(2016, 1, 2, 13, 14, 15, 16, DateTimeKind.Local);
            string result = conv.Convert(dt, typeof(string), null, null) as string;
            Assert.That(result, Is.EqualTo("02-01-2016 13:14:15.016"));
        }

        [Test]
        public void TestTokyoTime()
        {
            DateTimeToStringConverter conv = new DateTimeToStringConverter();
            DateTimeToStringConverter.TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            DateTime dt = new DateTime(2016, 1, 2, 13, 14, 15, 16, DateTimeKind.Local);
            string result = conv.Convert(dt, typeof(string), null, null) as string;
            Assert.That(result, Is.EqualTo("02-01-2016 21:14:15"));
        }

        [Test]
        public void TestCentralStandardTime()
        {
            DateTimeToStringConverter conv = new DateTimeToStringConverter();
            DateTimeToStringConverter.TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            DateTime dt = new DateTime(2016, 1, 2, 13, 14, 15, 16, DateTimeKind.Local);
            string result = conv.Convert(dt, typeof(string), null, null) as string;
            Assert.That(result, Is.EqualTo("02-01-2016 6:14:15"));
        }
        
        #endregion

		
    }  
}