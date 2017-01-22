using System;
using System.Collections;
using System.Collections.Generic;
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
    public class UpdateTests
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

        public UpdateTests()
        {
            this.alice = new User("Alice");
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

        [TestMethod]
        public void TestActorChange()
        {
            var charles = new User("Charles", "charles@example.com");
            
            var rightService = this.BuildRightService(
                charles,
                _ => _.Any<User>().With(u => !string.IsNullOrWhiteSpace(u.Email)).Can(see).The(redPrinter));

            charles.Email = "";
            rightService.Update(charles);

            var hasAccess = rightService.HasRight(charles, see, redPrinter);

            Assert.IsFalse(hasAccess);
        }

        [TestMethod]
        public void TestResourceChange()
        {
            var printer = new Printer("Best printer ever") {IsActive = true};
            
            var rightService = this.BuildRightService(
                printer,
                _ => _.Any<User>().Can(see).All<Printer>().With(p => printer.IsActive));

            Assert.IsTrue(rightService.HasRight(alice, see, printer));

            printer.IsActive = false;
            rightService.Update(printer);
            
            Assert.IsFalse(rightService.HasRight(alice, see, printer));
        }

        [TestMethod]
        public void TestResourceChangeWithTwoActors()
        {
            var printer = new Printer("Best printer ever") { IsActive = true };

            var rightService = this.BuildRightService(
                printer,
                _ => _.Actor(this.alice).Can(see).All<Printer>().With(p => printer.IsActive),
                _ => _.Actor(this.bob).Can(see).All<Printer>().With(p => !printer.IsActive));

            Assert.IsTrue(rightService.HasRight(alice, see, printer));
            Assert.IsFalse(rightService.HasRight(bob, see, printer));

            printer.IsActive = false;
            rightService.Update(printer);

            Assert.IsFalse(rightService.HasRight(alice, see, printer));
            Assert.IsTrue(rightService.HasRight(bob, see, printer));
        }

        private RightService BuildRightService(IEnumerable<IActor> serviceActors, IEnumerable<IResource> serviceResources, params Func<Root, ICompleteRule>[] rules)
        {
            var rightService = new RightService(new InMemoryRightStore(), new InMemoryRepository<IActor>(serviceActors), new InMemoryRepository<IResource>(serviceResources), rights);

            foreach (var rule in rules)
            {
                rightService.AddRule(rule);
            }

            rightService.Initialize();

            return rightService;
        }

        private RightService BuildRightService(params Func<Root, ICompleteRule>[] rules)
        {
            return this.BuildRightService(this.actors, this.resources, rules);
        }

        private RightService BuildRightService(IActor actor, params Func<Root, ICompleteRule>[] rules)
        {
            return this.BuildRightService(new[] {actor}, this.resources, rules);
        }

        private RightService BuildRightService(IResource resource, params Func<Root, ICompleteRule>[] rules)
        {
            return this.BuildRightService(this.actors, new[] { resource }, rules);
        }
    }
}