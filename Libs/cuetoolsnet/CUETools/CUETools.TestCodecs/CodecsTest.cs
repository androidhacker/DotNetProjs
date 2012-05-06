﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using CUETools.Codecs;
using System.IO;

namespace CUETools.TestCodecs
{
	/// <summary>
	///This is a test class for CUETools.Codecs.AudioBuffer and is intended
	///to contain all CUETools.Codecs.AudioBuffer Unit Tests
	///</summary>
	[TestClass()]
	public class AudioBufferTest
	{


		private TestContext testContextInstance;

		private int[,] testSamples = new int[,] { { 0, 1 }, { -2, -3 }, { 32767, 32766 }, { -32765, -32764 } };
		private byte[] testBytes = new byte[] { 0, 0, 1, 0, 254, 255, 253, 255, 255, 127, 254, 127, 3, 128, 4, 128 };

		private int[,] testSamples2 = new int[,] { { 0, 1 }, { -2, -3 }, { 32767, 32766 }, { -32765, -32764 }, { 42, 42 } };
		private byte[] testBytes2 = new byte[] { 0, 0, 1, 0, 254, 255, 253, 255, 255, 127, 254, 127, 3, 128, 4, 128, 42, 0, 42, 0 };

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}
		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for Bytes
		///</summary>
		[TestMethod()]
		public void BytesTest()
		{
			AudioBuffer target = new AudioBuffer(AudioPCMConfig.RedBook, 1);
			target.Prepare(testSamples, testSamples.GetLength(0));
			CollectionAssert.AreEqual(testBytes, target.Bytes, "CUETools.Codecs.AudioBuffer.Bytes was not set correctly.");
			target.Prepare(testSamples2, testSamples2.GetLength(0));
			CollectionAssert.AreEqual(testBytes2, target.Bytes, "CUETools.Codecs.AudioBuffer.Bytes was not set correctly.");
		}

		/// <summary>
		///A test for Samples
		///</summary>
		[TestMethod()]
		public void SamplesTest()
		{
			AudioBuffer target = new AudioBuffer(AudioPCMConfig.RedBook, 1);
			target.Prepare(testBytes, testSamples.GetLength(0));
			CollectionAssert.AreEqual(testSamples, target.Samples, "CUETools.Codecs.AudioBuffer.Samples was not set correctly.");
			target.Prepare(testBytes2, testSamples2.GetLength(0));
			CollectionAssert.AreEqual(testSamples2, target.Samples, "CUETools.Codecs.AudioBuffer.Samples was not set correctly.");
		}

        public static void AreEqual(AudioBuffer buff1, AudioBuffer buff2)
        {
            var bytes1 = new byte[buff1.ByteLength];
            var bytes2 = new byte[buff2.ByteLength];
            Array.Copy(buff1.Bytes, bytes1, buff1.ByteLength);
            Array.Copy(buff2.Bytes, bytes2, buff2.ByteLength);
            CollectionAssert.AreEqual(bytes1, bytes2);
        }
    }


	/// <summary>
	///This is a test class for CUETools.Codecs.WAVReader and is intended
	///to contain all CUETools.Codecs.WAVReader Unit Tests
	///</summary>
	[TestClass()]
	public class WAVReaderTest
	{


		private TestContext testContextInstance;
		private WAVReader pipe = null;
		private WAVReader wave = null;

		public readonly static int[,] pipeSamples = new int[,] { { -1, 1 }, { 0, -1 }, { -1, 0 }, { 0, -1 }, { -1, -1 }, { -1, -1 }, { 1, 0 }, { -3, -2 }, { 3, 1 }, { -4, -3 }, { 3, 2 }, { -4, -3 }, { 3, 1 }, { -3, -1 }, { 1, -1 }, { -1, 1 }, { -1, -3 }, { 0, 3 }, { -2, -4 }, { 0, 3 }, { -1, -4 }, { -1, 3 }, { 0, -3 }, { -2, 1 }, { 1, -1 }, { -2, -1 }, { 0, 1 }, { -1, -2 }, { -1, 1 }, { 1, -2 }, { -3, 1 }, { 3, -2 }, { -4, 0 }, { 3, -1 }, { -4, -1 }, { 3, -1 }, { -3, 0 }, { 1, -1 }, { -1, 1 }, { -1, -2 }, { 0, 2 }, { -1, -3 }, { 0, 2 }, { 0, -3 }, { -1, 1 }, { 1, -1 }, { -2, -1 }, { 2, 1 }, { -2, -3 }, { 1, 3 }, { -1, -4 }, { -1, 3 }, { 1, -4 }, { 0, 0 } };
		public readonly static int[,] testSamples = new int[,] { { -1, 1 }, { 0, -1 }, { -1, 0 }, { 0, -1 }, { -1, -1 }, { -1, -1 }, { 1, 0 }, { -3, -2 }, { 3, 1 }, { -4, -3 }, { 3, 2 }, { -4, -3 }, { 3, 1 }, { -3, -1 }, { 1, -1 }, { -1, 1 }, { -1, -3 }, { 0, 3 }, { -2, -4 }, { 0, 3 }, { -1, -4 }, { -1, 3 }, { 0, -3 }, { -2, 1 }, { 1, -1 }, { -2, -1 }, { 0, 1 }, { -1, -2 }, { -1, 1 }, { 1, -2 }, { -3, 1 }, { 3, -2 }, { -4, 0 }, { 3, -1 }, { -4, -1 }, { 3, -1 }, { -3, 0 }, { 1, -1 }, { -1, 1 }, { -1, -2 }, { 0, 2 }, { -1, -3 }, { 0, 2 }, { 0, -3 }, { -1, 1 }, { 1, -1 }, { -2, -1 }, { 2, 1 }, { -2, -3 }, { 1, 3 }, { -1, -4 }, { -1, 3 }, { 1, -4 }};

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}
		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//
		[ClassInitialize()]
		public static void MyClassInitialize(TestContext testContext)
		{
		}

		//
		//Use ClassCleanup to run code after all tests in a class have run
		//
		[ClassCleanup()]
		public static void MyClassCleanup()
		{
		}
		//
		//Use TestInitialize to run code before running each test
		//
		[TestInitialize()]
		public void MyTestInitialize()
		{
			pipe = new WAVReader("pipe.wav", null);
			wave = new WAVReader("test.wav", null);
		}
		//
		//Use TestCleanup to run code after each test has run
		//
		[TestCleanup()]
		public void MyTestCleanup()
		{
			pipe.Close();
			pipe = null;
			wave.Close();
			wave = null;
		}
		//
		#endregion

		private void DumpSamples(int[,] samples)
		{
			StringWriter sw = new StringWriter();
			for (int i = 0; i < samples.GetLength(0); i++)
				sw.Write(" {0}{1}, {2}{3},", "{", samples[i, 0], samples[i, 1], "}");
			TestContext.WriteLine("{0}", sw.ToString());
		}

		/// <summary>
		///A test for Read (AudioBuffer, int)
		///</summary>
		[TestMethod()]
		public void ReadTest()
		{
			Assert.AreEqual(-1L, pipe.Length, "CUETools.Codecs.WAVReader.Length did not return the expected value.");
			AudioBuffer buff = new AudioBuffer(pipe, 54);
			int actual = pipe.Read(buff, -1);
			Assert.AreEqual(53, actual, "CUETools.Codecs.WAVReader.Read did not return the expected value.");
			Assert.AreEqual(actual, buff.Length, "CUETools.Codecs.WAVReader.Read did not return the expected value.");
			CollectionAssert.AreEqual(pipeSamples, buff.Samples, "AudioBuffer.Samples was not set correctly.");
			actual = pipe.Read(buff, -1);
			Assert.AreEqual(0, actual, "CUETools.Codecs.WAVReader.Read did not return the expected value.");
		}


		/// <summary>
		///A test for BitsPerSample
		///</summary>
		[TestMethod()]
		public void BitsPerSampleTest()
		{
			Assert.AreEqual(16, pipe.PCM.BitsPerSample, "CUETools.Codecs.WAVReader.BitsPerSample was not set correctly.");
		}

		/// <summary>
		///A test for BlockAlign
		///</summary>
		[TestMethod()]
		public void BlockAlignTest()
		{
			Assert.AreEqual(4, pipe.PCM.BlockAlign, "CUETools.Codecs.WAVReader.BlockAlign was not set correctly.");
		}

		/// <summary>
		///A test for ChannelCount
		///</summary>
		[TestMethod()]
		public void ChannelCountTest()
		{
			Assert.AreEqual(2, pipe.PCM.ChannelCount, "CUETools.Codecs.WAVReader.ChannelCount was not set correctly.");
		}

		/// <summary>
		///A test for Length
		///</summary>
		[TestMethod()]
		public void LengthTest()
		{
			Assert.AreEqual(-1L, pipe.Length, "CUETools.Codecs.WAVReader.Length was not set correctly.");
			Assert.AreEqual(53L, wave.Length, "CUETools.Codecs.WAVReader.Length was not set correctly.");
		}

		/// <summary>
		///A test for SampleRate
		///</summary>
		[TestMethod()]
		public void SampleRateTest()
		{
			Assert.AreEqual(44100, pipe.PCM.SampleRate, "CUETools.Codecs.WAVReader.SampleRate was not set correctly.");
		}

		/// <summary>
		///A test for ReadAllSamples (string, Stream)
		///</summary>
		[TestMethod()]
		public void ReadAllSamplesTest()
		{
			AudioBuffer buff = WAVReader.ReadAllSamples("test.wav", null);
			CollectionAssert.AreEqual(testSamples, buff.Samples, "AudioBuffer.Samples was not set correctly.");
		}
	}
	/// <summary>
	///This is a test class for CUETools.Codecs.WAVWriter and is intended
	///to contain all CUETools.Codecs.WAVWriter Unit Tests
	///</summary>
	[TestClass()]
	public class WAVWriterTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}
		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for WAVWriter (string, int, int, int, Stream)
		///</summary>
		[TestMethod()]
		public void ConstructorTest()
		{
			AudioBuffer buff = WAVReader.ReadAllSamples("test.wav", null);
			WAVWriter target;

			target = new WAVWriter("wavwriter0.wav", null, buff.PCM);
			//target.FinalSampleCount = buff.Length;
			target.Write(buff);
			target.Close();
			CollectionAssert.AreEqual(File.ReadAllBytes("test.wav"), File.ReadAllBytes("wavwriter0.wav"), "wavwriter0.wav doesn't match.");

			target = new WAVWriter("wavwriter1.wav", null, buff.PCM);
			target.FinalSampleCount = buff.Length;
			target.Write(buff);
			target.Close();
			CollectionAssert.AreEqual(File.ReadAllBytes("test.wav"), File.ReadAllBytes("wavwriter1.wav"), "wavwriter1.wav doesn't match.");
		}

	}
}
