using System.Collections.Generic;
using System.Reflection;
using HtmlTags;
using StoryTeller.Domain;
using StoryTeller.Execution;
using StoryTeller.Model;
using StoryTeller.Samples.Grammars;
using StoryTeller.UserInterface.Editing.HTML;
using StoryTeller.Workspace;

namespace StoryTeller.Testing.JavaScript
{
    public class SampleFile : JavaScriptTestFile
    {
        public SampleFile()
            : base("Sample Test Editor")
        {
            TestFile("SampleTestEditor.js");

            Fixtures(x => x.AddFixturesFromAssemblyContaining<SentenceFixture>());
        }

        public void Open()
        {
            OpenInBrowser();
        }

        protected override void referenceFiles()
        {
        }

        protected override void addChildren(string title)
        {
            var select = Add("h2").Text("Test:  ").Child<SelectTag>();
            select.Id("testName");


            Project project = DataMother.GrammarProject();

            AddTest("Blank", new Test("Blank"));
            select.Option("Blank", "Blank");

            project.LoadTests().GetAllTests().Each(test =>
            {
                string testName = test.Name.Replace(" ", "_");

                select.Option(test.Name, testName);

                AddTest(testName, test);
            });

            Add("hr");

            FixtureLibrary library = FixtureLibrary.For(x => x.AddFixturesFromAssemblyContaining<SentenceFixture>());
            Add("div").Child(new TestEditorTag(library));
        }
    }



    // STOP RIGHT THERE!
    // THIS IS STRICTLY FOR JEREMY's TESTING AND 100% LIKELY TO FAIL ON YOUR BOX!
    public class BlueTestFile : JavaScriptTestFile
    {
        private readonly ProjectTestRunner _runner = new ProjectTestRunner(@"C:\\svn\\blue\\StoryTeller.xml");

        public BlueTestFile()
            : base("Blue Files")
        {
            TestFile("SampleTestEditor.js");

            
            Fixtures(_runner.GetLibary());
        }

        public void Open()
        {
            OpenInBrowser();
        }

        protected override void referenceFiles()
        {
        }

        protected override void addChildren(string title)
        {
            var select = Add("h2").Text("Test:  ").Child<SelectTag>();
            select.Id("testName");


            IProject project = _runner.Project;

            AddTest("Blank", new Test("Blank"));
            select.Option("Blank", "Blank");

            project.LoadTests().GetAllTests().Each(test =>
            {
                string testName = test.Name.Replace(" ", "_");

                select.Option(test.LocatorPath(), testName);

                AddTest(testName, test);
            });

            Add("hr");

            FixtureLibrary library = _runner.GetLibary();
            Add("div").Child(new TestEditorTag(library));
        }
    }

}