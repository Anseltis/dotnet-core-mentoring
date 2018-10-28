using System;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using Xunit;

using ESystems.Mentoring.FileSystem.Interface;
using ESystems.Mentoring.FileSystem.Interface.Data;
using ESystems.Mentoring.FileSystem.Interface.Exceptions;
using ESystems.Mentoring.FileVisiting;
using AutoFixture;

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
                bool predicate(FileData file) => false;
                var visitor = new FileSystemVisitor(service, predicate);
            }));
        }

        [Fact]
        public void ConstructorWithTwoArgumentss_NullService_ArgumentNullException()
        {
            Record.Exception(() =>
                {
                    bool predicate(FileData file) => false;
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

        [Fact]
        public void Visit_TwoFoldersThreeFilesNoFilter_ThreeFiles()
        {
            var fixture = new Fixture();

            var path = fixture.Create<string>();

            var folder1 = new FolderData(fixture.Create<string>(), fixture.Create<string>());
            var folder2 = new FolderData(fixture.Create<string>(), fixture.Create<string>());

            var file1 = new FileData(fixture.Create<string>(), fixture.Create<string>());
            var file2 = new FileData(fixture.Create<string>(), fixture.Create<string>());
            var file3 = new FileData(fixture.Create<string>(), fixture.Create<string>());

            var service = A.Fake<IFileSystemService>(options => options.Strict());
            A.CallTo(() => service.GetFolders(path)).Returns(new [] {folder1, folder2});
            A.CallTo(() => service.GetFiles(folder1.FullName)).Returns(new[] { file1, file2 });
            A.CallTo(() => service.GetFiles(folder2.FullName)).Returns(new[] { file3 });
            var visitor = new FileSystemVisitor(service);

            var expected = new[] {file1, file2, file3};
            var actual = visitor.Visit(path).ToArray();
            
            actual.Should().BeEquivalentTo(expected);

            A.CallTo(() => service.GetFolders(path)).MustHaveHappenedOnceExactly()
                .Then(A.CallTo(() => service.GetFiles(folder1.FullName)).MustHaveHappenedOnceExactly())
                .Then(A.CallTo(() => service.GetFiles(folder2.FullName)).MustHaveHappenedOnceExactly());
        }

        [Fact]
        public void Visit_TwoFoldersThreeFilesFilterForSecondFile_OneFile()
        {
            var fixture = new Fixture();

            var path = fixture.Create<string>();

            var folder1 = new FolderData(fixture.Create<string>(), fixture.Create<string>());
            var folder2 = new FolderData(fixture.Create<string>(), fixture.Create<string>());

            var file1 = new FileData(fixture.Create<string>(), fixture.Create<string>());
            var file2 = new FileData(fixture.Create<string>(), fixture.Create<string>());
            var file3 = new FileData(fixture.Create<string>(), fixture.Create<string>());

            var service = A.Fake<IFileSystemService>(options => options.Strict());
            A.CallTo(() => service.GetFolders(path)).Returns(new[] { folder1, folder2 });
            A.CallTo(() => service.GetFiles(folder1.FullName)).Returns(new[] { file1, file2 });
            A.CallTo(() => service.GetFiles(folder2.FullName)).Returns(new[] { file3 });

            bool predicate(FileData file) => file == file2;
            var visitor = new FileSystemVisitor(service, predicate);

            var expected = new[] { file2 };
            var actual = visitor.Visit(path).ToArray();

            actual.Should().BeEquivalentTo(expected);

            A.CallTo(() => service.GetFolders(path)).MustHaveHappenedOnceExactly()
                .Then(A.CallTo(() => service.GetFiles(folder1.FullName)).MustHaveHappenedOnceExactly())
                .Then(A.CallTo(() => service.GetFiles(folder2.FullName)).MustHaveHappenedOnceExactly());
        }

        [Fact]
        public void Visit_TwoFoldersSeceondWithAccessDenied_AccessDenied()
        {
            var fixture = new Fixture();

            var path = fixture.Create<string>();

            var folder1 = new FolderData(fixture.Create<string>(), fixture.Create<string>());
            var folder2 = new FolderData(fixture.Create<string>(), fixture.Create<string>());

            var file1 = new FileData(fixture.Create<string>(), fixture.Create<string>());
            var file2 = new FileData(fixture.Create<string>(), fixture.Create<string>());
            var file3 = new FileData(fixture.Create<string>(), fixture.Create<string>());

            var service = A.Fake<IFileSystemService>(options => options.Strict());
            A.CallTo(() => service.GetFolders(path)).Returns(new[] {folder1, folder2});
            A.CallTo(() => service.GetFiles(folder1.FullName)).Returns(new[] {file1, file2});
            A.CallTo(() => service.GetFiles(folder2.FullName))
                .Throws(new AccessDeniedFileSystemServiceException(folder2.FullName));

            Record.Exception(() =>
                {
                    var visitor = new FileSystemVisitor(service);
                    visitor.Visit(path).ToArray();
                }).Should().BeOfType<AccessDeniedFileSystemServiceException>()
                .Which.Path.Should().Be(folder2.FullName);

            A.CallTo(() => service.GetFolders(path)).MustHaveHappenedOnceExactly()
                .Then(A.CallTo(() => service.GetFiles(folder1.FullName)).MustHaveHappenedOnceExactly())
                .Then(A.CallTo(() => service.GetFiles(folder2.FullName)).MustHaveHappenedOnceExactly());
        }
    }
}
