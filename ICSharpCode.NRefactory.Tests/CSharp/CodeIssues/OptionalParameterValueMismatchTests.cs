﻿// 
// OptionalParameterValueMismatchTests.cs
// 
// Author:
//      Luís Reis <luiscubal@gmail.com>
// 
// Copyright (c) 2013 Luís Reis
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using ICSharpCode.NRefactory.CSharp.CodeActions;
using ICSharpCode.NRefactory.CSharp.Refactoring;
using NUnit.Framework;

namespace ICSharpCode.NRefactory.CSharp.CodeIssues
{
	public class OptionalParameterValueMismatchTests : InspectionActionTestBase
	{
		[Test]
		public void TestSimpleCase ()
		{
			Test<OptionalParameterValueMismatchIssue>(@"
class Base
{
	public virtual void TestMethod(int value = 1) {}
}
class Derived : Base
{
	public override void TestMethod(int value = 2) {}
}", @"
class Base
{
	public virtual void TestMethod(int value = 1) {}
}
class Derived : Base
{
	public override void TestMethod(int value = 1) {}
}");
		}

		[Test]
		public void TestInterface ()
		{
			Test<OptionalParameterValueMismatchIssue>(@"
interface IBase
{
	void TestMethod(int value = 1);
}
class Derived : IBase
{
	public void TestMethod(int value = 2) {}
}", @"
interface IBase
{
	void TestMethod(int value = 1);
}
class Derived : IBase
{
	public void TestMethod(int value = 1) {}
}");
		}

		[Test]
		public void TestDisabledForCorrect ()
		{
			Test<OptionalParameterValueMismatchIssue>(@"
interface IBase
{
	void TestMethod(int value = 1);
}
class Derived : IBase
{
	public void TestMethod(int value = 1) {}
}", 0);
		}

		[Test]
		public void TestDisabledForAddOptional ()
		{
			Test<OptionalParameterValueMismatchIssue>(@"
class Base
{
	public virtual void TestMethod(int value) {}
}
class Derived : Base
{
	public override void TestMethod(int value = 2) {}
}", 0);
		}

		[Test]
		public void TestRemoveOptional ()
		{
			Test<OptionalParameterValueMismatchIssue>(@"
class Base
{
	public virtual void TestMethod(int value = 1) {}
}
class Derived : Base
{
	public override void TestMethod(int value) {}
}", @"
class Base
{
	public virtual void TestMethod(int value = 1) {}
}
class Derived : Base
{
	public override void TestMethod(int value = 1) {}
}");
		}
	}
}