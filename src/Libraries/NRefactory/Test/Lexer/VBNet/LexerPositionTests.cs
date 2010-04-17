﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <author name="unknown"/>
//     <version>$Revision$</version>
// </file>

using ICSharpCode.NRefactory.Parser.VB;
using System;
using System.IO;
using ICSharpCode.NRefactory.Parser;
using NUnit.Framework;

namespace ICSharpCode.NRefactory.Tests.Lexer.VB
{
	[TestFixture]
	public class LexerPositionTests
	{
		ILexer GenerateLexer(string s)
		{
			return ParserFactory.CreateLexer(SupportedLanguage.VBNet, new StringReader(s));
		}
		
		[Test]
		public void TestNewLine()
		{
			ILexer l = GenerateLexer("public\nstatic");
			Token t = l.NextToken();
			Assert.AreEqual(Tokens.Public, t.Kind);
			Assert.AreEqual(new Location(1, 1), t.Location);
			Assert.AreEqual(new Location(7, 1), t.EndLocation);
			t = l.NextToken();
			Assert.AreEqual(Tokens.EOL, t.Kind);
			Assert.AreEqual(new Location(7, 1), t.Location);
			Assert.AreEqual(new Location(1, 2), t.EndLocation);
			t = l.NextToken();
			Assert.AreEqual(Tokens.Static, t.Kind);
			Assert.AreEqual(new Location(1, 2), t.Location);
			Assert.AreEqual(new Location(7, 2), t.EndLocation);
		}
		
		[Test]
		public void TestCarriageReturnNewLine()
		{
			ILexer l = GenerateLexer("public\r\nstatic");
			Token t = l.NextToken();
			Assert.AreEqual(Tokens.Public, t.Kind);
			Assert.AreEqual(new Location(1, 1), t.Location);
			Assert.AreEqual(new Location(7, 1), t.EndLocation);
			t = l.NextToken();
			Assert.AreEqual(Tokens.EOL, t.Kind);
			Assert.AreEqual(new Location(7, 1), t.Location);
			Assert.AreEqual(new Location(1, 2), t.EndLocation);
			t = l.NextToken();
			Assert.AreEqual(Tokens.Static, t.Kind);
			Assert.AreEqual(new Location(1, 2), t.Location);
			Assert.AreEqual(new Location(7, 2), t.EndLocation);
		}
		
		[Test]
		public void TestPositionOfEOF1()
		{
			ILexer l = GenerateLexer("public");
			l.NextToken(); // public
			Token t = l.NextToken();
			Assert.AreEqual(Tokens.EOL, t.Kind);
			Assert.AreEqual(new Location(7, 1), t.Location);
			Assert.AreEqual(new Location(7, 1), t.EndLocation);
			
			t = l.NextToken();
			Assert.AreEqual(Tokens.EOF, t.Kind);
			Assert.AreEqual(new Location(7, 1), t.Location);
			Assert.AreEqual(new Location(7, 1), t.EndLocation);
		}
		
		[Test]
		public void TestPositionOfEOF2()
		{
			ILexer l = GenerateLexer("public _\n ");
			l.NextToken(); // public
			Token t = l.NextToken();
			Assert.AreEqual(Tokens.EOL, t.Kind);
			Assert.AreEqual(new Location(2, 2), t.Location);
			Assert.AreEqual(new Location(2, 2), t.EndLocation);
			
			t = l.NextToken();
			Assert.AreEqual(Tokens.EOF, t.Kind);
			Assert.AreEqual(new Location(2, 2), t.Location);
			Assert.AreEqual(new Location(2, 2), t.EndLocation);
		}
	}
}
