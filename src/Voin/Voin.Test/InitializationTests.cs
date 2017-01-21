using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voin.Core;
using Voin.Core.Rules.CoreDsl;
using Voin.Core.Rules.EnglishDsl;
using Voin.Test.Models.Actors;
using Voin.Test.Models.Resources;
using Voin.Test.Models.Rights;

namespace Voin.Test
{
    [TestClass]
    public class InitializationTests
    {
        private readonly User alice;
        private readonly User bob;
        private readonly Right see;
        private readonly Right use;
        private readonly Printer redPrinter;
        private readonly Printer pinkPrinter;
        private readonly File imageFile;
        private readonly File videoFile;

        private readonly IRepository<IActor> actors;
        private readonly InMemoryRepository<IResource> resources;
        private readonly InMemoryRepository<IRight> rights;

        public InitializationTests()
        {
            this.alice = new User("Alice", "alice@example.com");
            this.bob = new User("Bob");
            this.see = new Right("see");
            this.use = new Right("use");
            this.redPrinter = new Printer("Red Printer");
            this.pinkPrinter = new Printer("Pink Printer");
            this.imageFile = new File("Image File");
            this.videoFile = new File("Video File");
            this.actors = new InMemoryRepository<IActor>(new[] { alice, bob });
            this.resources = new InMemoryRepository<IResource>(new IResource[] { redPrinter, pinkPrinter, imageFile, videoFile });
            this.rights = new InMemoryRepository<IRight>(new[] { see, use });
        }
        
        public void TestMethod1()
        {
            //basics

            // right on group

            // right on parent group

            // delegate

            // owner can read and write

            // owner can only read, not write

            // Get all users which can access a particuliar resource

            // get all users which cannot access a resource

            // MAJ des données quand modification du modèle
        }

        [TestMethod]
        public void TestBasic()
        {
            var rightService = this.BuildRightService();
            rightService.AddRule("Alice can see the red printer", alice, redPrinter, see);

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }

        [TestMethod]
        public void TestBasicNotAllowed()
        {
            var rightService = this.BuildRightService();
            rightService.AddRule("Alice can see the red printer", alice, redPrinter, see);

            var hasAccess = rightService.HasRight(bob, redPrinter, see);

            Assert.IsFalse(hasAccess);
        }

        [TestMethod]
        public void TestBasic2()
        {
            var rightService = this.BuildRightService();
            rightService.AddRule("Any user can see the red printer", actor => actor.Type == "User", redPrinter, see);

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }

        [TestMethod]
        public void TestAnyOneCanSeeTheRedPrinter()
        {
            var rightService = this.BuildRightService(_ => _.AnyOne().Can(see).The(redPrinter));

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }


        [TestMethod]
        public void TestAnyUserCanSeeTheRedPrinter()
        {
            var rightService = this.BuildRightService(_ => _.Any("User").Can(see).The(redPrinter));

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }

        [TestMethod]
        public void TestAnyUserWithAnEmailCanSeeTheRedPrinter()
        {
            var rightService = this.BuildRightService(_ => _.Any<User>().With(u => !string.IsNullOrWhiteSpace(u.Email)).Can(see).The(redPrinter));

            var aliceHasAccess = rightService.HasRight(alice, redPrinter, see);
            var bobHasAccess = rightService.HasRight(bob, redPrinter, see);

            Assert.IsTrue(aliceHasAccess);
            Assert.IsFalse(bobHasAccess);
        }

        [TestMethod]
        public void TestAnyTypeUserCanSeeTheRedPrinter()
        {
            var rightService = this.BuildRightService(_ => _.Any<User>().Can(see).The(redPrinter));

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }

        [TestMethod]
        public void TestOnlyUserCanSeeTheRedPrinter()
        {
            var rightService = this.BuildRightService(_ => _.Only("User").Can(see).The(redPrinter));

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }

        [TestMethod]
        public void TestOnlyTypeUserCanSeeTheRedPrinter()
        {
            var rightService = this.BuildRightService(_ => _.Only<User>().Can(see).The(redPrinter));

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }

        [TestMethod]
        public void TestOnlyAliceCanSeeTheRedPrinter()
        {
            var rightService = this.BuildRightService(_ => _.Only(alice).Can(see).The(redPrinter));

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }

        [TestMethod]
        public void TestAllUserCanSeeTheRedPrinter()
        {
            var rightService = this.BuildRightService(_ => _.All("User").Can(see).The(redPrinter));

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }

        [TestMethod]
        public void TestAllTypeUserCanSeeTheRedPrinter()
        {
            var rightService = this.BuildRightService(_ => _.All<User>().Can(see).The(redPrinter));

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }

        [TestMethod]
        public void TestAllUserCanSeeAllPrinter()
        {
            var rightService = this.BuildRightService(_ => _.All("User").Can(see).All("Printer"));

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }

        [TestMethod]
        public void TestAllUserCanSeeAllTypePrinter()
        {
            var rightService = this.BuildRightService(_ => _.All("User").Can(see).All<Printer>());

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }

        [TestMethod]
        public void TestAllUserCanSeeOnlyTheRedPrinter()
        {
            var rightService = this.BuildRightService(_ => _.All("User").Can(see).OnlyThe(redPrinter));

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }

        [TestMethod]
        public void TestAllUserCanSeeAndUseOnlyTheRedPrinter()
        {
            var rightService = this.BuildRightService(_ => _.All("User").Can(see).And(use).OnlyThe(redPrinter));

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }

        private RightService BuildRightService(params Func<Root, ICompleteRule>[] rules)
        {
            var rightService = new RightService(new InMemoryRightStore(), this.actors, resources, rights);

            foreach (var rule in rules)
            {
                rightService.AddRule(rule);
            }

            rightService.Initialize();

            return rightService;
        }
    }
}
