using System;
using FakeItEasy;
using FluentAssertions;
using Xunit;

using ESystems.Mentoring.FileSystem.Interface;
using ESystems.Mentoring.FileSystem.Interface.Data;
using ESystems.Mentoring.FileVisiting;

namespace FileVisitingTest
{
    public class FileSystemVisitorTest
    {
        [Fact]
        public void ConstructorWithOneArgument_CorrectArguments_NoException()
        {
            Assert.Null(Record.Exception(() =>
            {
                var service = A.Fake<IFileSystemService>();
                var visitor = new FileSystemVisitor(service);
            }));
        }

        [Fact]
        public void ConstructorWithOneArgument_NullService_ArgumentNullException()
        {
            Record.Exception(() =>
                {
                    var visitor = new FileSystemVisitor(null);
                }).Should().BeOfType<ArgumentNullException>()
                .Which.ParamName.Should().Be("fileSystemService");
        }

        [Fact]
        public void ConstructorWithTwoArguments_CorrectParameterss_NoException()
        {
            Assert.Null(Record.Exception(() =>
            {
                var service = A.Fake<IFileSystemService>();
                Func<FileData, bool> predicate = file => false;
                var visitor = new FileSystemVisitor(service, predicate);
            }));
        }

        [Fact]
        public void ConstructorWithTwoArgumentss_NullService_ArgumentNullException()
        {
            Record.Exception(() =>
                {
                    Func<FileData, bool> predicate = file => false;
                    var visitor = new FileSystemVisitor(null, predicate);
                }).Should().BeOfType<ArgumentNullException>()
                .Which.ParamName.Should().Be("fileSystemService");
        }

        [Fact]
        public void ConstructorWithTwoArguments_NullPredicate_ArgumentNullException()
        {
            Record.Exception(() =>
                {
                    var service = A.Fake<IFileSystemService>();
                    var visitor = new FileSystemVisitor(service, null);
                }).Should().BeOfType<ArgumentNullException>()
                .Which.ParamName.Should().Be("predicate");
        }

        [Fact]
        public void ConstructorWithTwoArguments_NullArguments_ArgumentNullException()
        {
            Record.Exception(() =>
            {
                var visitor = new FileSystemVisitor(null, null);
            }).Should().BeOfType<ArgumentNullException>();
        }
    }
}
