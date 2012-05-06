﻿using System;
using System.Linq;
using System.Reflection;
using CLAP;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Execute_Verb()
        {
            var printer = new Printer();
            var sample = new Sample_02 { Printer = printer };

            Parser.Run(new[]
            {
                "print",
                "/c=5",
                "/msg=test",
                "/prefix=hello_",
            }, sample);

            Assert.AreEqual(5, printer.PrintedTexts.Count);
            Assert.IsTrue(printer.PrintedTexts.All(t => t.Equals("hello_test")));
        }

        [Test]
        public void Execute_DefaultVerb()
        {
            var printer = new Printer();
            var sample = new Sample_02 { Printer = printer };

            Parser.Run(new[]
            {
                "/c=5",
                "/msg=test",
                "/prefix=hello_",
            }, sample);

            Assert.AreEqual(5, printer.PrintedTexts.Count);
            Assert.IsTrue(printer.PrintedTexts.All(t => t.Equals("hello_test")));
        }

        [Test]
        public void Execute_DefaultVerbWithDefaultValue()
        {
            var printer = new Printer();
            var sample = new Sample_02_Default { Printer = printer };

            Parser.Run(new string[]
            {
            }, sample);

            Assert.AreEqual("bar", printer.PrintedTexts[0]);
        }

        [Test]
        public void Execute_DefaultVerbWithoutDefaultValue()
        {
            var printer = new Printer();
            var sample = new Sample_02_No_Default { Printer = printer };

            Parser.Run(new string[]
            {
            }, sample);

            Assert.AreEqual(null, printer.PrintedTexts[0]);
        }

        [Test]
        public void Execute_DefaultVerb_Switch()
        {
            var printer = new Printer();
            var sample = new Sample_02 { Printer = printer };

            Parser.Run(new[]
            {
                "/c=5",
                "/msg=test",
                "/prefix=hello_",
                "/u",
            }, sample);

            Assert.AreEqual(5, printer.PrintedTexts.Count);
            Assert.IsTrue(printer.PrintedTexts.All(t => t.Equals("HELLO_TEST")));
        }

        [Test]
        public void Execute_WithEnum()
        {
            var printer = new Printer();
            var sample = new Sample_04 { Printer = printer };

            Parser.Run(new[]
            {
                "/count=5",
                "/msg=test",
                "/prefix=hello_",
                "/c:Upper",
            }, sample);

            Assert.AreEqual(5, printer.PrintedTexts.Count);
            Assert.IsTrue(printer.PrintedTexts.All(t => t.Equals("HELLO_TEST")));
        }

        [Test]
        public void Execute_WithDefaultEnum()
        {
            var printer = new Printer();
            var sample = new Sample_06 { Printer = printer };

            Parser.Run(new[]
            {
                "/count=5",
                "/msg=test",
                "/prefix=hello_",
            }, sample);

            Assert.AreEqual(5, printer.PrintedTexts.Count);
            Assert.IsTrue(printer.PrintedTexts.All(t => t.Equals("hello_test")));
        }

        [Test]
        public void Execute_DuplicateParameterNames()
        {
            try
            {
                var printer = new Printer();
                var sample = new Sample_05 { Printer = printer };

                Parser.Run(new[]
                {
                    "p",
                }, sample);

                Assert.Fail();
            }
            catch (InvalidOperationException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Duplicate parameter names found in Print: c, x"));
            }
        }

        [Test]
        [ExpectedException(typeof(MissingArgumentValueException))]
        public void Execute_NoParameterValue()
        {
            var printer = new Printer();
            var sample = new Sample_02 { Printer = printer };

            Parser.Run(new[]
            {
                "print",
                "/c=5",
                "/msg=",
                "/prefix=hello_",
            }, sample);
        }

        [Test]
        public void Validation_MoreThan()
        {
            var sample = new ValidationSample_01();
            Parser.Run(new[]
            {
                "morethan5",
                "/n=10",
            }, sample);

            try
            {
                Parser.Run(new[]
                {
                    "morethan5",
                    "/n=1",
                }, sample);

                Assert.Fail();
            }
            catch (ValidationException)
            {
            }
        }

        [Test]
        public void Validation_MoreOrEqualTo()
        {
            var sample = new ValidationSample_01();

            Parser.Run(new[]
            {
                "moreorequalto10",
                "/n=10",
            }, sample);

            Parser.Run(new[]
            {
                "moreorequalto10",
                "/n=11",
            }, sample);

            try
            {
                Parser.Run(new[]
                {
                    "moreorequalto10",
                    "/n=9",
                }, sample);

                Assert.Fail();
            }
            catch (ValidationException)
            {
            }
        }

        [Test]
        public void Validation_LessThan()
        {
            var sample = new ValidationSample_01();

            Parser.Run(new[]
            {
                "lessthan20",
                "/n=10",
            }, sample);

            try
            {
                Parser.Run(new[]
                {
                    "lessthan20",
                    "/n=20",
                }, sample);

                Assert.Fail();
            }
            catch (ValidationException)
            {
            }
        }

        [Test]
        public void Validation_LessOrEqualTo()
        {
            var sample = new ValidationSample_01();

            Parser.Run(new[]
            {
                "lessorequalto30",
                "/n=10",
            }, sample);

            Parser.Run(new[]
            {
                "lessorequalto30",
                "/n=30",
            }, sample);

            try
            {
                Parser.Run(new[]
                {
                    "lessorequalto30",
                    "/n=40",
                }, sample);

                Assert.Fail();
            }
            catch (ValidationException)
            {
            }
        }

        [Test]
        public void Validation_RegexMatchesEmail()
        {
            var sample = new ValidationSample_01();

            Parser.Run(new[]
            {
                "regexmatchesemail",
                "/text=name@email.com",
            }, sample);

            Parser.Run(new[]
            {
                "regexmatchesemail",
                "/text=more@some.email.co.il",
            }, sample);

            try
            {
                Parser.Run(new[]
                {
                    "regexmatchesemail",
                    "/text=no_email",
                }, sample);

                Assert.Fail();
            }
            catch (ValidationException)
            {
            }
        }

        [Test]
        public void Required()
        {
            var printer = new Printer();
            var sample = new Sample_07 { Printer = printer };

            Parser.Run(new[]
            {
                "print",
                "/prefix:hello",
            }, sample);

            Assert.AreEqual(1, printer.PrintedTexts.Count);
            Assert.AreEqual("HELLO", printer.PrintedTexts[0]);
        }

        [Test]
        public void Required_Exception()
        {
            try
            {
                var printer = new Printer();
                var sample = new Sample_07 { Printer = printer };

                Parser.Run(new[]
                {
                    "print",
                    "/message:world",
                }, sample);

                Assert.Fail();
            }
            catch (CommandLineParserException)
            {
            }
        }

        [Test]
        public void Array_Strings()
        {
            var printer = new Printer();
            var sample = new Sample_08 { Printer = printer };

            // array
            //
            Parser.Run(new[]
            {
                "print",
                "/messages:a,b,c",
                "/prefix:test_",
            }, sample);

            Assert.AreEqual(3, printer.PrintedTexts.Count);
            Assert.AreEqual("test_a", printer.PrintedTexts[0]);
            Assert.AreEqual("test_b", printer.PrintedTexts[1]);
            Assert.AreEqual("test_c", printer.PrintedTexts[2]);

            // JSON
            //
            Parser.Run(new[]
            {
                "print",
                "/messages:['a','b','c']",
                "/prefix:test_"
            }, sample);

            Assert.AreEqual(6, printer.PrintedTexts.Count);
            Assert.AreEqual("test_a", printer.PrintedTexts[3]);
            Assert.AreEqual("test_b", printer.PrintedTexts[4]);
            Assert.AreEqual("test_c", printer.PrintedTexts[5]);
        }

        [Test]
        public void Array_Strings_WithSeparator()
        {
            var printer = new Printer();
            var sample = new Sample_69 { Printer = printer };

            // array
            //
            Parser.Run(new[]
            {
                "print",
                "/messages:a|b|c",
                "/prefix:test_",
            }, sample);

            Assert.AreEqual(3, printer.PrintedTexts.Count);
            Assert.AreEqual("test_a", printer.PrintedTexts[0]);
            Assert.AreEqual("test_b", printer.PrintedTexts[1]);
            Assert.AreEqual("test_c", printer.PrintedTexts[2]);

            sample.Printer.Reset();

            Parser.Run(new[]
            {
                "printnumbers",
                "/numbers:1-78-100",
                "/prefix:test_",
            }, sample);

            Assert.AreEqual(3, printer.PrintedTexts.Count);
            Assert.AreEqual("test_1", printer.PrintedTexts[0]);
            Assert.AreEqual("test_78", printer.PrintedTexts[1]);
            Assert.AreEqual("test_100", printer.PrintedTexts[2]);
        }

        [Test]
        public void Array_Strings_NoInput()
        {
            var printer = new Printer();
            var sample = new Sample_08 { Printer = printer };

            Parser.Run(new[]
            {
                "print",
                "/prefix:test_",
            }, sample);

            Assert.AreEqual(0, printer.PrintedTexts.Count);
        }

        [Test]
        public void Array_Numbers()
        {
            var printer = new Printer();
            var sample = new Sample_08 { Printer = printer };

            // array
            //
            Parser.Run(new[]
            {
                "printnumbers",
                "/numbers:1,2,3",
                "/prefix:test_",
            }, sample);

            Assert.AreEqual(3, printer.PrintedTexts.Count);
            Assert.AreEqual("test_1", printer.PrintedTexts[0]);
            Assert.AreEqual("test_2", printer.PrintedTexts[1]);
            Assert.AreEqual("test_3", printer.PrintedTexts[2]);

            // JSON
            //
            Parser.Run(new[]
            {
                "printnumbers",
                "/numbers:[1,2,3]",
                "/prefix:test_",
            }, sample);

            Assert.AreEqual(6, printer.PrintedTexts.Count);
            Assert.AreEqual("test_1", printer.PrintedTexts[3]);
            Assert.AreEqual("test_2", printer.PrintedTexts[4]);
            Assert.AreEqual("test_3", printer.PrintedTexts[5]);
        }

        [Test]
        public void Array_Numbers_NoInput()
        {
            var printer = new Printer();
            var sample = new Sample_08 { Printer = printer };

            Parser.Run(new[]
            {
                "printnumbers",
                "/prefix:test_",
            }, sample);

            Assert.AreEqual(0, printer.PrintedTexts.Count);
        }

        [Test]
        public void Array_Enum()
        {
            var printer = new Printer();
            var sample = new Sample_08 { Printer = printer };

            // using array convertion
            //
            Parser.Run(new[]
            {
                "printenums",
                "/enums:Upper,Lower",
                "/prefix:test_",
            }, sample);

            Assert.AreEqual(2, printer.PrintedTexts.Count);
            Assert.AreEqual("test_Upper", printer.PrintedTexts[0]);
            Assert.AreEqual("test_Lower", printer.PrintedTexts[1]);

            // using JSON deserialization
            //
            Parser.Run(new[]
            {
                "printenums",
                "/enums:['Upper','Lower']",
                "/prefix:test_",
            }, sample);

            Assert.AreEqual(4, printer.PrintedTexts.Count);
            Assert.AreEqual("test_Upper", printer.PrintedTexts[2]);
            Assert.AreEqual("test_Lower", printer.PrintedTexts[3]);
        }

        [Test]
        public void Array_Enum_NoInput()
        {
            var printer = new Printer();
            var sample = new Sample_08 { Printer = printer };

            Parser.Run(new[]
            {
                "printenums",
                "/prefix:test_",
            }, sample);

            Assert.AreEqual(0, printer.PrintedTexts.Count);
        }

        [Test]
        public void RegisterParameterHandler_CallsTheHandler_IgnoreTheValue()
        {
            var printer = new Printer();
            var sample = new Sample_02 { Printer = printer };

            int x = 0;

            var p = new Parser<Sample_02>();

            // with and without description for coverage
            //
            p.Register.ParameterHandler("dec,d", delegate { x--; });
            p.Register.ParameterHandler("inc", delegate { x++; }, "description");

            p.Run("print /c=5 /msg=test /prefix=hello_ /inc".Split(' '), sample);

            Assert.AreEqual(1, x);

            p.Run("print /c=5 /msg=test /prefix=hello_ /dec".Split(' '), sample);
            p.Run("print /c=5 /msg=test /prefix=hello_ /d".Split(' '), sample);

            Assert.AreEqual(-1, x);
        }

        [Test]
        public void RegisterParameterHandler_CallsTheHandler_UseTheValue()
        {
            var printer = new Printer();
            var sample = new Sample_02 { Printer = printer };

            var debug = String.Empty;

            var p = new Parser<Sample_02>();
            p.Register.ParameterHandler<string>("debug", str => debug = str);

            p.Run("print /c=5 /msg=test /prefix=hello_ /debug=true".Split(' '), sample);

            Assert.AreEqual("true", debug);
        }

        [Test]
        public void Global_Defined_WithArg()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_10 { Printer = mock.Object };

            var p = new Parser<Sample_10>();

            p.Run("print -foo:blah".Split(' '), sample);

            mock.Verify(o => o.Print("blah"));
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void Global_Defined_MoreThanOneParameter_Exception()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_12 { Printer = mock.Object };

            var p = new Parser<Sample_12>();

            p.Run("print -foo".Split(' '), sample);
        }

        [Test]
        public void Global_Defined_NoArgs()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_10 { Printer = mock.Object };

            var p = new Parser<Sample_10>();

            p.Run("print -bar".Split(' '), sample);

            mock.Verify(o => o.Print("zoo"));
        }

        [Test]
        public void Global_Defined_WithValidation()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_10 { Printer = mock.Object };

            var p = new Parser<Sample_10>();

            p.Run("print -mish:5".Split(' '), sample);

            mock.Verify(o => o.Print("mesh"), Times.Exactly(5));
        }

        [Test]
        [ExpectedException(typeof(TypeConvertionException))]
        public void Global_Defined_BadConvertion()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_10 { Printer = mock.Object };

            var p = new Parser<Sample_10>();

            p.Run("print -abra1:cadabra".Split(' '), sample);
        }

        [Test]
        public void Help_Defined()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_10 { Printer = mock.Object };

            var p = new Parser<Sample_10>();

            p.Run("-showhelp".Split(' '), sample);
            p.Run("showhelp".Split(' '), sample);

            mock.Verify(o => o.Print("help"), Times.Exactly(2));
        }

        [Test]
        public void Help_Registered()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_11 { Printer = mock.Object };

            var p = new Parser<Sample_11>();

            p.Register.HelpHandler("help", s => sample.Print());

            p.Run("-help".Split(' '), sample);
            p.Run("help".Split(' '), sample);

            mock.Verify(o => o.Print("x"), Times.Exactly(2));
        }

        [Test]
        public void EmptyHandler_NonStatic_CalledWhenNoArgs()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_09 { Printer = mock.Object };

            var p = new Parser<Sample_09>();

            p.Run(new string[] { }, sample);

            mock.Verify(o => o.Print("a"));
        }

        [Test]
        public void EmptyHandler_Static_CalledWhenNoArgs()
        {
            var mock = new Mock<IPrinter>();

            Sample_16.StaticPrinter = mock.Object;

            var p = new Parser<Sample_16>();

            p.RunStatic(new string[] { });

            mock.Verify(o => o.Print("a"));

            Sample_16.StaticPrinter = null;
        }

        [Test]
        [ExpectedException(typeof(ArgumentMismatchException))]
        public void EmptyDefined_WithParameters_Exception()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_20 { Printer = mock.Object };

            var p = new Parser<Sample_20>();

            p.Run(new string[] { }, sample);
        }

        [Test]
        public void Empty_Registered()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_11 { Printer = mock.Object };

            var p = new Parser<Sample_11>();

            p.Register.EmptyHandler(() => sample.Print());

            p.Run(new string[] { }, sample);

            mock.Verify(o => o.Print("x"));
        }

        [Test]
        [ExpectedException(typeof(MoreThanOneEmptyHandlerException))]
        public void Empty_MoreThanOne_Exception()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_13 { Printer = mock.Object };

            var p = new Parser<Sample_13>();

            p.Run(new string[] { }, sample);
        }

        [Test]
        [ExpectedException(typeof(ParserExecutionTargetException))]
        public void Empty_Defined_Static_TargetNotNull_Exception()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_14 { Printer = mock.Object };

            var p = new Parser<Sample_14>();

            p.Run(new string[] { }, sample);
        }

        [Test]
        [ExpectedException(typeof(ParserExecutionTargetException))]
        public void Empty_Defined_NotStatic_TargetNull_Exception()
        {
            var p = new Parser<Sample_15>();

            p.RunStatic(new string[] { });
        }

        [Test]
        public void EmptyHelp_Defined_Called()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_17 { Printer = mock.Object };

            var p = new Parser<Sample_17>();

            p.Run(new string[] { }, sample);

            mock.Verify(o => o.Print("a"));
        }

        [Test]
        [ExpectedException(typeof(InvalidHelpHandlerException))]
        public void EmptyHelp_Defined_IntParameter_Exception()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_18 { Printer = mock.Object };

            var p = new Parser<Sample_18>();

            p.Run(new string[] { }, sample);
        }

        [Test]
        [ExpectedException(typeof(InvalidHelpHandlerException))]
        public void EmptyHelp_Defined_TwoParameters_Exception()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_19 { Printer = mock.Object };

            var p = new Parser<Sample_19>();

            p.Run(new string[] { }, sample);
        }

        [Test]
        public void Help_WithAliases()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_21 { Printer = mock.Object };

            var p = new Parser<Sample_21>();

            p.Run(new[] { "help" }, sample);
            p.Run(new[] { "h" }, sample);
            p.Run(new[] { "?" }, sample);
            p.Run(new[] { "-help" }, sample);
            p.Run(new[] { "-h" }, sample);
            p.Run(new[] { "-?" }, sample);

            mock.Verify(o => o.Print("help"), Times.Exactly(6));
        }

        [Test]
        public void _Help_WithEverything_Coverage()
        {
            var p = new Parser<Sample_10, Sample_10_x>();

            p.Register.ParameterHandler("param", delegate { }, "description");

            p.GetHelpString();
        }

        [Test]
        [ExpectedException(typeof(ParserExecutionTargetException))]
        public void Help_Static_CalledWithTarget_Exception()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_22 { Printer = mock.Object };

            var p = new Parser<Sample_22>();

            p.Run(new[] { "-?" }, sample);
        }

        [Test]
        [ExpectedException(typeof(ParserExecutionTargetException))]
        public void Help_NonStatic_CalledWithNull_Exception()
        {
            var p = new Parser<Sample_23>();

            p.RunStatic(new[] { "-?" });
        }

        [Test]
        public void Help_Static()
        {
            var mock = new Mock<IPrinter>();

            Sample_22.StaticPrinter = mock.Object;

            var p = new Parser<Sample_22>();

            p.RunStatic(new[] { "-?" });

            Sample_22.StaticPrinter = null;
        }

        [Test]
        public void Help_NonStatic()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_23 { Printer = mock.Object };

            var p = new Parser<Sample_23>();

            p.Run(new[] { "-?" }, sample);

            mock.Verify(o => o.Print("help"));
        }

        [Test]
        [ExpectedException(typeof(InvalidHelpHandlerException))]
        public void Help_MoreThanOneParameter_Exception()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_24 { Printer = mock.Object };

            var p = new Parser<Sample_24>();

            p.Run(new[] { "-?" }, sample);
        }

        [Test]
        [ExpectedException(typeof(InvalidHelpHandlerException))]
        public void Help_IntParameter_Exception()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_25 { Printer = mock.Object };

            var p = new Parser<Sample_25>();

            p.Run(new[] { "-?" }, sample);
        }

        [Test]
        [ExpectedException(typeof(MissingArgumentPrefixException))]
        public void MapArguments_InvalidPrefix_Exception()
        {
            var printer = new Printer();
            var sample = new Sample_02 { Printer = printer };

            Parser.Run(new[]
            {
                "print",
                "*c=5",
                "/msg=test",
                "/prefix=hello_",
            }, sample);
        }

        [Test]
        [ExpectedException(typeof(MoreThanOneEmptyHandlerException))]
        public void RegisterHelpHandler_MoreThanOnce_Exception()
        {
            var p = new Parser<Sample_25>();

            p.Register.EmptyHandler(delegate { });
            p.Register.EmptyHandler(delegate { });
        }

        [Test]
        public void RegisterEmptyHelpHandler_Called()
        {
            var p = new Parser<Sample_25>();

            string help = null;

            p.Register.EmptyHelpHandler(h => help = h);

            Assert.IsNull(help);

            p.RunStatic(new string[] { });

            Assert.IsNotNull(help);
        }

        [Test]
        public void RegisterEmptyHelpHandler_EmptyArguments_Called()
        {
            var p = new Parser<Sample_25>();

            string help = null;

            p.Register.EmptyHelpHandler(h => help = h);

            Assert.IsNull(help);

            p.RunStatic(new string[] { });

            Assert.IsNotNull(help);
        }

        [Test]
        [ExpectedException(typeof(MoreThanOneEmptyHandlerException))]
        public void RegisterEmptyHelpHandler_MoreThanOnce_Exception()
        {
            var p = new Parser<Sample_25>();

            p.Register.EmptyHelpHandler(delegate { });
            p.Register.EmptyHelpHandler(delegate { });
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RegisterHelpHandler_MoreThanOnce_SameKey_Exception()
        {
            var p = new Parser<Sample_25>();

            p.Register.HelpHandler("a", delegate { });
            p.Register.HelpHandler("b", delegate { });
            p.Register.HelpHandler("a", delegate { });
        }

        [Test]
        [ExpectedException(typeof(VerbNotFoundException))]
        public void Run_Verb_NoMatchingMethod_Exception()
        {
            var p = new Parser<Sample_25>();

            p.RunStatic(new[] { "boo!" });
        }

        [Test]
        [ExpectedException(typeof(MissingDefaultVerbException))]
        public void Run_NoVerb_NoDefaultVerb_Exception()
        {
            var p = new Parser<Sample_25>();

            p.RunStatic(new string[] { "-x" });
        }

        [Test]
        public void GenericParser_Run()
        {
            var mock = new Mock<IPrinter>();

            Sample_27.StaticPrinter = mock.Object;

            Parser.Run<Sample_27>("foo -x:bar".Split(' '));

            Sample_27.StaticPrinter = null;

            mock.Verify(o => o.Print("bar"));
        }

        [Test]
        public void Parse_GuidParameter()
        {
            var mock = new Mock<IPrinter>();

            var sample = new Sample_28 { Printer = mock.Object };

            Parser.Run("foo -x:string -g:{0813A561-AC86-4C82-8EB1-0B6814637C7C}".Split(' '), sample);

            mock.Verify(o => o.Print("string0813A561-AC86-4C82-8EB1-0B6814637C7C".ToLower()));
        }

        [Test]
        public void Parse_GuidParameter_NoInput()
        {
            var mock = new Mock<IPrinter>();

            var sample = new Sample_28 { Printer = mock.Object };

            Parser.Run("foo -x:string".Split(' '), sample);

            mock.Verify(o => o.Print("string" + Guid.Empty));
        }

        [Test]
        public void Parse_GuidParameter_WithDefault()
        {
            var mock = new Mock<IPrinter>();

            var sample = new Sample_28 { Printer = mock.Object };

            Parser.Run("bar -x:string".Split(' '), sample);

            mock.Verify(o => o.Print("string2FBBAAAA-02AF-4F40-BADE-957F566B221E".ToLower()));
        }

        [Test]
        public void GetHelp_GuidParameter()
        {
            var mock = new Mock<IPrinter>();

            var sample = new Sample_28 { Printer = mock.Object };

            // should not fail. I don't care what the help string is
            //
            Parser.Run(new string[0], sample);
        }

        [Test]
        public void Default_IntAsString()
        {
            var mock = new Mock<IPrinter>();

            var sample = new Sample_28 { Printer = mock.Object };

            Parser.Run(new[] { "zoo" }, sample);

            mock.Verify(o => o.Print("5"));
        }

        [Test]
        public void Parse_UriParameter()
        {
            var mock = new Mock<IPrinter>();

            var sample = new Sample_29 { Printer = mock.Object };

            Parser.Run("foo -x:string -u:http://www.com".Split(' '), sample);

            mock.Verify(o => o.Print("stringhttp://www.com/".ToLower()));
        }

        [Test]
        public void Parse_UriParameter_NoInput()
        {
            var mock = new Mock<IPrinter>();

            var sample = new Sample_29 { Printer = mock.Object };

            Parser.Run("foo -x:string".Split(' '), sample);

            mock.Verify(o => o.Print("string"));
        }

        [Test]
        public void Parse_UriParameter_WithDefault()
        {
            var mock = new Mock<IPrinter>();

            var sample = new Sample_29 { Printer = mock.Object };

            Parser.Run("bar -x:string".Split(' '), sample);

            mock.Verify(o => o.Print("stringhttp://www.com/".ToLower()));
        }

        [Test]
        public void Validation_FileExists()
        {
            var sample = new ValidationSample_01();

            Parser.Run(new[]
            {
                "fileexists",
                @"/path=c:\windows\system32\cmd.exe",
            }, sample);

            Parser.Run(new[]
            {
                "fileexists",
                @"/path=%WINDIR%\system32\cmd.exe",
            }, sample);

            Parser.Run(new[]
            {
                "urifileexists",
                @"/path=c:\windows\system32\cmd.exe",
            }, sample);

            Parser.Run(new[]
            {
                "urifileexists",
                @"/path=%WINDIR%\system32\cmd.exe",
            }, sample);

            try
            {
                Parser.Run(new[]
                {
                    "fileexists",
                    @"/path=y:\{B2C97314-4C55-4EB9-9049-63BB65AC980A}.{6E8698D0-4CFA-4ACB-8AA3-26476F490228}",
                }, sample);

                Assert.Fail();
            }
            catch (ValidationException)
            {
            }

            try
            {
                Parser.Run(new[]
                { 
                    "urifileexists",
                    @"/path=y:\{B2C97314-4C55-4EB9-9049-63BB65AC980A}.{6E8698D0-4CFA-4ACB-8AA3-26476F490228}",
                }, sample);

                Assert.Fail();
            }
            catch (ValidationException)
            {
            }
        }

        [Test]
        public void Validation_DirectoryExists()
        {
            var sample = new ValidationSample_01();

            Parser.Run(new[]
            {
                "directoryexists",
                @"/path=c:\windows\system32",
            }, sample);

            Parser.Run(new[]
            {
                "directoryexists",
                @"/path=%WINDIR%\system32",
            }, sample);

            Parser.Run(new[]
            {
                "uridirectoryexists",
                @"/path=c:\windows\system32",
            }, sample);

            Parser.Run(new[]
            {
                "uridirectoryexists",
                @"/path=%WINDIR%\system32",
            }, sample);

            try
            {
                Parser.Run(new[]
                {
                    "directoryexists",
                    @"/path=y:\{B2C97314-4C55-4EB9-9049-63BB65AC980A}.{6E8698D0-4CFA-4ACB-8AA3-26476F490228}",
                }, sample);

                Assert.Fail();
            }
            catch (ValidationException)
            {
            }

            try
            {
                Parser.Run(new[]
                {
                    "uridirectoryexists",
                    @"/path=y:\{B2C97314-4C55-4EB9-9049-63BB65AC980A}.{6E8698D0-4CFA-4ACB-8AA3-26476F490228}",
                }, sample);

                Assert.Fail();
            }
            catch (ValidationException)
            {
            }
        }

        [Test]
        public void Validation_PathExists()
        {
            var sample = new ValidationSample_01();

            Parser.Run(new[] { "pathexists", @"/path=c:\windows\system32" }, sample);
            Parser.Run(new[] { "pathexists", @"/path=%WINDIR%\system32" }, sample);
            Parser.Run(new[] { "pathexists", @"/path=c:\windows\system32\cmd.exe" }, sample);
            Parser.Run(new[] { "pathexists", @"/path=%WINDIR%\system32\cmd.exe" }, sample);

            Parser.Run(new[] { "uripathexists", @"/path=c:\windows\system32\cmd.exe" }, sample);
            Parser.Run(new[] { "uripathexists", @"/path=%WINDIR%\system32\cmd.exe" }, sample);
            Parser.Run(new[] { "uripathexists", @"/path=c:\windows\system32" }, sample);
            Parser.Run(new[] { "uripathexists", @"/path=%WINDIR%\system32" }, sample);

            try
            {
                Parser.Run(new[]
                {
                    "pathexists",
                    @"/path=y:\{B2C97314-4C55-4EB9-9049-63BB65AC980A}.{6E8698D0-4CFA-4ACB-8AA3-26476F490228}",
                }, sample);

                Assert.Fail();
            }
            catch (ValidationException)
            {
            }

            try
            {
                Parser.Run(new[]
                {
                    "uripathexists",
                    @"/path=y:\{B2C97314-4C55-4EB9-9049-63BB65AC980A}.{6E8698D0-4CFA-4ACB-8AA3-26476F490228}",
                }, sample);

                Assert.Fail();
            }
            catch (ValidationException)
            {
            }
        }

        [Test]
        public void Unhandled_Exception_One()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_02 { Printer = mock.Object };

            var p = new Parser<Sample_02>();

            try
            {
                p.Run(new[]
                {
                    "-count:1",
                    "-message:a",
                    "-prefix:p",
                    "-upper",
                    "-what:x"
                }, sample);

                Assert.Fail();
            }
            catch (UnhandledParametersException ex)
            {
                Assert.AreEqual(1, ex.UnhandledParameters.Count());
                Assert.AreEqual("what", ex.UnhandledParameters.First().Key);
                Assert.AreEqual("x", ex.UnhandledParameters.First().Value);
            }
        }

        [Test]
        public void Unhandled_Exception_Some()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_02 { Printer = mock.Object };

            var p = new Parser<Sample_02>();

            try
            {
                p.Run(new[]
                {
                    "-count:1",
                    "-who:me",
                    "-foo:bar",
                    "-message:a",
                    "-prefix:p",
                    "-upper",
                    "-what:x"
                }, sample);

                Assert.Fail();
            }
            catch (UnhandledParametersException ex)
            {
                Assert.AreEqual(3, ex.UnhandledParameters.Count());
                Assert.IsTrue(ex.UnhandledParameters.Keys.Contains("who"));
                Assert.IsTrue(ex.UnhandledParameters.Keys.Contains("foo"));
                Assert.IsTrue(ex.UnhandledParameters.Keys.Contains("what"));
            }
        }

        [Test]
        public void Execute_DefaultVerb_NoInput()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_30 { Printer = mock.Object };

            Parser.Run(new string[] { }, sample);

            mock.Verify(o => o.Print("works!"));
        }

        [Test]
        public void Execute_DefaultVerb_NoInput_MethodWithParameters()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_31 { Printer = mock.Object };

            Parser.Run(new string[] { }, sample);

            mock.Verify(o => o.Print("works!"));
        }

        [Test]
        public void Execute_HandleError_Registered_NoRethrow()
        {
            var p = new Parser<Sample_39>();
            var handled = false;

            p.Register.ErrorHandler(ex =>
            {
                handled = true;
            });

            p.RunStatic(new string[] { });

            Assert.IsTrue(handled);
        }

        [Test]
        public void Execute_HandleError_Registered_DefaultNoRethrow()
        {
            var p = new Parser<Sample_39>();
            var handled = false;

            p.Register.ErrorHandler(ex =>
            {
                handled = true;
            });

            p.RunStatic(new string[] { });

            Assert.IsTrue(handled);
        }

        [Test]
        public void Execute_HandleError_Registered_Rethrow()
        {
            var p = new Parser<Sample_39>();
            var handled = false;

            p.Register.ErrorHandler(ex =>
            {
                handled = true;

                ex.ReThrow = true;
            });

            try
            {
                p.RunStatic(new string[] { });

                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.IsTrue(handled);
            }
        }

        [Test]
        public void Execute_HandleError_Registered_UnhandledParameters()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_02 { Printer = mock.Object };

            var p = new Parser<Sample_02>();

            var handled = false;

            p.Register.ErrorHandler(ex =>
            {
                handled = true;
            });

            p.Run(new[]
            {
                "-count:1",
                "-message:a",
                "-prefix:p",
                "-upper",
                "-what:x"
            }, sample);

            Assert.IsTrue(handled);
        }

        [Test]
        public void Execute_HandleError_Registered_ValidationError()
        {
            var sample = new ValidationSample_01();

            var p = new Parser<ValidationSample_01>();
            var handled = false;

            p.Register.ErrorHandler(ex =>
            {
                handled = true;
            });

            p.Run(new[] { "morethan5", "/n=1" }, sample);

            Assert.IsTrue(handled);
        }

        [Test]
        [ExpectedException(typeof(MoreThanOneErrorHandlerException))]
        public void Error_MoreThanOne_Exception()
        {
            var mock = new Mock<IPrinter>();
            var sample = new Sample_32 { Printer = mock.Object };

            var p = new Parser<Sample_32>();

            p.Run(new string[] { }, sample);
        }

        [Test]
        [ExpectedException(typeof(MoreThanOneErrorHandlerException))]
        public void RegisterError_MoreThanOne_Exception()
        {
            var p = new Parser<Sample_02>();

            p.Register.ErrorHandler(delegate { });
            p.Register.ErrorHandler(delegate { });
        }

        [Test]
        [ExpectedException(typeof(ArgumentMismatchException))]
        public void Error_DefinedWithInt_Exception()
        {
            Parser.Run<Sample_35>(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentMismatchException))]
        public void Error_DefinedWithBadException_Exception()
        {
            Parser.Run<Sample_36>(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentMismatchException))]
        public void Error_DefinedWithMoreThanOneParameter_Exception()
        {
            Parser.Run<Sample_37>(null);
        }

        [Test]
        public void Error_Handled_1()
        {
            var sample = new Sample_33();

            Assert.IsNull(sample.Ex);

            try
            {
                Parser.Run(new[] { "foo1" }, sample);

                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(sample.Ex);
                Assert.AreEqual(ex, sample.Ex);
            }
        }

        [Test]
        public void Error_Handled_2()
        {
            var sample = new Sample_33();

            Assert.IsNull(sample.Ex);

            try
            {
                Parser.Run(new[] { "foo" }, sample);

                Assert.Fail();
            }
            catch (TargetInvocationException tex)
            {
                Assert.IsNotNull(sample.Ex);
                Assert.AreEqual(tex.InnerException, sample.Ex);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(sample.Ex);
                Assert.AreEqual(ex, sample.Ex);
            }
        }

        [Test]
        public void Error_Handled_Empty()
        {
            var sample = new Sample_34();

            Assert.IsFalse(sample.Handled);

            try
            {
                Parser.Run(new[] { "foo" }, sample);

                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(sample.Handled);
            }
        }

        [Test]
        public void _Validation_Descriptions_Coverage()
        {
            var p = new Parser<All_Validations_Sample>();

            p.GetHelpString();
        }

        [Test]
        [ExpectedException(typeof(MoreThanOneDefaultVerbException))]
        public void DefaultVerb_MoreThanOnce_Exception()
        {
            new Parser<Sample_38>();
        }

        [Test]
        public void ValidateExpression_TwoInts_Pass()
        {
            Parser.Run<Sample_40>(new[]
            {   
                "foo1",
                "-p1:5",
                "-p2:3",
            });
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ValidateExpression_TwoInts_Fail()
        {
            Parser.Run<Sample_40>(new[]
            {
                "foo1",
                "-p1:1",
                "-p2:3",
            });
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ValidateExpression_TwoInts_TwoValidators_Fail()
        {
            Parser.Run<Sample_40>(new[]
            {
                "foo2",
                "-p1:8",
                "-p2:3",
            });
        }

        [Test]
        public void ValidateExpression_TwoInts_TwoValidators_Pass()
        {
            Parser.Run<Sample_40>(new[]
            {
                "foo2",
                "-p1:11",
                "-p2:3",
            });
        }

        [Test]
        public void ValidateExpression_String_Pass()
        {
            Parser.Run<Sample_40>(new[]
            {
                "foo3",
                "-str:def",
            });
        }

        [Test]
        public void ValidateExpression_String_DifferentCase_Pass()
        {
            Parser.Run<Sample_40>(new[]
            {
                "foo3",
                "-str:DEF",
            });
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ValidateExpression_String_DifferentCase_Fail()
        {
            Parser.Run<Sample_40>(new[]
            {
                "foo5",
                "-str:DEF",
            });
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ValidateExpression_String_Like_Fail()
        {
            Parser.Run<Sample_40>(new[]
            {
                "foo4",
                "-str:blah",
            });
        }

        [Test]
        public void ValidateExpression_String_Like_Pass()
        {
            Parser.Run<Sample_40>(new[]
            {
                "foo4",
                "-str:blahfoo",
            });
        }

        [Test]
        public void ValidateExpression_Global_Pass()
        {
            Parser.Run<Sample_40>(new[]
            {
                "dummy",
                "-boing:20",
            });
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ValidateExpression_Global_Fail()
        {
            Parser.Run<Sample_40>(new[]
            {
                "dummy",
                "-boing:5",
            });
        }

        [Test]
        public void Execute_WithFileInput_String()
        {
            var s = new Sample_41();

            var mock = new Mock<FileSystemHelper.IFileSystem>();

            mock.Setup(f => f.ReadAllText("some_dummy_file")).Returns("kicks ass!");

            FileSystemHelper.FileHandler = mock.Object;

            Parser.Run(new[]
            {
                "-str@=some_dummy_file"
            }, s);

            Assert.AreEqual("kicks ass!", s.Values["str"]);

            mock.VerifyAll();
        }

        [Test]
        public void Execute_WithFileInput_Int()
        {
            var s = new Sample_41();

            var mock = new Mock<FileSystemHelper.IFileSystem>();

            mock.Setup(f => f.ReadAllText("some_dummy_file")).Returns("567");

            FileSystemHelper.FileHandler = mock.Object;

            Parser.Run(new[]
            {
                "-num@=some_dummy_file"
            }, s);

            Assert.AreEqual(567, s.Values["num"]);

            mock.VerifyAll();
        }

        [Test]
        public void Execute_WithFileInput_Bool()
        {
            var s = new Sample_41();

            var mock = new Mock<FileSystemHelper.IFileSystem>();

            mock.Setup(f => f.ReadAllText("some_dummy_file")).Returns("false");

            FileSystemHelper.FileHandler = mock.Object;

            Parser.Run(new[]
            {
                "-b@=some_dummy_file"
            }, s);

            Assert.AreEqual(false, s.Values["b"]);

            mock.VerifyAll();
        }

        [Test]
        public void Execute_WithFileInput_Enum()
        {
            var s = new Sample_41();

            var mock = new Mock<FileSystemHelper.IFileSystem>();

            mock.Setup(f => f.ReadAllText("some_dummy_file")).Returns("Unchanged");

            FileSystemHelper.FileHandler = mock.Object;

            Parser.Run(new[]
            {
                "-c@=some_dummy_file"
            }, s);

            Assert.AreEqual(Case.Unchanged, s.Values["c"]);

            mock.VerifyAll();
        }

        [Test]
        public void Execute_WithFileInput_Array()
        {
            var s = new Sample_41();

            var mock = new Mock<FileSystemHelper.IFileSystem>();

            mock.Setup(f => f.ReadAllText("some_dummy_file")).Returns("301,7,99");

            FileSystemHelper.FileHandler = mock.Object;

            Parser.Run(new[]
            {
                "-numbers@=some_dummy_file"
            }, s);

            var arr = (int[])s.Values["numbers"];
            Assert.AreEqual(3, arr.Length);
            Assert.AreEqual(301, arr[0]);
            Assert.AreEqual(7, arr[1]);
            Assert.AreEqual(99, arr[2]);

            mock.VerifyAll();
        }

        [Test]
        public void ComplexType_JsonDeserialized_MyType()
        {
            var s = new Sample_42();

            Parser.Run(new[]
            {
                "-t:{ Number: 56, Name: 'blah' }", 
            }, s);

            Assert.AreEqual(56, s.TheType.Number);
            Assert.AreEqual("blah", s.TheType.Name);
        }

        [Test]
        public void ComplexType_XmlDeserialized_MyType()
        {
            var s = new Sample_42();

            Parser.Run(new[]
            {
                "-t:<MyType><Number>56</Number><Name>blah</Name></MyType>", 
            }, s);

            Assert.AreEqual(56, s.TheType.Number);
            Assert.AreEqual("blah", s.TheType.Name);
        }

        [Test]
        public void ComplexType_JsonDeserialized_MyType_AsGlobal()
        {
            var s = new Sample_42();

            Parser.Run(new[]
            {
                "-glob:{ Number: 881, Name: 'balooloo' }", 
            }, s);

            Assert.AreEqual(881, s.TheType_Global.Number);
            Assert.AreEqual("balooloo", s.TheType_Global.Name);
        }

        [Test]
        public void ComplexType_JsonDeserialized_MultiArray()
        {
            var s = new Sample_42();

            Parser.Run(new[]
            {
                "bar",
                "-arr:[[1,2,3],[4,5,6],[7,8]]", 
            }, s);

            Assert.AreEqual(3, s.Array.Length);

            var arr0 = s.Array[0];
            var arr1 = s.Array[1];
            var arr2 = s.Array[2];

            Assert.AreEqual(1, arr0[0]);
            Assert.AreEqual(2, arr0[1]);
            Assert.AreEqual(3, arr0[2]);

            Assert.AreEqual(4, arr1[0]);
            Assert.AreEqual(5, arr1[1]);
            Assert.AreEqual(6, arr1[2]);

            Assert.AreEqual(7, arr2[0]);
            Assert.AreEqual(8, arr2[1]);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ComplexType_WithTypeValidation_Fail()
        {
            var s = new Sample_42();

            Parser.Run(new[]
            {
                "val",
                "-t:{Number: 5, Name: 'bar'}",
            }, s);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ArrayOfComplexType_WithTypeValidation_Fail_1()
        {
            var s = new Sample_42();

            Parser.Run(new[]
            {
                "ZooCollection",
                "-t:[{Number: 100},{Number: 5}]",
            }, s);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ArrayOfComplexType_WithTypeValidation_Fail_2()
        {
            var s = new Sample_42();

            Parser.Run(new[]
            {
                "ValCollection",
                "-t:[{Number: 100},{Number: 5}]",
            }, s);
        }

        [Test]
        public void ComplexType_WithTypeValidation_Pass()
        {
            var s = new Sample_42();

            Parser.Run(new[]
            {
                "val",
                "-t:{Number: 50, Name: 'barfoo'}",
            }, s);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ComplexGraphType_WithTypeValidation_Fail()
        {
            var s = new Sample_42();

            Parser.Run(new[]
            {
                "complex",
                "-t:{Number: 40, Name: 'foobar', Validated: { Number: 100, Name: 'blah' }}",
            }, s);
        }

        [Test]
        public void ComplexGraphType_WithTypeValidation_Pass()
        {
            var s = new Sample_42();

            Parser.Run(new[]
            {
                "complex",
                "-t:{Number: 40, Name: 'bar', Validated: { Number: 100, Name: 'barfoo' }}",
            }, s);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ComplexGraphType_WithPropertyValidation_Fail()
        {
            var s = new Sample_42();

            Parser.Run(new[]
            {
                "props",
                "-t:{Number: 40, Name: 'foobar', Validated: { Number: 100, Name: 'blah' }}",
            }, s);
        }

        [Test]
        public void ComplexGraphType_WithPropertyValidation_Pass()
        {
            var s = new Sample_42();

            Parser.Run(new[]
            {
                "props",
                "-t:{Number: 40000, Name: 'something', Validated: { Number: 40, Name: 'foo' }}",
            }, s);
        }

        [Test]
        public void ComplexType_WithPropertySingleValidation_Pass()
        {
            var s = new Sample_42();

            Parser.Run(new[]
            {
                "zoo",
                "-t:{Number: 20}",
            }, s);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ComplexType_WithPropertySingleValidation_Fail()
        {
            var s = new Sample_42();

            Parser.Run(new[]
            {
                "zoo",
                "-t:{Number: 5}",
            }, s);
        }

        [Test]
        public void Interception_PreAndPostExecuted_VerbExecuted()
        {
            var s = new Sample_43();

            Assert.IsNull(s.PreContext);
            Assert.IsNull(s.PostContext);
            Assert.IsFalse(s.VerbExecuted);

            Parser.Run(new[]
            {
                "foo",
                "-str=blah",
                "-num=554",
            }, s);

            Assert.IsNotNull(s.PreContext);
            Assert.AreEqual("blah", s.PreContext.Parameters.First(p => p.Parameter.ParameterInfo.Name == "str").Value);
            Assert.AreEqual(554, s.PreContext.Parameters.First(p => p.Parameter.ParameterInfo.Name == "num").Value);

            Assert.IsNotNull(s.PostContext);
            Assert.AreEqual("blah", s.PostContext.Parameters.First(p => p.Parameter.ParameterInfo.Name == "str").Value);
            Assert.AreEqual(554, s.PostContext.Parameters.First(p => p.Parameter.ParameterInfo.Name == "num").Value);

            Assert.AreEqual(54, s.PostContext.UserContext["a"]);

            Assert.IsTrue(s.VerbExecuted);
        }

        [Test]
        public void Interception_PreAndPostExecuted_VerbNotExecuted()
        {
            var s = new Sample_44();

            Assert.IsNull(s.PreContext);
            Assert.IsNull(s.PostContext);
            Assert.IsFalse(s.VerbExecuted);

            Parser.Run(new[]
            {
                "foo",
                "-str=blah",
                "-num=554",
            }, s);

            Assert.IsNotNull(s.PreContext);
            Assert.IsNotNull(s.PostContext);
            Assert.IsFalse(s.VerbExecuted);
        }

        [Test]
        [ExpectedException(typeof(MoreThanOnePreVerbInterceptorException))]
        public void Interception_MoreThanOnePre_Exception()
        {
            Parser.Run<Sample_45>(new[] { "foo" });
        }

        [Test]
        [ExpectedException(typeof(MoreThanOnePreVerbInterceptorException))]
        public void Interception_RegisterMoreThanOnePre_Exception()
        {
            var p = new Parser<Sample_02>();

            p.Register.PreVerbInterceptor(c => { });
            p.Register.PreVerbInterceptor(c => { });
        }

        [Test]
        [ExpectedException(typeof(MoreThanOnePostVerbInterceptorException))]
        public void Interception_MoreThanOnePost_Exception()
        {
            Parser.Run<Sample_46>(new[] { "foo" });
        }

        [Test]
        [ExpectedException(typeof(MoreThanOnePostVerbInterceptorException))]
        public void Interception_RegisterMoreThanOnePost_Exception()
        {
            var p = new Parser<Sample_02>();

            p.Register.PostVerbInterceptor(c => { });
            p.Register.PostVerbInterceptor(c => { });
        }

        [Test]
        public void Interception_ExecutionFailed_PostCalled()
        {
            var s = new Sample_47();

            try
            {
                Parser.Run(new[] { "" }, s);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("ha!", ex.Message);
            }

            Assert.IsTrue(s.Context.Failed);
        }

        [Test]
        public void Interception_ExecutionFailed_PostCalled_WithErrorHandler_NoReThrow()
        {
            var s = new Sample_48();

            Parser.Run(new[] { "" }, s);

            Assert.IsTrue(s.Context.Failed);
        }

        [Test]
        public void Interception_ExecutionFailed_PostCalled_WithErrorHandler_ReThrow()
        {
            var s = new Sample_49();

            try
            {
                Parser.Run(new[] { "" }, s);

                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("ha!", ex.Message);
            }

            Assert.IsTrue(s.Context.Failed);
        }

        [Test]
        public void Interception_Registered_Called()
        {
            var printer = new Printer();
            var sample = new Sample_02 { Printer = printer };

            var p = new Parser<Sample_02>();

            var preInterceptorCalled = false;
            var postInterceptorCalled = false;

            p.Register.PreVerbInterceptor(c => preInterceptorCalled = true);
            p.Register.PostVerbInterceptor(c => postInterceptorCalled = true);

            p.Run(new[]
            {
                "print",
                "/c=5",
                "/msg=test",
                "/prefix=hello_",
            }, sample);

            Assert.AreEqual(5, printer.PrintedTexts.Count);
            Assert.IsTrue(printer.PrintedTexts.All(t => t.Equals("hello_test")));

            Assert.IsTrue(preInterceptorCalled);
            Assert.IsTrue(postInterceptorCalled);
        }

        [Test]
        public void Interception_DefinedOverType_PreAndPostExecuted_VerbExecuted()
        {
            var s = new Sample_50();

            Assert.IsFalse(s.VerbExecuted);

            Parser.Run(new[]
            {
                "foo",
                "-str=blah",
                "-num=554",
            }, s);

            Assert.IsTrue(s.VerbExecuted);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Register_EmptyHandler_Null_Exception()
        {
            var p = new Parser<Sample_02>();

            p.Register.EmptyHandler(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Register_EmptyHelpHandler_Null_Exception()
        {
            var p = new Parser<Sample_02>();

            p.Register.EmptyHelpHandler(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Register_ErrorHandler_Null_Exception()
        {
            var p = new Parser<Sample_02>();

            p.Register.ErrorHandler(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Register_HelpHandler_Null_Exception()
        {
            var p = new Parser<Sample_02>();

            p.Register.HelpHandler("help", null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Register_ParameterHandler_Null_Exception_1()
        {
            var p = new Parser<Sample_02>();

            p.Register.ParameterHandler("p", null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Register_ParameterHandler_Null_Exception_2()
        {
            var p = new Parser<Sample_02>();

            p.Register.ParameterHandler<int>("p", null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Register_ParameterHandler_Null_Exception_3()
        {
            var p = new Parser<Sample_02>();

            p.Register.ParameterHandler("p", null, "description");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Register_ParameterHandler_Null_Exception_4()
        {
            var p = new Parser<Sample_02>();

            p.Register.ParameterHandler<string>("p", null, new ParameterOptions { Description = "description" });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Register_PostVerbInterceptor_Null_Exception()
        {
            var p = new Parser<Sample_02>();

            p.Register.PostVerbInterceptor(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Register_PreVerbInterceptor_Null_Exception()
        {
            var p = new Parser<Sample_02>();

            p.Register.PreVerbInterceptor(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentMismatchException))]
        public void PreVerbExecution_MismatchArgs_Exception_1()
        {
            Parser.Run<Sample_56>(new[] { "" });
        }

        [Test]
        [ExpectedException(typeof(ArgumentMismatchException))]
        public void PostVerbExecution_MismatchArgs_Exception_1()
        {
            Parser.Run<Sample_57>(new[] { "" });
        }

        [Test]
        [ExpectedException(typeof(ArgumentMismatchException))]
        public void PreVerbExecution_MismatchArgs_Exception_2()
        {
            Parser.Run<Sample_58>(new[] { "" });
        }

        [Test]
        [ExpectedException(typeof(ArgumentMismatchException))]
        public void PostVerbExecution_MismatchArgs_Exception_2()
        {
            Parser.Run<Sample_59>(new[] { "" });
        }

        //[Test, Ignore]
        //public void Parameter_NameAsAlias()
        //{
        //    Parser.Run<Sample_60>(new[]
        //    {
        //        "foo",
        //        "-c=5",
        //    });
        //}

        [Test]
        public void Parameter_NullableInt()
        {
            var s = new Sample_61();

            Parser.Run(new[]
            {
                "bar",
                "-p1=5",
            }, s);

            Assert.AreEqual(5, s.P1);
            Assert.AreEqual(5, s.P2);
            Assert.AreEqual(5, s.P3);
            Assert.AreEqual(null, s.P4);
            Assert.AreEqual(null, s.P5);
        }

        [Test]
        public void Parameter_WithDefault_1()
        {
            var t = new ParameterWithDefaults_1();

            Parser.Run(new string[] { "foo" }, t);

            Assert.AreEqual("def1", t.P1);
            Assert.AreEqual("def2", t.P2);
        }

        [Test]
        public void Parameter_WithDefault_2()
        {
            var t = new ParameterWithDefaults_1();

            Parser.Run(new string[] { "foo", "-p2=bar" }, t);

            Assert.AreEqual("def1", t.P1);
            Assert.AreEqual("bar", t.P2);
        }

        [Test]
        public void Parameter_WithDefault_3()
        {
            var t = new ParameterWithDefaults_1();

            Parser.Run(new string[] { "foo", "-p1=zoo" }, t);

            Assert.AreEqual("zoo", t.P1);
            Assert.AreEqual("def2", t.P2);
        }

        [Test]
        public void Parameter_WithDefault_4()
        {
            var t = new ParameterWithDefaults_1();

            Parser.Run(new string[] { "foo", "-p1=zoo", "-p2=bar" }, t);

            Assert.AreEqual("zoo", t.P1);
            Assert.AreEqual("bar", t.P2);
        }

        [Test]
        [ExpectedException(typeof(AmbiguousParameterDefaultException))]
        public void Parameter_WithDefault_AmbiguousDefaults()
        {
            var t = new ParameterWithDefaults_2();

            Parser.Run(new string[] { "foo" }, t);
        }

        [Test]
        [ExpectedException(typeof(InvalidParameterDefaultProviderException))]
        public void Parameter_WithDefault_BadDefaultProvider()
        {
            var t = new ParameterWithDefaults_3();

            Parser.Run(new string[] { "foo" }, t);
        }

        [Test]
        public void Parameters_WithAutoPrefixes()
        {
            var s = new Sample_62();
            Parser.Run(new[]
            {
                "foo","-c=5","-n=bar"
            }, s);

            Assert.AreEqual(5, s.Count);
            Assert.AreEqual("bar", s.Name);
        }

        [Test]
        [ExpectedException(typeof(UnhandledParametersException))]
        public void Parameters_WithoutAutoPrefixes()
        {
            var s = new Sample_62();
            Parser.Run(new[]
            {
                "bar","-c=5","-n=bar"
            }, s);
        }

        [Test]
        [ExpectedException(typeof(DuplicateGlobalHandlerException))]
        public void DuplicateGlobalName_Exception()
        {
            Parser.Run<Sample_63>(new[]
            {
                "foo"
            });
        }

        [Test]
        public void HandlePreMatureError_DefinedHandler()
        {
            var s = new Sample_64();

            Assert.IsFalse(s.Handled);

            Parser.Run(new[] { "." }, s);

            Assert.IsTrue(s.Handled);
        }

        [Test]
        public void HandlePreMatureError_RegisteredHandler()
        {
            var s = new Sample_65();
            var p = new Parser<Sample_65>();

            var handled = false;

            p.Register.ErrorHandler(c => handled = true);

            p.RunTargets(new[] { "." }, s);

            Assert.IsTrue(handled);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void DeserializeComplexTypeWithCollection_Json_ValidationError()
        {
            var s = new Sample_66();

            var json = "{Age: 34,Name: 'Adrian',Email: 'adrianaisemberg@gmail.com',PhoneNumbers:[{Type:'Home',Number:'001-2232322'},{Type:'Mobile',Number:'002-7787787'},{Type: 'Office',Number: '003-4463565'}]}";

            Parser.Run(new[] { "foo", "-p=" + json }, s);

            Assert.AreEqual("Adrian", s.Person.Name);
            Assert.AreEqual(3, s.Person.PhoneNumbers.Count);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void DeserializeComplexTypeWithCollection_Xml_ValidationError()
        {
            var s = new Sample_66();

            var xml = "<Person><Age>34</Age><Name>Adrian</Name><Email>adrianaisemberg@gmail.com</Email><PhoneNumbers><PhoneNumber><Type>Home</Type><Number>001-2232322</Number></PhoneNumber><PhoneNumber><Type>Mobile</Type><Number>002-7787787</Number></PhoneNumber><PhoneNumber><Type>Office</Type><Number>003-4463565</Number></PhoneNumber></PhoneNumbers></Person>";

            Parser.Run(new[] { "foo", "-p=" + xml }, s);

            Assert.AreEqual("Adrian", s.Person.Name);
            Assert.AreEqual(3, s.Person.PhoneNumbers.Count);
        }

        [Test]
        public void DeserializeComplexTypeWithCollection_Json()
        {
            var s = new Sample_66();

            var json = "{Age: 34,Name: 'Adrian',Email: 'adrianaisemberg@gmail.com',PhoneNumbers:[{Type:'Home',Number:'001-2232322',SomeNumber:20},{Type:'Mobile',Number:'002-7787787',SomeNumber:21},{Type: 'Office',Number: '003-4463565',SomeNumber:30}]}";

            Parser.Run(new[] { "foo", "-p=" + json }, s);

            Assert.AreEqual("Adrian", s.Person.Name);
            Assert.AreEqual(3, s.Person.PhoneNumbers.Count);
        }

        [Test]
        public void DeserializeComplexTypeWithCollection_Xml()
        {
            var s = new Sample_66();

            var xml = "<Person><Age>34</Age><Name>Adrian</Name><Email>adrianaisemberg@gmail.com</Email><PhoneNumbers><PhoneNumber><Type>Home</Type><Number>001-2232322</Number><SomeNumber>20</SomeNumber></PhoneNumber><PhoneNumber><Type>Mobile</Type><Number>002-7787787</Number><SomeNumber>21</SomeNumber></PhoneNumber><PhoneNumber><Type>Office</Type><Number>003-4463565</Number><SomeNumber>30</SomeNumber></PhoneNumber></PhoneNumbers></Person>";

            Parser.Run(new[] { "foo", "-p=" + xml }, s);

            Assert.AreEqual("Adrian", s.Person.Name);
            Assert.AreEqual(3, s.Person.PhoneNumbers.Count);
        }

        [Test]
        public void ParametersWithSameAttributes_NoError()
        {
            Parser.Run<Sample_67>(new[] { "foo", "-x=a", "-y=b" });
        }

        [Test]
        [ExpectedException(typeof(NonArrayParameterWithSeparatorException))]
        public void NonArrayWithSeparator_Exception()
        {
            Parser.Run<Sample_68>(new[] { "foo", "-x=aaa" });
        }

        [Test]
        [ExpectedException(typeof(InvalidSeparatorException))]
        public void NonArrayWithInvalidSeparator_Exception_1()
        {
            Parser.Run<Sample_70>(new[] { "foo", "-enums=aaa bbb ccc" });
        }

        [Test]
        [ExpectedException(typeof(InvalidSeparatorException))]
        public void NonArrayWithInvalidSeparator_Exception_2()
        {
            Parser.Run<Sample_71>(new[] { "foo", "-enums=aaa bbb ccc" });
        }

        [Test]
        [ExpectedException(typeof(InvalidSeparatorException))]
        public void NonArrayWithInvalidSeparator_Exception_3()
        {
            Parser.Run<Sample_72>(new[] { "foo", "-enums=aaa bbb ccc" });
        }

        [Test]
        [ExpectedException(typeof(InvalidSeparatorException))]
        public void NonArrayWithInvalidSeparator_Exception_4()
        {
            Parser.Run<Sample_73>(new[] { "foo", "-enums=aaa bbb ccc" });
        }
    }
}